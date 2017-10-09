using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserInfoTests : ConstVariablesBase
    {
        [Test, Description("Создание пользователя")]
        public void UserInfoTest()
        {
            var responseInfo =
                (ApiResponse.ResponseUserInfo)apiRequest.GET("api/v1/cabinet/" + userKey + "/user_info.json",
                new NameValueCollection{});
            Assert.IsTrue(responseInfo.Success);}

        [Test, Description("Создание пользователя не успешное")]
        public void UserCreateErrorTest()
        {
            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + adminKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", ""},
                    {"email", ""},
                    {"password", ""},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "email:email обязательно к заполнению;password:Пароль обязательно к заполнению;official_name:official name обязательно к заполнению;");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + adminKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "a"},
                    {"password", "a"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "email:email должно быть корректным адресом электронной почты;");

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + adminKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "3@f.ru3"},
                    {"password", "a"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "email:email должно быть корректным адресом электронной почты;");
            
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + adminKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "vi-11@yandex.ru"},
                    {"password", "a"},
                    {"inn", "d"},
                    {"ogrn", "d"},
                    {"bank_bik", "d"},
                    {"bank_rs", "d"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "inn:inn должно быть не менее 10 символа(ов);" +
                                                             "ogrn:ogrn должно быть не менее 13 символа(ов);" +
                                                             "bank_bik:Длина поля bank bik должна быть равной 9 символа(ов);" +
                                                             "bank_rs:Длина поля bank rs должна быть равной 20 символа(ов);");
            
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + adminKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "a"},
                    {"email", "vi-11@yandex.ru"},
                    {"password", "a"},
                    {"inn", "1"},
                    {"ogrn", "1"},
                    {"bank_bik", "1"},
                    {"bank_rs", "1"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "inn:inn должно быть не менее 10 символа(ов);" +
                                                           "ogrn:ogrn должно быть не менее 13 символа(ов);" +
                                                           "bank_bik:Длина поля bank bik должна быть равной 9 символа(ов);" +
                                                           "bank_rs:Длина поля bank rs должна быть равной 20 символа(ов);");

           }
    }
}