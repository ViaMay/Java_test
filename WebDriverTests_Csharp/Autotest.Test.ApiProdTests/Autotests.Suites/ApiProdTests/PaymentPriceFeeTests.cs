using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class PaymentPriceFeeTests : ConstVariablesBase
    {
        [Test, Description("Комиссия за наложенный платеж")]
        public void PaymentPriceFeeTest()
        {
            var responsePaymentFeePrice = (ApiResponse.ResponsePaymentPriceFee)
                apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", "1"},
                });
            Assert.IsTrue(responsePaymentFeePrice.Success);
            
//            запрос на неправильную компанию            
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", "123456"},
                }
                );
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Указанная компания не существует");
        }
    }
}
