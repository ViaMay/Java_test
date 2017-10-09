using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class UserWarehouseInfoTests : ConstVariablesBase
    {
        [Test, Description("Получить информацию о скаде")]
        public void WarehousesInfoTest()
        {
//            Получения информации о текущем складе магазина
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/cabinet/" + userKey + "/warehouse_info/" + warehouseId + ".json");

            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
            }

        [Test, Description("Получить информацию о складе не корректное")]
        public void WarehousesInfoErrorTest()
        {
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
            }
    }
}