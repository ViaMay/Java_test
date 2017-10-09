using Autotests.Utilities.WebTestCore.Pages;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    public class User2Page : CommonPageBase
    {
        public User2Page()
        {
            Text = new StaticText(By.XPath("//html/head"));
        }

        public override void BrowseWaitVisible()
        {
            Text.WaitVisible();
        }
        public StaticText Text { get; private set; }
        
    }
}