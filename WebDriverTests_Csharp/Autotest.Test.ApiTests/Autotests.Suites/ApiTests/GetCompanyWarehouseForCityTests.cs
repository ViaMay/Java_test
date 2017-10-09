using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class GetCompanyWarehouseForCityTests : GetConstantBasePage
    {
        [Test, Description("get_company_warehouse_for_city.json Получить склады ТК для города")]
        public void GetCompanyWarehouseForCityTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyPickupId = GetCompanyIdByName(companyPickupName + "_2");
            
//           Получаем id склада компании
            var responseObjectsList = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/" + keyShopPublic + "/get_company_warehouse_for_city.json",
                new NameValueCollection
                {
                {"city", "434"},
                {"company", companyPickupId}
                });
            Assert.AreEqual(responseObjectsList.Response[0].Name, "test_Pickup_2_Warehouse");
            Assert.AreEqual(responseObjectsList.Response[0].Street, "Улица");
            Assert.AreEqual(responseObjectsList.Response[0].House, "Дом");
            Assert.AreEqual(responseObjectsList.Response[0].Flat, "123");
            Assert.AreEqual(responseObjectsList.Response[0].Schedule, "1:15-23:24,1:15-23:24,1:15-23:24,1:15-23:24,1:15-23:24,1:15-23:24,1:15-23:24");

//            ТК у которой нет  складов - пустой список
            var responseObjectsListNull = (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/" + keyShopPublic + "/get_company_warehouse_for_city.json",
                new NameValueCollection
                {
                {"city", "434"},
                {"company", GetCompanyIdByName(companyPickupName)}
                });

            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/get_company_warehouse_for_city.json",
    new NameValueCollection
                {
                {"city", "a"},
                {"company", "ы"}
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "city:city должно быть в промежутке от 0 до 9223372036854775807;company:company должно быть в промежутке от 0 до 9223372036854775807;");
            
            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/get_company_warehouse_for_city.json",
    new NameValueCollection
                {});
            Assert.AreEqual(responseFail.Response.ErrorText, "city:city обязательно к заполнению;company:company обязательно к заполнению;");
            
        }
    }
}