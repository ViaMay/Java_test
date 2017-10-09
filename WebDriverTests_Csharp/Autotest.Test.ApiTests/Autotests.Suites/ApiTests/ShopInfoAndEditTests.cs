using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class ShopInfoAndEditTests : SendOrdersBasePage
    {
        [Test, Description("Создание магазина через Api админа")]
        public void ShopInfoAndEditTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

            DeleteWarehouseByName(userWarehouseName + "_ApiAdminInfoShop");
            
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminInfoShop"},
                        {"flat", "138"},
                        {"city", "416"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            DeleteShopByName(userShopName + "_ApiAdminInfoShop");
            DeleteShopByName(userShopName + "_ApiAdminInfoShop2");
//            Создание магазина 
            var userKey = GetUserKeyByName(userNameAndPass);
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdminInfoShop"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о магазине
            var responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_info.json");
            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.PublicKey, GetShopKeyByName(userShopName + "_ApiAdminInfoShop"));
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiAdminInfoShop");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, responseWarehouse.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Address, "Москва");

//            редактирование магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", responseShop.Response.Id},
                    {"name", userShopName + "_ApiAdminInfoShop2"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

            CacheFlush();

//            Получения информации о магазине
            responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_info.json");
            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.PublicKey, GetShopKeyByName(userShopName + "_ApiAdminInfoShop2"));
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiAdminInfoShop2");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, responseWarehouse.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.Address, "Санкт-Питербург");
        }

        [Test, Description("Редактирование магазина через Api админа не удачное")]
        public void ShopEditErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var shopId = GetShopIdByName(userShopName);

//            не правильный id магазина
            var errorId = "123456";
            var responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", errorId},
                    {"name", userShopName + "_ApiAdmin2"},
                    {"address", "Санкт-Питербург"}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Shop not found");

//            не правильно указаны имя и адрес
            responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", shopId},
                    {"name", " "},
                    {"address", " "}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Название обязательно к заполнению");

         responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", shopId},
                    {"name", "фывы"},
                    {"address", " "}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Адрес обязательно к заполнению");
        }
    }
}