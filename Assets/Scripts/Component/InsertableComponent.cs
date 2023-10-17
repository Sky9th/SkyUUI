using Sky9th.SkyUUI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class InsertableComponent : Component
{

    private List<VisualElement> moveEle = new();

    public int originalCount = -1;
    public VisualElement insertNode;

    public InsertableComponent() : base()
    {
        originalCount = childCount;
        RegisterCallback<AttachToPanelEvent>(GetMoveElement);
    }

    public void GetMoveElement (AttachToPanelEvent evt)
    {
        insertNode = UIToolkitUtils.FindChildElement(this, "InsertNode");
        if (moveEle.Count == 0)
        {
            int count = 0;
            // 遍历所有子元素
            foreach (VisualElement child in Children())
            {
                if (count > originalCount - 1)
                {
                    moveEle.Add(child);
                }
                count++;
            }
            MoveElementIntoInsertNode();
        }
    }

    public void MoveElementIntoInsertNode()
    {
        if (insertNode != null)
        {
            for (int i = 0; i < moveEle.Count; i++)
            {
                insertNode.Add(moveEle[i]);
            }
        }
    }

}
