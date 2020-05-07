using UnityEditor;
using UnityEngine;
using UnityEditor.Build.Reporting;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;


class BuildAddressablesProcessor
{
    /// <summary>
    /// Run a clean build before export.
    /// </summary>
    static public void PreExport()
    {
        Debug.Log("BuildAddressablesProcessor.PreExport start");
        AddressableAssetSettings.CleanPlayerContent(
            AddressableAssetSettingsDefaultObject.Settings.ActivePlayerDataBuilder);
        AddressableAssetSettings.BuildPlayerContent();
        Debug.Log("BuildAddressablesProcessor.PreExport done");
    }
 
    [InitializeOnLoadMethod]
    private static void Initialize()
    {
        BuildPlayerWindow.RegisterBuildPlayerHandler(BuildPlayerHandler);
    }
 
    private static void BuildPlayerHandler(BuildPlayerOptions options)
    {
        if (EditorUtility.DisplayDialog("Build with Addressables",
            "Do you want to build a clean addressables before export?",
            "Build with Addressables", "Skip"))
        {
            PreExport();
        }
        BuildPlayerWindow.DefaultBuildMethods.BuildPlayer(options);
    }
 
}

public class BuildSetup : MonoBehaviour
{
    
    
    
    [MenuItem("Build/Build Windows")]
    public static void Sensors3DWindows()
    {
        BuildPlayerOptions buildPlayerOptions = new BuildPlayerOptions();
        string _todaysdate = DateTime.Now.ToString("MM-dd-yyyy h-mm-tt");

        List<string> activeScenePaths = new List<string>();
        foreach (EditorBuildSettingsScene scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                activeScenePaths.Add(scene.path);
            }
        }

        buildPlayerOptions.scenes = activeScenePaths.ToArray();
        buildPlayerOptions.locationPathName = "Build/" + _todaysdate + "/" + Application.productName + ".exe";
        buildPlayerOptions.target = BuildTarget.StandaloneWindows64;
        buildPlayerOptions.options = BuildOptions.None;

        BuildReport report = BuildPipeline.BuildPlayer(buildPlayerOptions);
        BuildSummary summary = report.summary;

        if (summary.result == BuildResult.Succeeded)
        {
            Debug.Log("Build succeeded: " + summary.totalSize + " bytes");
            Console.WriteLine("Build succeeded");
        }

        if (summary.result == BuildResult.Failed)
        {
            Debug.Log("Build failed");
            Console.WriteLine("Build failed");
        }

    }


}