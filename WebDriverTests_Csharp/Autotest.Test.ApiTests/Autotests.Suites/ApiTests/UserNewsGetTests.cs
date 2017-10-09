using System;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserNewsGetTests : SendOrdersBasePage
    {
        [Test, Description("новости")]
        public void Test()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
//                    запрос списка заказов - проверка что список выдан
            var responseOrderListRequest =
                (ApiResponse.ResponseNewsList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/get_news.json");
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");
            var newRow = FindRowByName("test_уведомление", responseOrderListRequest);
            Assert.AreEqual(newRow.Type, "2");
            Assert.AreEqual(newRow.Email, "false");
            Assert.AreEqual(newRow.Active, "false");

            newRow = FindRowByName("test_новость", responseOrderListRequest);
            Assert.AreEqual(newRow.Type, "1");
            Assert.AreEqual(newRow.Email, "false");
            Assert.AreEqual(newRow.Active, "true");
        }

        private ApiResponse.MessageNewList FindRowByName(string name, ApiResponse.ResponseNewsList responseNewsList)
        {
            for (var i = 0; i < responseNewsList.Response.Count(); i++)
            {
                if (responseNewsList.Response[i].Content.Contains(name))
                    return responseNewsList.Response[i];
            }
            throw new Exception(string.Format("не найдена новость с именем {0} в списке всех новостей", name));
        }
    }
}