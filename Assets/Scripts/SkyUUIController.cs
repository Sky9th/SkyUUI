using Sky9th.UUI;
using UnityEngine;
using UnityEngine.UIElements;

namespace Skyt9h.UUI
{
    public class SkyUUIController : MonoBehaviour
    {

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

        private void OnMouseEnter(MouseEnterEvent evt)
        {
        }

        private void OnMouseLeave(MouseLeaveEvent evt)
        {
        }

        private void OnDestroy()
        {
#if !UNITY_EDITOR
            SkyUUIProperties.UxmlBundle.Unload(true);
#endif
        }
    }
}
