namespace Unity;

using System;

public partial class TypeTreeNode
{
    internal class V1 : ITypeTreeNodeImpl
    {
        public short Version { get; }
        public byte Level { get; }
        public TypeFlags TypeFlags { get; }
        public uint TypeStrOffset { get; }
        public uint NameStrOffset { get; }
        public int ByteSize { get; }
        public int Index { get; }
        public TransferMetaFlags MetaFlag { get; }

        internal V1
        (
            short version,
            byte level,
            TypeFlags typeFlags,
            uint typeStrOffset,
            uint nameStrOffset,
            int byteSize,
            int index,
            TransferMetaFlags metaFlag
        )
        {
            Version = version;
            Level = level;
            TypeFlags = typeFlags;
            TypeStrOffset = typeStrOffset;
            NameStrOffset = nameStrOffset;
            ByteSize = byteSize;
            Index = index;
            MetaFlag = metaFlag;
        }

        public ref byte GetPinnableReference()
            => throw new NotImplementedException();
    }
}