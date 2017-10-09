using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserShopGetTests : GetConstantBasePage
    {
        [Test, Description("получения списка магазинов")]
        public void ShopGetTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

//            получаеминформаию по всем магазинам
            var responseGetShops = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_shops.json",
                new NameValueCollection{});
            Assert.IsTrue(responseGetShops.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            получения информации магазине по полученномым данным
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/"
                + responseGetShops.Response[0].Id + ".json");

//            сравнение
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseGetShops.Response[0].Id);
            Assert.AreEqual(responseInfo.Response.Name, responseGetShops.Response[0].Name);
            Assert.AreEqual(responseInfo.Response.Address, responseGetShops.Response[0].Address);
            Assert.AreEqual(responseInfo.Response.Warehouse, responseGetShops.Response[0].Warehouse);
        }
    }
}