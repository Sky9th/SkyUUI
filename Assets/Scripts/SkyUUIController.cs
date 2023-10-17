using Sky9th.SkyUUI;
using System.Collections;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

namespace Sky9th.SkyUUI
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
            root = doc.rootVisualElement;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

    }
}
