using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PickupOrdersTests : SendOrdersBasePage
    {
        [Test, Description("Список заказов на складе сортировки")]
        public void PickupOrdersTest()
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

//            подтверждаем что заказ на складе
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes01.Response.Barcodes[0] }, });
            Assert.IsTrue(responseConfirmDelivery.Success);
            responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes02.Response.Barcodes[0]} });
            Assert.IsTrue(responseConfirmDelivery.Success);

//            запрос списка
            var responseOrdersPickup = (ApiResponse.ResponsePickupOrders)apiRequest.GET("api/v1/pickup/" + pickupKey + "/orders.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });
            Assert.IsTrue(responseOrdersPickup.Success);

            var responseRowOrdersPickup = FindRowById(ordersId[0], responseOrdersPickup);
            Assert.AreEqual(responseRowOrdersPickup.DeliveryCompanyId, deliveryCompanyId);

            responseRowOrdersPickup = FindRowById(ordersId[1], responseOrdersPickup);
            Assert.AreEqual(responseRowOrdersPickup.DeliveryCompanyId, deliveryCompanyId);
        }

        private ApiResponse.MessagePickupOrders FindRowById(string ordersId, ApiResponse.ResponsePickupOrders response)
        {
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Id == ordersId)
                    return response.Response[i];
            }
            throw new Exception(string.Format("не найдена заказ с id {0} в списке всех заказов", ordersId));
        }
    }
}