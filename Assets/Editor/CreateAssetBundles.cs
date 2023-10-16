using UnityEditor;
using System.IO;

public class CreateAssetBundles
{

    [MenuItem("Assets/Build AssetBundle for Windows")]
    static void BuildAssetBundleForWindows()
    {
        string assetBundleDirectory = "Assets/StreamingAssets/StandaloneWindows";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Assets/Build AssetBundle for WebGL")]
    static void BuildAssetBundleForWebGL()
    {
        string assetBundleDirectory = "Assets/StreamingAssets/WebGL";
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory, BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }

}