namespace Unity;

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ManagedTypeTree = Unity.TypeTree;

public partial class TypeTree
{
    // Unity 2019.3 - 2022.1
    unsafe class V2019_3 : ITypeTreeImpl
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
            public IntPtr ReferencedTypes;
            [MarshalAs(UnmanagedType.U1)]
            public bool PoolOwned;
        }

        private TypeTree Tree;
        private TypeTreeNode[] m_Nodes;

        public IReadOnlyList<byte> StringBuffer => Tree.Data->StringBuffer;
        public IReadOnlyList<uint> ByteOffsets => Tree.Data->ByteOffsets;
        public IReadOnlyList<TypeTreeNode> Nodes => m_Nodes;

        public V2019_3(ManagedTypeTree owner, SymbolResolver resolver)
        {
            var constructor = (delegate* unmanaged[Cdecl]<TypeTree*, MemLabelId*, void>)
                resolver.Resolve($"??0TypeTree@@Q{NameMangling.Ptr64}AA@A{NameMangling.Ptr64}BUMemLabelId@@@Z");
            var label = resolver.Resolve<MemLabelId>("?kMemTypeTree@@3UMemLabelId@@A");
            TypeTree tree;
            constructor(&tree, label);
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