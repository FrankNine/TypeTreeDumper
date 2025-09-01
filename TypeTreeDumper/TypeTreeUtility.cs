namespace TypeTreeDumper;

using System.IO;

using Unity;

internal class TypeTreeUtility
{
    internal static void CreateBinaryDump(TypeTree tree, BinaryWriter writer)
    {
        writer.Write(tree.Count);
        writer.Write(tree.StringBuffer.Count);
        for (int i = 0, n = tree.Count; i < n; i++)
        {
            var node = tree[i];
            writer.Write(node.Version);
            writer.Write(node.Level);
            writer.Write((byte)node.TypeFlags);
            writer.Write(node.TypeStrOffset);
            writer.Write(node.NameStrOffset);
            writer.Write(node.ByteSize);
            writer.Write(node.Index);
            writer.Write((uint)node.MetaFlag);
        }
        for (int i = 0, n = tree.StringBuffer.Count; i < n; i++)
            writer.Write(tree.StringBuffer[i]);
    }

    internal static void CreateTextDump(UnityNode node, StreamWriter writer)
    {
        for (int j = 0; j < node.Level; j++)
            writer.Write('\t');
        string type = node.TypeName;
        string name = node.Name;
        writer.WriteLine("{0} {1} // ByteSize{{{2:x}}}, Index{{{3:x}}}, Version{{{4:x}}}, IsArray{{{5:x}}}, MetaFlag{{{6:x}}}"
            , type, name, node.ByteSize, node.Index, node.Version, node.TypeFlags, node.MetaFlag);

        if (node.SubNodes != null)
        {
            for (int i = 0; i < node.SubNodes.Count; i++)
            {
                CreateTextDump(node.SubNodes[i], writer);
            }
        }
    }
}