using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupDocumentsReturnTests : SendOrdersBasePage
    {
        [Test, Description("Запрос на получение документов на возврат")]
        public void PickupDocumentsReturnTest()
        {
            string[] ordersId = SendOrdersRequest();
            ProcessIOrders();
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            подтверждаем что заказ на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);

//            делаем возврат
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

//            формируем документы на возврат
            var shopId = GetShopIdByName(userShopName);
            var responseDocumentsReturn = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsTrue(responseDocumentsReturn.Success);
            responseDocumentsReturn.Response.Confirm.Contains("http://s.");
            responseDocumentsReturn.Response.Confirm.Contains(".pdf?token=");

//            формируем документы - нет заказов
            var responseDocumentsError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsFalse(responseDocumentsError.Success);
            Assert.AreEqual(responseDocumentsError.Response.ErrorText, "Заказы не найдены");

//            формируем документы - нет id
            responseDocumentsError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_return.json");
            Assert.IsFalse(responseDocumentsError.Success);
            Assert.AreEqual(responseDocumentsError.Response.ErrorText, "Укажите ID интернет-магазина");
        }
    }
}