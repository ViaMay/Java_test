using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class Dd247GetOrderInfoTests : SendOrdersBasePage
    {
        [Test, Description("Dd247 получаем информацию о заказе")]
        public void OrderGetInfoTest()
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
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "1300.99"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "to_street"},
		        {"to_house", "to_house"},
		        {"to_flat", "to_flat"},
		        {"to_phone", "79129999999"},
		        {"to_add_phone", "71234567890"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            ProcessIOrders();
            SetOutordersNumber(responseCreateOrders.Response.OrderId);

//           Инфо заявки из DD247
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo) apiRequest.POST("api/v1/" + keyShopPublic
                                                                                    + "/dd247_get_order_info/" +
                                                                                    responseCreateOrders.Response
                                                                                        .OrderId + ".json",
                new NameValueCollection
                {
                    {"order_id", responseCreateOrders.Response.OrderId},
                });

             Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
             Assert.AreEqual(responseOrderInfo.Response.DeliveryCity, "Москва (г. Москва)");
             Assert.AreEqual(responseOrderInfo.Response.OrderId, responseCreateOrders.Response.OrderId);
             Assert.AreEqual(responseOrderInfo.Response.OrderNum, "dd-" + responseCreateOrders.Response.OrderId);
             Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "1301");
             Assert.AreEqual(responseOrderInfo.Response.ToHouse, "to_house");
             Assert.AreEqual(responseOrderInfo.Response.ToFlat, "to_flat");
             Assert.AreEqual(responseOrderInfo.Response.ToStreet, "to_street");
             Assert.AreEqual(responseOrderInfo.Response.CompanyOrderNumber, "test-number");
             Assert.AreEqual(responseOrderInfo.Response.StageStatus, "13");
        }
        
        [Test, Description("Dd247 получаем информацию о заказе кривой номер заказа")]
        public void OrderGetInfoErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            
//           Инфо заявки из DD247
            var responseOrderInfo = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic
                + "/dd247_get_order_info/" + "as" + ".json",
                new NameValueCollection
                {
                    {"order_id", "ad"},
                });
            Assert.AreEqual(responseOrderInfo.Response.ErrorText, "order_not_found");
        }
    }
}