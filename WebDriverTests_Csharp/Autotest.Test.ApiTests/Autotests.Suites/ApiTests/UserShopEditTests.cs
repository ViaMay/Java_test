using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserShopEditTests : SendOrdersBasePage
    {
        [Test, Description("Создание магазина через Api пользователя, не дописан тест")]
        public void ShopEditTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);
            var werahouseId = GetWarehouseIdByName(userWarehouseName);
            DeleteWarehouseByName(userWarehouseName + "_ApiUserShopEdit");

            var responseWarehouse2 = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiUserShopEdit2"},
                    {"flat", "138"},
                    {"city", "415"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                }
                );
            Assert.IsTrue(responseWarehouse2.Success, "Ожидался ответ true на отправленный запрос POST по API");
            var werahouseId2 = responseWarehouse2.Response.Id;

            //            удаление магазинов если они были;
            DeleteShopByName(userShopName + "_ApiUserShopEdit");
            DeleteShopByName(userShopName + "_ApiUserShopEdit2");

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserShopEdit"},
                    {"warehouse", werahouseId},
                    {"address", "Квебек"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о магазине
            var responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + responseShop.Response.Id + ".json");

            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.PublicKey, responseShop.Response.Key);
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiUserShopEdit");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, werahouseId);
            Assert.AreEqual(responseShopInfo.Response.Address, "Квебек");

//            редактирование магазина
            responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + responseShop.Response.Id + ".json",
                new NameValueCollection
                {
                    {"api_key", userKey},
                    {"id", responseShop.Response.Id},
                    {"name", userShopName + "_ApiUserShopEdit2"},
                    {"warehouse", werahouseId2},
                    {"address", "Санкт-Питербург"},
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            Получения информации о магазине
            responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + responseShop.Response.Id + ".json");

            Assert.IsTrue(responseShopInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseShopInfo.Response.Id, responseShop.Response.Id);
            Assert.AreEqual(responseShopInfo.Response.PublicKey, responseShop.Response.Key);
            Assert.AreEqual(responseShopInfo.Response.Name, userShopName + "_ApiUserShopEdit2");
            Assert.AreEqual(responseShopInfo.Response.Warehouse, werahouseId2);
            Assert.AreEqual(responseShopInfo.Response.Address, "Санкт-Питербург");

//            редактируем тип забора

            var idPickupCompany = GetCompanyIdByName(companyPickupName);
            var idPickupCompany2 = GetCompanyIdByName(companyPickupName+ "_2");

            var shopId = GetShopIdByName("test_userShops_via_Pro");
            var responseShop1 = apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + shopId + ".json",
               new NameValueCollection
                {
                    {"api_key", userKey},
                    {"pickup_type", "1"},
                }
               );
            Assert.IsTrue(responseShop1.Success, "Ожидался ответ true на отправленный запрос POST по API");

            //            Получения информации о магазине
            responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + shopId + ".json");
            Assert.AreEqual(responseShopInfo.Response.PickupInfo.DefaultType, "1");

            var responseShopError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + shopId + ".json",
               new NameValueCollection
                {
                    {"api_key", userKey},
                    {"pickup_type", "6"},
                }
               );
            Assert.AreEqual(responseShopError.Response.ErrorText, "Тип забора 6 для магазина не доступен");
            
            responseShopError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + shopId + ".json",
               new NameValueCollection
                {
                    {"api_key", userKey},
                    {"pickup_type", "2"},
                    {"pickup_company", idPickupCompany2},
                }
               );
            Assert.AreEqual(responseShopError.Response.ErrorText, "Компания не доступна для данного типа забора");
            
            responseShop1 = apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + shopId + ".json",
               new NameValueCollection
                {
                    {"api_key", userKey},
                    {"pickup_type", "3"},
                    {"pickup_company", idPickupCompany},
                }
               );
            Assert.IsTrue(responseShop1.Success, "Ожидался ответ true на отправленный запрос POST по API");
            //            Получения информации о магазине
            responseShopInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + shopId + ".json");
            Assert.AreEqual(responseShopInfo.Response.PickupInfo.DefaultType, "3");
            Assert.AreEqual(responseShopInfo.Response.PickupInfo.DefaultCompany, idPickupCompany);
        }

        [Test, Description("Редактирование магазина через Api админа не удачное")]
        public void ShopEditErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

//            не правильный id магазина
            var idShop = "123456";
            var responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"name", userShopName + "_ApiAdmin2"},
                    {"address", "Санкт-Питербург"}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Shop not found");

            idShop = GetShopIdByName(userShopName);
//            не правильно указаны имя и адрес
            responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"name", ""},
                    {"address", ""}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Название обязательно к заполнению");
        
            responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"name", "name"},
                    {"address", ""}
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Адрес обязательно к заполнению");

            responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"pickup_type", "5"},
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Тип забора 5 для магазина не доступен");

            responseError = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_edit/" + idShop + ".json",
                new NameValueCollection
                {
                    {"_id", idShop},
                    {"pickup_type", "5"},
                });
            Assert.IsFalse(responseError.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseError.Response.ErrorText, "Тип забора 5 для магазина не доступен");
        }
    }
}