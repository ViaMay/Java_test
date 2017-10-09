using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;

using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderStatusFullTests : SendOrdersBasePage
    {
        [Test, Description("Получить развернутое состояние заказа")]
        public void OrderStatusFullTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

//            не указана pickup_date то есть текущая дата
            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
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
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

//           Порверка полного статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_full_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
//            Assert.AreEqual(responseOrderStatus.Response.DeliveryDate,
//                nowDate.AddDays(3).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            Assert.AreEqual(responseOrderStatus.Response.DeliveryCompanyOderNumber, "");
            Assert.AreEqual(responseOrderStatus.Response.PostTrack, "");
            Assert.AreEqual(responseOrderStatus.Response.DeliveryCompany, deliveryCompanyId);

            ProcessIOrders();

//            еще раз проверка статуса
            responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_full_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
//            Assert.AreEqual(responseOrderStatus.Response.DeliveryDate,
//                nowDate.AddDays(3).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
            Assert.AreEqual(responseOrderStatus.Response.DeliveryCompanyOderNumber, "");
            Assert.AreEqual(responseOrderStatus.Response.PostTrack, "");
            Assert.AreEqual(responseOrderStatus.Response.DeliveryCompany, deliveryCompanyId);
        }
    }
}