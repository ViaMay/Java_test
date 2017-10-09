using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderCourirsDeleteTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа курьерской")]
        public void OrderCourirsDeleteTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");

             Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
             Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "100");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
             Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
             Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
             Assert.AreEqual(responseOrderInfo.Response.ToStreet, "Барна");
             Assert.AreEqual(responseOrderInfo.Response.ToHouse, "3a");
             Assert.AreEqual(responseOrderInfo.Response.ToFlat, "12");
             Assert.AreEqual(responseOrderInfo.Response.ToName, "Ургудан Рабат Мантов");
             Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
             Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
             Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "");
             Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
             Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "300");
             Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "Памперс");
             Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment");

             var responseOrderDelete = (ApiResponse.ResponseAddObject)apiRequest.GET("api/v1/" + keyShopPublic + "/order_delete/"
                 + responseCreateOrders.Response.OrderId + ".json");
             Assert.AreEqual(responseOrderDelete.Response.Id, responseCreateOrders.Response.OrderId);

             var responseOrderDeleteError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_delete/"
                 + responseCreateOrders.Response.OrderId + ".json");
             Assert.AreEqual(responseOrderDeleteError.Response.ErrorText, "Order not found");

//           Инфо заявки 
             responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                 + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");

             Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
             Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "100");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
             Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
             Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
             Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
             Assert.AreEqual(responseOrderInfo.Response.ToStreet, "Барна");
             Assert.AreEqual(responseOrderInfo.Response.ToHouse, "3a");
             Assert.AreEqual(responseOrderInfo.Response.ToFlat, "12");
             Assert.AreEqual(responseOrderInfo.Response.ToName, "Ургудан Рабат Мантов");
             Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
             Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
             Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "");
             Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
             Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "300");
             Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "Памперс");
             Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment");
        }
    }
}