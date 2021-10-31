using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

namespace BuildWebGL
{
    public static class PostBuildActions
    {
        [PostProcessBuild]
        public static void OnPostProcessBuild(BuildTarget target, string targetPath)
        {
            var path = Path.Combine(targetPath, "Build/UnityLoader.js");
            var text = File.ReadAllText(path);
            text = text.Replace("UnityLoader.SystemInfo.mobile",
                "Si el juego se visualiza mal, cambia al modo 'vista de ordenador'");
            File.WriteAllText(path, text);
        }
    }
}