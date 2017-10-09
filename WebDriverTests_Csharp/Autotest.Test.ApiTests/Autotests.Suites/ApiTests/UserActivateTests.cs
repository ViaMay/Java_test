using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserActivateTests : GetConstantBasePage
    {
        [Test, Description("Активация"), Ignore]
        public void UserActivateTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var email = userNameAndPass + "ation.com";

            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", GetUserIdByName(email) } });
            
            var response =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/user_create.json",
                new NameValueCollection
                {
                    {"official_name", "Наименование юр лица"},
                    {"email", email},
                    {"password", email},
                    {"name", "Контактное лицо"},
                    {"phone", "phone"},
                    {"director", "Ген. директор"},
                    {"on_basis", "Действует на основании"},
                    {"official_address", "Юридический адрес"},
                    {"address", "Фактический адрес"},
                    {"inn", "2222222223"},
                    {"ogrn", "1234567890123"},
                    {"bank_name", "Наименование банка"},
                    {"bank_bik", "123456789"},
                    {"bank_ks", "к/с банка"},
                    {"bank_rs", "12312312311231231231"}
                }
                );
            Assert.IsTrue(response.Success);

            var responseInfoFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/is_fastery_active.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Не активировано");
            
            responseInfoFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/is_prostore_active.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Не активировано");

            var responseInfo =
               (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/fastery_activate.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Ваша заявка на подключение отправлена. Наши менеджеры свяжутся с вами в ближайшее время.");

            responseInfo =
                (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/prostore_activate.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Ваша заявка на подключение отправлена. Наши менеджеры свяжутся с вами в ближайшее время.");
            
            responseInfo =
               (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/is_fastery_active.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Активировано");

            responseInfo =
                (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/is_prostore_active.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Активировано");
            
            responseInfoFail =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/fastery_activate.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Уже активировано");

            responseInfoFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + GetUserKeyByName(email) + "/prostore_activate.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Уже активировано");

            apiRequest.GET("admin/api/v1/" + adminKey + "/delete_user.json", new NameValueCollection { { "id", response.Response.Id } });
        }
    }
}