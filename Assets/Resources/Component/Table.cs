using Sky9th.SkyUUI;
using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Table : InsertableComponent
{
    public new class UxmlFactory : UxmlFactory<Table, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {
        UxmlBoolAttributeDescription resizeable = new() { name = "resizeable", defaultValue = false };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Table;

            //ate.reszieable = resizeable.GetValueFromBag(bag, cc);
            ate.Init();
        }
    }

    public Table()
    {
        name = "Table1";
        AddToClassList("Table");
    }

    public new void Init()
    {
        Debug.Log(parent);
    }



}
