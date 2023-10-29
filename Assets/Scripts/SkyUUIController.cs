using System;
using UnityEngine;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

namespace Sky9th.SkyUUI
{
    public class SkyUUIController : MonoBehaviour
    {

        private UIDocument doc;
        private VisualElement root;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("SkyUUIController");
            SKyUUIProproperties.init();
            doc = GetComponent<UIDocument>();
            root = doc.rootVisualElement;
        }

        // UpdateText is called once per frame
        void Update()
        {
        }

    }
}
