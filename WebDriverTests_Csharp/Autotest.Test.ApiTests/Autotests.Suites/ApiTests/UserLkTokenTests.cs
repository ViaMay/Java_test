using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserLkTokenTests : GetConstantBasePage
    {
        [Test, Description("высылаем на емеил письмо")]
        public void UserLkTokenTest()
        {
            var responseLkAuth =
                   (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                   new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });

            var response = apiRequest.GET("api/v1/cabinet/lk_token_valid.json", 
                new NameValueCollection
                {
                    {"token", responseLkAuth.Response.Token},
                });
            Assert.IsTrue(response.Success);

            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_token_valid.json", 
                new NameValueCollection
                {
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "token:token обязательно к заполнению;");

            responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/lk_token_valid.json", 
                new NameValueCollection
                {
                    {"token", "1234567"},
                });
            Assert.AreEqual(responseFail.Response.ErrorText, "Токен не найден");
        }
    }
}