using Sky9th.UUI;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Skyt9h.UUI
{
    public class SkyUUIController : MonoBehaviour
    {
        [SerializeField]
        private VisualTreeAsset visualTreeAsset;

        private UIDocument doc;
        private VisualElement root;
        private VisualElement hoverElement;

        private Texture2D cursor;
        private Texture2D move;

        // Start is called before the first frame update
        void Start()
        {
            doc = GetComponent<UIDocument>();
#if !UNITY_EDITOR
            StartCoroutine(LoadAssetsBundle());
#else
            doc.enabled = true;
#endif
        }

        // Update is called once per frame
        void Update()
        {
            if (SkyUUIBundle.UIBundle != null && root == null)
            {
                doc.enabled = true;
                root = doc.rootVisualElement;
            }
        }

        private IEnumerator LoadAssetsBundle()
        {
            Debug.Log("LoadAssetsBundle start");
#if UNITY_STANDALONE_WIN
            string path = Application.streamingAssetsPath + "/StandaloneWindows/skyuui_uxml";
            SkyUUIProperties.UxmlBundle = AssetBundle.LoadFromFile(path);
            yield return null;
#else
            string assetBundlePath = "StreamingAssets/WebGL/skyuui_uxml";
            var request = UnityEngine.Networking.UnityWebRequestAssetBundle.GetAssetBundle(assetBundlePath, 0);
            yield return request.Send();
            AssetBundle bundle = UnityEngine.Networking.DownloadHandlerAssetBundle.GetContent(request);
            SkyUUIBundle.UIBundle = bundle;
#endif
            Debug.Log(SkyUUIBundle.UIBundle);
            Debug.Log("LoadAssetsBundle end");
        }


        private void OnDestroy()
        {
#if !UNITY_EDITOR
            SkyUUIProperties.UxmlBundle.Unload(true);
#endif
        }
    }
}
