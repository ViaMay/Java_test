using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserShopDeleteTests : SendOrdersBasePage
    {
        [Test, Description("Создание магазина")]
        public void ShopDeleteTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            //            удаление магазинов если они были
            DeleteShopByName(userShopName + "_ApiUserDeleteShop");

            //            получение id склада
            var werahouseId = GetWarehouseIdByName(userWarehouseName);

            //            создание магазина
            var responseShop =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                    new NameValueCollection
                    {
                        {"name", userShopName + "_ApiUserDeleteShop"},
                        {"warehouse", werahouseId},
                        {"address", "Квебек"}
                    }
                    );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

            CacheFlush();

            //            удаление пользователем магазина
            var responseShopUserDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_delete/" + responseShop.Response.Id + ".json");
            Assert.IsTrue(responseShopUserDelete.Success);
            Assert.AreEqual(responseShopUserDelete.Response.Message, "Магазин успешно удален");

            //            реальное удаление
            var responseShopDelete2 =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + responseShop.Response.Id + ".json");
            Assert.IsTrue(responseShopDelete2.Success);
            Assert.AreEqual(responseShopDelete2.Response.Message, "Магазин успешно удален");
        }
    }
}