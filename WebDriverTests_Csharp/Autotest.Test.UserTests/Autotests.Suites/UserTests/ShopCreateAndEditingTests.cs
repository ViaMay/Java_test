using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class ShopCreateAndEditingTests : ConstVariablesTestBase
    {
        [Test, Description("Создания и редактирование магазина")]
        public void ShopCreateAndEditingTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_2");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                var idShop = shopsPage.Table.GetRow(0).ID.GetText();
                var responseShop = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + idShop + ".json",
                new NameValueCollection { });
                Assert.IsTrue(responseShop.Success);
                Assert.AreEqual(responseShop.Response.Message, "Магазин успешно удален");
                shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_2");
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }

            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_3");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            while (shopsPage.Table.GetRow(0).Name.IsPresent)
            {
                var idShop = shopsPage.Table.GetRow(0).ID.GetText();
                var responseShop = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + idShop + ".json",
                new NameValueCollection { });
                Assert.IsTrue(responseShop.Success);
                Assert.AreEqual(responseShop.Response.Message, "Магазин успешно удален");
                shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_3");
                shopsPage = shopsPage.SeachButtonRowClickAndGo();
            }

            shopsPage.UserLogOut.Click();
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();

            shopsListPage.ShopsCreate.Click();
            var shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName + "_2");
            shopCreatePage.Address.SetValueAndWait("Квебек");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName + " (Москва)");
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            var row = shopsListPage.Table.FindRowByName(userShopName + "_2");
            row.Address.WaitText("Квебек");
            row.OrdersCreateSelf.WaitText("Доставка до пункта самовывоза");
            row.OrdersCreateCourier.WaitText("Доставка курьером до двери");
            row.ActionsEdit.WaitText("Редактировать");
            row.ActionsDelete.WaitText("Удалить");

            row.ActionsEdit.Click();
            shopCreatePage = shopsListPage.GoTo<UserShopCreatePage>();
            shopCreatePage.Name.SetValueAndWait(userShopName + "_3");
            shopCreatePage.Address.SetValueAndWait("Квебек3");
            shopCreatePage.Warehouse.SelectValue(userWarehouseName); 
            shopCreatePage.CreateButton.Click();
            shopsListPage = shopCreatePage.GoTo<UserShopsPage>();
            row = shopsListPage.Table.FindRowByName(userShopName + "_3");
            row.Address.WaitText("Квебек3");
        }
    }
}