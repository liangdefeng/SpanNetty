﻿/*
 * Copyright 2012 The Netty Project
 *
 * The Netty Project licenses this file to you under the Apache License,
 * version 2.0 (the "License"); you may not use this file except in compliance
 * with the License. You may obtain a copy of the License at:
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations
 * under the License.
 *
 * Copyright (c) Microsoft. All rights reserved.
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 *
 * Copyright (c) 2020 The Dotnetty-Span-Fork Project (cuteant@outlook.com)
 *
 *   https://github.com/cuteant/dotnetty-span-fork
 *
 * Licensed under the MIT license. See LICENSE file in the project root for full license information.
 */

namespace DotNetty.Buffers
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;
    using DotNetty.Common;
    using DotNetty.Common.Internal;
    using DotNetty.Common.Internal.Logging;
    using DotNetty.Common.Utilities;

    sealed partial class AdvancedLeakAwareByteBuffer : SimpleLeakAwareByteBuffer
    {
        private const string PropAcquireAndReleaseOnly = "io.netty.leakDetection.acquireAndReleaseOnly";
        private static readonly bool AcquireAndReleaseOnly;

        private static readonly IInternalLogger Logger = InternalLoggerFactory.GetInstance<AdvancedLeakAwareByteBuffer>();

        static AdvancedLeakAwareByteBuffer()
        {
            AcquireAndReleaseOnly = SystemPropertyUtil.GetBoolean(PropAcquireAndReleaseOnly, false);

            if (Logger.DebugEnabled)
            {
                Logger.Debug("-D{}: {}", PropAcquireAndReleaseOnly, AcquireAndReleaseOnly);
            }
        }

        internal AdvancedLeakAwareByteBuffer(IByteBuffer buf, IResourceLeakTracker leak)
            : base(buf, leak)
        {
        }

        internal AdvancedLeakAwareByteBuffer(IByteBuffer wrapped, IByteBuffer trackedByteBuf, IResourceLeakTracker leak)
            : base(wrapped, trackedByteBuf, leak)
        {
        }

        internal static void RecordLeakNonRefCountingOperation(IResourceLeakTracker leak)
        {
            if (!AcquireAndReleaseOnly)
            {
                leak.Record();
            }
        }

        public override IByteBuffer Slice()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.Slice();
        }

        public override IByteBuffer Slice(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.Slice(index, length);
        }

        public override IByteBuffer Duplicate()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.Duplicate();
        }

        public override IByteBuffer ReadSlice(int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadSlice(length);
        }

        public override IByteBuffer DiscardReadBytes()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.DiscardReadBytes();
        }

        public override IByteBuffer DiscardSomeReadBytes()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.DiscardSomeReadBytes();
        }

        public override IByteBuffer EnsureWritable(int minWritableBytes)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.EnsureWritable(minWritableBytes);
        }

        public override int EnsureWritable(int minWritableBytes, bool force)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.EnsureWritable(minWritableBytes, force);
        }

        public override bool GetBoolean(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetBoolean(index);
        }

        public override byte GetByte(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetByte(index);
        }

        public override int GetUnsignedMedium(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetUnsignedMedium(index);
        }

        public override short GetShort(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetShort(index);
        }

        public override int GetInt(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetInt(index);
        }

        public override long GetLong(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetLong(index);
        }

        public override IByteBuffer GetBytes(int index, IByteBuffer dst, int dstIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetBytes(index, dst, dstIndex, length);
        }

        public override IByteBuffer GetBytes(int index, byte[] dst)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetBytes(index, dst);
        }

        public override IByteBuffer GetBytes(int index, byte[] dst, int dstIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetBytes(index, dst, dstIndex, length);
        }

        public override IByteBuffer SetBoolean(int index, bool value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBoolean(index, value);
        }

        public override IByteBuffer SetByte(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetByte(index, value);
        }

        public override IByteBuffer SetMedium(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetMedium(index, value);
        }

        public override IByteBuffer SetShort(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetShort(index, value);
        }

        public override IByteBuffer SetInt(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetInt(index, value);
        }

        public override IByteBuffer SetLong(int index, long value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetLong(index, value);
        }

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBytes(index, src, length);
        }

        public override IByteBuffer SetBytes(int index, IByteBuffer src, int srcIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBytes(index, src, srcIndex, length);
        }

        public override IByteBuffer SetBytes(int index, byte[] src)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBytes(index, src);
        }

        public override IByteBuffer SetBytes(int index, byte[] src, int srcIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBytes(index, src, srcIndex, length);
        }

        public override Task<int> SetBytesAsync(int index, Stream input, int length, CancellationToken cancellationToken)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetBytesAsync(index, input, length, cancellationToken);
        }

        public override IByteBuffer SetZero(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetZero(index, length);
        }

        public override bool ReadBoolean()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBoolean();
        }

        public override byte ReadByte()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadByte();
        }

        public override short ReadShort()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadShort();
        }

        public override int ReadMedium()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadMedium();
        }

        public override int ReadUnsignedMedium()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadUnsignedMedium();
        }

        public override int ReadInt()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadInt();
        }

        public override long ReadLong()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadLong();
        }

        public override IByteBuffer ReadBytes(int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(length);
        }

        public override IByteBuffer ReadBytes(IByteBuffer dst, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(dst, length);
        }

        public override IByteBuffer ReadBytes(IByteBuffer dst, int dstIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(dst, dstIndex, length);
        }

        public override IByteBuffer ReadBytes(byte[] dst)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(dst);
        }

        public override IByteBuffer ReadBytes(byte[] dst, int dstIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(dst, dstIndex, length);
        }

        public override IByteBuffer ReadBytes(Stream output, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadBytes(output, length);
        }

        public override IByteBuffer SkipBytes(int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SkipBytes(length);
        }

        public override IByteBuffer WriteBoolean(bool value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBoolean(value);
        }

        public override IByteBuffer WriteByte(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteByte(value);
        }

        public override IByteBuffer WriteShort(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteShort(value);
        }

        public override IByteBuffer WriteInt(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteInt(value);
        }

        public override IByteBuffer WriteMedium(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteMedium(value);
        }

        public override IByteBuffer WriteLong(long value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteLong(value);
        }

        public override IByteBuffer WriteBytes(IByteBuffer src, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBytes(src, length);
        }

        public override IByteBuffer WriteBytes(IByteBuffer src, int srcIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBytes(src, srcIndex, length);
        }

        public override IByteBuffer WriteBytes(byte[] src)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBytes(src);
        }

        public override IByteBuffer WriteBytes(byte[] src, int srcIndex, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBytes(src, srcIndex, length);
        }

        public override Task WriteBytesAsync(Stream input, int length, CancellationToken cancellationToken)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteBytesAsync(input, length, cancellationToken);
        }

        public override IByteBuffer WriteZero(int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteZero(length);
        }

        public override int ForEachByte(int index, int length, IByteProcessor processor)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ForEachByte(index, length, processor);
        }

        public override int ForEachByteDesc(int index, int length, IByteProcessor processor)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ForEachByteDesc(index, length, processor);
        }

        public override IByteBuffer Copy(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.Copy(index, length);
        }

        public override bool IsSingleIoBuffer
        {
            get
            {
                RecordLeakNonRefCountingOperation(Leak);
                return base.IsSingleIoBuffer;
            }
        }

        public override int IoBufferCount
        {
            get
            {
                RecordLeakNonRefCountingOperation(Leak);
                return base.IoBufferCount;
            }
        }

        public override ArraySegment<byte> GetIoBuffer(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetIoBuffer(index, length);
        }

        public override ArraySegment<byte>[] GetIoBuffers(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetIoBuffers(index, length);
        }

        public override IByteBuffer AdjustCapacity(int newCapacity)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.AdjustCapacity(newCapacity);
        }

        public override short GetShortLE(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetShortLE(index);
        }

        public override int GetUnsignedMediumLE(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetUnsignedMediumLE(index);
        }

        public override int GetIntLE(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetIntLE(index);
        }

        public override long GetLongLE(int index)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetLongLE(index);
        }

        public override IByteBuffer SetShortLE(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetShortLE(index, value);
        }

        public override IByteBuffer SetIntLE(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetIntLE(index, value);
        }

        public override IByteBuffer SetMediumLE(int index, int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetMediumLE(index, value);
        }

        public override IByteBuffer SetLongLE(int index, long value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.SetLongLE(index, value);
        }

        public override short ReadShortLE()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadShortLE();
        }

        public override int ReadMediumLE()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadMediumLE();
        }

        public override int ReadUnsignedMediumLE()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadUnsignedMediumLE();
        }

        public override int ReadIntLE()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadIntLE();
        }

        public override long ReadLongLE()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadLongLE();
        }

        public override IByteBuffer WriteShortLE(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteShortLE(value);
        }

        public override IByteBuffer WriteMediumLE(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteMediumLE(value);
        }

        public override IByteBuffer WriteIntLE(int value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteIntLE(value);
        }

        public override IByteBuffer WriteLongLE(long value)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.WriteLongLE(value);
        }

        public override IByteBuffer GetBytes(int index, Stream destination, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.GetBytes(index, destination, length);
        }

        public override IByteBuffer AsReadOnly()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.AsReadOnly();
        }

        public override IReferenceCounted Retain()
        {
            Leak.Record();
            return base.Retain();
        }

        public override IReferenceCounted Retain(int increment)
        {
            Leak.Record();
            return base.Retain(increment);
        }

        public override IByteBuffer RetainedSlice()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.RetainedSlice();
        }

        public override IByteBuffer RetainedSlice(int index, int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.RetainedSlice(index, length);
        }

        public override IByteBuffer RetainedDuplicate()
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.RetainedDuplicate();
        }

        public override IByteBuffer ReadRetainedSlice(int length)
        {
            RecordLeakNonRefCountingOperation(Leak);
            return base.ReadRetainedSlice(length);
        }

        public override IReferenceCounted Touch()
        {
            Leak.Record();
            return this;
        }

        public override IReferenceCounted Touch(object hint)
        {
            Leak.Record(hint);
            return this;
        }

        public override bool Release()
        {
            Leak.Record();
            return base.Release();
        }

        public override bool Release(int decrement)
        {
            Leak.Record();
            return base.Release(decrement);
        }

        protected override SimpleLeakAwareByteBuffer NewLeakAwareByteBuffer(IByteBuffer buf, IByteBuffer trackedByteBuf, IResourceLeakTracker leakTracker) =>
            new AdvancedLeakAwareByteBuffer(buf, trackedByteBuf, leakTracker);
    }
}