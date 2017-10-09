using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserOrdersGetTests : SendOrdersBasePage
    {
        [Test, Description("Запрос списка заказов")]
        public void OrdersListTest01()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var shopId = GetShopIdByName(userShopName);
            var keyShopPublic = GetShopKeyByName(userShopName);
            var usersWarehousId = GetWarehouseIdByName(userWarehouseName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
//                     создание нескольких ордеров
            int n = 0;
            var responseCreateOrder = new ApiResponse.ResponseAddOrder[3];

            while (n < 3)
            {
                responseCreateOrder[n] =
                    (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                        new NameValueCollection
                        {
                            {"api_key", keyShopPublic},
                            {"type", "2"},
                            {"to_city", "151185"},
                            {"delivery_company", deliveryCompanyId},
                            {"shop_refnum", userShopName + "1"},
                            {"dimension_side1", "4"},
                            {"dimension_side2", "4"},
                            {"dimension_side3", "4"},
                            {"confirmed", "true"},
                            {"weight", "4"},
                            {"declared_price", "100"},
                            {"payment_price", "0"},
                            {"to_name", "Ургудан Рабат Мантов"},
                            {"to_street", "Барна"},
                            {"to_house", "3a"},
                            {"to_flat", "12"},
                            {"to_phone", "9999999999"},
                            {"to_email", userNameAndPass},
                            {"goods_description", "Памперс"},
                            {"metadata", "[{'name': 'Описание', 'article': 'Артикул', 'count': 1}]"}
                        });
                Assert.IsTrue(responseCreateOrder[n].Success, "Ожидался ответ true на отправленный запрос POST по API");
                n = n + 1;
            }

//                    запрос списка заказов - проверка что список выдан
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

//                      запрос с лимитом x меньше N числа заказов
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"limit", "3"}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 3);

//                      запрос с лимитом больше 100 теперь пропускаем
             responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"limit", "102"}
                        });
             Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

//                          запрос лимит 0 смещение 1 
             responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"limit", "0"},
                            {"offset", "1"}
                        });
             Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
             Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0);

//                         задаем смещение и проверяем по ИД ордера
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"limit", "1"},
                            {"offset", "2"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 1);

//                         задаем смещение и проверяем по ИД ордера
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"limit", "3"},
                            {"offset", "2"}
                        });
            Assert.IsTrue(
                (responseOrderListRequest.Response.Rows[0].Id==responseCreateOrder[0].Response.OrderId)||
                (responseOrderListRequest.Response.Rows[0].Id==responseCreateOrder[1].Response.OrderId),
                "Ожидалось совпадение значений в связи со смещением");
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 3);

//              запрос поле shop_id корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"shop_id", shopId},
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].Shop, shopId);
            }


//            запрос order_id корректно 
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"id", responseCreateOrder[2].Response.OrderId}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows[0].Id, responseCreateOrder[2].Response.OrderId);

//            запрос order_id некорректно  
            var responseOrderListRequestFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"id", "000"}
                        });
            Assert.AreEqual(responseOrderListRequestFail.Response.ErrorText,
                "id:id должно быть в промежутке от 0 до 9223372036854775807;", "Ожидалось количество строк 0");

//          запрос type корректно 1   
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"type", "1"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].Type, "1");
            }

//          запрос type корректно 2   
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"type", "2"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].Type, "2");
            }

//            запрос type некорректно 
            responseOrderListRequestFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"type", "3"}
                        });
            Assert.AreEqual(responseOrderListRequestFail.Response.ErrorText,
                "type:type может содержать один из доступных вариантов;");

//              запрос to_city корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"to_city_id", "151185"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].CityTo, "151185");
            }

            //              запрос to_city некорректно
            responseOrderListRequestFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"to_city_id", "hghjghj"}
                        });
            Assert.AreEqual(responseOrderListRequestFail.Response.ErrorText,
                "to_city_id:to city id должно быть в промежутке от 0 до 9223372036854775807;");

//              запрос to_city некорректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"to_city_id", "9223372036854775806"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0, "Ожидалось ноль заказов");

//              запрос from_city_id корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"from_city_id", "151184"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].CityFrom, "151184");
            }

//              запрос from_city_id некорректно
            responseOrderListRequestFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"from_city_id", "hghjghj"}
                        });
            Assert.AreEqual(responseOrderListRequestFail.Response.ErrorText,
                "from_city_id:from city id должно быть в промежутке от 0 до 9223372036854775807;");

//              запрос from_city_id некорректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"from_city_id", "9223372036854775806"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0, "Ожидалось ноль заказов");

            //              запрос status корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"status", "20"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].Status, "20");
            }

//              запрос status некорректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"status", "9223372036854775806"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0, "Ожидалось ноль заказов");

//              запрос shop_refnum корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"shop_refnum", userShopName + "1"}
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].ShopRefnum, userShopName + "1");
            }

//              запрос shop_refnum некорректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"shop_refnum", "9223372036854775806"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0, "Ожидалось ноль заказов");

//              запрос поле warehouse_id корректно
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"warehouse_id", usersWarehousId},
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.AreEqual(responseOrderListRequest.Response.Rows[i].Warehouse, usersWarehousId);
            }

//             запрос поле warehouse_id некорректно
            responseOrderListRequestFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"warehouse_id", "000"}
                        });
            Assert.AreEqual(responseOrderListRequestFail.Response.ErrorText,
                "warehouse_id:warehouse id должно быть в промежутке от 0 до 9223372036854775807;");

//              запрос почта россии        
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"type", "2"},
                            {"post_filter", "1"}
                        });
            Assert.AreEqual(responseOrderListRequest.Response.Rows.Count(), 0);

            //              запрос почта россии        
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey},
                            {"type", "2"},
                            {"post_filter", "2"}
                        });
            Assert.IsTrue(responseOrderListRequest.Response.Rows.Any());

            //              запрос почта россии     id 44, 91, 50, 54, 89, 55, 88, 90     
            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + "de9fe3971aa18d5d809206d2f1679b7a" + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", "de9fe3971aa18d5d809206d2f1679b7a"},
                            {"post_filter", "1"},
                            {"type", "2"},
                        });
            for (var i = 0; i < responseOrderListRequest.Response.Rows.Count(); i++)
            {
                Assert.IsTrue(
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "44") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "50") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "54") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "55") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "88") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "89") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "90") ||
                    (responseOrderListRequest.Response.Rows[i].DeliveryCompany == "91"));
            }

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + "de9fe3971aa18d5d809206d2f1679b7a" + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", "de9fe3971aa18d5d809206d2f1679b7a"},
                            {"packing", "true"},
                            {"limit", "1"},
                        });
            Assert.IsTrue(responseOrderListRequest.Response.Rows[0].Packing == "1");

            responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + "de9fe3971aa18d5d809206d2f1679b7a" + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", "de9fe3971aa18d5d809206d2f1679b7a"},
                            {"packing", "false"},
                            {"limit", "1"},
                        });
            Assert.IsTrue(responseOrderListRequest.Response.Rows[0].Packing == "0");
        }
    }
}