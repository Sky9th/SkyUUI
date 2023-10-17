using Sky9th.SkyUUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Switch : ValidatorComponent<bool>
{
    public new class UxmlFactory : UxmlFactory<Switch, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : ValidatorComponent<bool>.UxmlTraits
    {
        UxmlStringAttributeDescription label = new() { name = "Label", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Switch;

            ate.label = label.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string label { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }

    private VisualElement _switch;
    private Label textLabel;

    public Switch()
    {
        _switch = UIToolkitUtils.FindChildElement(this, "Switch");
        textLabel = UIToolkitUtils.FindChildElement(this, "Label") as Label;

        _switch.RegisterCallback<ClickEvent>(OnClick);
    }

    private void OnClick(ClickEvent evt)
    {
        if (_switch.ClassListContains("checked"))
        {
            value = false;
            _switch.RemoveFromClassList("checked");
        } else
        {
            value = true;
            _switch.AddToClassList("checked");
        }
    }

    public void Init()
    {
        textLabel.text = label;
        AddToClassList(type.ToString());
    }
}