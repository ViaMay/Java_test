using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserCreateUserAndInfoTests : GetConstantBasePage
    {
        [Test, Description("Создание пользователя")]
        public void UserCreateTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var email1 = userNameAndPass + "u";
            var email2 = userNameAndPass + "uu";
            var email3 = userNameAndPass + "uuu";

            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { {"id", GetUserIdByName(email1)}  });
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { {"id", GetUserIdByName(email2)}  });
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { {"id", GetUserIdByName(email3)}  });
            
            var response01 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица"},
                    {"email", email1},
                    {"password", email1},
                    {"name", "Контактное лицо"},
                    {"phone", "phone"},
                    {"director", "Ген. директор"},
                    {"on_basis", "Действует на основании"},
                    {"official_address", "Юридический адрес"},
                    {"address", "Фактический адрес"},
                    {"inn", "1111111116"},
                    {"ogrn", "1234567890123"},
                    {"bank_name", "Наименование банка"},
                    {"bank_bik", "123456789"},
                    {"bank_ks", "к/с банка"},
                    {"bank_rs", "12312312311231231231"}
                }
                );
            Assert.IsTrue(response01.Success);

            var responseInfo =
                (ApiResponse.ResponseUserInfo)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email1) + "/user_info.json",
                new NameValueCollection{});
            Assert.IsTrue(responseInfo.Success);

            Assert.AreEqual(responseInfo.Response.Name,"Контактное лицо");
            Assert.AreEqual(responseInfo.Response.Phone,"phone");
            Assert.AreEqual(responseInfo.Response.Username, email1);
            Assert.AreEqual(responseInfo.Response.OfficialName,"Наименование юр лица");
            Assert.AreEqual(responseInfo.Response.Director,"Ген. директор");
            Assert.AreEqual(responseInfo.Response.OnBasis,"Действует на основании");
            Assert.AreEqual(responseInfo.Response.OfficialAddress,"Юридический адрес");
            Assert.AreEqual(responseInfo.Response.Address,"Фактический адрес");
            Assert.AreEqual(responseInfo.Response.Inn, "1111111116");
            Assert.AreEqual(responseInfo.Response.Ogrn,"1234567890123");
            Assert.AreEqual(responseInfo.Response.BankName,"Наименование банка");
            Assert.AreEqual(responseInfo.Response.BankBik,"123456789");
            Assert.AreEqual(responseInfo.Response.BankKs,"к/с банка");
            Assert.AreEqual(responseInfo.Response.BankRs,"12312312311231231231");

            var response02 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица2"},
                    {"email", email2},
                    {"password", email2},
                    {"inn", "123456789012"},
                    {"ogrn", "123456789012312"},
                }
                );
            Assert.IsTrue(response02.Success);
            responseInfo =
                (ApiResponse.ResponseUserInfo)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email2) + "/user_info.json");
            Assert.IsTrue(responseInfo.Success);

            Assert.AreEqual(responseInfo.Response.Name, "");
            Assert.AreEqual(responseInfo.Response.Phone, "");
            Assert.AreEqual(responseInfo.Response.Username, email2);
            Assert.AreEqual(responseInfo.Response.OfficialName, "Наименование юр лица2");
            Assert.AreEqual(responseInfo.Response.Director, "");
            Assert.AreEqual(responseInfo.Response.OnBasis, "");
            Assert.AreEqual(responseInfo.Response.OfficialAddress, "");
            Assert.AreEqual(responseInfo.Response.Address, "");
            Assert.AreEqual(responseInfo.Response.Inn, "123456789012");
            Assert.AreEqual(responseInfo.Response.Ogrn, "123456789012312");
            Assert.AreEqual(responseInfo.Response.BankName, "");
            Assert.AreEqual(responseInfo.Response.BankBik, "");
            Assert.AreEqual(responseInfo.Response.BankKs, "");
            Assert.AreEqual(responseInfo.Response.BankRs, "");

            var response03 =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица3"},
                    {"email", email3},
                    {"password", email3},
                }
                );
            Assert.IsTrue(response03.Success);
            responseInfo =
                            (ApiResponse.ResponseUserInfo)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email3) + "/user_info.json");
            Assert.IsTrue(responseInfo.Success);

            Assert.AreEqual(responseInfo.Response.Name, "");
            Assert.AreEqual(responseInfo.Response.Phone, "");
            Assert.AreEqual(responseInfo.Response.Username, email3);
            Assert.AreEqual(responseInfo.Response.OfficialName, "Наименование юр лица3");
            Assert.AreEqual(responseInfo.Response.Director, "");
            Assert.AreEqual(responseInfo.Response.OnBasis, "");
            Assert.AreEqual(responseInfo.Response.OfficialAddress, "");
            Assert.AreEqual(responseInfo.Response.Address, "");
            Assert.AreEqual(responseInfo.Response.Inn, "");
            Assert.AreEqual(responseInfo.Response.Ogrn, "");
            Assert.AreEqual(responseInfo.Response.BankName, "");
            Assert.AreEqual(responseInfo.Response.BankBik, "");
            Assert.AreEqual(responseInfo.Response.BankKs, "");
            Assert.AreEqual(responseInfo.Response.BankRs, "");

            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", response01.Response.Id } });
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", response02.Response.Id } });
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", response03.Response.Id } });
        }

        [Test, Description("Создание пользователя не успешное")]
        public void UserCreateErrorTest()
        {
            var email1 = userNameAndPass + "u";
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", GetUserIdByName(email1) } });
            var userKey = GetUserKeyByName(userNameAndPass);
            var response =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица"},
                    {"email", email1},
                    {"password", email1},
                    {"inn", "3333333338"},
                }
                );
            Assert.IsTrue(response.Success);

            var email2 = userNameAndPass + "uu";
            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", GetUserIdByName(email2) } });
            
            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + GetUserKeyByName(email1) + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица2"},
                    {"email", email2},
                    {"password", email2},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "only master can access");

            responseFail =
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
                    {"email", email1},
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
                    {"email", email1},
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

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {                     
                {"official_name", "Наименование юр лица3"},
                {"email", email1},
                {"password", email1},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "Email уже используется");
            
            responseFail =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {                     
                {"official_name", "Наименование юр лица3"},
                {"email", email1 + "u"},
                {"password", email1 + "u"},
                {"inn", "3333333338"},
                }
                );
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "Данный ИНН уже имеется в системе");

            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", response.Response.Id } });
        }
    }
}