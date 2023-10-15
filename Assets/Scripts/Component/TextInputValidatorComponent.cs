using Sky9th;
using Sky9th.UIT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;

public class TextInputValidatorComponent : TextField, IVerify<string>
{
    public new class UxmlFactory : UxmlFactory<TextInputValidatorComponent, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : TextField.UxmlTraits
    {
        UxmlStringAttributeDescription validator = new() { name = "Validator", defaultValue = "" };
        UxmlStringAttributeDescription errorMsgStr = new() { name = "errorMsgStr", defaultValue = "" };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as TextInputValidatorComponent;

            ate.validator = validator.GetValueFromBag(bag, cc);
            ate.errorMsgStr = errorMsgStr.GetValueFromBag(bag, cc);

            ValidatorUtils<string>.CheckValidator(ate);
        }
    }

    public string validator { get; set; }
    public string errorMsgStr { get; set; }
    public string[] errorMsg { get; set; }
    public List<string> validatorCallback { get; set; }
    public bool isDirty { get; set; }
    public bool[] isError { get; set; }
    public HashSet<string> errorMsgList { get; set; }

    public TextInputValidatorComponent()
    {
        RegisterCallback<ChangeEvent<string>>(OnChange);
    }

    public void OnChange(ChangeEvent<string> evt)
    {
        Verify();
    }

    public bool Verify ()
    {
        return ValidatorUtils<string>.Verify(this);
    }
}
