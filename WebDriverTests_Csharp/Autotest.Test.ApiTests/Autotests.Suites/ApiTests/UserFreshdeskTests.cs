using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserFreshdeskTests : GetConstantBasePage
    {
        [Test, Description("FreshdeskUrl")]
        public void UserFreshdeskTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var responseFreshdesk = (ApiResponse.ResponseFreshdesk)apiRequest.GET("api/v1/cabinet/" + userKey + "/freshdesk.json", 
                new NameValueCollection{});
            Assert.IsTrue(responseFreshdesk.Success);
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("https://ddelivery.freshdesk.com/"));
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("name=tester%40user.ru&"));
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("email=tester%40user.ru&"));
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("company=test_legalUser"));
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("timestamp"));
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("hash"));
        }
    }
}