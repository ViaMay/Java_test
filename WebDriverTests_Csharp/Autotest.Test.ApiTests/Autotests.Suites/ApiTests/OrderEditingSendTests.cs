using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderEditingSendTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа курьерски и редактирование")]
        public void OrderCourirsEditingTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
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
                        {"to_postal_code", "123123"},
                        {"to_street", "Барна"},
                        {"to_house", "3a"},
                        {"to_flat", "12"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"is_cargo_volume", "true"},
                        {"packing", "false"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseEditOrders = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                                                     responseCreateOrders.Response
                                                                                         .OrderId + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_postal_code", "333333"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"to_add_phone", "74444444444"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"},
                    {"is_cargo_volume", "false"},
                    {"items_count", "3"},
                    {"packing", "true"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "Подтверждена");

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");

            //            курьерка
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("1");
//            orderPage.TableSender.Name.WaitText(legalEntityName);
//            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
//            orderPage.TableSender.Delivery.WaitText("Курьерская");
//            orderPage.TableSender.OrderComment.WaitText("");
            //            orderPage.TableSender.IsCargoVolume.WaitText("да"); 
            //            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "5");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "5");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "5");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "5");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, "2" + userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "to_street");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "to_house");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "to_flat");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "to_name");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "1199999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "74444444444");
            Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "333333");
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "1100");
            Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "1300");
            Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "goods_description");
            Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment2");
            Assert.AreEqual(responseOrderInfo.Response.Packing, "false");
            Assert.AreEqual(responseOrderInfo.Response.DeliveryCompanyName, companyName);
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.DeliveryPrice, "38");
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.PickupPrice, "21");
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.TotalPrice, "59");
        }

        [Test, Description("Создание заказа самовывоза и редактирование")]
        public void OrderSelfEditingTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"delivery_point", deliveryPoinId},
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
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "1234567891234567890123456789001"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"},
                        {"packing", "true"},
                    });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

         var responseEditOrders = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                                                     responseCreateOrders.Response
                                                                                         .OrderId + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"to_add_phone", "74444444444"},
                    {"goods_description", "goods_description"},
                    {"to_email", "2" + userNameAndPass},
                    {"order_comment", "order_comment2"},
                    {"is_cargo_volume", "false"},
                    {"items_count", "3"},
                    {"packing", "false"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "Подтверждена");

//            получение инормации
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");
            //            Самовывоз
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("1");
            //            orderPage.TableSender.Name.WaitText(legalEntityName);
            //            orderPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            //            orderPage.TableSender.Delivery.WaitText("Самовывоз");
            //            orderPage.TableSender.IsCargoVolume.WaitText("да"); 
            //            orderPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "5");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "5");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "5");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "5");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, "2" + userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "to_street");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "to_house");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "to_flat");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "to_name");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "1199999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "74444444444");
            Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "");
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "1100");
            Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "1300");
            Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "goods_description");
            Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment2");
            Assert.AreEqual(responseOrderInfo.Response.DeliveryCompanyName, companyName);
            Assert.AreEqual(responseOrderInfo.Response.Packing, "true");
            Assert.AreEqual(responseOrderInfo.Response.DeliveryPointid, deliveryPoinId);
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.DeliveryPrice, "45");
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.PickupPrice, "21");
            Assert.AreEqual(responseOrderInfo.Response.PriceInfo.TotalPrice, "66");
        }

        [Test, Description("Создание заказа курьерски и редактирование не корректное")]
        public void OrderErrorEditingTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
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

            string orderIdError = "123456";
            var responseFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                                          orderIdError + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", orderIdError},
                    {"dimension_side1", "5"},
                    {"dimension_side2", "5"},
                    {"dimension_side3", "5"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", userNameAndPass}
                });
            Assert.IsFalse(responseFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseFail.Response.ErrorText, "Order not found");

            var responseOrderFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                                      responseCreateOrders.Response.OrderId
                                                                      + ".json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"confirmed", "true"},
                    {"weight", "5"},
                    {"declared_price", "1100"},
                    {"payment_price", "1300"},
                    {"to_name", "to_name"},
                    {"to_street", "to_street"},
                    {"to_house", "to_house"},
                    {"to_flat", "to_flat"},
                    {"to_phone", "1199999999"},
                    {"goods_description", "goods_description"},
                    {"to_email", "123"}
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.ErrorText, "Email должно быть корректным адресом электронной почты");

            responseOrderFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"dimension_side1", "500"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.ErrorText, "Ошибка просчета цены, или маршрут недоступен");


            responseOrderFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"order_comment", "1231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111111231231111111111111111111111112"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.ErrorText, "Длина поля &laquo;Комментарий к заказу&raquo; должна быть не более 300 символов");

            responseOrderFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_update/" +
                                                          responseCreateOrders.Response.OrderId
                                                          + ".json",
                                                          new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"order_id", responseCreateOrders.Response.OrderId},
                    {"to_add_phone", "1234567890123456789012345678901"},
                });
            Assert.IsFalse(responseOrderFail.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseOrderFail.Response.ErrorText, "Длина поля &laquo;Дополнительный телефон получателя&raquo; должна быть не более 30 символов");
        
        }
    }
}