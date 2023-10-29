using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.SkyUUI
{
    public class Cursor : MonoBehaviour
    {

        [SerializeField]
        private Texture2D normalCursor;
        [SerializeField]
        private Texture2D moveCursor;
        [SerializeField]
        private Texture2D textCursor;
        [SerializeField]
        private Texture2D pointerCursor;
        [SerializeField]
        private Texture2D resizeSlashLeftCursor;
        [SerializeField]
        private Texture2D resizeSlashRightCursor;
        [SerializeField]
        private Texture2D resizeVerticalCursor;
        [SerializeField]
        private Texture2D resizeHorizontalCursor;


        private bool propagation = true;

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("Cursor");
            SKyUUIProproperties.init();
            SetNormalCursor();
            SKyUUIProproperties.Document.rootVisualElement.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        }

        // UpdateText is called once per frame
        void Update()
        {

        }

        private void OnMouseMove(MouseMoveEvent evt)
        {
            if (propagation)
            {
                var target = evt.target;
                if (target is TextElement textElement)
                {
                    if (textElement.ClassListContains("unity-text-element--inner-input-field-component"))
                    {
                        SetTextCursor();
                    }
                    if (textElement.ClassListContains("unity-button"))
                    {
                        SetPointCursor();
                    }
                }
                else
                {
                    SetNormalCursor();
                }
            }
        }

        public void SetResizeHorizontalCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(resizeHorizontalCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        public void SetResizeVerticalCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(resizeVerticalCursor, new Vector2(16, 16), CursorMode.Auto);
        }
        public void SetResizeSlashLeftCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(resizeSlashLeftCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        public void SetResizeSlashRightCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(resizeSlashRightCursor, new Vector2(16, 16), CursorMode.Auto);
        }

        public void SetNormalCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(normalCursor, new Vector2(5.4f, 1.25f), CursorMode.Auto);
        }

        public void SetPointCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(pointerCursor, new Vector2(13.5f, 1.25f), CursorMode.Auto);
        }

        public void SetTextCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(textCursor, new Vector2(15.5f, 15.5f), CursorMode.Auto);
        }
        public void SetMoveCursor(bool propagation = true)
        {
            this.propagation = propagation;
            UnityEngine.Cursor.SetCursor(moveCursor, new Vector2(16, 16), CursorMode.Auto);
        }
    }

}