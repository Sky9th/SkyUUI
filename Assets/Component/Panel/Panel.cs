using UnityEngine;
using UnityEngine.UIElements;

public class Panel : Component
{
    public new class UxmlFactory : UxmlFactory<Panel, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Panel;

            ate.Init();
        }
    }

    public Panel()
    {
    }

    public void Init ()
    {
    }

}
