using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin.Controls
{
    public class CompanyPickupRadioButtons 

    {
        public CompanyPickupRadioButtons(int index)
        {
            RadioButton = new RadioButtonControl(By.XPath(string.Format("//tbody/tr[{0}]/td[4]/input", index+1)));
        }

        public RadioButtonControl RadioButton { get; set; }
    }
}