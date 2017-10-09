using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserPickupListTests : ConstVariablesBase
    {
        [Test, Description("pickup_list.json Получить список заборов пользователя")]
        public void UserPickupListDateTest()
        {
            //  проверка даты
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
        }
    }
}