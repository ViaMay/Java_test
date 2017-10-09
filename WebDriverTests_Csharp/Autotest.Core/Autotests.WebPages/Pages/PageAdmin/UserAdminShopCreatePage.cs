using System.Web.UI.WebControls;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageAdmin.Controls;
using Autotests.WebPages.Pages.PageUser;
using OpenQA.Selenium;
using CheckBox = Autotests.Utilities.WebTestCore.SystemControls.CheckBox;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class UserAdminShopCreatePage : AdminPageBase
    {
        public UserAdminShopCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            Address = new TextInput(By.Name("address"));
//           TODO Это над переписать настроки компании
            CompanyPickup2 = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 3));
            CompanyPickup3 = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 4));

            ManagersLegalEntity = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            Warehouse = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));

            CreateButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
        }

        public CompanyPickupRadioButtons GetPickupRadioButton(int index)
        {
            var row = new CompanyPickupRadioButtons(index);
            return row;
        }

        public CompanyPickupCheckBoxs GetPickupCheckBox(int index)
        {
            var row = new CompanyPickupCheckBoxs(index);
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
            Address.WaitVisible();
            Warehouse.WaitVisible();
        }

        public TextInput Name { get; set; }
        public TextInput Address { get; set; }
        public AutocompleteControl CompanyPickup3 { get; set; }
        public AutocompleteControl CompanyPickup2 { get; set; }
        public AutocompleteControl Warehouse { get; set; }
        public AutocompleteControl ManagersLegalEntity { get; set; }
        public ButtonInput CreateButton { get; set; }
    }
}
