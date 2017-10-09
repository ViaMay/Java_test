using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupTransferTests : SendOrdersBasePage
    {
        [Test, Description("pickup_transfer.json Перенести забор")]
        public void UserPickupTransferTest()
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
            var pickupCompanyId = GetCompanyIdByName(companyPickupName + "_2");
            var pickupDate = nowDate.AddDays(2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";

            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var responseCreate =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate.Response.Message, "Успешно создана заявка на забор №" + responseCreate.Response.OrderId);

//                    запрос списка заказов забора
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "1"}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

//                    запрос перемещения
            pickupDate = nowDate.AddDays(7).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 10:20:00";
            var pickupId = responseOrderListRequest.Response.Rows[0].PickupId;
            var responseTransfer =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_transfer.json",
                        new NameValueCollection
                        {
                            {"order", pickupId},
                            {"pickup_date", pickupDate}
                        });
            Assert.IsTrue(responseTransfer.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseTransfer.Response.Message,
                "Заявка на забор №" + responseOrderListRequest.Response.Rows[0].PickupId +
                " отменена и перенесена в новую заявку №" + responseTransfer.Response.OrderId);

//  запрос списка заказов забора проверяем что закрыт и создан новый
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "20"}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseTransfer.Response.OrderId)
                    Assert.AreEqual(row.PickupDate, pickupDate);
                if (row.PickupId == pickupId)
                    Assert.AreEqual(row.Status, "40");
            }

            var responseTransferFail =
                (ApiResponse.ResponseFail)
        apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_transfer.json",
            new NameValueCollection
                        {
                            {"order", "j"},
                            {"pickup_date", "j"}
                        });
            Assert.IsFalse(responseTransferFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseTransferFail.Response.ErrorText, "order:order должно быть в промежутке от 0 до 9223372036854775807;pickup_date:pickup date должно быть датой;");
            
            responseTransferFail =
                (ApiResponse.ResponseFail)
        apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_transfer.json",
            new NameValueCollection
                        {
                            {"order", "123123"},
                            {"pickup_date", pickupDate}
                        });
            Assert.IsFalse(responseTransferFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseTransferFail.Response.ErrorText, "Заявка не найдена");

            responseTransferFail =
                (ApiResponse.ResponseFail)
        apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_transfer.json",
            new NameValueCollection
                        {});
            Assert.IsFalse(responseTransferFail.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseTransferFail.Response.ErrorText, "order:order обязательно к заполнению;pickup_date:pickup date обязательно к заполнению;");
        }
    }
}