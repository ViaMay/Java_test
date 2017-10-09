using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T10CreateWarehouseAndShopTests : ConstVariablesTestBase
    {
        [Test, Description("Создания Склада для тестов")]
        public void T01_CreateWarehouseTest()
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
            warehousesPage.UserLogOut.Click();
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserWarehouses.Click();
            var warehousesListPage = userPage.GoTo<UserWarehousesPage>();

            warehousesListPage.WarehousesCreate.Click();
            var warehouseCreatePage = warehousesListPage.GoTo<UserWarehouseCreatePage>();
            warehouseCreatePage.Name.SetValueAndWait(userWarehouseName);
            warehouseCreatePage.Street.SetValueAndWait("Улица");
            warehouseCreatePage.House.SetValueAndWait("Дом");
            warehouseCreatePage.Flat.SetValueAndWait("Квартира");
            warehouseCreatePage.ContactPerson.SetValueAndWait(legalEntityName);
            warehouseCreatePage.ContactPhone.SetValueAndWait("1111111111");
            warehouseCreatePage.PostalCode.SetValueAndWait("555444");
            warehouseCreatePage.ContactEmail.SetValueAndWait(userNameAndPass);
            warehouseCreatePage.City.SetFirstValueSelect("Москва");

            for (int i = 0; i < 7; i++)
            {
                warehouseCreatePage.GetDay(i).FromHour.SetValueAndWait("1:12");
                warehouseCreatePage.GetDay(i).ToHour.SetValueAndWait("23:23");
            }

            warehouseCreatePage.CreateButton.Click();
            warehousesListPage = warehouseCreatePage.GoTo<UserWarehousesPage>();

            warehousesListPage.Table.GetRow(0).Name.WaitPresence();
        }

        [Test, Description("Создания магазина для тестов")]
        public void T02_CreateShopTest()
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
            shopsPage.UserLogOut.Click();
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName);
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName);
     
            shopsListPage.UseProfile.Click();
            shopsListPage.UserLogOut.Click();
            adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName);
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            shopsPage.Table.GetRow(0).ActionsEdit.Click();
            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup3.SetFirstValueSelect(companyPickupName);
            shopEditPage.GetPickupRadioButton(2).RadioButton.Click();
            shopEditPage.GetPickupCheckBox(2).CheckBox.CheckAndWait();
            shopEditPage.GetPickupCheckBox(0).CheckBox.UncheckAndWait();
            shopEditPage.CreateButton.Click();
        }
        
        [Test, Description("Создания магазина для тестов")]
        public void T03_CreateShopTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_Pro");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                var idShop = shopsPage.Table.GetRow(0).ID.GetText();
                var responseShop = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + idShop + ".json");
                Assert.IsTrue(responseShop.Success);
                Assert.AreEqual(responseShop.Response.Message, "Магазин успешно удален");
                shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_Pro");
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }
            shopsPage.UserLogOut.Click();
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName + "_Pro");
            shopCreatePage.Address.SetValueAndWait("Москва");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName);
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName + "_Pro");

            shopsListPage.UseProfile.Click();
            shopsListPage.UserLogOut.Click();
            adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_Pro");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            shopsPage.Table.GetRow(0).ActionsEdit.Click();
            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup3.SetFirstValueSelect(companyPickupName);
            shopEditPage.GetPickupRadioButton(0).RadioButton.Click();
            shopEditPage.GetPickupCheckBox(0).CheckBox.CheckAndWait();
            shopEditPage.GetPickupCheckBox(1).CheckBox.CheckAndWait();
            shopEditPage.GetPickupCheckBox(2).CheckBox.CheckAndWait();
            shopEditPage.GetPickupCheckBox(3).CheckBox.CheckAndWait();
            shopEditPage.CreateButton.Click();
        }
    }
}