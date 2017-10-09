using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehouseInfoTests : SendOrdersBasePage
    {
        [Test, Description("Получить информацию о скаде")]
        public void WarehousesInfoTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var idWarehouse = GetWarehouseIdByName(userWarehouseName);
           
//            Получения информации о текущем складе магазина
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + idWarehouse + ".json");

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, idWarehouse);
            Assert.AreEqual(responseInfo.Response.Name, userWarehouseName);
            Assert.AreEqual(responseInfo.Response.House, "Дом");
            Assert.AreEqual(responseInfo.Response.Flat, "Квартира");
            Assert.AreEqual(responseInfo.Response.PostalCode, "555444");
            Assert.AreEqual(responseInfo.Response.City, "151184");
            Assert.AreEqual(responseInfo.Response.Street, "Улица");
            Assert.AreEqual(responseInfo.Response.ContactPerson, "test_legalEntity");
            Assert.AreEqual(responseInfo.Response.ContactPhone, "+7 (111)111-1111");
            Assert.AreEqual(responseInfo.Response.ContactEmail, userNameAndPass);
            Assert.AreEqual(responseInfo.Response.Schedule, "1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");
        }

        [Test, Description("Получить информацию о складе не корректное")]
        public void WarehousesInfoErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var idWarehouse = "123456";
//            Получения информации о текущем складе не корректное/ не корректное id склада 
            var responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + idWarehouse + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");
            
            idWarehouse = " ";
            //            Получения информации о текущем складе не корректное/ пустое id склада 
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + idWarehouse + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "id:id должно быть в промежутке от 0 до 9223372036854775807;");

            DeleteWarehouseByName(userWarehouseName + "_ApiUserWarehouse");

            var response = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiUserWarehouse"},
                    {"flat", "138"},
                    {"city", "416"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                });

            idWarehouse = response.Response.Id;
//            удаление склада -не реальное
            var responseShopUserDelete =
        (ApiResponse.ResponseMessage)
            apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_delete/" + idWarehouse + ".json");
            Assert.IsTrue(responseShopUserDelete.Success);
            Assert.AreEqual(responseShopUserDelete.Response.Message, "Склад успешно удален");

            //            Получения информации о текущем складе не корректное/удаленный склад
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + idWarehouse + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");

            //            реальное удаление
            var responseShopDelete2 =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + idWarehouse + ".json");
            Assert.IsTrue(responseShopDelete2.Success);
            Assert.AreEqual(responseShopDelete2.Response.Message, "Склад успешно удален"); idWarehouse = response.Response.Id;
            //            Получения информации о текущем складе не корректное/удаленный склад
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + idWarehouse + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");
        }
    }
}