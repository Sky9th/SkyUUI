using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
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

        hoverElement.RegisterCallback<MouseEnterEvent>(OnMouseEnter);
        hoverElement.RegisterCallback<MouseLeaveEvent>(OnMouseLeave);
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
}
