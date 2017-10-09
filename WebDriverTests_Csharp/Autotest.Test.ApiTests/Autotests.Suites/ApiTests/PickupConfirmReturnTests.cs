using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupConfirmReturnTests : SendOrdersBasePage
    {
        [Test, Description("Принять заказ для возврата по штрих-коду или почтовому идентификатору")]
        public void PickupConfirmReturnTest()
        {
            string[] ordersId = SendOrdersRequest();
            ProcessIOrders();

            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseBarcodes01 =
                (ApiResponse.ResponseUserBarcodes)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                        new NameValueCollection {{"order_id", ordersId[0]},});
            var responseBarcodes02 =
                (ApiResponse.ResponseUserBarcodes)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                        new NameValueCollection {{"order_id", ordersId[1]},});

//            запрашиваем документы на возврат - ошибка
            var responseConfirmReturnFail =
                (ApiResponse.ResponseFail) apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                    new NameValueCollection {{"barcode", responseBarcodes01.Response.Barcodes[0]},});
            Assert.IsFalse(responseConfirmReturnFail.Success);
            Assert.AreEqual(responseConfirmReturnFail.Response.ErrorText,
                "Заказ #" + responseBarcodes01.Response.Barcodes[0]
                + " невозможно вернуть. Заказ еще не отправлялся и находится на складе интернет-магазина");

            responseConfirmReturnFail =
                (ApiResponse.ResponseFail) apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                    new NameValueCollection {{"barcode", responseBarcodes02.Response.Barcodes[0]},});
            Assert.IsFalse(responseConfirmReturnFail.Success);
            Assert.AreEqual(responseConfirmReturnFail.Response.ErrorText,
                "Заказ #" + responseBarcodes02.Response.Barcodes[0]
                + " невозможно вернуть. Заказ еще не отправлялся и находится на складе интернет-магазина");

//            подтверждаем что заказ на складе
            var responseConfirmDelivery =
                (ApiResponse.ResponseStatusConfirm)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                        new NameValueCollection {{"barcode", responseBarcodes01.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

            responseConfirmDelivery =
                (ApiResponse.ResponseStatusConfirm)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                        new NameValueCollection {{"barcode", responseBarcodes02.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            проверяем теперь
            var responseConfirmReturn =
                (ApiResponse.ResponseStatusConfirm)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                        new NameValueCollection {{"barcode", responseBarcodes01.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #" + responseBarcodes01.Response.Barcodes[0]
                                                                    +
                                                                    " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

            responseConfirmReturn =
                (ApiResponse.ResponseStatusConfirm)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                        new NameValueCollection {{"barcode", responseBarcodes02.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Message, "Заказ #" + responseBarcodes02.Response.Barcodes[0]
                                                                    +
                                                                    " подтвержден для возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

//            при повторной отправке на возврат
            responseConfirmReturnFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                        new NameValueCollection {{"barcode", responseBarcodes01.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturnFail.Response.ErrorText,
                "Заказ #" + responseBarcodes01.Response.Barcodes[0]
                + " уже ожидает возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");

            responseConfirmReturnFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                        new NameValueCollection {{"barcode", responseBarcodes02.Response.Barcodes[0]},});
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturnFail.Response.ErrorText,
                "Заказ #" + responseBarcodes02.Response.Barcodes[0]
                + " уже ожидает возврата. Заказ подтвержден для возврата и ожидает отправки в интернет-магазин");
        }

        [Test, Description("Принять заказ для возврата по штрих-коду или почтовому идентификатору - неудачный запрос")]
        public void PickupConfirmReturnErrorTest()
        {
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var errorOrderId = "dd-123456";
            var responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
               new NameValueCollection
                {
                    {"barcode", errorOrderId},
                }
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
//            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден. Сверьте номер на штрих-коде, соответствует ли он коду " + errorOrderId);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден");

            responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
               new NameValueCollection
                {}
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Укажите штрих-код или почтовый идентификатор");
        }
    }
}