namespace Unity;

using System.Collections.Generic;
using System.Runtime.CompilerServices;
using ManagedTypeTree = Unity.TypeTree;

public partial class TypeTree
{
    // Unity 2019.1 - 2019.2
    unsafe class V2019_1 : ITypeTreeImpl
    {
        internal struct TypeTreeShareableData
        {
            public DynamicArray<TypeTreeNode.V2019_1.TypeTreeNode, MemLabelId> Nodes;
            public DynamicArray<byte, MemLabelId> StringBuffer;
            public DynamicArray<uint, MemLabelId> ByteOffsets;
            public TransferInstructionFlags FlagsAtGeneration;
            public int RefCount;
            public MemLabelId* MemLabel;
        }

        internal struct TypeTree
        {
            public TypeTreeShareableData* Data;
            public TypeTreeShareableData PrivateData;
        }

        private TypeTree Tree;
        private TypeTreeNode[] m_Nodes;

        public IReadOnlyList<byte> StringBuffer => Tree.Data->StringBuffer;
        public IReadOnlyList<uint> ByteOffsets => Tree.Data->ByteOffsets;
        public IReadOnlyList<TypeTreeNode> Nodes => m_Nodes;

        public V2019_1(ManagedTypeTree owner, SymbolResolver resolver)
        {
            var constructor = (delegate* unmanaged[Cdecl]<TypeTree*, MemLabelId*, byte, void>)
                resolver.Resolve($"??0TypeTree@@Q{NameMangling.Ptr64}AA@A{NameMangling.Ptr64}BUMemLabelId@@_N@Z");
            var label = resolver.Resolve<MemLabelId>("?kMemTypeTree@@3UMemLabelId@@A");
            TypeTree tree;
            constructor(&tree, label, 0);
            Tree = tree;
        }

        public ref byte GetPinnableReference()
            => ref Unsafe.As<TypeTree, byte>(ref Tree);

        public void CreateNodes(ManagedTypeTree owner)
        {
            var nodes = new TypeTreeNode[Tree.Data->Nodes.Size];

            for (int i = 0; i < nodes.Length; i++)
                nodes[i] = new TypeTreeNode(new TypeTreeNode.V2019_1(Tree.Data->Nodes.Ptr[i]), owner);

            m_Nodes = nodes;
        }
    }
}