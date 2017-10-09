using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class SignatureGenerateTests : GetConstantBasePage
    {
        [Test, Description("высылаем на емеил письмо")]
        public void SignatureGenerateTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var responseSignatureGenerate = apiRequest.GET("api/v1/" + keyShopPublic + "/signature_generate.json", 
                new NameValueCollection
                {
                    {"key", keyShopPublic},
                });
            Assert.IsTrue(responseSignatureGenerate.Success);

            var responseSignatureGenerateFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/signature_generate.json",
                new NameValueCollection
                {
                });
            Assert.AreEqual(responseSignatureGenerateFail.Response.ErrorText, "not found key in params");
        }
    }
}