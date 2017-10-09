using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Tests.ApiTests
{
    public class UserShopInfoTests : SendOrdersBasePage
    {
        [Test, Description("Получить информацию о текущем магазине")]
        public void ShopInfoTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var idPickupCompany = GetCompanyIdByName(companyPickupName);
            var idWarehouse = GetWarehouseIdByName(userWarehouseName);
            var idShop = GetShopIdByName(userShopName);
            
//            Получения информации о текущем магазине
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + idShop + ".json");
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            Assert.AreEqual(responseInfo.Response.Id, idShop);
            Assert.AreEqual(responseInfo.Response.PublicKey, GetShopKeyByName(userShopName));
            Assert.AreEqual(responseInfo.Response.Name, userShopName);
            Assert.AreEqual(responseInfo.Response.Address, "Москва");
            Assert.AreEqual(responseInfo.Response.Warehouse, idWarehouse);
            Assert.AreEqual(responseInfo.Response.PickupInfo.DefaultCompany, idPickupCompany);
            Assert.AreEqual(responseInfo.Response.PickupInfo.DefaultType, "3");
            Assert.AreEqual(responseInfo.Response.PickupInfo.PickupType[0].Company, idPickupCompany);
            Assert.AreEqual(responseInfo.Response.PickupInfo.PickupType[0].Type, "3");
            Assert.AreEqual(responseInfo.Response.PickupInfo.PickupType[0].TypeName, "Единый забор Курьером");

            foreach (var availableCompany in responseInfo.Response.PickupInfo.PickupType[0].AvailableCompaniesList)
            {
                if (availableCompany.Id == idPickupCompany)
                {
                    Assert.AreEqual(availableCompany.Name, companyPickupName);
                    break;
                }
                if (availableCompany == responseInfo.Response.PickupInfo.PickupType[0].AvailableCompaniesList.Last()) throw new NoSuchElementException("Не нашли заказ, хотя долны были");
            }
        }

        [Test, Description("Получить информацию о текущем магазине не корректное")]
        public void ShopsInfoErrorTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

            var idShop = "123456";
//            Получения информации о текущем складе не корректное/ не корректное id склада 
            var responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + idShop + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Shop not found");
            
            idShop = " ";
//            Получения информации о текущем складе не корректное/ пустое id склада 
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + idShop + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "id:id должно быть в промежутке от 0 до 9223372036854775807;");

            var warehouseId = GetWarehouseIdByName(userWarehouseName);
            DeleteShopByName(userShopName + "_ApiUserInfoShop");

            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiUserInfoShop"},
                    {"warehouse", warehouseId},
                    {"address", "Москва"}
                }
                );
            var shopId = responseShop.Response.Id;
//            удаление но не реальное
            var responseShopDelete1 = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_delete/" + shopId + ".json");
            Assert.IsTrue(responseShopDelete1.Success);
//            Получения информации о текущем складе не корректное/удаленный склад
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + shopId + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Shop not found");

//            реальное удаление
            responseShopDelete1 = (ApiResponse.ResponseMessage)apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + shopId + ".json");
            Assert.IsTrue(responseShopDelete1.Success);
//            Получения информации о текущем складе не корректное/удаленный склад
            responseErrorWarehouse = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + shopId + ".json");
            Assert.IsFalse(responseErrorWarehouse.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseErrorWarehouse.Response.ErrorText, "Shop not found");
        }
    }
}