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
        UxmlBoolAttributeDescription moveable = new() { name = "moveable", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Panel;

            ate.reszieable = resizeable.GetValueFromBag(bag, cc);
            ate.moveable = moveable.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    public bool reszieable { get; set; }

    private float width = 0;
    private float height = 0;
    private float left = 0;
    private float top = 0;

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

    private readonly int resizeAreaSize = 5;
    private bool isResizingWidthLeft = false;
    private bool isResizingWidthRight = false;
    private bool isResizingHeightTop = false;
    private bool isResizingHeightDown = false;
    private Vector2 clickPos;

    public bool moveable { get; set; }
    private bool isMoving;
    private readonly int moveAreaSize = 10;
    private Vector2 topMoveAreaTLP;
    private Vector2 topMoveAreaBRP;

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
            }
        });


    }

    private void OnMouseUp(MouseUpEvent evt)
    {
        Debug.Log("OnMouseUp");
        EndResizing();
        isMoving = false;
        backdrop.style.display = DisplayStyle.None;
        SKyUUIProproperties.Cursor.SetNormalCursor(true);
        Init();
    }

    private void OnBackdropMove(MouseMoveEvent evt)
    {
        Debug.Log("OnBackdropMove");
        Vector2 mouPos = evt.mousePosition;

        float realMinHeight = GetRealLength(minHeight);
        float realMaxHeight = GetRealLength(maxHeight, true);
        float realMinWidth = GetRealLength(minWidth);
        float realMaxWidth = GetRealLength(maxWidth, true);

        if (isMoving)
        {
            float uWidth = clickPos.x - mouPos.x;
            float uHeight = clickPos.y - mouPos.y;
            float uTop = top - uHeight;
            float uLeft = left - uWidth;
            if (0 < uTop && uTop < parent.worldBound.height - height)
            {
                style.top = uTop;
            }
            if (0 < uLeft && uLeft < parent.worldBound.width - width)
            {
                style.left = uLeft;
            }
        }

        if (isResizingHeightDown)
        {
            float uHeight = mouPos.y - clickPos.y;
            style.height = height + uHeight;
        }
        if (isResizingHeightTop)
        {
            float uHeight = clickPos.y - mouPos.y;
            float updateHeight = height + uHeight;
            if (realMinHeight < updateHeight && updateHeight < realMaxHeight)
            {
                style.top = top - uHeight;
                style.height = updateHeight;
            }
        }
        if (isResizingWidthRight)
        {
            float uWidth = mouPos.x - clickPos.x;
            float updateWidth = width + uWidth;
            if (updateWidth > 0)
            {
                style.width = updateWidth;
            }
        }
        if (isResizingWidthLeft)
        {
            float uWidth = clickPos.x - mouPos.x;
            float updateWidth = width + uWidth;
            if (realMinWidth < updateWidth && updateWidth < realMaxWidth)
            {
                style.left = left - uWidth;
                style.width = width + uWidth;
            }
        }
    }

    private void OnMouseDown(MouseDownEvent evt)
    {
        BringToFront();
        Vector2 mouPos = evt.mousePosition;
        clickPos = mouPos;
        Debug.Log("OnMouseDown");
        if (IsInArea(topLeftResizeAreaTLP, topLeftResizeAreaBRP, mouPos)) 
        {
            isResizingWidthLeft = true;
            isResizingHeightTop = true;
        }
        if (IsInArea(bottomRightResizeAreaTLP, bottomRightResizeAreaBRP, mouPos)) 
        {
            isResizingWidthRight = true;
            isResizingHeightDown = true;
        }
        if (IsInArea(topRightResizeAreaTLP, topRightResizeAreaBRP, mouPos))
        {
            isResizingWidthRight = true;
            isResizingHeightTop = true;
        }
        if (IsInArea(bottomLeftResizeAreaTLP, bottomLeftResizeAreaBRP, mouPos))
        {
            isResizingWidthLeft = true;
            isResizingHeightDown = true;
        }
        if (IsInArea(topResizeAreaTLP, topResizeAreaBRP, mouPos))
        {
            isResizingHeightTop = true;
        }
        if (IsInArea(bottomResizeAreaTLP, bottomResizeAreaBRP, mouPos))
        {
            isResizingHeightDown = true;
        }
        if (IsInArea(leftResizeAreaTLP, leftResizeAreaBRP, mouPos))
        {
            isResizingWidthLeft = true;
        }
        if (IsInArea(rightResizeAreaTLP, rightResizeAreaBRP, mouPos))
        {
            isResizingWidthRight = true;
        }
        if (IsInArea(topMoveAreaTLP, topMoveAreaBRP, mouPos))
        {
            isMoving = true;
        }

        if (LockCursor())
        {
            backdrop.style.display = DisplayStyle.Flex;
            backdrop.BringToFront();
        }
    }

    private void OnMouseOut(MouseOutEvent evt)
    {
        if (!LockCursor())
        {
            SKyUUIProproperties.Cursor.SetNormalCursor(true);
        }
    }

    private void OnMouseMove(MouseMoveEvent evt)
    {
        Vector2 mouPos = evt.mousePosition;
        if (!LockCursor())
        {
            if (IsInArea(topLeftResizeAreaTLP, topLeftResizeAreaBRP, mouPos) || IsInArea(bottomRightResizeAreaTLP, bottomRightResizeAreaBRP, mouPos))
            {
                SKyUUIProproperties.Cursor.SetResizeSlashLeftCursor(false);
            }
            else if (IsInArea(topRightResizeAreaTLP, topRightResizeAreaBRP, mouPos) || IsInArea(bottomLeftResizeAreaTLP, bottomLeftResizeAreaBRP, mouPos))
            {
                SKyUUIProproperties.Cursor.SetResizeSlashRightCursor(false);
            }
            else if (IsInArea(topResizeAreaTLP, topResizeAreaBRP, mouPos) || IsInArea(bottomResizeAreaTLP, bottomResizeAreaBRP, mouPos))
            {
                SKyUUIProproperties.Cursor.SetResizeVerticalCursor(false);
            }
            else if (IsInArea(leftResizeAreaTLP, leftResizeAreaBRP, mouPos) || IsInArea(rightResizeAreaTLP, rightResizeAreaBRP, mouPos))
            {
                SKyUUIProproperties.Cursor.SetResizeHorizontalCursor(false);
            }
            else if (IsInArea(topMoveAreaTLP, topMoveAreaBRP, mouPos))
            {
                SKyUUIProproperties.Cursor.SetMoveCursor(false);
            }
            else
            {
                SKyUUIProproperties.Cursor.SetNormalCursor(true);
            }
        }
    }

    public new void Init ()
    {
        position = container.worldBound.position;
        width = container.worldBound.width;
        height = container.worldBound.height;
        left = worldBound.xMin;
        top = worldBound.yMin;


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

        topMoveAreaTLP = new Vector2(position.x, position.y + resizeAreaSize);
        topMoveAreaBRP = new Vector2(position.x + width, position.y + resizeAreaSize + moveAreaSize);
    }

    private bool IsInArea (Vector2 topLeft, Vector2 rightBottom, Vector2 targetPos)
    {
        return topLeft.x < targetPos.x && targetPos.x < rightBottom.x && topLeft.y < targetPos.y && targetPos.y < rightBottom.y;
    }

    private bool IsResizing()
    {
        return isResizingHeightDown || isResizingHeightTop || isResizingWidthLeft || isResizingWidthRight;
    }
    private void EndResizing()
    {
        isResizingHeightDown = isResizingHeightTop = isResizingWidthLeft = isResizingWidthRight = false;
    }

    private bool LockCursor ()
    {
        return IsResizing() || isMoving;
    }

    private float GetRealLength(string value, bool max = false)
    {
        if (value == "")
        {
            return max ? float.MaxValue : 0;
        }
        if (minHeight.EndsWith("%"))
        {
            return parent.worldBound.height * float.Parse(value.Replace("%", ""));
        }
        else
        {
            return float.Parse(value);
        }
    }

}
