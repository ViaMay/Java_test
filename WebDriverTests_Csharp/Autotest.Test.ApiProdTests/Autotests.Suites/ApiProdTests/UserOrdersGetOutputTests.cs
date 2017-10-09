using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserOrdersGetOutputTests : ConstVariablesBase
    {
        [Test, Description("Скачивание файла")]
        public void OrdersTest01()
        {
//                    запрос списка заказов - проверка что список выдан
            var responseOrderListRequest =
                (ApiResponse.ResponseMessage)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_orders.json",
                        new NameValueCollection
                        {
                            {"output_type", "2"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
        }
    }
}