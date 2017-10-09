using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class PaymentPriceFeeTests : SendOrdersBasePage
    {
        [Test, Description("Комиссия за наложенный платеж")]
        public void PaymentPriceFeeTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyId = GetCompanyIdByName(companyName);

            var responsePaymentFeePrice = (ApiResponse.ResponsePaymentPriceFee)
                apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", "1"},
                });
            Assert.IsTrue(responsePaymentFeePrice.Success);
            Assert.AreEqual(responsePaymentFeePrice.Response.From, "0");
            Assert.AreEqual(responsePaymentFeePrice.Response.Min, "0");
            Assert.AreEqual(responsePaymentFeePrice.Response.Percent, "1.77");
            Assert.AreEqual(responsePaymentFeePrice.Response.PercentCard, "2.95");
            
//            запрос на компанию у которой нет комиссии
            var responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
                new NameValueCollection
                {
                    {"api_key", keyShopPublic},
                    {"company", companyId},
                }
                );
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Не найдено комиссий для указанной компании");

            //            запрос на неправильную компанию            
            responseError = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/payment_price_fee.json",
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
