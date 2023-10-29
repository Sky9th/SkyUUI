using Sky9th.SkyUUI;
using System;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UIElements;

public class Select : ValidatorComponent<string>
{
    public new class UxmlFactory : UxmlFactory<Select, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : ValidatorComponent<string>.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "placeholder", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };
        UxmlStringAttributeDescription choice = new() { name = "Choice", defaultValue = "" };
        UxmlStringAttributeDescription value = new() { name = "value", defaultValue = "" };
        UxmlBoolAttributeDescription multiple = new() { name = "Multiple", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Select;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.choice = choice.GetValueFromBag(bag, cc);
            ate.multiple = multiple.GetValueFromBag(bag, cc);
            ate.value = value.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string placeholder { get; set; }

    [SerializeField]
    private bool multiple { get; set; }
    [SerializeField]
    private string choice { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }

    private VisualElement root;
    private VisualTreeAsset uxml;
    private VisualTreeAsset menuUxml;
    private VisualElement select;
    private VisualElement container;
    private VisualElement input;
    private Label placeholderLabel;
    private Label textLabel;
    private VisualElement icon;
    private VisualElement iconImg;
    private VisualElement menu;
    private VisualElement backdrop;
    private bool menuDisplay = false;

    private HashSet<string> valueList = new();
    private string valueStr = "";
    private string[] choiceList;

    public Select()
    {
        select = this;

        container = UIToolkitUtils.FindChildElement(this, "Container");
        input = UIToolkitUtils.FindChildElement(this, "Input");
        placeholderLabel = UIToolkitUtils.FindChildElement(this, "Placeholder") as Label;
        textLabel = UIToolkitUtils.FindChildElement(this, "TextLabel") as Label;
        icon = UIToolkitUtils.FindChildElement(this, "Icon");
        iconImg = UIToolkitUtils.FindChildElement(this, "IconImg");


        //menuUxml = SkyUUIBundle.LoadUxml("SelectMenu");
        menuUxml = Resources.Load<VisualTreeAsset>("Uxml/SelectMenu");
        menu = menuUxml.Instantiate();

        menu.name = "SelectMenu";
        UIToolkitUtils.ClearChildrenElements(menu);

        input.RegisterCallback<ClickEvent>(OnClick);

        backdrop = UIToolkitUtils.CreateBackDrop(this);
        backdrop.RegisterCallback<ClickEvent>(OnClickBackDrop);

    }

    private void OnClickBackDrop(ClickEvent evt)
    {
        HideMenu();
    }

    private void OnClick(ClickEvent evt)
    {
        if (menuDisplay)
        {
            HideMenu();
        } else
        {
            ShowMenu();
        }
    }

    public void ShowMenu()
    {
        root = GameObject.FindFirstObjectByType<UIDocument>().rootVisualElement;
        root.Add(menu);
        menu.style.position = Position.Absolute;
        menu.style.width = select.worldBound.width;
        menu.style.left = select.worldBound.position.x;
        menu.style.top = select.worldBound.position.y + select.worldBound.height;
        menu.style.display = DisplayStyle.Flex;
        backdrop.style.display = DisplayStyle.Flex;
        menuDisplay = true;
        isDirty = true;
    }

    public void HideMenu()
    {
        menu.style.display = DisplayStyle.None;
        backdrop.style.display = DisplayStyle.None;
        menuDisplay = false;
    }

    public new void Init()
    {
        choiceList = choice.Split(",");
        for (int i = 0; i < choiceList.Length; i++)
        {
            menu.Add(CreateMenuItem(choiceList[i]));
        }
        select.AddToClassList(type.ToString().ToLower());
        placeholderLabel.text = placeholder;
        SetValue(value);
    }

    public void UpdateText()
    {
        textLabel.text = value;
        if (value.Length > 0)
        {
            textLabel.style.display = DisplayStyle.Flex;
        } else
        {
            textLabel.style.display = DisplayStyle.None;
        }
        if (valueList.Count > 0)
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
        else
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
    }

    private VisualElement CreateMenuItem(string name)
    {
        VisualElement menuItemContainer = new();
        menuItemContainer.name = "MenuItemContainer";

        VisualElement menuItem = new();
        menuItem.name = "MenuItem";

        Label menuItemLabel = new();
        menuItemLabel.text = name;
        menuItemLabel.name = "MenuItemLabel";

        menuItem.Add(menuItemLabel);
        menuItemContainer.Add(menuItem);

        menuItemContainer.RegisterCallback<ClickEvent>(OnSelectItem, TrickleDown.TrickleDown);

        return menuItemContainer;
    }

    private void OnSelectItem(ClickEvent evt)
    {
        Label menuItemLabel = UIToolkitUtils.FindChildElement(evt.target as VisualElement, "MenuItemLabel") as Label;
        string currentValue = menuItemLabel.text;
        SetValue(currentValue);
        HideMenu();
    }

    private void SetValue (string currentValue)
    {
        bool update = false;
        if (multiple)
        {
            if (valueList.Contains(currentValue))
            {
                valueList.Remove(currentValue);
            }
            else
            {
                valueList.Add(currentValue);
            }
            update = true;
        }
        else
        {
            if (!valueList.Contains(currentValue))
            {
                valueList = new()
                {
                    currentValue
                };
                update = true;
            }
        }
        valueStr = string.Join(",", valueList);
        if (update)
        {
            using ChangeEvent<string> changeEvent = ChangeEvent<string>.GetPooled(value, valueStr);
            changeEvent.target = this;
            SendEvent(changeEvent);
        }
        value = valueStr;
        UpdateText();
    }
}