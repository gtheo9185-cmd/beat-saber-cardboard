using UnityEngine;

public class APKBuilder : MonoBehaviour
{
    /// <summary>
    /// Script para facilitar o build do APK
    /// Use o Menu: Build → Build APK
    /// </summary>
}

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;

public class BuildAPK
{
    [MenuItem("Build/Build APK")]
    public static void BuildAndroidAPK()
    {
        string[] scenePaths = EditorBuildSettingsScene.GetActiveScenes();
        string buildPath = "Builds/beat-saber-cardboard.apk";
        
        // Configurações do build
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenePaths;
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.None;
        
        Debug.Log("🚀 Iniciando build do APK para Android...");
        Debug.Log($"📁 Caminho: {buildPath}");
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;
        
        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ Build completado com sucesso!");
            Debug.Log($"📊 Tamanho do APK: {summary.totalSize / (1024f * 1024f):F2} MB");
            Debug.Log($"⏱️ Tempo de build: {summary.totalTime.TotalSeconds:F2} segundos");
        }
        else if (summary.result == BuildResult.Failed)
        {
            Debug.LogError($"❌ Build falhou!");
        }
    }
    
    [MenuItem("Build/Build APK (Development)")]
    public static void BuildAndroidAPKDevelopment()
    {
        string[] scenePaths = EditorBuildSettingsScene.GetActiveScenes();
        string buildPath = "Builds/beat-saber-cardboard-dev.apk";
        
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        buildPlayerOptions.scenes = scenePaths;
        buildPlayerOptions.locationPathName = buildPath;
        buildPlayerOptions.target = BuildTarget.Android;
        buildPlayerOptions.options = BuildOptions.Development | BuildOptions.AllowDebugging;
        
        Debug.Log("🔧 Iniciando build de desenvolvimento do APK...");
        
        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        
        if (report.summary.result == BuildResult.Succeeded)
        {
            Debug.Log($"✅ Build de desenvolvimento completado!");
        }
    }
}
#endif
