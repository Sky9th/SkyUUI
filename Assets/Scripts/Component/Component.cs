using Sky9th.UUI;
using UnityEngine;
using UnityEngine.UIElements;

public class Component : VisualElement
{
    private VisualTreeAsset uxml;

    public new class UxmlFactory : UxmlFactory<Component, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription maxWidth = new() { name = "maxWidth", defaultValue = "" };
        UxmlStringAttributeDescription maxHeight = new() { name = "maxHeight", defaultValue = "" };
        UxmlStringAttributeDescription width = new() { name = "width", defaultValue = "" };
        UxmlStringAttributeDescription height = new() { name = "height", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Component;

            ate.maxWidth = maxWidth.GetValueFromBag(bag, cc);
            ate.maxHeight = maxHeight.GetValueFromBag(bag, cc);
            ate.width = width.GetValueFromBag(bag, cc);
            ate.height = height.GetValueFromBag(bag, cc);

            ate.init();
        }
    }

    public string maxWidth { get; set; }
    public string maxHeight { get; set; }
    public string width { get; set; }
    public string height { get; set; }


    public Component()
    {
        string className = GetType().ToString();
        if (!uxml && className.IndexOf("FormControl") >= 0)
        {
            //uxml = Resources.Load<VisualTreeAsset>(componentPath + "FormControl");
            uxml = SkyUUIProperties.LoadUxml("FormControl") as VisualTreeAsset;
        }
        else
        {
            //uxml = Resources.Load<VisualTreeAsset>(componentPath + className);
            uxml = SkyUUIProperties.LoadUxml(className) as VisualTreeAsset;
        }

        if (uxml)
        {
            uxml.CloneTree(this);
        }
    }

    internal void init()
    {
        if (width != null) { SetSize(width, true, false); }
        if (height != null) { SetSize(height, false, false); }
        if (maxWidth != null) { SetSize(maxWidth, true, true); }
        if (maxHeight != null) { SetSize(maxHeight, false, true); }
    }

    public void SetSize(string value, bool isWidth, bool isMax)
    {
        if (float.TryParse(value.TrimEnd('%'), out float number))
        {
            LengthUnit unit = value.EndsWith("%") ? LengthUnit.Percent : LengthUnit.Pixel;

            if (isWidth)
            {
                if (!isMax)
                    style.width = new Length(number, unit);
                else
                    style.maxWidth = new Length(number, unit);
            }
            else
            {
                if (!isMax)
                    style.height = new Length(number, unit);
                else
                    style.maxHeight = new Length(number, unit);
            }
        }
    }

}
