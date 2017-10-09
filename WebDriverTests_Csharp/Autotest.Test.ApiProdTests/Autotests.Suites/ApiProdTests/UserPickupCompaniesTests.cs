using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserPickupCompaniesTests : ConstVariablesBase
    {
        [Test, Description("pickup_companies.json Получить список компаний забора")]
        public void UserPickupCompaniesTest()
        {
//            получение шрикодов
            var responsePickupCompanies =
                (ApiResponse.ResponsePickupCompanies)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_companies.json");

            Assert.IsTrue(responsePickupCompanies.Success);
        }
    }
}