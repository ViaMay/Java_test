using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PickupOrderByBarcodeTests : SendOrdersBasePage
    {
        [Test, Description("Возвращает номер заказ по штрих-коду")]
        public void PickupOrderByBarcodeTest()
        {
            var userId = GetUserIdByName(userNameAndPass);
            SetUserBarcodeLimit(userId, "10");
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);
            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "1");
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var userKey = GetUserKeyByName(userNameAndPass);
            CacheFlush();

            //            получение шрикодов
            var response =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json");
            Assert.AreEqual(response.Response.Barcodes.Count(), 10);

            //            оправляем заказ используя три номера из списка
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/pro_order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response.Response.Barcodes[2]+ 
                        ", " + response.Response.Barcodes[1] + ", " + response.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "true"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responsePackagesOrder =
                (ApiResponse.ResponseBarcodeMessage)apiRequest.GET("api/v1/pickup/" + pickupKey + "/order_by_barcode.json",
                    new NameValueCollection
                    {
                        {"barcode", response.Response.Barcodes[1]},
                    });
            Assert.AreEqual(responsePackagesOrder.Response.OrderNumber, responseCreateOrders.Response.OrderId);
            Assert.AreEqual(responsePackagesOrder.Response.TrackNumber, "");
            Assert.AreEqual(responsePackagesOrder.Response.PackageNumber, "M02");

            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/order_by_barcode.json",
                    new NameValueCollection
                    {
                    {"barcode", response.Response.Barcodes[5]},
                    });
            Assert.AreEqual(responseFail.Response.ErrorText, "Заказ не найден");
        }
    }
}