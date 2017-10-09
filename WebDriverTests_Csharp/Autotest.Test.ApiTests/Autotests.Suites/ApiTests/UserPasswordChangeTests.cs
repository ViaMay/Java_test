using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPasswordChangeTests : GetConstantBasePage
    {
        [Test, Description("смена паспорта")]
        public void UserPasswordTest()
        {
            var email = "tester@ddelivery.ru";
            var passEmail = "123456789";
            var idEmail = GetUserIdByName(email);
            var responseEmailSend =
                (ApiResponse.ResponseEmailSend)apiRequest.POST("api/v1/cabinet/restore/password_restore.json",
                    new NameValueCollection
                    {
                        {"email", email},
                    });
            Assert.IsTrue(responseEmailSend.Success);
            Assert.AreEqual(responseEmailSend.Response.IsSend, "ok");
            WaitDocuments(1000);
            var emailBasePage = new GetEmailBasePage();
            var token = emailBasePage.GetTokenEmail(email, passEmail);

            var responseEmailChangeFail = (ApiResponse.ResponseFail)
                apiRequest.POST("api/v1/cabinet/" + token + "/password_change.json");
            Assert.AreEqual(responseEmailChangeFail.Response.ErrorText, "id:id должно быть в промежутке от 0 до 9223372036854775807;" +
                                                                        "password:Пароль обязательно к заполнению;" +
                                                                       "password_confirm:password confirm обязательно к заполнению;");

            var passwordNew =  "12312312";
            var responseEmailChange = (ApiResponse.ResponseMessage)
                 apiRequest.POST("api/v1/cabinet/" + token + "/password_change.json",
                 new NameValueCollection
                {
                        {"id", idEmail},
                        {"password", passwordNew},
                        {"password_confirm", passwordNew}
                });
            Assert.AreEqual(responseEmailChange.Response.Message, "Пароль успешно изменен");

            responseEmailChangeFail = (ApiResponse.ResponseFail)
                apiRequest.POST("api/v1/cabinet/" + token + "/password_change.json",
                new NameValueCollection
                {
                        {"id", idEmail},
                        {"password", passwordNew},
                        {"password_confirm", passwordNew}
                });
            Assert.AreEqual(responseEmailChangeFail.Response.ErrorText, "Невозможно восстановить пароль. Токен некорректен, либо его время жизни истекло");

        }
    }
}