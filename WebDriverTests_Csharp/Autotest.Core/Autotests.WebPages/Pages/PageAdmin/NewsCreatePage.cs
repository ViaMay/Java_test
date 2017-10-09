using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class NewsCreatePage : AdminBaseListCreatePage
    {
        public NewsCreatePage()
        {
            Text = new TextInput(By.ClassName("redactor-editor"));
            Type = new Select(By.Name("type"));
            Active = new CheckBox(By.Name("active"));
            Email = new CheckBox(By.Name("email"));
        }

        public TextInput Text { get; set; }
        public Select Type { get; set; }
        public CheckBox Active { get; set; }
        public CheckBox Email { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Text.WaitVisible();
        }
    }
}