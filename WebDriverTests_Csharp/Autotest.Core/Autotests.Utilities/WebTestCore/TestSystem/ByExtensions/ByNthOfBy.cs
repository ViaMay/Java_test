﻿using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace Autotests.Utilities.WebTestCore.TestSystem.ByExtensions
{
    public class ByNthOfBy : By
    {
        private readonly By by;
        private readonly int index;

        internal ByNthOfBy(By by, int index)
        {
            this.by = by;
            this.index = index;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = by.FindElements(context);
            if (index < 0 || index >= elements.Count)
                throw new NoSuchElementException(
                    string.Format("Element with by '{0}' and index '{1}' not found (actual count='{2}')", by, index,
                        elements.Count));
            return elements[index];
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements = by.FindElements(context);
            if (index < 0 || index >= elements.Count)
                return new ReadOnlyCollection<IWebElement>(new IWebElement[0]);
            return new ReadOnlyCollection<IWebElement>(new[] {elements[index]});
        }

        public override string ToString()
        {
            return string.Format("ByNthOfBy: <{0}>[{1}]", by, index);
        }
    }
}