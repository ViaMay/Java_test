using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class CompanyPickupCheckBoxs 

    {
        public CompanyPickupCheckBoxs(int index)
        {
            CheckBox = new CheckBox(By.Name(string.Format("shoppickup_type_{0}", index + 1)));
        }

        public CheckBox CheckBox { get; set; }
    }
}