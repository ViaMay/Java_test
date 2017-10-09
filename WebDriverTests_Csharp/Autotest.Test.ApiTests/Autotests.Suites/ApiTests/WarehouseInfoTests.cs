using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class WarehouseInfoTests : SendOrdersBasePage
    {
        [Test, Description("Получить информацию о текущем складе магазина Api админа")]
        public void WarehousesInfoTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            
//            удаление склада
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminInfoWarehouse");
            
//            Создания склада
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminInfoWarehouse"},
                        {"flat", "138"},
                        {"city", "416"},
                        {"postal_code", "123456"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");
            DeleteShopByName(userShopName + "_ApiAdminInfoWarehouse");

//            Создание магазина
            var userKey = GetUserKeyByName(userNameAndPass);
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdminInfoWarehouse"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);

//            Получения информации о текущем складе магазина
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/warehouse_info.json");

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseWarehouse.Response.Id);
            Assert.AreEqual(responseInfo.Response.Name, userWarehouseName + "_ApiAdminInfoWarehouse");
            Assert.AreEqual(responseInfo.Response.Street, "street");
            Assert.AreEqual(responseInfo.Response.House, "house");
            Assert.AreEqual(responseInfo.Response.Flat, "138");
            Assert.AreEqual(responseInfo.Response.ContactEmail, userNameAndPass);
            Assert.AreEqual(responseInfo.Response.PostalCode, "123456");
            Assert.AreEqual(responseInfo.Response.City, "416");
            Assert.AreEqual(responseInfo.Response.ContactPerson, "contact_person");
            Assert.AreEqual(responseInfo.Response.ContactPhone, "contact_phone");
            Assert.AreEqual(responseInfo.Response.Schedule, "10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,NODAY,NODAY");
        }

        [Test, Description("Получить информацию о текущем складе магазина Api админа не удачное")]
        public void WarehousesInfoErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

//            удаление склада
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminInfoWarehouse"); 
//            Создания склада
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminInfoWarehouse"},
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

            DeleteShopByName(userShopName + "_ApiAdminInfoWarehouse");

            var userKey = GetUserKeyByName(userNameAndPass);

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdminInfoWarehouse"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);

            //            удаления склада 
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminInfoWarehouse"); 

//            Получения информации о текущем складе магазина
            var responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/warehouse_info.json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");
        }
    }
}