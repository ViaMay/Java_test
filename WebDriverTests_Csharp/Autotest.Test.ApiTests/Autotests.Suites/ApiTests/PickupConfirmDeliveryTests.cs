using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupConfirmDeliveryTests : SendOrdersBasePage
    {
        [Test, Description("Принять заказ на склад по штрих-коду")]
        public void PickupConfirmDeliveryTest()
        {
//            отправляем две заявки
            string[] ordersId = SendOrdersRequest();
            ProcessIOrders();
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseBarcodes = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection {{ "order_id", ordersId[0] },});

//            шлем запрос подтверить их
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes.Response.Barcodes[0] +
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

            responseBarcodes = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes.Response.Barcodes[0] +
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");
        }

        [Test, Description("Принять заказ на склад по штрих-коду - неудачный запрос")]
        public void PickupConfirmDeliveryErrorTest()
        {
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var errorOrderId = "dd-123456";

//            отправляем запрос на подтверждение с некорректным номером заявки
            var responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
               new NameValueCollection
                {
                    {"barcode", errorOrderId},
                }
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
//            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден. Сверьте номер на штрих-коде, соответствует ли он коду " + errorOrderId);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Заказ не найден");

//            отправляем запрос с пустым номером заявки
            responseConfirmDelivery = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
               new NameValueCollection
                {}
               );
            Assert.IsFalse(responseConfirmDelivery.Success);
            Assert.AreEqual(responseConfirmDelivery.Response.ErrorText, "Укажите штрих-код");
        }
    }
}