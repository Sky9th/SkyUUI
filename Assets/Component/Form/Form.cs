using Sky9th.UUI;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class Form : InsertableComponent
{
    public delegate void OnSubmit(object data);
    public new class UxmlFactory : UxmlFactory<Form, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : VisualElement.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Form;

            ate.Init();
        }
    }

    private Button confirmBtn;
    private Button resetBtn;

    public OnSubmit onSubmitEvent;

    public Form()
    {
        confirmBtn = UIToolkitUtils.FindChildElement(this, "Confirm") as Button;
        resetBtn = UIToolkitUtils.FindChildElement(this, "Reset") as Button;

        confirmBtn.RegisterCallback<ClickEvent>(OnConfirm);
        resetBtn.RegisterCallback<ClickEvent>(OnReset);
    }

    private void OnReset(ClickEvent evt)
    {
        Debug.Log("Reset");
    }

    private void OnConfirm(ClickEvent evt)
    {
        bool verify = true;
        VisualElement formNode = UIToolkitUtils.FindChildElement(this, "InsertNode");

        if (formNode.childCount > 0)
        {
            foreach (VisualElement formControl in formNode.Children())
            {
                VisualElement insertNode = UIToolkitUtils.FindChildElement(formControl, "InsertNode");
                if( insertNode != null && insertNode.childCount > 0 )
                {
                    VisualElement inputField = insertNode.Children().First();
                    Type inputType = inputField.GetType();
                    if (inputType == typeof(TextInputField))
                    {
                        TextInputField control = inputField as TextInputField;
                        verify = control.Verify();
                    }
                    else if (inputType == typeof(Select))
                    {
                        Select control = inputField as Select;
                        verify = control.Verify();
                    }
                    else if (inputType == typeof(CheckBox))
                    {
                        CheckBox control = inputField as CheckBox;
                        verify = control.Verify();
                    }
                    else if (inputType == typeof(Radio))
                    {
                        Radio control = inputField as Radio;
                        verify = control.Verify();
                    }
                    else if (inputType == typeof(Slider))
                    {
                        Slider control = inputField as Slider;
                        verify = control.Verify();
                    }
                    else if (inputType == typeof(Switch))
                    {
                        Switch control = inputField as Switch;
                        verify = control.Verify();
                    }
                }
            }
        }

        if (!verify)
        {
            return;
        }

        if (onSubmitEvent != null)
        {
            onSubmitEvent.Invoke("test");
        }
    }

    public void Init ()
    {
    }
}
