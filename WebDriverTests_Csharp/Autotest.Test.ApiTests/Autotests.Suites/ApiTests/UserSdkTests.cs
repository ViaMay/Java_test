using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserSdkTests : SendOrdersBasePage
    {
        const string userKey = "de9fe3971aa18d5d809206d2f1679b7a";
        const string shopKey = "645b9896dfb18fcef17cb00bb6845761";

        [Test, Description("Информация о магазине")]
        public void UserSdkInfoTest()
        {
            var shopKey1 = "c0dbcd6dd89837104de0d7f42b9fe7b2";
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_info.json",
                new NameValueCollection
                {
                {"shop_key", ""},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "shop_key:shop key обязательно к заполнению;");

            var responseSdk = (ApiResponse.ResponseSdk)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_info.json",
                new NameValueCollection
                {
                {"shop_key", shopKey1}
                });
            Assert.IsTrue(responseSdk.Response.Info.EnterPoint.Contains("sdk2.ddelivery.ru")||
                responseSdk.Response.Info.EnterPoint.Contains("sdk.ddelivery.ru")||
                responseSdk.Response.Info.EnterPoint.Contains("dab.dev.ddelivery.ru/example/ajax.php") ||
                responseSdk.Response.Info.EnterPoint.Contains("insales2.ddelivery.ru"));
            Assert.IsTrue(responseSdk.Response.Info.Cms.Equals("0") || responseSdk.Response.Info.Cms.Equals("insales"));
            Assert.IsTrue(responseSdk.Response.Info.Version.Equals("1")||responseSdk.Response.Info.Version.Equals("1.0"));
        }

        [Test]
        public void UserSdkEnterTest()
        {
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_enter.json",
                new NameValueCollection
                {
                {"shop_key", ""},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "shop_key:shop key обязательно к заполнению;");

            var responseSdk = (ApiResponse.ResponseSdk)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_enter.json",
                new NameValueCollection
                {
                {"shop_key", shopKey}
                });
            Assert.IsTrue(responseSdk.Response.Location.Contains("sdk2.dev.ddelivery.ru"));
            Assert.IsTrue(responseSdk.Response.Location.Contains(@"/api/v1/passport/"));
            Assert.IsTrue(responseSdk.Response.Location.Contains("cabinet.json"));
        }

        [Test]
        public void UserSdkResetTest()
        {
            var keyShop = "852af44bafef22e96d8277f3227f0998";

            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_reset.json",
                new NameValueCollection
                {
                {"shop_key", ""},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "shop_key:shop key обязательно к заполнению;");
            
            var response = apiRequest.GET("api/v1/cabinet/" + userKey + "/sdk_reset.json",
                new NameValueCollection
                {
                {"shop_key", keyShop},
                });
            Assert.IsTrue(response.Success);
        }
    }
}