using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class GetCompanyWarehouseForCityTests : ConstVariablesBase
    {
        [Test, Description("get_company_warehouse_for_city.json Получить склады ТК для города")]
        public void GetCompanyWarehouseForCityTest()
        {
//           Получаем id склада компании
            var responseObjectsList = (ApiResponse.ResponseObjectsList)apiRequest.GET("api/v1/" + keyShopPublic + "/get_company_warehouse_for_city.json",
                new NameValueCollection
                {
                {"city", "151184"},
                {"company", companyId}
                });
            Assert.IsTrue(responseObjectsList.Success);

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