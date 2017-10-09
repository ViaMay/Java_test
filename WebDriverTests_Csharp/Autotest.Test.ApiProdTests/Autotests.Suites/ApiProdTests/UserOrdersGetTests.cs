using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserOrdersGetTests : ConstVariablesBase
    {
        [Test, Description("Запрос списка заказов")]
        public void OrdersListTest01()
        {
            

//                    запрос списка заказов - проверка что список выдан
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"api_key", userKey}
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

//             запрос поле warehouse_id некорректно
            var responseOrderListRequestFail =
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
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

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
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
        }
    }
}