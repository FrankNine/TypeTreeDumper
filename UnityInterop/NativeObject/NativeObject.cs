namespace Unity;

using System;

public unsafe partial class NativeObject
{
    private interface INativeObjectImpl
    {
        int InstanceID { get; }
        void* Pointer { get; }
        public byte TemporaryFlags { get; }
        public HideFlags HideFlags { get; }
        public bool IsPersistent { get; }
        public uint CachedTypeIndex { get; }
    }

    INativeObjectImpl nativeObject;

    NativeObjectFactory factory;
    PersistentTypeID persistentTypeID;

    public int InstanceID => nativeObject.InstanceID;
    public void* Pointer => nativeObject.Pointer;
    public byte TemporaryFlags => nativeObject.TemporaryFlags;
    public HideFlags HideFlags => nativeObject.HideFlags;
    public bool IsPersistent => nativeObject.IsPersistent;
    public uint CachedTypeIndex => nativeObject.CachedTypeIndex;

    public NativeObject(void* ptr, NativeObjectFactory factory, PersistentTypeID persistentTypeID, UnityVersion version)
    {
        if (ptr == null)
            throw new ArgumentNullException(nameof(ptr));

        nativeObject = version < UnityVersion.Unity5_0 ? new V1(ptr) : new V5_0(ptr);

        this.factory = factory;
        this.persistentTypeID = persistentTypeID;
    }
}