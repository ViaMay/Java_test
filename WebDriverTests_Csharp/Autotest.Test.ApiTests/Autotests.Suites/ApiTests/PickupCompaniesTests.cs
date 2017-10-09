using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PickupCompaniesTests : SendOrdersBasePage
    {
        [Test, Description("Список компаний, имеющих заказы на доставку на складе сортировки")]
        public void PickupCompaniesTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyId = GetCompanyIdByName(companyName);
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            
            var responseCreateOrder =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"to_city", "151184"},
                        {"delivery_company", companyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "true"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_street", "Барна"},
                        {"to_house", "3a"},
                        {"to_flat", "12"},
                        {"to_phone", "9999999999"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                    });
            Assert.IsTrue(responseCreateOrder.Success, "Ожидался ответ true на отправленный запрос POST по API");

            ProcessIOrders();

            var userKey = GetUserKeyByName(userNameAndPass);

            var responseBarcodes = (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_packages_by_order.json",
                new NameValueCollection { { "order_id", responseCreateOrder.Response.OrderId }, });

            //            шлем запрос подтверить их
            var responseConfirmDelivery = (ApiResponse.ResponseStatusConfirm)apiRequest.GET("api/v1/pickup/" + pickupKey + "/confirm_delivery.json",
                new NameValueCollection { { "barcode", responseBarcodes.Response.Barcodes[0] }, });

            Assert.IsTrue(responseConfirmDelivery.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responsePickupCompanies =
                (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/pickup/" + pickupKey + "/companies.json");
            Assert.IsTrue(responsePickupCompanies.Success);
            Assert.AreEqual(responsePickupCompanies.Response[0].Id, companyId);
            Assert.AreEqual(responsePickupCompanies.Response[0].Name, companyName);
        }
        
        [Test, Description("Список компаний, имеющих заказы на доставку на складе сортировки - не удачная отправка"), Ignore]
//        ignore - так как пока не понятнокак сформирвать все документы так как теперь нцжен ответ для вход. заявки
        public void PickupCompaniesErrorTest()
        {
            var pickupKey = GetUserKeyByName(pickupNameAndPass);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
//            формируем документы чтобы не было списка заказов 
            var responseDocumentsDeliveryError = apiRequest.GET("api/v1/pickup/" + pickupKey + "/documents_delivery.json",
               new NameValueCollection { { "delivery_company_id", deliveryCompanyId }, });

//            Заказы не найден
            var responsePickupCompaniesError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/pickup/" + pickupKey + "/companies.json");
            Assert.IsFalse(responsePickupCompaniesError.Success);
            Assert.AreEqual(responsePickupCompaniesError.Response.ErrorText, "Заказы не найдены");
        }
    }
}