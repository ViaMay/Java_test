using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserWarehousesGetTests : ConstVariablesBase
    {
        [Test, Description("получения списка складов")]
        public void UserWarehouseGetTest()
        {
            var responseGetUserWarehouses = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_warehouses.json",
                new NameValueCollection{});
            Assert.IsTrue(responseGetUserWarehouses.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }
    }
}