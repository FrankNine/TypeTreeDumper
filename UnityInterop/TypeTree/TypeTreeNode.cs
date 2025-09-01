namespace Unity;

using System;

public partial class TypeTreeNode
{
    internal interface ITypeTreeNodeImpl
    {
        public short Version { get; }
        public byte Level { get; }
        public TypeFlags TypeFlags { get; }
        public uint TypeStrOffset { get; }
        public uint NameStrOffset { get; }
        public int ByteSize { get; }
        public int Index { get; }
        public TransferMetaFlags MetaFlag { get; }
        ref byte GetPinnableReference();
    }

    private readonly ITypeTreeNodeImpl node;
    private readonly TypeTree owner;


    public short Version => node.Version;
    public byte Level => node.Level;
    public TypeFlags TypeFlags => node.TypeFlags;
    public uint TypeStrOffset => node.TypeStrOffset;
    public uint NameStrOffset => node.NameStrOffset;
    public int ByteSize => node.ByteSize;
    public int Index => node.Index;
    public TransferMetaFlags MetaFlag => node.MetaFlag;

    public string TypeName => owner.GetString(node.TypeStrOffset);
    public string Name => owner.GetString(node.NameStrOffset);

    internal TypeTreeNode(ITypeTreeNodeImpl impl, TypeTree owner)
    {
        node = impl;
        this.owner = owner;
    }

    public TypeTreeNode(UnityVersion version, TypeTree owner, IntPtr address)
    {
        this.owner = owner;

        node = UnityVersion.Unity2019_1 <= version 
            ? new V2019_1(address) 
            : new V5_0(address);
    }
}