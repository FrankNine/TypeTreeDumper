namespace TypeTreeDumper.Examples;

using Unity;

public class ExamplePostProcessingPlugin : IDumperPlugin
{
    public void Initialize(IDumperEngine dumper)
    {
        dumper.OnExportCompleted += PostProcessExport;
    }

    public bool TryGetInterface<T>(UnityVersion version, out T result)
    {
        // This plugin doesn't provide any engine interfaces
        result = default;
        return false;
    }

    private void PostProcessExport(UnityEngine engine, ExportOptions options)
    {
    }
}