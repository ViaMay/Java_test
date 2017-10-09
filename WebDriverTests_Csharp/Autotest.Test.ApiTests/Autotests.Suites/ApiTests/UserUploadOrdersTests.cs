using System;
using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserUploadOrdersTests : SendOrdersBasePage
    {
        [Test, Description("upload_orders.json Загрузить заказы")]
        public void UserUploadOrdersTest()
        {
            var currentDirectory = Environment.CurrentDirectory + "\\ApiTests\\Files\\orders.xls";
            var userKey = GetUserKeyByName(userNameAndPass);
            var idShop = GetShopIdByName(userShopName);
            var response = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/cabinet/" + userKey + "/upload_orders.json",
                new NameValueCollection
                {
                    {"shop_id", idShop},
                    {"pickup_type", "3"},
                    {"orders", currentDirectory},
                }
                );
            Assert.IsTrue(response.Response.File.StartsWith("http://file.ddelivery.ru/download/"));
            Assert.AreEqual(response.Response.Message, @"Операция успешно завершена <br/><a href=""" + response.Response.File + @""">Скачать отчет</a>");

            var responseFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/upload_orders.json",
                new NameValueCollection
                {
                    {"shop_id", idShop},
                    {"pickup_type", "3"},
                }
                );
            Assert.AreEqual(responseFail.Response.ErrorText, "Ошибка при загрузке файла");

            responseFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/upload_orders.json");
            Assert.AreEqual(responseFail.Response.ErrorText, "Ошибка при загрузке файла");

            responseFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/upload_orders.json",
                new NameValueCollection
                {
                    {"shop_id", "ы"},
                    {"pickup_type", "7"},
                }
                );
            Assert.AreEqual(responseFail.Response.ErrorText, "pickup_type:pickup type может содержать один из доступных вариантов;shop_id:shop id должно быть в промежутке от 0 до 9223372036854775807;");
        }
    }
}