using System.Web.UI.WebControls;
using Autotests.Utilities.WebTestCore.SystemControls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class OrderOutputEditingPage : AdminPageBase
    {
        public OrderOutputEditingPage()
        {
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            TransferCDDate = new StaticText(By.Name("pickup_date"));
            TestSentToTC = new ButtonInput(By.LinkText("Тест отправки в ТК"));
            ConpanyStatus = new Select(By.Name("api_status"));
            CompanyOrderNumber = new TextInput(By.Name("company_order_number"));
            SaveButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary"));
        } 

        public StaticText LabelDirectory { get; set; }
        public Select ConpanyStatus { get; set; }
        public TextInput CompanyOrderNumber { get; set; }
        public StaticText TransferCDDate { get; set; }
        public ButtonInput TestSentToTC { get; set; }
        public ButtonInput SaveButton { get; set; }
        
        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            LabelDirectory.WaitTextContains("Исходящая заявка");
        }
    }
}