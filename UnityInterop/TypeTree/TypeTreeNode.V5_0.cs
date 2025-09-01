namespace Unity;

using System;
using System.Runtime.CompilerServices;

public partial class TypeTreeNode
{
    internal unsafe class V5_0 : ITypeTreeNodeImpl
    {
        internal struct TypeTreeNode
        {
            public short Version;
            public byte Level;
            public TypeFlags TypeFlags;
            public uint TypeStrOffset;
            public uint NameStrOffset;
            public int ByteSize;
            public int Index;
            public TransferMetaFlags MetaFlag;
        }

        internal TypeTreeNode Node;

        public short Version => Node.Version;
        public byte Level => Node.Level;
        public TypeFlags TypeFlags => Node.TypeFlags;
        public uint TypeStrOffset => Node.TypeStrOffset;
        public uint NameStrOffset => Node.NameStrOffset;
        public int ByteSize => Node.ByteSize;
        public int Index => Node.Index;
        public TransferMetaFlags MetaFlag => Node.MetaFlag;

        internal V5_0(TypeTreeNode node)
            => Node = node;

        public V5_0(IntPtr address)
        {
            if (address == IntPtr.Zero)
                throw new ArgumentNullException(nameof(address));

            Node = *(TypeTreeNode*)address;
        }

        public ref byte GetPinnableReference()
            => ref Unsafe.As<TypeTreeNode, byte>(ref Node);
    }
}