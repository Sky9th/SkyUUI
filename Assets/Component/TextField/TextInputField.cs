using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;
using MethodInfo = System.Reflection.MethodInfo;

public class TextInputField : TextInputValidatorComponent
{
    public new class UxmlFactory : UxmlFactory<TextInputField, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : TextInputValidatorComponent.UxmlTraits
    {
        UxmlStringAttributeDescription placeholder = new() { name = "Placeholder", defaultValue = "" };
        UxmlEnumAttributeDescription<IconEnum> icon = new() { name = "Icon" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as TextInputField;

            ate.placeholder = placeholder.GetValueFromBag(bag, cc);
            ate.icon = icon.GetValueFromBag(bag, cc);
            ate.Init();
        }
    }

    // Must expose your element class to a { get; set; } property that has the same name 
    // as the name you set in your UXML attribute description with the camel case format
    public string placeholder
    {
        get { return placeholderLabel.text; }
        set { placeholderLabel.text = value; }
    }
    public IconEnum icon { get; set; }

    private VisualElement iconContainer;
    private VisualElement errorMsgContainer;
    public Texture2D BackgroundImage
    {
        get
        {
            var image = iconContainer.style.backgroundImage.value.texture;
            return image != null ? (Texture2D)image : null;
        }
        set { iconContainer.style.backgroundImage = value != null ? value : Texture2D.blackTexture; }
    }
    private Label placeholderLabel;

    public TextInputField() : base()
    {
        VisualElement container = this.Children().First();

        StyleSheet styleSheet = Resources.Load<StyleSheet>("Uss/TextInputField");
        styleSheets.Add(styleSheet);

        placeholderLabel = new Label();
        placeholderLabel.AddToClassList("placeholder");
        placeholderLabel.name = "Placeholder";

        iconContainer = new VisualElement();
        iconContainer.AddToClassList("icon");
        iconContainer.style.display = DisplayStyle.None;
        iconContainer.name = "Icon";

        RegisterCallback<FocusInEvent>(OnFocusIn);
        RegisterCallback<FocusOutEvent>(OnFocusOut);

        container.Insert(0, iconContainer);
        container.Insert(1, placeholderLabel);

        Add(errorMsgContainer);

    }

    private void Init()
    {
        UpdatePlaceholder();
        UpdateIcon();
        if (isReadOnly)
        {
            AddToClassList("readonly");
        }
        style.marginBottom = 0;
        style.marginLeft = 0;
        style.marginRight = 0;
        style.marginTop = 0;
    }

    private void UpdateIcon()
    {
        string iconName = icon.ToString();
        if (iconName == "NONE")
        {
            iconContainer.style.display = DisplayStyle.None;
        }
        else
        {
            Sprite closeSprite = Resources.Load<Sprite>("Images/" + iconName);
            StyleBackground backgroundImage = new StyleBackground(closeSprite);
            iconContainer.style.backgroundImage = backgroundImage;
            iconContainer.style.display = DisplayStyle.Flex;
        }
    }

    private void UpdatePlaceholder()
    {
        if (placeholder == null)
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
        else
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
    }

    private void OnFocusIn(FocusInEvent evt)
    {
        if (string.IsNullOrEmpty(text))
        {
            placeholderLabel.style.display = DisplayStyle.None;
        }
    }

    private void OnFocusOut(FocusOutEvent evt)
    {
        if (string.IsNullOrEmpty(text))
        {
            placeholderLabel.style.display = DisplayStyle.Flex;
        }
        isDirty = true;
    }

}
