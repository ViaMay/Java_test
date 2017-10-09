using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class BikDataTests : ConstVariablesBase
    {
        [Test, Description("Получить информации о баке по bik")]
        public void BikDataTest()
        {
            var responseBikData =
               (ApiResponse.ResponseBikData)apiRequest.GET("api/v1/" + keyShopPublic + "/bikdata.json",
                   new NameValueCollection { { "bik", "044525225" }});
            Assert.IsTrue(responseBikData.Success);
        }
        
        [Test, Description("Получить информации о баке по bik")]
        public void BikDataErrorTest()
        {
            var responseBikData =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/bikdata.json",
                   new NameValueCollection { { "bik", "123" }});
            Assert.AreEqual(responseBikData.Response.ErrorText, "bik:Длина поля bik должна быть равной 9 символа(ов);");
            
            responseBikData =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/bikdata.json",
                   new NameValueCollection { { "bik", "04452522а" } });
            Assert.AreEqual(responseBikData.Response.ErrorText, "bik:bik должно быть целым числом;");
        }
    }
}