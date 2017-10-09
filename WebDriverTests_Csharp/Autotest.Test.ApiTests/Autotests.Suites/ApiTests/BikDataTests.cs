using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class BikDataTests : SendOrdersBasePage
    {
        [Test, Description("Получить информации о баке по bik")]
        public void BikDataTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);

            var responseBikData =
               (ApiResponse.ResponseBikData)apiRequest.GET("api/v1/" + keyShopPublic + "/bikdata.json",
                   new NameValueCollection { { "bik", "044525225" }});
            Assert.IsTrue(responseBikData.Success);
            Assert.AreEqual(responseBikData.Response.Bik, "044525225");
//            Assert.AreEqual(responseBikData.Response.Id, "1272");
            Assert.AreEqual(responseBikData.Response.Name, "ПАО СБЕРБАНК");
            Assert.AreEqual(responseBikData.Response.Ks, "30101810400000000225");
        }
        
        [Test, Description("Получить информации о баке по bik")]
        public void BikDataErrorTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);

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