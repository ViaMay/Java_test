using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehouseEditTests : SendOrdersBasePage
    {
        [Test, Description("Редактирование склада через Api")]
        public void WarehousesEditTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            //            Создания склада
            DeleteWarehouseByName(userWarehouseName + "_ApiUserWarehouse");
            DeleteWarehouseByName(userWarehouseName + "_ApiUserWarehouse2");

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
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
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseEditWarehouse =
//                (ApiResponse.ResponseAddObject) apiRequest.POST("cabinet/" + userKey + "/warehouse_edit/"
                apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                                                                + responseWarehouse.Response.Id + ".json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiUserWarehouse2"},
                        {"flat", "flat139"},
                        {"city", "417"},
                        {"contact_person", "contact_person2"},
                        {"contact_phone", "contact_phone2"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23"},
                        {"street", "street2"},
                        {"house", "house2"}
                    }
                    );
            Assert.IsTrue(responseEditWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о текущем складе 
            var responseInfo =
                (ApiResponse.ResponseInfoObject)
                    apiRequest.GET(
                        "api/v1/cabinet/" + userKey + "/warehouse_info/" + responseWarehouse.Response.Id + ".json");

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseWarehouse.Response.Id);
            Assert.AreEqual(responseInfo.Response.Name, userWarehouseName + "_ApiUserWarehouse2");
            Assert.AreEqual(responseInfo.Response.House, "house2");
            Assert.AreEqual(responseInfo.Response.Flat, "flat139");
            Assert.AreEqual(responseInfo.Response.PostalCode, "");
            Assert.AreEqual(responseInfo.Response.City, "417");
            Assert.AreEqual(responseInfo.Response.Street, "street2");
            Assert.AreEqual(responseInfo.Response.ContactPerson, "contact_person2");
            Assert.AreEqual(responseInfo.Response.ContactPhone, "contact_phone2");
            Assert.AreEqual(responseInfo.Response.ContactEmail, userNameAndPass);
            Assert.AreEqual(responseInfo.Response.Schedule,
                "1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23,1:12-23:23");
        }
    
        [Test, Description("Редактирвоание склада через Api неудачное")]
        public void WarehousesEditErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var warehouseId = GetWarehouseIdByName(userWarehouseName);
            
//            Редктирование склада неудачное пустое id склада
            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiUserWarehouse"},
                        {"flat", ""},
                        {"city", ""},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Warehouse not found");

//            Редктирование склада неудачное не указан город
            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_Api"},
                        {"flat", ""},
                        {"city", ""},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "City not found");

//            Редктирование склада неудачное

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", ""},
                        {"flat", ""},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", ""},
                        {"house", ""}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Название обязательно к заполнению");

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", "ddd"},
                        {"flat", ""},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", ""},
                        {"house", ""}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Улица обязательно к заполнению");
            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", "ddd"},
                        {"flat", ""},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", "street"},
                        {"house", ""}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Дом обязательно к заполнению");

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", "ddd"},
                        {"flat", ""},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Номер помещения обязательно к заполнению");

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", "ddd"},
                        {"flat", "123"},
                        {"city", "715"},
                        {"contact_person", ""},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Контактное лицо обязательно к заполнению");

            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_edit/"
                + warehouseId + ".json",
                    new NameValueCollection
                    {
                        {"name", "ddd"},
                        {"flat", "123"},
                        {"city", "715"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", ""},
                        {"contact_email", ""},
                        {"schedule", ""},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Контактный телефон обязательно к заполнению");
        }
    }

}