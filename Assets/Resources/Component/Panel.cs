using Sky9th.SkyUUI;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Panel : InsertableComponent
{
    public new class UxmlFactory : UxmlFactory<Panel, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {
        UxmlBoolAttributeDescription resizeable = new() { name = "resizeable", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Panel;

            ate.reszieable = resizeable.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    public bool reszieable { get; set; }

    private float width = 0;
    private float height = 0;

    private VisualElement container;
    private VisualElement backdrop;
    private Vector2 position;

    private Vector2 topResizeAreaTLP;
    private Vector2 topResizeAreaBRP;

    private Vector2 bottomResizeAreaTLP;
    private Vector2 bottomResizeAreaBRP;

    private Vector2 leftResizeAreaTLP;
    private Vector2 leftResizeAreaBRP;

    private Vector2 rightResizeAreaTLP;
    private Vector2 rightResizeAreaBRP;

    private Vector2 topLeftResizeAreaTLP;
    private Vector2 topLeftResizeAreaBRP;

    private Vector2 topRightResizeAreaTLP;
    private Vector2 topRightResizeAreaBRP;

    private Vector2 bottomLeftResizeAreaTLP;
    private Vector2 bottomLeftResizeAreaBRP;

    private Vector2 bottomRightResizeAreaTLP;
    private Vector2 bottomRightResizeAreaBRP;

    private readonly int resizeAreaSize = 3;
    private bool isResizingWidth = false;
    private bool isResizingHeight = false;
    private Vector2 clickPos;

    public Panel()
    {
        container = UIToolkitUtils.FindChildElement(this, "Panel");
        container.RegisterCallback<MouseMoveEvent>(OnMouseMove);
        container.RegisterCallback<MouseOutEvent>(OnMouseOut);
        container.RegisterCallback<MouseDownEvent>(OnMouseDown);
        container.RegisterCallback<MouseUpEvent>(OnMouseUp);

        backdrop = UIToolkitUtils.FindChildElement(this, ".backdrop");
        backdrop.style.display = DisplayStyle.None;
        backdrop.RegisterCallback<MouseMoveEvent>(OnBackdropMove);

        RegisterCallback<GeometryChangedEvent>(evt =>
        {
            if (worldBound.width > 0 && worldBound.height > 0 && !(width > 0 && height > 0))
            {
                Init();
                Debug.Log("First Init");
            }
        });


    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        isResizingWidth = false;
        isResizingHeight = false;
        backdrop.style.display = DisplayStyle.None;
        Init();
    }

    private void OnBackdropMove(MouseMoveEvent evt)
    {
        Vector2 mouPos = evt.mousePosition;
        if (isResizingHeight)
        {
            float uHeight = mouPos.y - clickPos.y;
            style.height = width + uHeight;
        }

        if (isResizingWidth)
        {
            float uWidth = mouPos.x - clickPos.x;
            style.width = height + uWidth;
        }
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        Vector2 mouPos = evt.mousePosition;
        clickPos = mouPos;
        if (isInArea(topLeftResizeAreaTLP, topLeftResizeAreaBRP, mouPos) || isInArea(bottomRightResizeAreaTLP, bottomRightResizeAreaBRP, mouPos))
        {
            isResizingWidth = true;
            isResizingHeight = true;
        }
        else if (isInArea(topRightResizeAreaTLP, topRightResizeAreaBRP, mouPos) || isInArea(bottomLeftResizeAreaTLP, bottomLeftResizeAreaBRP, mouPos))
        {
            isResizingWidth = true;
            isResizingHeight = true;
        }
        else if (isInArea(topResizeAreaTLP, topResizeAreaBRP, mouPos) || isInArea(bottomResizeAreaTLP, bottomResizeAreaBRP, mouPos))
        {
            isResizingHeight = true;
        }
        else if (isInArea(leftResizeAreaTLP, leftResizeAreaBRP, mouPos) || isInArea(rightResizeAreaTLP, rightResizeAreaBRP, mouPos))
        {
            isResizingWidth = true;
        }

        if (isResizingHeight || isResizingWidth)
        {
            backdrop.style.display = DisplayStyle.Flex;
        }
    }

    private void OnMouseOut(MouseOutEvent evt)
    {
        SKyUUIProproperties.Cursor.SetNormalCursor(true);
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {

        Vector2 mouPos = evt.mousePosition;
        if (isInArea(topLeftResizeAreaTLP, topLeftResizeAreaBRP, mouPos) || isInArea(bottomRightResizeAreaTLP, bottomRightResizeAreaBRP, mouPos))
        {
            SKyUUIProproperties.Cursor.SetResizeSlashLeftCursor(false);
        }
        else if (isInArea(topRightResizeAreaTLP, topRightResizeAreaBRP, mouPos) || isInArea(bottomLeftResizeAreaTLP, bottomLeftResizeAreaBRP, mouPos))
        {
            SKyUUIProproperties.Cursor.SetResizeSlashRightCursor(false);
        }
        else if (isInArea(topResizeAreaTLP, topResizeAreaBRP, mouPos) || isInArea(bottomResizeAreaTLP, bottomResizeAreaBRP, mouPos))
        {
            SKyUUIProproperties.Cursor.SetResizeVerticalCursor(false);
        }
        else if (isInArea(leftResizeAreaTLP, leftResizeAreaBRP, mouPos) || isInArea(rightResizeAreaTLP, rightResizeAreaBRP, mouPos))
        {
            SKyUUIProproperties.Cursor.SetResizeHorizontalCursor(false);
        }
        else
        {
            if (!isResizingWidth && !isResizingHeight)
            {
                SKyUUIProproperties.Cursor.SetNormalCursor(true);
            }
        }
    }

    public new void Init ()
    {
        base.Init();

        position = container.worldBound.position;
        width = container.worldBound.width;
        height = container.worldBound.height;

        topResizeAreaTLP = position;
        topResizeAreaBRP = new Vector2(position.x + width, position.y + resizeAreaSize);

        bottomResizeAreaTLP = new Vector2(position.x, position.y + height - resizeAreaSize);
        bottomResizeAreaBRP = new Vector2(position.x + width, position.y + height);

        leftResizeAreaTLP = position;
        leftResizeAreaBRP = new Vector2(position.x + resizeAreaSize, position.y + height);

        rightResizeAreaTLP = new Vector2(position.x + width - resizeAreaSize, position.y);
        rightResizeAreaBRP = new Vector2(position.x + width, position.y + height);

        topLeftResizeAreaTLP = position;
        topLeftResizeAreaBRP = new Vector2(position.x + resizeAreaSize, position.y + resizeAreaSize);

        topRightResizeAreaTLP = new Vector2(position.x + width - resizeAreaSize, position.y);
        topRightResizeAreaBRP = new Vector2(position.x + width, position.y + resizeAreaSize);

        bottomLeftResizeAreaTLP = new Vector2(position.x, position.y + height - resizeAreaSize);
        bottomLeftResizeAreaBRP = new Vector2(position.x + resizeAreaSize, position.y + height);

        bottomRightResizeAreaTLP = new Vector2(position.x + width - resizeAreaSize, position.y + height - resizeAreaSize);
        bottomRightResizeAreaBRP = new Vector2(position.x + width, position.y + height);

    }

    private bool isInArea (Vector2 topLeft, Vector2 rightBottom, Vector2 targetPos)
    {
        return topLeft.x < targetPos.x && targetPos.x < rightBottom.x && topLeft.y < targetPos.y && targetPos.y < rightBottom.y;
    }

}
