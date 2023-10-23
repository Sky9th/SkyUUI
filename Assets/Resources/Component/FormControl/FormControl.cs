using Sky9th.SkyUUI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class FormControl <T>: InsertableComponent
{
    public new class UxmlFactory : UxmlFactory<FormControl <T>, UxmlTraits> { }

    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        UxmlStringAttributeDescription label = new() { name = "Label", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as FormControl <T>;
                
            ate.label = label.GetValueFromBag(bag, cc);
            ate.Init();
        }
    }

    private string label { get; set; }

    private Label labelText;
    private VisualElement errorListContainer;
    private VisualElement control;

    public FormControl() : base()
    {
        labelText = UIToolkitUtils.FindChildElement(this, "LabelText") as Label;
        errorListContainer = UIToolkitUtils.FindChildElement(this, ".errorMsgList") as Label;

    }

    private void OnChange(ChangeEvent<T> evt)
    {
        if (evt.currentTarget.GetType() == typeof(TextInputField))
        {
            TextInputField t = evt.currentTarget as TextInputField;
            if (t.errorMsgList.Count > 0)
            {
                VisualElement err = UIToolkitUtils.FindChildElement(this, "Error");
                UIToolkitUtils.ClearChildrenElements(err);
                foreach (string errMsg in t.errorMsgList)
                {
                    Label l = new()
                    {
                        text = errMsg
                    };
                    err.Add(l);
                }
            }
        }
    }

    public void Init ()
    {
        //labelText.text = label;
    }
}
