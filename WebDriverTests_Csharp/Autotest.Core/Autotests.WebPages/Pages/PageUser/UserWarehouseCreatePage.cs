﻿using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class UserWarehouseCreatePage : UserPageBase
    {
        public UserWarehouseCreatePage()
        {
            
            LabelDirectory = new StaticText(By.CssSelector("legend"));
            Name = new TextInput(By.Name("name"));
            Name = new TextInput(By.Name("name"));
            Street = new TextInput(By.Name("street"));
            House = new TextInput(By.Name("house"));
            Flat = new TextInput(By.Name("flat"));
            ContactPerson = new TextInput(By.Name("contact_person"));
            ContactPhone = new TextInput(By.Name("contact_phone"));
            ContactEmail = new TextInput(By.Name("contact_email"));
            PostalCode = new TextInput(By.Name("postal_code"));
            City = new AutocompleteControl(BY.NthOfClass("ajax-combobox", 0));
            Freshlogic = new TextInput(By.Name("freshlogic_id"));

            CreateButton = new ButtonInput(By.CssSelector("input.btn.btn-primary"));
            WarehousesBack = new Link(By.Name("freshlogic_id"));

            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"));
        }

        public TimeWorkRowControl GetDay(int index)
        {
            var row = new TimeWorkRowControl(index);
            row.WaitPresenceWithRetries();
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Name.WaitVisible();
            Street.WaitVisible();
        }

        public TextInput Name { get; set; }
        public TextInput Street { get; set; }
        public TextInput House { get; set; }
        public TextInput Flat { get; set; }
        public TextInput ContactPerson { get; set; }
        public TextInput ContactPhone { get; set; }
        public TextInput PostalCode { get; set; }
        public TextInput ContactEmail { get; set; }
        public AutocompleteControl City { get; set; }
        public TextInput Freshlogic { get; set; }

        public ButtonInput CreateButton { get; set; }
        public Link WarehousesBack { get; set; }

        public ErrorTextControl ErrorText { get; set; }
        public StaticText LabelDirectory { get; set; }
    }
}
