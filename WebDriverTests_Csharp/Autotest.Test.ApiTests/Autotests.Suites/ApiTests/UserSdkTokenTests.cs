using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserSdkTokenTests : SendOrdersBasePage
    {
        const string userKey = "de9fe3971aa18d5d809206d2f1679b7a";
        const string keyShop = "645b9896dfb18fcef17cb00bb6845761";
        [Test]
        public void UserSdkTokenTest()
        {
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Amount must be no less than 0.001.,Dimension Side1 must be no less than 0.001.,Dimension Side2 must be no less than 0.001.,Dimension Side3 must be no less than 0.001.,Weight must be no less than 0.001.");

            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                {"amount", "1"},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Dimension Side1 must be no less than 0.001.,Dimension Side2 must be no less than 0.001.,Dimension Side3 must be no less than 0.001.,Weight must be no less than 0.001.");

            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                {"amount", "1"},
                {"dimension_side1", "1"},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Dimension Side2 must be no less than 0.001.,Dimension Side3 must be no less than 0.001.,Weight must be no less than 0.001.");

            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                {"amount", "1"},
                {"dimension_side1", "1"},
                {"dimension_side2", "1"},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Dimension Side3 must be no less than 0.001.,Weight must be no less than 0.001.");

            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                {"amount", "1"},
                {"dimension_side1", "1"},
                {"dimension_side2", "1"},
                {"dimension_side3", "1"},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Weight must be no less than 0.001.");

            var responseSdk = (ApiResponse.ResponseSdk)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_token.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                {"amount", "1"},
                {"dimension_side1", "1"},
                {"dimension_side2", "1"},
                {"dimension_side3", "1"},
                {"weight", "1"},
                });
            Assert.IsTrue(
                (responseSdk.Response.SdkToken.Contains("http://sdk.ddelivery.ru/api/v1/delivery/"))||
                (responseSdk.Response.SdkToken.Contains("http://sdk2.dev.ddelivery.ru/api/v1/delivery/"))||
                (responseSdk.Response.SdkToken.Contains("http://devsdk.ddelivery.ru/api/v1/delivery/")));
            Assert.IsTrue(responseSdk.Response.SdkToken.Contains("/index.json"));
        }
    }
}