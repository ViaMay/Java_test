using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserShopCreateTests : SendOrdersBasePage
    {
        [Test, Description("Создание магазина")]
        public void ShopCreateTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var werahouseId = GetWarehouseIdByName(userWarehouseName);

//            удаление магазинов если они были
            DeleteShopByName(userShopName + "_ApiUserCreateShop");
            DeleteShopByName(userShopName + "_ApiUserCreateShop2");

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserCreateShop"},
                    {"warehouse", werahouseId},
                    {"address", "Квебек"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о текущем магазине
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + responseShop.Response.Id + ".json");
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseInfo.Response.PublicKey, responseShop.Response.Key);
            Assert.AreEqual(responseInfo.Response.Name, userShopName + "_ApiUserCreateShop");
            Assert.AreEqual(responseInfo.Response.Address, "Квебек");
            Assert.AreEqual(responseInfo.Response.Warehouse, werahouseId);

//            повторное создание магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserCreateShop2"},
                    {"warehouse", werahouseId},
                    {"address", "Квебек2"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }
        
        [Test, Description("Создание магазина через Api админа неудачное")]
        public void ShopCreateErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var werahouseId = GetWarehouseIdByName(userWarehouseName);

            //            Создание магазина - ошибка пусто адрес
            var responseShop = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserCreateShop"},
                    {"warehouse", werahouseId},
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.ErrorText, "address:address обязательно к заполнению;");

            //            Создание магазина - ошибка пусто склад
            responseShop = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserCreateShop"},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.ErrorText, "warehouse:warehouse обязательно к заполнению;");

            //            пустое имя магазина
            responseShop = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"warehouse", werahouseId},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.ErrorText, "name:name обязательно к заполнению;");

            //            такое имя уже было
            responseShop = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName},
                    {"warehouse", werahouseId},
                    {"address", "Санкт-Питербург"}
                }
                );
            Assert.IsFalse(responseShop.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseShop.Response.ErrorText, "Такое имя уже существует");
        }
    }
}