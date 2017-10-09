using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserWarehouseCreateTests : SendOrdersBasePage
    {
        [Test, Description("Создание склада")]
        public void WarehouseCreateTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            //            удаление склад если он был до этого
            DeleteWarehouseByName(userWarehouseName + "_ApiUserCreatWarehouse");

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiUserCreatWarehouse"},
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

//            Получения информации о складе 
            var responseInfo =
                (ApiResponse.ResponseInfoObject)
                    apiRequest.GET(
                        "api/v1/cabinet/" + userKey + "/warehouse_info/" + responseWarehouse.Response.Id + ".json");

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, responseWarehouse.Response.Id);
            Assert.AreEqual(responseInfo.Response.Name, userWarehouseName + "_ApiUserCreatWarehouse");
            Assert.AreEqual(responseInfo.Response.House, "house");
            Assert.AreEqual(responseInfo.Response.Flat, "138");
            Assert.AreEqual(responseInfo.Response.PostalCode, "");
            Assert.AreEqual(responseInfo.Response.Street, "street");
            Assert.AreEqual(responseInfo.Response.City, "416");
            Assert.AreEqual(responseInfo.Response.ContactPerson, "contact_person");
            Assert.AreEqual(responseInfo.Response.ContactPhone, "contact_phone");
            Assert.AreEqual(responseInfo.Response.ContactEmail, userNameAndPass);
            Assert.AreEqual(responseInfo.Response.Schedule,
                "10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,10:00-19:00,NODAY,NODAY");
        }

        [Test, Description("Создание склада через Api админа неудачное")]
        public void WarehousesCreateErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

//            Создания склада неудачное пустой город

            var responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiUserCreatWarehouse"},
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
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "city:city должно быть в промежутке от 0 до 9223372036854775807;flat:flat обязательно к заполнению;");

            //            Создания склада неудачное все поля пустые
            responseErrorWarehouse =
                (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
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
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "house:house обязательно к заполнению;" +
                                                                       "flat:flat обязательно к заполнению;" +
                                                                       "contact_person:contact person обязательно к заполнению;" +
                                                                       "contact_phone:contact phone обязательно к заполнению;" +
                                                                       "contact_email:contact email обязательно к заполнению;" +
                                                                       "schedule:schedule обязательно к заполнению;" +
                                                                       "street:street обязательно к заполнению;");
        }
    }
}