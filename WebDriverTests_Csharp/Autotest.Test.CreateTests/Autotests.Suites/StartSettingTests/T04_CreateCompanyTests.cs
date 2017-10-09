using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T04CreateCompanyTests : ConstVariablesTestBase
    {
        [Test, Description("Создания компании Pickup")]
        public void CreateCompatyPickup1Test()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Create.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyPickupName);
            companyCreatePage.CompanyDriver.SelectValue("Aplix");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.PickupType.SelectValue("Единый забор Курьером");

            companyCreatePage.ItemsMax.SetValueAndWait("3");
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.ManagersPickup.SetFirstValueSelect(legalPickupName);
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.LinkSchedules.Click();
                        for (int i = 0; i < 7; i++)
                        {
                            companyCreatePage.GetDay(i).FromHour.SetValueAndWait("0:01");
                            companyCreatePage.GetDay(i).ToHour.SetValueAndWait("23:59");
                        }
            companyCreatePage.SaveButton.Click();
                        companiesPage = companyCreatePage.GoTo<CompaniesPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }

        [Test, Description("Создания компании Pickup")]
        public void CreateCompatyPickup2Test()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName + "_2");
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyPickupName + "_2");
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Create.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyPickupName + "_2");
            companyCreatePage.CompanyDriver.SelectValue("Aplix");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.PickupType.SelectValue("Самостоятельный подвоз до склада Консолидации");

            companyCreatePage.ItemsMax.SetValueAndWait("3");
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
//            companyCreatePage.ManagersPickup.SetFirstValueSelect(legalPickupName);
            companyCreatePage.SaveButton.Click();

            companiesPage = companyCreatePage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName + "_2");
            companiesPage = companiesPage.SeachButtonRowClickAndGo();
            companiesPage.Table.GetRow(0).ActionsEdit.Click();
            companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.LinkSchedules.Click();
            for (int i = 0; i < 7; i++)
            {
                companyCreatePage.GetDay(i).FromHour.SetValueAndWait("0:01");
                companyCreatePage.GetDay(i).ToHour.SetValueAndWait("23:59");
            }
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<CompaniesPage>();

            var page = LoadPage<AdminBaseListPage>("admin/companywarehouses");
            page.LabelDirectory.WaitText(@"Справочник ""Склады компаний""");
            page.Table.RowSearch.Name.SetValue("test_Pickup_2_Warehouse");
            page = page.SeachButtonRowClickAndGo();

            while (page.Table.GetRow(0).Name.IsPresent)
            {
                page.Table.GetRow(0).ActionsDelete.Click();
                page.Aletr.Accept();
                page = page.GoTo<AdminBaseListPage>();
                page.Table.RowSearch.Name.SetValue("test_Pickup_2_Warehouse");
                page = page.SeachButtonRowClickAndGo();
            }
            page.Create.Click();
            var createPage = page.GoTo<AdminWarehouseCreatePage>();
            createPage.LabelDirectory.WaitText(@"Склад компании");

            createPage.Name.SetValueAndWait("test_Pickup_2_Warehouse");
            createPage.Street.SetValueAndWait("Улица");
            createPage.House.SetValueAndWait("Дом");
            createPage.Flat.SetValueAndWait("123");
            createPage.ContactPerson.SetValueAndWait(legalEntityName);
            createPage.ContactPhone.SetValueAndWait("1231231321");
            createPage.ContactEmail.SetValueAndWait(userNameAndPass);
            createPage.City.SetFirstValueSelect("Челябинск");
            createPage.Company.SetFirstValueSelect(companyPickupName + "_2");

            for (int i = 0; i < 7; i++)
            {
                createPage.GetDay(i).FromHour.SetValueAndWait("1:15");
                createPage.GetDay(i).ToHour.SetValueAndWait("23:24");
            }
            createPage.CreateButton.Click();
            page = createPage.GoTo<AdminBaseListPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }

        [Test, Description("Создания компании")]
        public void CreateCompatyTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<AdminBaseListPage>();
            companiesPage.LabelDirectory.WaitText(@"Справочник ""Компании""");
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<AdminBaseListPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
            companiesPage.Create.Click();
            var companyCreatePage = companiesPage.GoTo<CompanyCreatePage>();
            companyCreatePage.Name.SetValueAndWait(companyName);
            companyCreatePage.CompanyPickup.SetFirstValueSelect(companyPickupName);
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.CompanyPickup.SetFirstValueSelect(companyPickupName + "_2");
            companyCreatePage.CompanyPickupAddButton.Click();
            companyCreatePage.CompanyDriver.SelectValue("Hermes");
            companyCreatePage.CompanyAddress.SetValueAndWait("Address");
            companyCreatePage.PackingRequired.UncheckAndWait();
            companyCreatePage.PackingPaid.UncheckAndWait();
            companyCreatePage.EnabledOrderEdit.UncheckAndWait();
            companyCreatePage.ItemsMax.SetValueAndWait("3");
            companyCreatePage.ManagersLegalEntity.SetFirstValueSelect(legalEntityName);
            companyCreatePage.ManagersPickup.SetFirstValueSelect(legalPickupName + "u");
            companyCreatePage.Term.SetValue("12");
            companyCreatePage.Prolongation.CheckAndWait();
            companyCreatePage.SaveButton.Click();
            companiesPage = companyCreatePage.GoTo<AdminBaseListPage>();
            var adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");

            //            удаление календаря если он был
            adminMaintenancePage.AdminCompanies.Click();
            adminMaintenancePage.Calendars.Click();
            var calendarsPage = adminMaintenancePage.GoTo<AdminBaseListPage>();
            calendarsPage.LabelDirectory.WaitText(@"Справочник ""Календарь выходных дней компании""");
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }

            adminMaintenancePage = LoadPage<AdminMaintenancePage>("admin/maintenance/cache_flush");
            adminMaintenancePage.AlertText.WaitText("Cache flushed!");
        }

        [Test, Description("Создания наложенного платежа")]
        public void CreatePaymentPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PaymentPrice.Click();
            var рaymentPricePage = adminPage.GoTo<AdminBaseListPage>();
            рaymentPricePage.LabelDirectory.WaitText(@"Справочник ""Возможность наложенного платежа""");
            рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
            рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();

            while (рaymentPricePage.Table.GetRow(0).Name.IsPresent)
            {
                рaymentPricePage.Table.GetRow(0).ActionsDelete.Click();
                рaymentPricePage.Aletr.Accept();
                рaymentPricePage = рaymentPricePage.GoTo<AdminBaseListPage>();
                рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
                рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();
            }
            рaymentPricePage.Create.Click();
            var рaymentPriceCreatePage = рaymentPricePage.GoTo<PaymentPriceCreatePage>();
            рaymentPriceCreatePage.Company.SetFirstValueSelect(companyName);
            рaymentPriceCreatePage.City.SetFirstValueSelect("Москва");
            рaymentPriceCreatePage.SaveButton.Click();
            рaymentPricePage = рaymentPriceCreatePage.GoTo<AdminBaseListPage>();

            рaymentPricePage.Table.RowSearch.CompanyName.SetValue(companyName);
            рaymentPricePage = рaymentPricePage.SeachButtonRowClickAndGo();
            рaymentPricePage.Table.GetRow(0).Name.WaitText(companyName);
        }

        [Test, Description("Создание графика забора")]
        public void PickupTimetableTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
            pickupTimetablePage.LabelDirectory.WaitText(@"Справочник ""Время отправки забора""");
            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();

            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }

            pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
            pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            while (pickupTimetablePage.Table.GetRow(0).Name.IsPresent)
            {
                pickupTimetablePage.Table.GetRow(0).ActionsDelete.Click();
                pickupTimetablePage.Aletr.Accept();
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }

            pickupTimetablePage.Create.Click();
            var pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName);
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();

            pickupTimetablePage.Create.Click();
            pickupTimetableCreatePage = pickupTimetablePage.GoTo<PickupTimetableCreatePage>();
            pickupTimetableCreatePage.Company.SetFirstValueSelect(companyPickupName + "_2");
            pickupTimetableCreatePage.PickupTime.SelectByText("23:45");
            pickupTimetableCreatePage.PickupPeriod.SelectByText("Сегодня");
            pickupTimetableCreatePage.SaveButton.Click();
            pickupTimetablePage = pickupTimetableCreatePage.GoTo<AdminBaseListPage>();
        }
    }
}