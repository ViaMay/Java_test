﻿using System.Collections.Generic;
using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.ClearTests
{
    public class ClearTestTest : ConstVariablesTestBase
    {
        [Test, Description("Удаление документов")]
        public void T01_DeleteDocumentsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            var documentsPage = LoadPage<AdminBaseListPage>("admin/documents/?&filters[warehouse]=" + userWarehouseName);
            while (documentsPage.Table.GetRow(0).ID.IsPresent)
            {
                var ids = new List<string>();
                var i = 0;
                do
                {
                    var id = documentsPage.Table.GetRow(i).ID.GetText();
                    ids.Add(id);
                    i++;
                } while (documentsPage.Table.GetRow(i).ID.IsPresent);
                foreach (var id in ids)
                {
                    documentsPage = LoadPage<AdminBaseListPage>("admin/documents/delete/" + id);
                }
                documentsPage =
                    LoadPage<AdminBaseListPage>("admin/documents/?&filters[warehouse]=" + userWarehouseName);
            }
        }

        [Test, Description("Удаление заявок")]
        [TestCase("outgoingorders", "company")]
        [TestCase("pickuporders", "pickup_company")]
        [TestCase("pickuporders", "warehouse")]
        public void T02_DeleteOrdersTest(string page, string filters)
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var filtersValue = "";
            if (filters == "company") filtersValue = companyName;
            if (filters == "pickup_company") filtersValue = companyPickupName;
            if (filters == "warehouse") filtersValue = userWarehouseName;
            var оrdersPage = LoadPage<AdminBaseListPage>("admin/" + page + "?&filters[" + filters + "]=" + filtersValue);
            while (оrdersPage.Table.GetRow(0).ID.IsPresent)
            {
                var ids = new List<string>();
                var i = 0;
                do
                {
                    var id = оrdersPage.Table.GetRow(i).ID.GetText();
                    ids.Add(id);
                    i++;
                } while (оrdersPage.Table.GetRow(i).ID.IsPresent);

                foreach (var id in ids)
                {
                    оrdersPage = LoadPage<AdminBaseListPage>("admin/" + page + "/delete/" + id);
                }
                оrdersPage = LoadPage<AdminBaseListPage>("admin/" + page + "?&filters[" + filters + "]=" + filtersValue);
            }
        }

        [Test, Description("Удаление заявок входящих")]
        [TestCase("orders", "shop")]
        [TestCase("orders", "delivery_company")]
        public void T02_DeleteOrdersInputTest(string page, string filters)
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            var filtersValue = "";
            if (filters == "shop") filtersValue = userShopName;
            if (filters == "delivery_company") filtersValue = companyName;
            var оrdersPage = LoadPage<OrdersInputPage>("admin/" + page + "?&filters[" + filters + "]=" + filtersValue);
            while (оrdersPage.Table.GetRow(0).ID.IsPresent)
            {
                var ids = new List<string>();
                var i = 0;
                do
                {
                    var id = оrdersPage.Table.GetRow(i).ID.GetText();
                    ids.Add(id);
                    i++;
                } while (оrdersPage.Table.GetRow(i).ID.IsPresent);
                
                foreach (var id in ids)
                {
                    оrdersPage = LoadPage<OrdersInputPage>("admin/" + page + "/delete/" + id);
                }
                оrdersPage = LoadPage<OrdersInputPage>("admin/" + page + "?&filters[" + filters + "]=" + filtersValue);
            } 
        }

        [Test, Description("Удаляем склады")]
        public void T03_DeletePriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Prices.Mouseover();
            adminPage.PricesPickup.Click();
            var pricesPickupPage = adminPage.GoTo<PricesPickupPage>();
            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }

            pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();

            while (pricesPickupPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                pricesPickupPage.Aletr.Accept();
                pricesPickupPage = pricesPickupPage.GoTo<PricesPickupPage>();
                pricesPickupPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesPickupPage = pricesPickupPage.SeachButtonRowClickAndGo();
            }
            pricesPickupPage.AdminCompanies.Click();
            pricesPickupPage.Prices.Mouseover();
            pricesPickupPage.PricesCourier.Click();
            var pricesCourierPage = adminPage.GoTo<PricesPage>();
            pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();

            while (pricesCourierPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                pricesCourierPage.Aletr.Accept();
                pricesCourierPage = pricesCourierPage.GoTo<PricesPage>();
                pricesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesCourierPage = pricesCourierPage.SeachButtonRowClickAndGo();
            }

            pricesCourierPage.AdminCompanies.Click();
            pricesCourierPage.Prices.Mouseover();
            pricesCourierPage.PricesSelf.Click();
            var pricesSelfPage = adminPage.GoTo<PricesPage>();
            pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();

            while (pricesSelfPage.Table.GetRow(0).CompanyName.IsPresent)
            {
                pricesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                pricesSelfPage.Aletr.Accept();
                pricesSelfPage = pricesSelfPage.GoTo<PricesPage>();
                pricesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                pricesSelfPage = pricesSelfPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем графики")]
        public void T04_DeleteTimesTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Times.Mouseover();
            adminPage.TimesPickup.Click();
            var timesPickupPage = adminPage.GoTo<AdminBaseListPage>();
            timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
            timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();

            while (timesPickupPage.Table.GetRow(0).Name.IsPresent)
            {
                timesPickupPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesPickupPage = timesPickupPage.GoTo<AdminBaseListPage>();
                timesPickupPage.Table.RowSearch.CompanyName.SetValue(companyPickupName);
                timesPickupPage = timesPickupPage.SeachButtonRowClickAndGo();
            }

            timesPickupPage.AdminCompanies.Click();
            timesPickupPage.Times.Mouseover();
            timesPickupPage.TimesCourier.Click();
            var timesCourierPage = adminPage.GoTo<AdminBaseListPage>();
            timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();

            while (timesCourierPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesCourierPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesCourierPage = timesCourierPage.GoTo<AdminBaseListPage>();
                timesCourierPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesCourierPage = timesCourierPage.SeachButtonRowClickAndGo();
            }
            timesCourierPage.AdminCompanies.Click();
            timesCourierPage.Times.Mouseover();
            timesCourierPage.TimesSelf.Click();
            var timesSelfPage = adminPage.GoTo<AdminBaseListPage>();
            timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
            timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();

            while (timesSelfPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                timesSelfPage.Table.GetRow(0).ActionsDelete.Click();
                timesPickupPage.Aletr.Accept();
                timesSelfPage = timesSelfPage.GoTo<AdminBaseListPage>();
                timesSelfPage.Table.RowSearch.CompanyName.SetValue(companyName);
                timesSelfPage = timesSelfPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление точки доставки")]
        public void T05_DeleteDeliveryPointTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.DeliveryPoints.Click();
            var deliveryPointsPage = adminPage.GoTo<AdminBaseListPage>();
            deliveryPointsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();

            while (deliveryPointsPage.Table.GetRow(0).Name.IsPresent)
            {
                deliveryPointsPage.Table.GetRow(0).ActionsDelete.Click();
                deliveryPointsPage.Aletr.Accept();
                deliveryPointsPage = deliveryPointsPage.GoTo<AdminBaseListPage>();
                deliveryPointsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                deliveryPointsPage = deliveryPointsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление календаря")]
        public void T06_DeleteCalendarsTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            //            удаление календаря если он был
            adminPage.AdminCompanies.Click();
            adminPage.Calendars.Click();
            var calendarsPage = adminPage.GoTo<AdminBaseListPage>();
            calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
            calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            while (calendarsPage.Table.GetRow(0).ColumnThree.IsPresent)
            {
                calendarsPage.Table.GetRow(0).ActionsDelete.Click();
                calendarsPage.Aletr.Accept();
                calendarsPage = calendarsPage.GoTo<AdminBaseListPage>();
                calendarsPage.Table.RowSearch.CompanyName.SetValue(companyName);
                calendarsPage = calendarsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление графика забора")]
        public void T07_DeletePickupTimetableTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            //            удаление графика забора если он был
            adminPage.AdminCompanies.Click();
            adminPage.PickupTimetable.Click();
            var pickupTimetablePage = adminPage.GoTo<AdminBaseListPage>();
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
                pickupTimetablePage = pickupTimetablePage.GoTo<AdminBaseListPage>();
                pickupTimetablePage.Table.RowSearch.CompanyName.SetValue(companyName);
                pickupTimetablePage = pickupTimetablePage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаления веса")]
        public void T08_DeleteWeightTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsWeight.Click();
            var intervalsWeightPage = adminPage.GoTo<AdminBaseListPage>();
            intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
            intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();

            while (intervalsWeightPage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsWeightPage.Table.GetRow(0).ActionsDelete.Click();
                intervalsWeightPage.Aletr.Accept();
                intervalsWeightPage = intervalsWeightPage.GoTo<AdminBaseListPage>();
                intervalsWeightPage.Table.RowSearch.Name.SetValue(weightName);
                intervalsWeightPage = intervalsWeightPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление размера")]
        public void T09_DeleteSizeTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.Intervals.Mouseover();
            adminPage.IntervalsSize.Click();
            var intervalsSizePage = adminPage.GoTo<AdminBaseListPage>();
            intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
            intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();

            while (intervalsSizePage.Table.GetRow(0).Name.IsPresent)
            {
                intervalsSizePage.Table.GetRow(0).ActionsDelete.Click();
                intervalsSizePage.Aletr.Accept();
                intervalsSizePage = intervalsSizePage.GoTo<AdminBaseListPage>();
                intervalsSizePage.Table.RowSearch.Name.SetValue(sideName);
                intervalsSizePage = intervalsSizePage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление шаблона")]
        public void T10_DeletуOrderedIttemplatesTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.OrderedIttemplates.Click();
            var orderedIttemplatesPage = adminPage.GoTo<OrderedIttemplatesPage>();

            orderedIttemplatesPage.Table.RowSearch.CompanyName.SetValue(companyName);
            orderedIttemplatesPage = orderedIttemplatesPage.SeachButtonRowClickAndGo();

            while (orderedIttemplatesPage.Table.GetRow(0).Name.IsPresent)
            {
                orderedIttemplatesPage.Table.GetRow(0).ActionsDelete.Click();
                orderedIttemplatesPage.Aletr.Accept();
                orderedIttemplatesPage = orderedIttemplatesPage.GoTo<OrderedIttemplatesPage>();
                orderedIttemplatesPage.Table.RowSearch.CompanyName.SetValue(companyName);
                orderedIttemplatesPage = orderedIttemplatesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаление наложенного платежа")]
        public void T11_DeletуPaymentPriceTest()
        {
            AdminHomePage adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.PaymentPrice.Click();
            var рaymentPricePage = adminPage.GoTo<AdminBaseListPage>();
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
        }
        
        [Test, Description("удаление компании")]
        public void T12_DeleteCompanyTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminCompanies.Click();
            adminPage.Companies.Click();
            var companiesPage = adminPage.GoTo<CompaniesPage>();
            companiesPage.Table.RowSearch.Name.SetValue(companyName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }

            companiesPage.Table.RowSearch.Name.SetValue(companyPickupName);
            companiesPage = companiesPage.SeachButtonRowClickAndGo();

            while (companiesPage.Table.GetRow(0).Name.IsPresent)
            {
                companiesPage.Table.GetRow(0).ActionsDelete.Click();
                companiesPage.Aletr.Accept();
                companiesPage = companiesPage.GoTo<CompaniesPage>();
                companiesPage.Table.RowSearch.Name.SetValue(companyName);
                companiesPage = companiesPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем магазины usera")]
        public void T13_DeleteUserShopTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                var idShop = shopsPage.Table.GetRow(0).ID.GetText();
                var responseShop = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + idShop + ".json",
                new NameValueCollection { });
                Assert.IsTrue(responseShop.Success);
                Assert.AreEqual(responseShop.Response.Message, "Магазин успешно удален");
                shopsPage.Table.RowSearch.Name.SetValue(userShopName);
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("Удаляем склады usera")]
        public void T14_DeleteUserWarehouseTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersWarehouses.Click();
            var warehousesPage = adminPage.GoTo<AdminBaseListPage>();
            warehousesPage.LabelDirectory.WaitText(@"Справочник ""Склады""");
            warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
            warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            while (warehousesPage.Table.GetRow(0).Name.IsPresent)
            {
                var idwarehouse = warehousesPage.Table.GetRow(0).ID.GetText();
                var responseWarehouse = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + idwarehouse + ".json",
                new NameValueCollection { });
                Assert.IsTrue(responseWarehouse.Success);
                Assert.AreEqual(responseWarehouse.Response.Message, "Склад успешно удален");
                warehousesPage.Table.RowSearch.Name.SetValue(userWarehouseName);
                warehousesPage = warehousesPage.SeachButtonRowClickAndGo();
            }
        }
        
        [Test, Description("Удаляем usera")]
        public void T15_DeleteUserTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.Users.Click();
            var usersPage = adminPage.GoTo<UsersPage>();
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(userNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
            usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
            usersPage = usersPage.SeachButtonRowClickAndGo();
            while (usersPage.UsersTable.GetRow(0).UserEmail.IsPresent)
            {
                usersPage.UsersTable.GetRow(0).ActionsDelete.Click();
                usersPage.Aletr.Accept();
                usersPage = usersPage.GoTo<UsersPage>();
                usersPage.UsersTable.RowSearch.UserEmail.SetValue(pickupNameAndPass);
                usersPage = usersPage.SeachButtonRowClickAndGo();
            }
        }

        [Test, Description("удаление юр лица")]
        public void T16_DeleteLegalEntitiesTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<AdminBaseListPage>();

            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(0).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(0).ActionsDelete.Click();
                legalEntitiesPage.Aletr.Accept();
                legalEntitiesPage = legalEntitiesPage.GoTo<AdminBaseListPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
        }
        [Test, Description("удаление новостей")]
        public void T17_DeleteNewsTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.News.Click();
            var newsPage = adminPage.GoTo<AdminBaseListPage>();
            newsPage.LabelDirectory.WaitText(@"Справочник ""Новости и уведомления""");
            newsPage.Table.RowSearch.Content.SetValue("<p>test_новость</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p>test_новость</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Table.RowSearch.Content.SetValue("<p>test_уведомление</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p>test_уведомление</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_уведомление</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_уведомление</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_новость</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_новость</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
        }
    }
}