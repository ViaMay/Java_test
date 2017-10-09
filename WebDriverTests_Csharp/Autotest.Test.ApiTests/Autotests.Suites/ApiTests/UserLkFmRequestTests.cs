using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserLkFmRequestTests : SendOrdersBasePage
    {
        [Test, Description("логирование под пользователем")]
        public void Test01()
        {
            var responseLkAuth01 =
                (ApiResponse.ResponseLkAuth) apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", "user@ddelivery.ru"},
                        {"password", "jggl5gglgg3"}
                    });
            var responseFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_fm_request.json");
            Assert.AreEqual(responseFail.Response.ErrorText,
                "method:method обязательно к заполнению;date_start:date start обязательно к заполнению;date_end:date end обязательно к заполнению;");

            var responseFmPackageList =
                (ApiResponse.ResponseFmPackageList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_fm_request.json",
                        new NameValueCollection
                        {
                            {"method", "package"},
                            {"date_start", "01.01.2016"},
                            {"date_end", "11.07.2020"},
                            {"user_key", "de9fe3971aa18d5d809206d2f1679b7a"},
                        });
            Assert.IsTrue(responseFmPackageList.Success);
            Assert.AreEqual(responseFmPackageList.Response[0].AgentLink,
                "http://file.ddelivery.ru/download/24944?key=d50f80d6670f58b669efb3e255e7a31d");
        }

        [Test, Description("логирование под пользователем")]
        public void Test02()
        {
            var keyUser = GetUserKeyByName(userNameAndPass);
            var responseLkAuth01 =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });
            var responseFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_fm_request.json");
            Assert.AreEqual(responseFail.Response.ErrorText,
                "method:method обязательно к заполнению;date_start:date start обязательно к заполнению;date_end:date end обязательно к заполнению;");

            responseFail =
                (ApiResponse.ResponseFail)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_fm_request.json",
                        new NameValueCollection
                        {
                            {"method", "package"},
                            {"date_start", "01.01.2016"},
                            {"date_end", "11.07.2020"},
                            {"user_key", keyUser},
                        });
             Assert.AreEqual(responseFail.Response.ErrorText, "Ошибка Api запроса");
        }
    }
}