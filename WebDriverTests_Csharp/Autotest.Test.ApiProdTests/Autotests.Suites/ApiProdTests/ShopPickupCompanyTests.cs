using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class ShopPickupCompanyTests : ConstVariablesBase
    {
        [Test, Description("Получить компанию забора ИМ")]
        public void ShopPickupCompanуTest()
        {
//           Порверка если компания забора указана
            var responsePickupCompanу = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_pickup_company.json");
            Assert.IsFalse(responsePickupCompanу.Success);}
    }
}