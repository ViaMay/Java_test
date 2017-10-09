using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageAdmin
{
    public class CompanyCreatePage : AdminBaseListCreatePage
    {
        public CompanyCreatePage()
        {
            Name = new TextInput(By.Name("name"));
            CompanyDriver = new Select(By.Name("driver"));
            PickupType = new Select(By.Name("pickup_type"));
            CompanyAddress = new TextInput(By.Name("address"));
            CompanyPickup = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            CompanyPickupAddButton = new ButtonInput(By.XPath("//button[@type='button']"));

            Manager = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 1));
            ManagersPickup = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 3));
            ManagersLegalEntity = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 4));

            Term = new TextInput(By.Name("term"));
            ItemsMax = new TextInput(By.Name("items_max"));
            SinglePickup = new CheckBox(By.Name("single_pickup"));
            Prolongation = new CheckBox(By.Name("prolongation"));
            BarcodePull = new CheckBox(By.Name("enabled_barcode_pull"));
            PackingPaid = new CheckBox(By.Name("packing_paid"));
            PackingRequired = new CheckBox(By.Name("packing_required"));
            EnabledOrderEdit = new CheckBox(By.Name("packing_required"));

            LinkSchedules = new Link(By.Id("_link_schedules"));
            
        }

        public TimeWorkRowControl GetDay(int index)
        {
            var row = new TimeWorkRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }
        public TextInput Name { get; set; }
        public Select CompanyDriver { get; set; }
        public Select PickupType { get; set; }
        public TextInput CompanyAddress { get; set; }
        public AutocompleteControl CompanyPickup { get; set; }
        public ButtonInput CompanyPickupAddButton { get; set; }

        public AutocompleteControl Manager { get; set; }
        public AutocompleteControl ManagersPickup { get; set; }
        public AutocompleteControl ManagersLegalEntity { get; set; }

        public CheckBox Prolongation { get; set; }
        public CheckBox SinglePickup { get; set; }
        public CheckBox BarcodePull { get; set; }
        public CheckBox PackingPaid { get; set; }
        public CheckBox PackingRequired { get; set; }
        public CheckBox EnabledOrderEdit { get; set; }
        public TextInput Term { get; set; }
        public TextInput ItemsMax { get; set; }

        public Link LinkSchedules { get; set; }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
        }
    }
}