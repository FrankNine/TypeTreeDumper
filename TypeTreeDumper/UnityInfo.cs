using System.Collections.Generic;

using Unity;

namespace TypeTreeDumper;

internal class UnityInfo
{
    public string Version { get; set; }
    public List<UnityString> Strings { get; set; }
    public List<UnityClass> Classes { get; set; }

    public static UnityInfo Create
    (
        UnityEngine engine,
        TransferInstructionFlags releaseFlags = TransferInstructionFlags.SerializeGameRelease,
        TransferInstructionFlags editorFlags = TransferInstructionFlags.None
    ) 
    => new()
    {
        Version = engine.Version.ToString(),
        Strings = UnityString.MakeList(engine.CommonString),
        Classes = UnityClass.MakeList(engine, releaseFlags, editorFlags)
    };
}