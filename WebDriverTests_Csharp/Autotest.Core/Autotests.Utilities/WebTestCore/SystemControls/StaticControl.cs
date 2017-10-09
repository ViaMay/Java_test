﻿using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.SystemControls
{
    public class StaticControl : HtmlControl
    {
        public StaticControl(By locator, HtmlControl container = null)
            : base(locator, container)
        {
        }

        public StaticControl(string idLocator, HtmlControl container = null)
            : base(idLocator, container)
        {
        }
    }
}