using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserFreshdeskTests : ConstVariablesBase
    {
        [Test, Description("FreshdeskUrl")]
        public void UserFreshdeskTest()
        {
            var responseFreshdesk = (ApiResponse.ResponseFreshdesk)apiRequest.GET("api/v1/cabinet/" + userKey + "/freshdesk.json", 
                new NameValueCollection{});
            Assert.IsTrue(responseFreshdesk.Success);
            Assert.IsTrue(responseFreshdesk.Response.FreshdeskUrl.Contains("https://ddelivery.freshdesk.com/"));
        }
    }
}