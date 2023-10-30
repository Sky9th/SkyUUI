using Sky9th.SkyUUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Message : Component
{
    public new class UxmlFactory : UxmlFactory<Message, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {
        UxmlStringAttributeDescription title = new() { name = "title", defaultValue = "Notice" };
        UxmlStringAttributeDescription content = new() { name = "content", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Message;

            ate.title = title.GetValueFromBag(bag, cc);
            ate.content = content.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    public string title { get; set; }
    public string content { get; set; }

    private Label titleLabel;
    private Label contentLabel;

    public Message()
    {
        titleLabel = UIToolkitUtils.FindChildElement(this, "MessageTitleLabel") as Label;
        contentLabel = UIToolkitUtils.FindChildElement(this, "MessageContentLabel") as Label;

        OnMounted += Mounted;
    }

    public new void Init() {
        titleLabel.text = title;
        if (content == "")
        {
            contentLabel.parent.style.display = DisplayStyle.None;
            contentLabel.style.display = DisplayStyle.None;
        }
        else
        {
            contentLabel.text = content;
        }
    }

    public void Mounted()
    {
    }



}
