using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderCreateToShopApiKeyTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа курьерской используя апи кей магазина")]
        public void OrderCourirsTest()
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
                {"declared_price", "1000"},
		        {"payment_price", "10"},
                {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"goods_description", "Памперс"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"to_shop_api_key", keyShopPublic},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

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
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151184");
            //            курьерка
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Москва");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("2");
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "1000");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "Улица");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "Дом");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "Квартира");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "test_legalEntity");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
            Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "555444");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "10");
            Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "Памперс");
            Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment");
        }

        [Test, Description("Создание заказа курьерской используя апи кей магазина")]
        public void OrderCourirsTest02()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            //            Создания склада
            DeleteWarehouseByName(userWarehouseName + "_orders");

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_orders"},
                        {"city", "151185"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "+7 (999)999-3333"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"postal_code", "444333"},
                        {"house", "house"},
                        {"flat", "flat"},
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
            var userKey = GetUserKeyByName(userNameAndPass);
            //            удаление магазинов если они были
            DeleteShopByName(userShopName + "_orders");
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_orders"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Санкт-Петербург"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                 new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "1000"},
		        {"payment_price", "0"},
                {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"to_shop_api_key", responseShop.Response.Key},
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

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");

//            ordersPage.Table.GetRow(0).Type.WaitText("Курьерская");
//            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Санкт-Петербург");
//            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
//            orderCourirsEditingPage.ItemsCount.WaitValue("2");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151185");
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "1000");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "street");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "house");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "flat");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "contact_person");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
            Assert.AreEqual(responseOrderInfo.Response.ToPostalCode, "444333");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.PaymentPrice, "0");
            Assert.AreEqual(responseOrderInfo.Response.GoodsDescription, "Памперс");
            Assert.AreEqual(responseOrderInfo.Response.OrderComment, "order_comment");
        }

        [Test, Description("Создание заказа на самовывоз используя апи кей магазина")]
        public void OrderSelfTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName + "2");
            
            var responseCreateOrders =
                (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"type", "1"},
                        {"delivery_point", deliveryPoinId},
                        {"delivery_company", deliveryCompanyId},
                        {"shop_refnum", userShopName},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"confirmed", "false"},
                        {"weight", "4"},
                        {"declared_price", "100"},
                        {"payment_price", "0"},
                        {"to_phone", "79999999999"},
                        {"to_add_phone", "71234567890"},
                        {"goods_description", "Памперс"},
                        {"items_count", "2"},
                        {"to_shop_api_key", keyShopPublic},
                        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
                        {"is_cargo_volume", "true"},
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

//           Инфо заявки 
            var responseOrderInfo = (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/" + keyShopPublic
                + "/order_info/" + responseCreateOrders.Response.OrderId + ".json");
            Assert.AreEqual(responseOrderInfo.Response.ToEmail, userNameAndPass);
            Assert.AreEqual(responseOrderInfo.Response.ToCity, "151185");
            //            Самовывоз
            //            ordersPage.Table.GetRow(0).Route.WaitText("Москва - Санкт-Петербург");
            //            orderCourirsEditingPage.IsCargoVolume.WaitChecked();
            //            orderCourirsEditingPage.ItemsCount.WaitValue("2");
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
            Assert.AreEqual(responseOrderInfo.Response.DeclaredPice, "100");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide1, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide2, "4");
            Assert.AreEqual(responseOrderInfo.Response.DimensionSide3, "4");
            Assert.AreEqual(responseOrderInfo.Response.Weight, "4");
            Assert.AreEqual(responseOrderInfo.Response.ToStreet, "Улица");
            Assert.AreEqual(responseOrderInfo.Response.ToHouse, "Дом");
            Assert.AreEqual(responseOrderInfo.Response.ToFlat, "Квартира");
            Assert.AreEqual(responseOrderInfo.Response.ToName, "test_legalEntity");
            Assert.AreEqual(responseOrderInfo.Response.ToPhone, "79999999999");
            Assert.AreEqual(responseOrderInfo.Response.ToAddPhone, "71234567890");
        }
    }
}