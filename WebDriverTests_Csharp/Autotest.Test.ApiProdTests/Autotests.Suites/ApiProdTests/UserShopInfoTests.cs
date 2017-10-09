using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserShopInfoTests : ConstVariablesBase
    {
        [Test, Description("Получить информацию о текущем магазине")]
        public void ShopInfoTest()
        {
            
//            Получения информации о текущем магазине
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/shop_info/" + shopId + ".json");
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }

        [Test, Description("Получить информацию о текущем магазине не корректное")]
        public void ShopsInfoErrorTest()
        {
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
           
        }
    }
}