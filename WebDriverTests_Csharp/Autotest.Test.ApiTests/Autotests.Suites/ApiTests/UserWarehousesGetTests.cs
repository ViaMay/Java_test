using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehousesGetTests : GetConstantBasePage
    {
        [Test, Description("получения списка складов")]
        public void UserWarehouseGetTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var responseGetUserWarehouses = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_warehouses.json",
                new NameValueCollection{});
            Assert.IsTrue(responseGetUserWarehouses.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            получения информации магазине по полученномым данным
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/"
                + responseGetUserWarehouses.Response[0].Id + ".json");

//            сравнение
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseGetUserWarehouses.Response[0].Id);
            Assert.AreEqual(responseInfo.Response.City, responseGetUserWarehouses.Response[0].City);
            Assert.AreEqual(responseInfo.Response.ContactEmail, responseGetUserWarehouses.Response[0].ContactEmail);
            Assert.AreEqual(responseInfo.Response.ContactPerson, responseGetUserWarehouses.Response[0].ContactPerson);
            Assert.AreEqual(responseInfo.Response.ContactPhone, responseGetUserWarehouses.Response[0].ContactPhone);
            Assert.AreEqual(responseInfo.Response.Flat, responseGetUserWarehouses.Response[0].Flat);
            Assert.AreEqual(responseInfo.Response.House, responseGetUserWarehouses.Response[0].House);
            Assert.AreEqual(responseInfo.Response.PostalCode, responseGetUserWarehouses.Response[0].PostalCode);
            Assert.AreEqual(responseInfo.Response.Schedule, responseGetUserWarehouses.Response[0].Schedule);
            Assert.AreEqual(responseInfo.Response.Street, responseGetUserWarehouses.Response[0].Street);
        }
    }
}