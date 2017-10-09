using System.Collections.Specialized;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PaymentPriceTests : SendOrdersBasePage
    {
        [Test, Description("Возможность НПП в городе или регионе")]
        public void PaymentPriceTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyId = GetCompanyIdByName(companyName);
            
            var responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151184"}
                    });
            Assert.IsTrue(responsePaymentPrice.Success);

//            запрос на город без ННП
            responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyId},
                        {"city", "151185"}
                    });
            Assert.IsFalse(responsePaymentPrice.Success);

//            запрос на неправильную компанию
            var companyIdError = "123456";
            responsePaymentPrice = apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price.json",
                   new NameValueCollection
                    {
                        {"api_key", keyShopPublic},
                        {"company", companyIdError},
                        {"city", "151185"}
                    });
            Assert.IsFalse(responsePaymentPrice.Success);
        }
    }
}