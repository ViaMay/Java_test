using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderCreateWihtBarcod : SendOrdersBasePage
    {
        [Test, Description("Создание заказа курьерски и редактирование")]
        public void OrderCourirsEditingTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "1");

            var userId = GetUserIdByName(userNameAndPass);
            SetUserBarcodeLimit(userId, "10");

            var userKey = GetUserKeyByName(userNameAndPass);
            CacheFlush();
            
            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json");
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "2"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"to_city", "151184"},
                        {"delivery_company", deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "fasle"},
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
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},          
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
                    {"weight", "5"},
                    {"items_count", "3"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "В обработке");
            
//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "5");
            //            курьерка
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("2");
        }

        [Test, Description("Создание заказа самовывоза и редактирование")]
        public void OrderSelfEditingTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "1");

            var userId = GetUserIdByName(userNameAndPass);
            SetUserBarcodeLimit(userId, "10");

            var userKey = GetUserKeyByName(userNameAndPass);
            CacheFlush();

            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json");
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
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
                    {"weight", "5"},
                    {"items_count", "3"},
                });
            Assert.IsTrue(responseEditOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateOrders.Response.OrderId, responseEditOrders.Response.Id);

//           Порверка статуса заявки
            var responseOrderStatus = (ApiResponse.ResponseStatus)apiRequest.GET("api/v1/" + keyShopPublic + "/order_status.json",
                new NameValueCollection
                {
                {"order", responseCreateOrders.Response.OrderId}
                });
            Assert.AreEqual(responseOrderStatus.Response.StatusDescription, "В обработке");

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.Weight, "5");
            //            курьерка
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("2");
        }

        [Test, Description("Создание заказа курьерски и редактирование не корректное")]
        public void OrderErrorEditingTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);
            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "1");

            var userId = GetUserIdByName(userNameAndPass);
            SetUserBarcodeLimit(userId, "10");

            var userKey = GetUserKeyByName(userNameAndPass);
            CacheFlush();
            //            получение шрикодов
            var response1 =
                (ApiResponse.ResponseUserBarcodes)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_barcodes.json");
            Assert.AreEqual(response1.Response.Barcodes.Count(), 10);

            SetUserBarcodeLimit(userId, "0");

            var responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"}
                    });
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Резервирование штрихкодов, для данного Пользователя недоступно.");

            SetUserBarcodeLimit(userId, "10");
            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "0");
            CacheFlush();

            responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"}
                    });
//            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Резервирование штрих-кодов недоступно для указанной ТК");
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Ошибка обработки штрих-кодов");

            SetCompanyEnabledBbarcodPull(deliveryCompanyId, "1");
            CacheFlush();

            responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"barcodes", response1.Response.Barcodes[1] + ", 123456, " + response1.Response.Barcodes[0]},
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"}
                    });
//            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Передано недопустимое значение ШК");
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Ошибка обработки штрих-кодов");

            responseCreateOrders =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {
                            "barcodes", response1.Response.Barcodes[4] + ", " + response1.Response.Barcodes[5]
                         + ", " + response1.Response.Barcodes[6] + ", " + response1.Response.Barcodes[7]
                        },
                        {"delivery_point", deliveryPoinId},
                        {"to_city", "151184"},
                        {"delivery_company", "" + deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "300"},
                        {"to_name", "Ургудан Рабат Мантов"},
                        {"to_phone", "9999999999"},
                        {"to_add_phone", "71234567890"},
                        {"to_email", userNameAndPass},
                        {"goods_description", "Памперс"},
                        {"items_count", "1"},
                        {"is_cargo_volume", "true"},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"order_comment", "order_comment"}
                    });
//            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Ошибка обработки штрих-кодов");
            Assert.AreEqual(responseCreateOrders.Response.ErrorText, "Количество мест в заказе превышает допустимое для данной ТК");
        }
    }
}