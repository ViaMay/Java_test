using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserPickupShopDeliveryCompaniesTests : ConstVariablesBase
    {
        [Test, Description("pickup_companies.json Получить список компаний забора")]
        public void UserPickupShopDeliveryCompaniesTest()
        {
            var responsePickupWarehouseCompanies =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_company_warehouses.json");
            Assert.IsTrue(responsePickupWarehouseCompanies.Success);
        }
    }
}