using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.UUI
{
    public static class SkyUUIProperties
    {
        public static string UxmlPath { get; set; }

        public static AssetBundle UxmlBundle;

        public static void InitUxmlBundle()
        {
#if !UNITY_EDITOR
            if (UxmlBundle == null)
            {
                AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/skyuui_uxml");
                if (myLoadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                }
                UxmlBundle = myLoadedAssetBundle;
            }
#endif

        }

        public static VisualTreeAsset LoadUxml(string path)
        {

#if UNITY_EDITOR
            string uxmlPath = "assets/uxml/" + path.ToLower() + ".uxml";
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            return uxml;
#else

            if (UxmlBundle == null)
            {
                AssetBundle myLoadedAssetBundle = AssetBundle.LoadFromFile("Assets/AssetBundles/skyuui_uxml");
                if (myLoadedAssetBundle == null)
                {
                    Debug.Log("Failed to load AssetBundle!");
                }
                UxmlBundle = myLoadedAssetBundle;
            }
#endif
        }

    }
}
