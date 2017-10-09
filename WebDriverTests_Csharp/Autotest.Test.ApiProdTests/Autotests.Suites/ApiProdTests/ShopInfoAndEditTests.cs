using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class ShopInfoAndEditTests : ConstVariablesBase
    {
        [Test, Description("Создание магазина через Api админа")]
        public void ShopInfoAndEditTest()
        {
//            редактирование магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", shopId},
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о магазине
            var responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_info.json");
            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }

        [Test, Description("Редактирование магазина через Api админа не удачное")]
        public void ShopEditErrorTest()
        {
//            не правильный id магазина
            var errorId = "123456";
            var responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/shop_edit.json",
                new NameValueCollection
                {
                    {"_id", errorId},
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