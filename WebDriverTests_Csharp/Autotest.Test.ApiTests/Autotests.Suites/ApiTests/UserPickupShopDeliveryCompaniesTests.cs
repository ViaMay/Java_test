using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupShopDeliveryCompaniesTests : SendOrdersBasePage
    {
        [Test, Description("pickup_companies.json Получить список компаний забора")]
        public void UserPickupShopDeliveryCompaniesTest()
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
		        {"confirmed", "true"},
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
//            получение шрикодов
            ProcessIOrders();

            var userKey = GetUserKeyByName(userNameAndPass);
            var responsePickupWarehouseCompanies =
                (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_company_warehouses.json");

            var pickupWarehouseCompanyRow = FindRowByName("test_Pickup_2_Warehouse", responsePickupWarehouseCompanies);
            Assert.AreEqual(pickupWarehouseCompanyRow.Name, "test_Pickup_2_Warehouse");
            Assert.AreEqual(pickupWarehouseCompanyRow.Id, idPickupWarehouse);
        }

        private ApiResponse.MessageCompaniesOrShops FindRowByName(string pickupCompanyName, ApiResponse.ResponseCompaniesOrShops responsePickupCompanies)
        {
            for (var i = 0; i < responsePickupCompanies.Response.Count(); i++)
            {
                if (responsePickupCompanies.Response[i].Name == pickupCompanyName)
                    return responsePickupCompanies.Response[i];
            }
            throw new Exception(string.Format("не найден город с name {0} в списке всех городов", pickupCompanyName));
        }
    }
}