﻿/*
 * Copyright (c) Johnny Z. All rights reserved.
 *
 *   https://github.com/StormHub/NetUV
 *
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 *
 * Copyright (c) 2020 The Dotnetty-Span-Fork Project (cuteant@outlook.com)
 *
 *   https://github.com/cuteant/dotnetty-span-fork
 *
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 */

namespace DotNetty.NetUV.Handles
{
    using System;
    using DotNetty.NetUV.Native;

    [Flags]
    public enum PollMask
    {
        None = 0,
        Readable = 1, // UV_READABLE
        Writable = 2, // UV_WRITABLE
        Disconnect = 4  // UV_DISCONNECT
    };

    public readonly struct PollStatus
    {
        internal PollStatus(PollMask mask, Exception error)
        {
            Mask = mask;
            Error = error;
        }

        public PollMask Mask { get; }

        public Exception Error { get; }
    }

    public sealed class Poll : ScheduleHandle
    {
        internal static readonly uv_poll_cb PollCallback = (h, s, e) => OnPollCallback(h, s, e);

        private Action<Poll, PollStatus> _pollCallback;

        internal Poll(LoopContext loop, int fd)
            : base(loop, uv_handle_type.UV_POLL, new object[] { fd })
        { }

        internal Poll(LoopContext loop, IntPtr handle)
            : base(loop, uv_handle_type.UV_POLL, new object[] { handle })
        { }

        public void GetFileDescriptor(ref IntPtr value)
        {
            Validate();
            NativeMethods.GetFileDescriptor(InternalHandle, ref value);
        }

        public Poll Start(PollMask eventMask, Action<Poll, PollStatus> callback)
        {
            if (callback is null) { ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callback); }

            Validate();
            _pollCallback = callback;
            NativeMethods.PollStart(InternalHandle, eventMask);

            return this;
        }

        private void OnPollCallback(int status, int events)
        {
#if DEBUG
            if (Log.TraceEnabled)
            {
                Log.Trace("{} {} callback", HandleType, InternalHandle);
            }
#endif
            try
            {
                OperationException error = null;
                var mask = PollMask.None;
                if ((uint)status > SharedConstants.TooBigOrNegative) // < 0
                {
                    error = NativeMethods.CreateError((uv_err_code)status);
                }
                else
                {
                    mask = (PollMask)events;
                }

                _pollCallback?.Invoke(this, new PollStatus(mask, error));
            }
            catch (Exception exception)
            {
                Log.Handle_callback_error(HandleType, InternalHandle, exception);
                throw;
            }
        }

        private static void OnPollCallback(IntPtr handle, int status, int events)
        {
            var poll = HandleContext.GetTarget<Poll>(handle);
            poll?.OnPollCallback(status, events);
        }

        public void Stop() => StopHandle();

        protected override void Close() => _pollCallback = null;

        public void CloseHandle(Action<Poll> onClosed = null)
        {
            Action<ScheduleHandle> handler = null;
            if (onClosed is object)
            {
                handler = state => onClosed((Poll)state);
            }

            base.CloseHandle(handler);
        }
    }
}
