﻿using System;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class ErrorActionTextControl : HtmlControl
    {
        public ErrorActionTextControl(By locator, HtmlControl container = null)
            :
                base(locator, container)
        {
        }

        public int Count { 
            get { return element.FindElements(By.ClassName("alert-error")).Count; }
        }

        public StaticText this[int index]
        {
            get { return new StaticText(BY.NthOfClass("alert-error", index), this); }
        }

        public StaticText FindByName(string value)
        {
            for (int i = 0; i < Count; i++)
            {
                if (this[i].GetText() == value)
                    return this[i];
            }
            throw new Exception(string.Format("Не найден текст '{0}' в списке Error-oв", value));
        }
    }
}