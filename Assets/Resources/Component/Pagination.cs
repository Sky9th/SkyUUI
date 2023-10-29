using Sky9th.SkyUUI;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Pagination : Component
{
    public new class UxmlFactory : UxmlFactory<Pagination, UxmlTraits> { }

    // Add the two custom UXML attributes.
    public new class UxmlTraits : Component.UxmlTraits
    {
        UxmlIntAttributeDescription current = new() { name = "current", defaultValue = 1 };
        UxmlIntAttributeDescription total = new() { name = "total", defaultValue = 0 };
        UxmlIntAttributeDescription pageSize = new() { name = "page-size", defaultValue = 5 };
        UxmlIntAttributeDescription pagerNums = new() { name = "pager-nums", defaultValue = 5 };
        UxmlBoolAttributeDescription simpleMode = new() { name = "simple-mode", defaultValue = false };
        UxmlBoolAttributeDescription showPageInput = new() { name = "show-page-input", defaultValue = true };

        public override void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
        {
            base.Init(ve, bag, cc);
            var ate = ve as Pagination;

            ate.current = current.GetValueFromBag(bag, cc);
            ate.total = total.GetValueFromBag(bag, cc);
            ate.pageSize = pageSize.GetValueFromBag(bag, cc);
            ate.simpleMode = simpleMode.GetValueFromBag(bag, cc);
            ate.showPageInput = showPageInput.GetValueFromBag(bag, cc);
            ate.pagerNums = pagerNums.GetValueFromBag(bag, cc);

            ate.Init();
        }
    }

    public int current { get; set; }
    public int total { get; set; }
    public int pageSize { get; set; }
    public int pagerNums { get; set; }
    public bool simpleMode { get; set; }
    public bool showPageInput { get; set; }

    private int pages;
    private Button morePrev;
    private Button moreNext;
    private Button prev;
    private Button first;
    private Button next;
    private Button last;
    private Select sizeSelect;
    private TextInputField pageInput;
    private Button pageInputBtn;
    private VisualElement pager;

    public Pagination()
    {
        pager = UIToolkitUtils.FindChildElement(this, "Pager");
        first = UIToolkitUtils.FindChildElement(this, "First") as Button;
        prev = UIToolkitUtils.FindChildElement(this, "Prev") as Button;
        next = UIToolkitUtils.FindChildElement(this, "Next") as Button;
        morePrev = UIToolkitUtils.FindChildElement(this, "MorePrev") as Button;
        moreNext = UIToolkitUtils.FindChildElement(this, "MoreNext") as Button;
        last = UIToolkitUtils.FindChildElement(this, "Last") as Button;
        sizeSelect = UIToolkitUtils.FindChildElement(this, "SizeSelect") as Select;
        pageInput = UIToolkitUtils.FindChildElement(this, "TargetPage") as TextInputField;
        pageInputBtn = UIToolkitUtils.FindChildElement(this, "Confirm") as Button;

        prev.RegisterCallback<ClickEvent>((evt) => {
            SetPage(current - 1);
        });
        first.RegisterCallback<ClickEvent>((evt) => {
            SetPage(1);
        });
        next.RegisterCallback<ClickEvent>((evt) => {
            SetPage(current + 1);
        });
        last.RegisterCallback<ClickEvent>((evt) => {
            SetPage(pages);
        });
        sizeSelect.RegisterCallback<ChangeEvent<string>>((evt) => {
            Select select = evt.currentTarget as Select;
            if (select != null)
            {
                pageSize = int.Parse(select.value);
                Init();
            }
        });
        pageInputBtn.RegisterCallback<ClickEvent>((evt) => {
            if (int.Parse(pageInput.value) > 0)
            {
                SetPage(int.Parse(pageInput.value));
                Init();
            }
        });

    }

    public new void Init ()
    {
        pages = (int)Math.Ceiling((float)total / pageSize);
        moreNext.style.display = DisplayStyle.None;
        morePrev.style.display = DisplayStyle.None;

        int pagerStart, pagerEnd;

        if (pages <= pagerNums)
        {
            pagerStart = 1;
            pagerEnd = pages;
        }
        else
        {
            int pagerStartOffset = pagerNums / 2;
            pagerStart = current - pagerStartOffset;

            if (pagerStart <= 0)
            {
                pagerStart = 1;
                pagerEnd = pagerNums;
            }
            else if (pagerStart + pagerNums > pages)
            {
                pagerEnd = pages;
                pagerStart = pagerEnd - pagerNums + 1;
            }
            else
            {
                pagerEnd = pagerStart + pagerNums - 1;
            }
        }

        UIToolkitUtils.ClearChildrenElements(pager);

        for (int i = pagerStart; i <= pagerEnd; i++)
        {
            Button _p = new Button()
            {
                text = i.ToString()
            };

            if (i == current)
            {
                _p.AddToClassList("current");
                _p.SetEnabled(false);
            }

            _p.RegisterCallback<ClickEvent>((evt) => {
                Debug.Log(evt.target);
                Button btn = evt.target as Button;
                SetPage(int.Parse(btn.text));
            });

            pager.Add(_p);
        }

        if (current == 1)
        {
            first.AddToClassList("disabled");
            first.SetEnabled(false);
            prev.AddToClassList("disabled");
            prev.SetEnabled(false);
        }
        else
        {
            first.RemoveFromClassList("disabled");
            first.SetEnabled(true);
            prev.RemoveFromClassList("disabled");
            prev.SetEnabled(true);
        }

        if (current == pages)
        {
            last.AddToClassList("disabled");
            last.SetEnabled(false);
            next.AddToClassList("disabled");
            next.SetEnabled(false);
        }
        else
        {
            last.RemoveFromClassList("disabled");
            last.SetEnabled(true);
            next.RemoveFromClassList("disabled");
            next.SetEnabled(true);
        }

        if (pagerStart > 1)
        {
            morePrev.style.display = DisplayStyle.Flex;
        }

        if (pagerEnd < pages)
        {
            moreNext.style.display = DisplayStyle.Flex;
        }
    }

    private void SetPage(int page)
    {
        if (page > pages)
        {
            return;
        }
        if (page < 0)
        {
            return;
        }
        current = page;
        Init();
    }




}
