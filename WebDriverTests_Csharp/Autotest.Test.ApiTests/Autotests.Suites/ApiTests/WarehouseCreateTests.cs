using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class WarehouseCreateTests : SendOrdersBasePage
    {
        [Test, Description("Создание склада через Api админа")]
        public void WarehousesCreateTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

            //            Создания склада
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminCreateWarehouse");

            var responseWarehouse =
//                (ApiResponse.ResponseAddObject) apiRequest.POST(keyShopPublic + "/warehouse_create.json",
                 apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminCreateWarehouse"},
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
        }

        [Test, Description("Создание склада через Api админа неудачное")]
        public void WarehousesCreateErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);

//            Создания склада неудачное пустой город
            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminCreateWarehouse"},
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

            //            Создания склада неудачное все поля пустые
            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
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