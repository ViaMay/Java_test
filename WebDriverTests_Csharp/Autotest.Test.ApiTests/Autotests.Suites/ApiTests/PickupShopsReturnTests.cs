using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
namespace Autotests.Tests.ApiTests
{
    public class PickupShopsReturnTests : SendOrdersBasePage
    {
        [Test, Description("Список интернет-магазинов, имеющих заказы на возврат на складе сортировки")]
        public void PickupShopsReturnTest()
        {
            var ordersId = SendOrdersRequest();
            ProcessIOrders();
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseBarcodes01 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[0] }, });
            var responseBarcodes02 = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", ordersId[1] }, });

//            подтверждаем что заказы на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
//            делаем возврат
            var responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");
            responseConfirmReturn = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_return.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmReturn.Success);
            Assert.AreEqual(responseConfirmReturn.Response.Status, "40");

//            запрашиваем магазины 
            var responsePickupShop =
                (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/pickup/" + pickupKey + "/shops_return.json");
            Assert.IsTrue(responsePickupShop.Success);
            Assert.AreEqual(responsePickupShop.Response.Count(), 1);
            Assert.AreEqual(responsePickupShop.Response[0].Name, userShopName);
            string shopId = GetShopIdByName(userShopName);
            Assert.AreEqual(responsePickupShop.Response[0].Id, shopId);

//            формируем документы на возврат
            var responseDocumentsReturn = (ApiResponse.ResponseDocumentPickup)apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_return.json",
               new NameValueCollection { { "shop_id", shopId }, });
            Assert.IsTrue(responseDocumentsReturn.Success);

//            снова запрашиваем магазин когда нет никого
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/shops_return.json");
            Assert.IsFalse(responseError.Success);
            Assert.AreEqual(responseError.Response.ErrorText, "Заказы не найдены");
        }
    }
}