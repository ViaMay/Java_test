using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class ShopPickupCompanyTests : SendOrdersBasePage
    {
        [Test, Description("Получить компанию забора ИМ")]
        public void ShopPickupCompanуTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyPickupId = GetCompanyIdByName(companyPickupName);

//           Порверка если компания забора указана
            var responsePickupCompanу = (ApiResponse.ResponsePickupCompany)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_pickup_company.json");
            Assert.IsTrue(responsePickupCompanу.Success);
            Assert.AreEqual(responsePickupCompanу.Response.Id, companyPickupId);
            Assert.AreEqual(responsePickupCompanу.Response.Name, companyPickupName);

//            проверка если нету компании забора
            var warehousesId = GetWarehouseIdByName(userWarehouseName);
            DeleteShopByName(userShopName + "_ApiAdminPickup");
//            Создание магазина
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdminPickup"},
                    {"warehouse", warehousesId},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);
            
//            удаление компании забора
            var shopId = GetShopIdByName(userShopName + "_ApiAdminPickup");
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/shop_delete_pickup.json",
        new NameValueCollection
                    {
                        {"shop_id", shopId},
                    });
            Assert.IsTrue(response.Success);

//            отправляем запрос так что в магазин с пустой компанией забора
            var responsePickupCompanуError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_pickup_company.json");
            Assert.IsFalse(responsePickupCompanуError.Success);
            Assert.AreEqual(responsePickupCompanуError.Response.ErrorText, "У интернет-магазина не указана компания забора");
        }
    }
}