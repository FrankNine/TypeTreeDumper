namespace TypeTreeDumper;

using System;

using Unity;

public interface IDumperEngine
{
    event Action<UnityEngine, ExportOptions> OnExportCompleted;
}