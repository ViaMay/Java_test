using System;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class ShopDeliveryCompanyСonditionsTests : ConstVariablesBase
    {
        [Test, Description("Получить информацию о переводе наличных денежных средств")]
        public void ShopDeliveryCompanyСonditionsTest()
        {
//           Получить информацию о переводе наличных денежных средств
            var responseDeliveryCompanу = (ApiResponse.ResponseCompaniesСonditions)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_delivery_companies_conditions.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
        }
    }
}