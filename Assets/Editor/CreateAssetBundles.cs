using UnityEditor;
using System.IO;

public class CreateAssetBundles
{

    [MenuItem("Assets/Build AssetBundle for Windows")]
    static void BuildAssetBundleForWindows()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);
    }

    [MenuItem("Assets/Build AssetBundle for WebGL")]
    static void BuildAssetBundleForWebGL()
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.WebGL);
    }

}