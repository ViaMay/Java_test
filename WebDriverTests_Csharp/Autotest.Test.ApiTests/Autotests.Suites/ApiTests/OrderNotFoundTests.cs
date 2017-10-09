using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderNotFoundTests : SendOrdersBasePage
    {
        [Test, Description("Отправка запросов по id не существующего order")]
        public void OrderErrorTest()
        {
            var orderErrorId = "123123";
            var keyShopPublic = GetShopKeyByName(userShopName);
            //           Подтверждение заявки c неправильным id заявки
            var responseFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", orderErrorId},
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found!");

            //           Порверка статуса заявки id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order",  orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            //           Инфо заявки id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + orderErrorId + ".json");
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            //         Отмена ордера id заявки
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

//            проверка полного статуса 
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_full_status.json",
                 new NameValueCollection
                {
                {"order", orderErrorId}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");
        }
    }
}