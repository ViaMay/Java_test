using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPasswordRestoreValidTests : GetConstantBasePage
    {
        [Test, Description("высылаем на емеил письмо")]
        public void UserPasswordRestoreValidTest()
        {
            var email = "tester@ddelivery.ru";
            var passEmail = "123456789";
            var idEmail = GetUserIdByName(email);
            var responseEmailSend =
                (ApiResponse.ResponseEmailSend)apiRequest.POST("api/v1/" + "cabinet/restore/password_restore.json",
                    new NameValueCollection
                    {
                        {"email", email},
                    });
            Assert.IsTrue(responseEmailSend.Success);
            Assert.AreEqual(responseEmailSend.Response.IsSend, "ok");
            WaitDocuments(1000);
            var emailBasePage = new GetEmailBasePage();
            var token = emailBasePage.GetTokenEmail(email, passEmail);
            
            var response = apiRequest.GET("api/v1/cabinet/" + token + "/password_restore_valid.json", new NameValueCollection
                        {
                            {"id", idEmail},
                        });
            Assert.IsTrue(response.Success, "Ожидался ответ true на отправленный запрос GET по API");
            
//            использовали токен
            var passwordNew = "12312312";
            var responseEmailChange = (ApiResponse.ResponseMessage)
                 apiRequest.POST("api/v1/cabinet/" + token + "/password_change.json",
                 new NameValueCollection
                {
                        {"id", idEmail},
                        {"password", passwordNew},
                        {"password_confirm", passwordNew}
                });
            Assert.AreEqual(responseEmailChange.Response.Message, "Пароль успешно изменен");

//            тот же запрос
            var responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + token + "/password_restore_valid.json", new NameValueCollection
                        {
                            {"id", idEmail},
                        });
            Assert.AreEqual(responseFail.Response.ErrorText, "Токен некорректен, либо его время жизни истекло");

//            не корректный id
            responseFail = (ApiResponse.ResponseFail) apiRequest.GET("api/v1/cabinet/" + token + "/password_restore_valid.json", new NameValueCollection
                        {
                            {"id", "в"},
                        });
            Assert.AreEqual(responseFail.Response.ErrorText, "id:id должно быть в промежутке от 0 до 9223372036854775807;");
            
            responseFail = (ApiResponse.ResponseFail) apiRequest.GET("api/v1/cabinet/" + token + "/password_restore_valid.json");
           Assert.AreEqual(responseFail.Response.ErrorText, "id:id должно быть в промежутке от 0 до 9223372036854775807;");

//            не корректный токен
           var responseLkAuth =  (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json", new NameValueCollection
                    {
                        {"login", email},
                        {"password", passwordNew}
                    });

           responseFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/password_restore_valid.json", new NameValueCollection
                        {
                            {"id", idEmail},
                        });
           Assert.AreEqual(responseFail.Response.ErrorText, "Токен некорректен, либо его время жизни истекло");
        }
    }
}
