using Sky9th.UUI;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Modal : InsertableComponent
{
    public new class UxmlFactory : UxmlFactory<Modal, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {
        UxmlStringAttributeDescription title = new() { name = "Title", defaultValue = "Panel" };
        UxmlFloatAttributeDescription width = new() { name = "Width", defaultValue = 300f };
        UxmlFloatAttributeDescription height = new() { name = "Height", defaultValue = 300f };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Modal;

            ate.title = title.GetValueFromBag(bag, cc);
            ate.width = width.GetValueFromBag(bag, cc);
            ate.height = height.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    private VisualTreeAsset uxml;
    private string title { get; set; }
    private float width { get; set; }
    private float height { get; set; }

    private Label titleLabel;
    private VisualElement header;
    private VisualElement body;
    private VisualElement logo;
    private VisualElement closeImg;

    public Modal()
    {
        header = UIToolkitUtils.FindChildElement(this, "ModalHeader");
        body = UIToolkitUtils.FindChildElement(this, "ModalBody");
        logo = UIToolkitUtils.FindChildElement(this, "ModalLogo");
        titleLabel = UIToolkitUtils.FindChildElement(this, "ModalTitle") as Label;
        closeImg = UIToolkitUtils.FindChildElement(this, "ModalClose");
    }

    public void Init ()
    {
        titleLabel.text = title;
        style.width = width;
        style.height = height;
    }
}
