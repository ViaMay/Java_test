using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CreatePickupRegistryTests : SendOrdersBasePage
    {
        [Test, Description("Создание реестра отгрузок")]
        public void CreatePickupRegistryTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var ordersId =SendOrdersRequest();
            var response = (ApiResponse.ResponsePickupRegistry)apiRequest.GET("api/v1/" + keyShopPublic + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids", ordersId[0] + "," + ordersId[1] + "," + "asd,333" } });
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Response.CreateForOrders[0], ordersId[0]);
            Assert.AreEqual(response.Response.CreateForOrders[1], ordersId[1]);
            Assert.AreEqual(response.Response.NotFoundOrders[0], "asd");
            Assert.AreEqual(response.Response.NotFoundOrders[1], "333");
            Assert.AreEqual(response.Response.RegistryIds.Count(), 1);
        }
        
        [Test, Description("Создание реестра отгрузок")]
        public void CreatePickupRegistryTest02()
        {
            var ordersId =SendOrdersRequest();
            ProcessIOrders();
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var shopKey = GetShopKeyByName(userShopName + "_ApiUsePickupShop");
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            //            отправляем заказ
            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + shopKey + "/pro_order_create.json",
                new NameValueCollection
                {
                {"api_key", shopKey},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "0"},
		        {"payment_price", "0"},
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
		        {"order_comment", "order_comment"},
		        {"pickup_type", "2"},
		        {"pickup_warehouse", idPickupWarehouse}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var response = (ApiResponse.ResponsePickupRegistry)apiRequest.GET("api/v1/" + shopKey + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids", ordersId[0] + "," + ordersId[1] + "," + responseCreateOrders.Response.OrderId } });
            Assert.IsTrue(response.Success); 
            Assert.AreEqual(response.Response.CreateForOrders[0], ordersId[0]);
            Assert.AreEqual(response.Response.CreateForOrders[1], ordersId[1]);
            Assert.AreEqual(response.Response.CreateForOrders[2], responseCreateOrders.Response.OrderId);
            Assert.AreEqual(response.Response.RegistryIds.Count(), 2);
        }

        [Test, Description("Создание реестра отгрузок, смена статусов")]
        public void CreatePickupRegistryChangeStatusTest()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var shopKey = GetShopKeyByName(userShopName + "_ApiUsePickupShop");
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            //            отправляем заказ
            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + shopKey + "/pro_order_create.json",
                new NameValueCollection
                {
                {"api_key", shopKey},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "0"},
		        {"payment_price", "0"},
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
		        {"order_comment", "order_comment"},
		        {"pickup_type", "2"},
		        {"pickup_warehouse", idPickupWarehouse}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var response = (ApiResponse.ResponsePickupRegistry)apiRequest.GET("api/v1/" + shopKey + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids",  responseCreateOrders.Response.OrderId } });
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Response.CreateForOrders[0], responseCreateOrders.Response.OrderId);
            Assert.AreEqual(response.Response.RegistryIds.Count(), 1);

            var responseStatus = apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                    new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "10"},
                   });
            Assert.IsTrue(responseStatus.Success);

            responseStatus = apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                    new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "30"},
                   });
            Assert.IsTrue(responseStatus.Success);

            responseStatus = apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                    new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "50"},
                   });
            Assert.IsTrue(responseStatus.Success);

            var responseError =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                   new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "60"},
                   });
            Assert.AreEqual(responseError.Response.ErrorText, "Undefined status");
        }

        [Test, Description("Создание реестра отгрузок, смена статусов")]
        public void CreatePickupRegistryChangeStatusTest02()
        {
            var shopKey = GetShopKeyByName(userShopName);
            var ordersId = SendOrdersRequest();
           
            var response = (ApiResponse.ResponsePickupRegistry)apiRequest.GET("api/v1/" + shopKey + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids", ordersId[0] + "," + ordersId[1] } });
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Response.RegistryIds.Count(), 1);

            ProcessIOrders();
            SetOutordersNumber(ordersId[0]);

            var responseStatusError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                new NameValueCollection {
                {"pickup_registry_id", response.Response.RegistryIds[0]},
                {"status", "20"},
                });
            Assert.AreEqual(responseStatusError.Response.ErrorText, "Order id = \"" + ordersId[1] + "\" do not have status S_API_SEND");


            var responseStatus = apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                    new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "20"},
                   });
            Assert.IsTrue(responseStatus.Success);

            responseStatus = apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                    new NameValueCollection {
                       {"pickup_registry_id", response.Response.RegistryIds[0]},
                       {"status", "40"},
                   });
            Assert.IsTrue(responseStatus.Success);
        }

        [Test, Description("Создание реестра отгрузок не удачное")]
        public void CreatePickupRegistryErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

            var response =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids", "" } });
            Assert.AreEqual(response.Response.ErrorText, "order_ids:order ids обязательно к заполнению;");
        }

        [Test, Description("Создание смена статуса не удачное")]
        public void CreatePickupRegistryChangeStatusErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

            var response =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/change_pickup_registry_status.json");
            Assert.AreEqual(response.Response.ErrorText, "pickup_registry_id:pickup registry id обязательно к заполнению;status:status обязательно к заполнению;");

            response =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/change_pickup_registry_status.json",
                   new NameValueCollection {
                       {"pickup_registry_id", "d"},
                       {"status", "d"},
                   });
            Assert.AreEqual(response.Response.ErrorText, "pickup_registry_id:pickup registry id должно быть в промежутке от 0 до 9223372036854775807;status:status должно быть в промежутке от 0 до 9223372036854775807;");
       
            response =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/change_pickup_registry_status.json",
                   new NameValueCollection {
                       {"pickup_registry_id", "40000"},
                       {"status", "10"},
                   });
            Assert.AreEqual(response.Response.ErrorText, "Pickup Registry not found");
        }
        
        [Test, Description("Создание смена статуса не удачное")]
        public void CreatePickupRegistryChangeStatusErrorTest02()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var shopKey = GetShopKeyByName(userShopName + "_ApiUsePickupShop");
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            //            отправляем заказ
            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + shopKey + "/pro_order_create.json",
                new NameValueCollection
                {
                {"api_key", shopKey},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "0"},
		        {"payment_price", "0"},
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
		        {"order_comment", "order_comment"},
		        {"pickup_type", "2"},
		        {"pickup_warehouse", idPickupWarehouse}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var response = (ApiResponse.ResponsePickupRegistry)apiRequest.GET("api/v1/" + shopKey + "/create_pickup_registry.json",
                   new NameValueCollection { { "order_ids", responseCreateOrders.Response.OrderId } });
            Assert.IsTrue(response.Success);
            Assert.AreEqual(response.Response.CreateForOrders[0], responseCreateOrders.Response.OrderId);
            Assert.AreEqual(response.Response.RegistryIds.Count(), 1);

            var responseStatus = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                new NameValueCollection {
                {"pickup_registry_id", response.Response.RegistryIds[0]},
                {"status", "40"},
                });
            Assert.AreEqual(responseStatus.Response.ErrorText, "Registry do not have status CLOSE");

            responseStatus = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                new NameValueCollection {
                {"pickup_registry_id", response.Response.RegistryIds[0]},
                {"status", "20"},
                });
            Assert.AreEqual(responseStatus.Response.ErrorText, "Order id = \"" + responseCreateOrders.Response.OrderId + "\" do not have status S_API_SEND");

            responseStatus = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + shopKey + "/change_pickup_registry_status.json",
                new NameValueCollection {
                {"pickup_registry_id", response.Response.RegistryIds[0]},
                {"status", "20"},
                });
            Assert.AreEqual(responseStatus.Response.ErrorText, "Not found orders");
        }
    }
}