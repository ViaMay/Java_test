using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupDocumentsDeliveryTests : SendOrdersBasePage
    {
        [Test, Description("Запрос на получение документов на доставку")]
        public void PickupDocumentsDeliveryTest()
        {
            string[] ordersId = SendOrdersRequest();
            ProcessIOrders();

            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

            //            подтверждения заявок
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes01.Response.Barcodes[0] + 
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseConfirmDelivery.Response.Message, "Заказ #" + responseBarcodes02.Response.Barcodes[0] +
                " подтвержден. Заказ подтвержден у вас на складе и ожидает отправки в транспортную компанию");
            Assert.AreEqual(responseConfirmDelivery.Response.Status, "20");

//            формирование документов
            var responseDocumentsDelivery = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_delivery.json",
                new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseDocumentsDelivery.Success);
            responseDocumentsDelivery.Response.View.Contains("http://s.");
            responseDocumentsDelivery.Response.View.Contains(".pdf?token=");
            responseDocumentsDelivery.Response.Confirm.Contains("http://s.");
            responseDocumentsDelivery.Response.Confirm.Contains(".pdf?token=");

//            формируем документы - нет заказов
            var responseDocumentsDeliveryError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsFalse(responseDocumentsDeliveryError.Success);
            Assert.AreEqual(responseDocumentsDeliveryError.Response.ErrorText, "Заказы не найдены");

//            формируем документы - нет id
            responseDocumentsDeliveryError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_delivery.json");
            Assert.IsFalse(responseDocumentsDeliveryError.Success);
            Assert.AreEqual(responseDocumentsDeliveryError.Response.ErrorText, "Укажите ID транспортной компании");
        }
    }
}