using UnityEditor;
using UnityEngine;


#if UNITY_EDITOR
[ExecuteAlways]
public static class AsseblyReloadLock
{
    [MenuItem("Reload/Lock %w")]
    private static void LockAssemblys()
    {
        EditorApplication.LockReloadAssemblies();
    }

    [MenuItem("Reload/Unlock %e")]
    private static void Unlock()
    {
        EditorApplication.UnlockReloadAssemblies();
    }

    [MenuItem("Reload/Refresh %r")]
    private static void Refresh()
    {
        EditorApplication.UnlockReloadAssemblies();
        AssetDatabase.Refresh();
    }
}
#endif