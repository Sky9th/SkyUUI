using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Sky9th.UUI
{
    public static class SkyUUIBundle
    {

        public static AssetBundle UIBundle;

        public static VisualTreeAsset LoadUxml(string path)
        {
            string uxmlPath = "assets/uxml/" + path.ToLower() + ".uxml";
#if UNITY_EDITOR
            VisualTreeAsset uxml = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(uxmlPath);
            return uxml;
#else
            VisualTreeAsset uxml = UxmlBundle.LoadAsset<VisualTreeAsset>(uxmlPath);
            return uxml;
#endif
        }

    }
}
