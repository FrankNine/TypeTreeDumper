namespace Unity;

using System;
using System.Collections.Specialized;

public partial class NativeObject
{
    internal unsafe class V1 : INativeObjectImpl
    {
        private static readonly BitVector32.Section MemLabelIdentifierSection = BitVector32.CreateSection(1 << 12);
        private static readonly BitVector32.Section IsRootOwnerSection = BitVector32.CreateSection(1 << 0, MemLabelIdentifierSection);
        private static readonly BitVector32.Section TemporaryFlagsSection = BitVector32.CreateSection(1 << 0, IsRootOwnerSection);
        private static readonly BitVector32.Section HideFlagsSection = BitVector32.CreateSection(1 << 3, TemporaryFlagsSection);
        private static readonly BitVector32.Section IsPersistentSection = BitVector32.CreateSection(1 << 0, HideFlagsSection);
        private static readonly BitVector32.Section CachedTypeIndexSection = BitVector32.CreateSection(1 << 11, IsPersistentSection);

        NativeObject* nativeObject;

        public int InstanceID => nativeObject->InstanceID;
        public void* Pointer => nativeObject;
        public byte TemporaryFlags => (byte)nativeObject->bits[TemporaryFlagsSection];
        public HideFlags HideFlags => (HideFlags)nativeObject->bits[HideFlagsSection];
        public bool IsPersistent => nativeObject->bits[IsPersistentSection] != 0;
        public uint CachedTypeIndex => (uint)nativeObject->bits[CachedTypeIndexSection];

        public V1(void* ptr)
            => nativeObject = (NativeObject*)ptr;

        internal struct NativeObject
        {
            public IntPtr* VirtualFunctionTable;
            public int InstanceID;
            public BitVector32 bits;
            // There are more fields but they aren't needed.
        }
    }
}