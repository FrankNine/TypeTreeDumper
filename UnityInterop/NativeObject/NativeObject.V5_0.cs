namespace Unity;

using System;
using System.Collections.Specialized;

public partial class NativeObject
{
    // Unity 5.0+
    internal unsafe class V5_0 : INativeObjectImpl
    {
        private static readonly BitVector32.Section MemLabelIdentifierSection = BitVector32.CreateSection(1 << 11);
        private static readonly BitVector32.Section TemporaryFlagsSection = BitVector32.CreateSection(1 << 0, MemLabelIdentifierSection);
        private static readonly BitVector32.Section HideFlagsSection = BitVector32.CreateSection(1 << 6, TemporaryFlagsSection);
        private static readonly BitVector32.Section IsPersistentSection = BitVector32.CreateSection(1 << 0, HideFlagsSection);
        private static readonly BitVector32.Section CachedTypeIndexSection = BitVector32.CreateSection(1 << 10, IsPersistentSection);

        NativeObject* nativeObject;

        public int InstanceID => nativeObject->InstanceID;
        public void* Pointer => nativeObject;
        public byte TemporaryFlags => (byte)nativeObject->bits[TemporaryFlagsSection];
        public HideFlags HideFlags => (HideFlags)nativeObject->bits[HideFlagsSection];
        public bool IsPersistent => nativeObject->bits[IsPersistentSection] != 0;
        public uint CachedTypeIndex => (uint)nativeObject->bits[CachedTypeIndexSection];

        public V5_0(void* ptr)
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