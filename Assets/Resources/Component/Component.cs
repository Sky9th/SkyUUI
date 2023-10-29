using Sky9th.SkyUUI;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Component : VisualElement
{
    private VisualTreeAsset uxml;

    public new class UxmlFactory : UxmlFactory<Component, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription ID = new() { name = "ID", defaultValue = "" };
        UxmlStringAttributeDescription maxWidth = new() { name = "maxWidth", defaultValue = "" };
        UxmlStringAttributeDescription maxHeight = new() { name = "maxHeight", defaultValue = "" };
        UxmlStringAttributeDescription minWidth = new() { name = "minWidth", defaultValue = "" };
        UxmlStringAttributeDescription minHeight = new() { name = "minHeight", defaultValue = "" };
        UxmlStringAttributeDescription initWidth = new() { name = "initWidth", defaultValue = "" };
        UxmlStringAttributeDescription initHeight = new() { name = "initHeight", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Component;

            ate.ID = ID.GetValueFromBag(bag, cc);
            ate.maxWidth = maxWidth.GetValueFromBag(bag, cc);
            ate.maxHeight = maxHeight.GetValueFromBag(bag, cc);
            ate.minWidth = minWidth.GetValueFromBag(bag, cc);
            ate.minHeight = minHeight.GetValueFromBag(bag, cc);
            ate.initWidth = initWidth.GetValueFromBag(bag, cc);
            ate.initHeight = initHeight.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    public string ID { get; set; }
    public string maxWidth { get; set; }
    public string maxHeight { get; set; }
    public string minWidth { get; set; }
    public string minHeight { get; set; }
    public string initWidth { get; set; }
    public string initHeight { get; set; }
    public float width { get; set; }
    public float height { get; set; }

    private bool isMounted = false;

    public Action OnMounted;


    public Component()
    {
        string className = GetType().ToString();
        if (className != "Component")
        {
            string componentPath = "Uxml/";
            if (!uxml && className.IndexOf("FormControl") >= 0)
            {
                uxml = Resources.Load<VisualTreeAsset>(componentPath + "FormControl");
            }
            else
            {
                uxml = Resources.Load<VisualTreeAsset>(componentPath + className);
            }
            uxml.CloneTree(this);
        }

        RegisterCallback<GeometryChangedEvent>(Mounted);

    }

    internal void Mounted (GeometryChangedEvent evt)
    {
        if (worldBound.width > 0 && worldBound.height > 0 && !isMounted)
        {
            width = worldBound.width;
            height = worldBound.height;
            isMounted = true;
            if (OnMounted != null)
            {
                OnMounted.Invoke();
            }
        }
    }

    internal void Init()
    {
        if (initWidth != null) { style.width = GetLength(initWidth); }
        if (initHeight != null) { style.height = GetLength(initHeight); }
        if (maxWidth != null) { style.maxWidth = GetLength(maxWidth); }
        if (maxHeight != null) { style.maxHeight = GetLength(maxHeight); }
        if (minWidth != null) { style.minWidth = GetLength(minWidth); }
        if (minHeight != null) { style.minHeight = GetLength(minHeight); }
    }

    private StyleLength GetLength (string value)
    {
        if (float.TryParse(value.TrimEnd('%'), out float number))
        {
            LengthUnit unit = value.EndsWith("%") ? LengthUnit.Percent : LengthUnit.Pixel;
            return new Length(number, unit);
        }
        return new StyleLength(StyleKeyword.Auto);
    }

}
