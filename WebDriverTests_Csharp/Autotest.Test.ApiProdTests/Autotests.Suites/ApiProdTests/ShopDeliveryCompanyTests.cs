using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class ShopDeliveryCompanyTests : ConstVariablesBase
    {
        [Test, Description("Получить список доступных компаний доставки если указана компания забора и если не указана")]
        public void ShopDeliveryCompanyTest()
        {
//           Проверка если компания забора указана
            var responseDeliveryCompanу = (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_delivery_companies.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
        }
    }
}