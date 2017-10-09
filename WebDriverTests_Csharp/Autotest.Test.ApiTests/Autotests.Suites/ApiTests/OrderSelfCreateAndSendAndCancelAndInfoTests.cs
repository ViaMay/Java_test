using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderSelfCreateAndSendAndCancelAndInfoTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа на самовывоз, запрос статусов, информации, подтверждения и отмена заявки")]
        public void OrderSelfTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
//		        {"to_city", "151184"},
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
		        {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"to_shop_api_key", keyShopPublic},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "В обработке");

//            получение инормации
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");

            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.ItemsCount.WaitValue("3");
            //            orderPage.TableSender.IsCargoVolume.WaitText("да"); 
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "Улица");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "Дом");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "Квартира");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "Ургудан Рабат Мантов");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "100");
            Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "300");
            Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "Памперс");
            Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment");
            Assert.AreEqual(responseOrderInfo.Response.DeliveryCompanyName, companyName);
            Assert.AreEqual(responseOrderInfo.Response.DeliveryPointid, deliveryPoinId);

//           Подтверждение заявки
            var responseConfirmationOrders = apiRequest.POST("api/v1/" + keyShopPublic + "/order_confirm.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId},
                });
            Assert.IsTrue(responseConfirmationOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

//           Порверка статуса заявки
            responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "Подтверждена");

//           Инфо заявки 
            responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToName, "Ургудан Рабат Мантов");
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
							        
//         Отмена ордера (неудачная)
            var responseOrderCancelFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderCancelFail.Response.ErrorText, "This order can not be canceled");

            ProcessIOrders();

//           Порверка статуса заявки
            responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "Обработка заказа в компании доставки");

            var responseOrderCancel = (ApiResponse.ResponseTrueCancel)apiRequest.GET("api/v1/" + keyShopPublic + "/order_cancel.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderCancel.Response.OrderId, responseCreateOrders.Response.OrderId);
        }
    }
}