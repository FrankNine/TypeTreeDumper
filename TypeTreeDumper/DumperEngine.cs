namespace TypeTreeDumper;

using System;

using Unity;

internal class DumperEngine : IDumperEngine
{
    public event Action<UnityEngine, ExportOptions> OnExportCompleted;

    internal void InvokeExportCompleted(UnityEngine engine, ExportOptions options)
        => OnExportCompleted?.Invoke(engine, options);
}