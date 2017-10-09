using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class WarehouseInfoTests : ConstVariablesBase
    {
        [Test, Description("Получить информацию о текущем складе магазина Api админа")]
        public void WarehousesInfoTest()
        {
//            лучения информации о текущем складе магазина
            var responseInfo = (ApiResponse.ResponseInfoObject)apiRequest.GET("api/v1/" + keyShopPublic + "/warehouse_info.json");
            Assert.IsTrue(responseInfo.Success, "Ожидался ответ true на отправленный запрос POST по API");
        }
    }
}