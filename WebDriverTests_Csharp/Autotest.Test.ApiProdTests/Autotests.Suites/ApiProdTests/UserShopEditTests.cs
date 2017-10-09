using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserShopEditTests : ConstVariablesBase
    {
        [Test, Description("Создание магазина через Api пользователя, не дописан тест")]
        public void ShopEditTest()
        {
//            редактирование магазина
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + shopId + ".json",
                new NameValueCollection
                {
                    {"api_key", userKey},
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }

        [Test, Description("Редактирование магазина через Api админа не удачное")]
        public void ShopEditErrorTest()
        {
//            не правильный id магазина
            var idShop = "123456";
            var responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"address", "Санкт-Питербург"}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Shop not found");
        }
    }
}