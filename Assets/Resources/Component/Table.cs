using Sky9th.SkyUUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Table : InsertableComponent
{
    public new class UxmlFactory : UxmlFactory<Table, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {
        UxmlBoolAttributeDescription headerFixed = new() { name = "headerFixed", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Table;

            ate.headerFixed = headerFixed.GetValueFromBag(bag, cc);
            ate.Init();
        }
    }

    public bool headerFixed { get; set; }

    public Table()
    {
        OnMounted += Mounted;
    }

    public void Mounted()
    {
        if (headerFixed)
        {
        }
    }



}
