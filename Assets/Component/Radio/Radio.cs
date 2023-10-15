using Sky9th.UUI;
using UnityEngine;
using UnityEngine.UIElements;

public class Radio : ValidatorComponent<string>
{
    public new class UxmlFactory : UxmlFactory<Radio, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : ValidatorComponent<string>.UxmlTraits
    {
        UxmlStringAttributeDescription choice = new() { name = "Choice", defaultValue = "" };
        UxmlEnumAttributeDescription<TypeEnum> type = new() { name = "Type" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Radio;

            ate.choice = choice.GetValueFromBag(bag, cc);
            ate.type = type.GetValueFromBag(bag, cc);
            ate.Init();
        }

    }

    [SerializeField]
    private string choice { get; set; }
    [SerializeField]
    private TypeEnum type { get; set; }


    private VisualTreeAsset uxml;
    private VisualTreeAsset itemUxml;
    private VisualElement radio;
    private VisualElement item;
    private VisualElement btn;
    private VisualElement round;
    private VisualElement point;
    private Label textLabel;

    private string[] choiceList;
    public Radio()
    {
        itemUxml = SkyUUIProperties.LoadUxml("RadioItem");
        item = itemUxml.Instantiate();

        radio = this;
        btn = UIToolkitUtils.FindChildElement(item, "Btn");
        round = UIToolkitUtils.FindChildElement(item, "Round");
        point = UIToolkitUtils.FindChildElement(item, "Point");
        textLabel = UIToolkitUtils.FindChildElement(item, "Label") as Label;

        UIToolkitUtils.ClearChildrenElements(radio);
    }

    public void Init()
    {
        radio.AddToClassList(type.ToString().ToLower());

        choiceList = choice.Split(",");

        VisualElement newItem;
        for (int i = 0; i < choiceList.Length; i++)
        {
            newItem = CreateItem(choiceList[i]);
            radio.Add(newItem);
        }
    }

    private VisualElement CreateItem (string text)
    {
        VisualElement newItem = itemUxml.Instantiate();
        Label textLabel = UIToolkitUtils.FindChildElement(newItem, "Label") as Label;
        textLabel.text = text;

        newItem.RegisterCallback<ClickEvent>(OnItemClick);

        return newItem;
    }

    private void OnItemClick(ClickEvent evt)
    {
        VisualElement target = evt.currentTarget as VisualElement;
        VisualElement Item = UIToolkitUtils.FindChildElement(target, "RadioItem");
        string newValue = "";
        foreach (var child in radio.Children())
        {
            UIToolkitUtils.FindChildElement(child, "RadioItem").RemoveFromClassList("checked");
            Label label =  UIToolkitUtils.FindChildElement(child, "Label") as Label;
            newValue = label.text;
        }
        Item.AddToClassList("checked");
        using ChangeEvent<string> changeEvent = ChangeEvent<string>.GetPooled(value, newValue);
        changeEvent.target = this;
        value = newValue;
        SendEvent(changeEvent);
    }
}