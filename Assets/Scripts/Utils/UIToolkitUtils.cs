using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UIElements;

namespace Sky9th.SkyUUI
{
    public class UIToolkitUtils
    {
        public static VisualElement FindChildElement(VisualElement parent, string searchString)
        {
            if (string.IsNullOrEmpty(searchString) || parent == null)
                return null;
            
            if (parent.name == searchString || parent.ClassListContains(searchString.Substring(1)))
                return parent;

            return searchString[0] == '.' ?
                FindChildByClassName(parent, searchString.Substring(1)) :
                FindChildByName(parent, searchString);
        }

        private static VisualElement FindChildByClassName(VisualElement parent, string className)
        {
            // 通过类名递归查找子元素
            foreach (var child in parent.Children())
            {
                if (child.ClassListContains(className))
                    return child;

                var result = FindChildByClassName(child, className);
                if (result != null)
                    return result;
            }

            return null;
        }

        private static VisualElement FindChildByName(VisualElement parent, string name)
        {
            // 通过名称递归查找子元素
            foreach (var child in parent.Children())
            {
                if (child.name == name)
                    return child;

                var result = FindChildByName(child, name);
                if (result != null)
                    return result;
            }

            return null;
        }

        public static List<VisualElement> FindChildElements(VisualElement parent, string searchString)
        {
            List<VisualElement> result = new List<VisualElement>();

            if (string.IsNullOrEmpty(searchString) || parent == null)
                return result;

            if (parent.name == searchString || parent.ClassListContains(searchString.Substring(1)))
                result.Add(parent);

            if (searchString[0] == '.')
                FindChildElementsByClassName(parent, searchString.Substring(1), result);
            else
                FindChildElementsByName(parent, searchString, result);

            return result;
        }

        private static void FindChildElementsByClassName(VisualElement parent, string className, List<VisualElement> result)
        {
            // 通过类名递归查找子元素
            foreach (var child in parent.Children())
            {
                if (child.ClassListContains(className))
                    result.Add(child);

                FindChildElementsByClassName(child, className, result);
            }
        }

        private static void FindChildElementsByName(VisualElement parent, string name, List<VisualElement> result)
        {
            // 通过名称递归查找子元素
            foreach (var child in parent.Children())
            {
                if (child.name == name)
                    result.Add(child);

                FindChildElementsByName(child, name, result);
            }
        }

        public static VisualElement CreateBackDrop(VisualElement parent)
        {
            VisualElement backdrop = new VisualElement();
            backdrop.name = "Backdrop";
            backdrop.AddToClassList("Backdrop");
            backdrop.style.position = Position.Absolute;
            backdrop.style.left = new StyleLength(new Length(-999, LengthUnit.Percent));
            backdrop.style.top = new StyleLength(new Length(-999, LengthUnit.Percent));
            backdrop.style.width = new StyleLength(new Length(99999, LengthUnit.Percent));
            backdrop.style.height = new StyleLength(new Length(99999, LengthUnit.Percent));
            backdrop.style.backgroundColor = new Color(0, 0, 0, 0.35f);
            backdrop.style.display = DisplayStyle.None;

            parent.Insert(0, backdrop);
            return backdrop;
        }
        public static void ClearChildrenElements(VisualElement parent)
        {
            while (parent != null && parent.childCount > 0)
            {
                // 移除子元素
                VisualElement child = parent.ElementAt(0);
                parent.Remove(child);
            }
        }
    }

}