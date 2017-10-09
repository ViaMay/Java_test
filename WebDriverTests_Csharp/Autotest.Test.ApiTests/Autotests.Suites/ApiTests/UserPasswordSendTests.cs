using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPasswordSendTests : GetConstantBasePage
    {
        [Test, Description("высылаем на емеил письмо")]
        public void UserPasswordErrorTest()
        {
            var responseEmailSendFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/restore/password_restore.json");
            Assert.AreEqual(responseEmailSendFail.Response.ErrorText, "email:email обязательно к заполнению;");

            responseEmailSendFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/restore/password_restore.json",
                new NameValueCollection
                {
                    {"email", "123456"},
                });
            Assert.AreEqual(responseEmailSendFail.Response.ErrorText, "email:email должно быть корректным адресом электронной почты;");

            responseEmailSendFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/restore/password_restore.json",
                new NameValueCollection
                {
                    {"email", adminName},
                });
            Assert.AreEqual(responseEmailSendFail.Response.ErrorText, "Невозвожно восстановить пароль для указанного пользователя");

            responseEmailSendFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/restore/password_restore.json",
                new NameValueCollection
                {
                    {"email", pickupNameAndPass},
                });
            Assert.AreEqual(responseEmailSendFail.Response.ErrorText, "Невозвожно восстановить пароль для указанного пользователя");
        }
        
        [Test, Description("высылаем на емеил письмо")]
        public void UserPasswordTest()
        {
            var email = "tester@ddelivery.ru";
            var passEmail = "123456789";
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
        }
    }
}
