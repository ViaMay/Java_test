using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupListTests : SendOrdersBasePage
    {
        [Test, Description("pickup_list.json Получить список заборов пользователя")]
        public void UserPickupListDateTest()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var userKey = GetUserKeyByName(userNameAndPass);
            var pickupCompanyId = GetCompanyIdByName(companyPickupName + "_2");
            var pickupDateFrom = nowDate.AddDays(10).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";
            var pickupDateFrom2 = nowDate.AddDays(10).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
            var pickupDateTo = nowDate.AddDays(11).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);

            var responseCreate =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDateFrom},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate.Response.Message, "Успешно создана заявка на забор №" + responseCreate.Response.OrderId);

            //  проверка даты
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"date_from", pickupDateFrom},
                            {"date_to", pickupDateTo},
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"date_from", pickupDateFrom},
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"date_to", pickupDateTo},
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"date_from", pickupDateTo},
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) throw new NoSuchElementException("Нашли заказ, хотя не долны были");
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"date_to", pickupDateFrom2},
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) throw new NoSuchElementException("Нашли заказ, хотя не долны были");
            }
            
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
          new NameValueCollection
                        {
                            {"date_from", "в"},
                            {"date_to", "в"},
                            {"limit", "10"},
                        });
            Assert.AreEqual(responseFail.Response.ErrorText, "date_from:date from должно быть датой;date_to:date to должно быть датой;");
        }

        [Test, Description("pickup_list.json Получить список заборов пользователя")]
        public void UserPickupListWarehouseTest()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var userKey = GetUserKeyByName(userNameAndPass);
            var pickupCompanyId = GetCompanyIdByName(companyPickupName + "_2");
            var pickupDate = nowDate.AddDays(10).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";

//            создание забора стипом 2
            var responseCreate =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate.Response.Message, "Успешно создана заявка на забор №" + responseCreate.Response.OrderId);

            //            Создание забора с типом 1
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
            ProcessIOrders();

            //  проверка склада
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"warehouse_type", "2"},
                            {"limit", "20"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }
            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                Assert.AreEqual(row.PickupWarehouseId, idPickupWarehouse);
                Assert.AreEqual(row.PickupWarehouseType, "2");
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"warehouse_id", idPickupWarehouse},
                            {"limit", "20"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            //            Не работает тут поиск по id склада компании. Если указан id склада, то тип по умолчанию 1
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(),0);

            var idWarehouse = GetWarehouseIdByName(userWarehouseName);
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"warehouse_type", "1"},
                            {"warehouse_id", idWarehouse},
                            {"limit", "20"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                Assert.AreEqual(row.PickupWarehouseId, idWarehouse);
                Assert.AreEqual(row.PickupWarehouseType, "1");
                if (row.PickupId == responseCreate.Response.OrderId) throw new NoSuchElementException("Нашли заказ, хотя не долны были");
            }

            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
          new NameValueCollection
                        {
                            {"warehouse_type", "s"},
                            {"warehouse_id", "a"},
                            {"limit", "10"},
                        });
            Assert.AreEqual(responseFail.Response.ErrorText, "warehouse_id:warehouse id должно быть в промежутке от 0 до 9223372036854775807;warehouse_type:warehouse type должно быть в промежутке от 0 до 9223372036854775807;");
        }

        [Test, Description("pickup_list.json Получить список заборов пользователя")]
        public void UserPickupListOtherTest()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var userKey = GetUserKeyByName(userNameAndPass);
            var pickupCompanyId = GetCompanyIdByName(companyPickupName + "_2");
            var pickupDate = nowDate.AddDays(10).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";

            //            создание забора стипом 2
            var responseCreate =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate.Response.Message,
                "Успешно создана заявка на забор №" + responseCreate.Response.OrderId);

            var responseCreate2 =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate2.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate2.Response.Message,
                "Успешно создана заявка на забор №" + responseCreate2.Response.OrderId);

            //      отмена
            var responseOrderCancel =
                (ApiResponse.ResponseMessage)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_cancel.json",
                        new NameValueCollection
                        {
                            {"order", responseCreate2.Response.OrderId}
                        });
            Assert.IsTrue(responseOrderCancel.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderCancel.Response.Message, "Заявка на отмену забора принята, мы делаем все возможное, чтобы оповестить об этом компанию забора.");
            
            //  проверка лимита
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "3"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 3);

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json");
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            var responseOrderListFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "0"},
                        });
            Assert.AreEqual(responseOrderListFail.Response.ErrorText, "limit:limit должно быть в промежутке от 0 до 101;");

            responseOrderListFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "101"},
                        });
            Assert.AreEqual(responseOrderListFail.Response.ErrorText, "limit:limit должно быть в промежутке от 0 до 101;");
            
            //  проверка отступа
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"offset", "2"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            responseOrderListFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"offset", "0"},
                        });
            Assert.AreEqual(responseOrderListFail.Response.ErrorText, "offset:offset должно быть в промежутке от 0 до 9223372036854775807;");


            //  проверка Прятать отмененные. Ищем все
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"hide_cancel", "0"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }
            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate2.Response.OrderId) break;
                if (row == responseOrderListRequest.Response.Rows.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"hide_cancel", "1"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId) break;
                throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }
            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate2.Response.OrderId) 
                    throw new NoSuchElementException("Нашли заказ, хотя не долны были");
            }
        }
    }
}