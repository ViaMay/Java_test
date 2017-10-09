using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T11CreateWarehouseAndShopPickupTest : ConstVariablesTestBase
    {
        [Test, Description("Создания склада и магазина для тестов")]
        public void T01_CreateWarehouseAndShopTest()
        {
            DeleteWarehouseByName(userWarehouseName + "_ApiUsePickupShop");
//            Создаем склад
            var userKey = GetUserKeyByName(userNameAndPass);
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiUsePickupShop"},
                    {"flat", "138"},
                    {"city", "434"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                }
                );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            DeleteShopByName(userShopName + "_ApiUsePickupShop");
//            создаем магазин
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUsePickupShop"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Челябинск"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
//            указываем тип и ТК забора
//            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + responseShop.Response.Id + ".json",
//    new NameValueCollection
//                {
//                    {"id", responseShop.Response.Id},
//                    {"pickup_type", "2"},
//                    {"pickup_company", GetCompanyIdByName(companyPickupName + "_2")},
//                }
//    );
//            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
            var adminPage =LoginAsAdmin(adminName, adminPass);
            adminPage.AdminUsers.Click();
            adminPage.UsersShops.Click();
            var shopsPage = adminPage.GoTo<UsersShopsPage>();
            shopsPage.Table.RowSearch.Name.SetValue(userShopName + "_ApiUsePickupShop");
            shopsPage = shopsPage.SeachButtonRowClickAndGo();
            shopsPage.Table.GetRow(0).ActionsEdit.Click();
            var shopEditPage = shopsPage.GoTo<UserAdminShopCreatePage>();
            shopEditPage.CompanyPickup2.SetFirstValueSelect(companyPickupName + "_2");
            shopEditPage.GetPickupRadioButton(1).RadioButton.Click();
            shopEditPage.GetPickupCheckBox(1).CheckBox.CheckAndWait();
            shopEditPage.CreateButton.Click();
        }

        private string GetCompanyIdByName(string name)
        {
            var response =
                (ApiResponse.ResponseCompanies)apiRequest.GET("admin/api/v1/" + adminKey + "/get_companies_by_name.json",
                    new NameValueCollection
                    {
                        {"company_name", name},
                    });
            Assert.IsTrue(response.Success);
            return response.Response.Companies[0].Id;
        }

        private string GetWarehouseIdByName(string warehouseName)
        {
            var responseLkAuth =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });

            var response =
                (ApiResponse.ResponseObjectsList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                        new NameValueCollection
                        {
                            {"method", "get_warehouses"},
                        });
            Assert.IsTrue(response.Success);
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Name == warehouseName)
                    return response.Response[i].Id;
            }
            return "";
        }

        private string GetShopIdByName(string shopName)
        {
            var responseLkAuth =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });

            var response =
                (ApiResponse.ResponseObjectsList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                        new NameValueCollection
                        {
                            {"method", "get_shops"},
                        });
            Assert.IsTrue(response.Success);
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Name == shopName)
                    return response.Response[i].Id;
            }
            return "";
        }
        private string GetUserKeyByName(string userName)
        {
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/get_user_key_by_name.json",
                    new NameValueCollection
                    {
                        {"email", userName},
                    });
            if (response.Success)
            {
                var response2 = (ApiResponse.ResponsePublicKey)response;
                return response2.Response.PublicKey;
            }
            return "";
        }
        private void DeleteShopByName(string shopName)
        {
            var id = GetShopIdByName(shopName);
            if (id != "")
            {
                var responseShopDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + id + ".json",
                            new NameValueCollection { });
                Assert.IsTrue(responseShopDelete.Success);
            }
        }

        private void DeleteWarehouseByName(string name)
        {
            var id = GetWarehouseIdByName(name);
            if (id != "")
            {
                var responseDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + id + ".json",
                            new NameValueCollection { });
                Assert.IsTrue(responseDelete.Success);
            }
        }
    }
}