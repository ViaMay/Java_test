using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserShopGetTests : ConstVariablesBase
    {
        [Test, Description("получения списка магазинов")]
        public void ShopGetTest()
        {
//            получаеминформаию по всем магазинам
            var responseGetShops = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_shops.json",
                new NameValueCollection{});
            Assert.IsTrue(responseGetShops.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }
    }
}