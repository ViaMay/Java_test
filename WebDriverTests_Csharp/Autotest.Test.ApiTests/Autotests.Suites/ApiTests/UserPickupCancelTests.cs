using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupCancelTests : SendOrdersBasePage
    {
        [Test, Description("pickup_cancel.json Отменить забор")]
        public void UserPickupCancelTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseCreateOrder =
                    (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                        new NameValueCollection
                        {
                            {"api_key", keyShopPublic},
                            {"type", "2"},
                            {"to_city", "151185"},
                            {"delivery_company", deliveryCompanyId},
                            {"shop_refnum", userShopName + "1"},
                            {"dimension_side1", "4"},
                            {"dimension_side2", "4"},
                            {"dimension_side3", "4"},
                            {"confirmed", "true"},
                            {"weight", "4"},
                            {"declared_price", "100"},
                            {"payment_price", "0"},
                            {"to_name", "Ургудан Рабат Мантов"},
                            {"to_street", "Барна"},
                            {"to_house", "3a"},
                            {"to_flat", "12"},
                            {"to_phone", "9999999999"},
                            {"to_email", userNameAndPass},
                            {"goods_description", "Памперс"},
                            {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                        });
            Assert.IsTrue(responseCreateOrder.Success, "Ожидался ответ true на отправленный запрос POST по API");
            ProcessIOrders();

//                    запрос списка заказов забора
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "1"}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            //      отмена
            var responseOrderCancel =
                (ApiResponse.ResponseMessage)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {
                            {"order", responseOrderListRequest.Response.Rows[0].PickupId}
                        });
            Assert.IsTrue(responseOrderCancel.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderCancel.Response.Message, "Заявка на отмену забора принята, мы делаем все возможное, чтобы оповестить об этом компанию забора.");
//            не правильный id
            var responseOrderCancelFail =
                (ApiResponse.ResponseFail)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {
                            {"order", "123123"}
                        });
            Assert.IsFalse(responseOrderCancelFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderCancelFail.Response.ErrorText, "Заявка не найдена");
            
//            валидации если нет id
            responseOrderCancelFail =
                (ApiResponse.ResponseFail)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {
                            {"order", ""}
                        });
            Assert.IsFalse(responseOrderCancelFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            
            Assert.AreEqual(responseOrderCancelFail.Response.ErrorText, "order:order должно быть в промежутке от 0 до 9223372036854775807;");responseOrderCancelFail =
                (ApiResponse.ResponseFail)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {});
            Assert.IsFalse(responseOrderCancelFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderCancelFail.Response.ErrorText, "order:order обязательно к заполнению;");

//            id заказа забора не того пользователя
            userKey = "de9fe3971aa18d5d809206d2f1679b7a";

            responseOrderCancelFail =
                (ApiResponse.ResponseFail)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {
                            {"order", responseOrderListRequest.Response.Rows[0].PickupId}
                        });
            Assert.IsFalse(responseOrderCancelFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderCancelFail.Response.ErrorText, "Заявка не найдена");
        }
    }
}