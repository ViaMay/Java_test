using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserOrdersGetOutputTests : SendOrdersBasePage
    {
        [Test, Description("Скачивание файла")]
        public void OrdersTest01()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var  responseCreateOrder =
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

//                    запрос списка заказов - проверка что список выдан
            var responseOrderListRequest =
                (ApiResponse.ResponseMessage)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"output_type", "2"},
                            {"id",responseCreateOrder.Response.OrderId}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.IsTrue(responseOrderListRequest.Response.File.Contains("http://file.ddelivery.ru/download/"));
            
            responseOrderListRequest =
                (ApiResponse.ResponseMessage)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"output_type", "1"},
                            {"id",responseCreateOrder.Response.OrderId}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.IsTrue(responseOrderListRequest.Response.File.Contains("http://file.ddelivery.ru/download/"));
        }
    }
}