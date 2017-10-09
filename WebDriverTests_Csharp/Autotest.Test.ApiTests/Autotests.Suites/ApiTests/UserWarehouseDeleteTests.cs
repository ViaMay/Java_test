using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehouseDeleteTests : SendOrdersBasePage
    {
        [Test, Description("Создание магазина")]
        public void ShopDeleteTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            //            удаление магазинов если они были
            DeleteWarehouseByName(userWarehouseName + "_ApiUserDeleteWarehouse");

            //            создание склада
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiUserDeleteWarehouse"},
                    {"flat", "138"},
                    {"city", "416"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                }
                );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            //            удаление пользователем склада
            var responseShopUserDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_delete/" + responseWarehouse.Response.Id + ".json");
            Assert.IsTrue(responseShopUserDelete.Success);
            Assert.AreEqual(responseShopUserDelete.Response.Message, "Склад успешно удален");

            //            реальное удаление
            var responseShopDelete2 =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + responseWarehouse.Response.Id + ".json");
            Assert.IsTrue(responseShopDelete2.Success);
            Assert.AreEqual(responseShopDelete2.Response.Message, "Склад успешно удален");
        }
    }
}