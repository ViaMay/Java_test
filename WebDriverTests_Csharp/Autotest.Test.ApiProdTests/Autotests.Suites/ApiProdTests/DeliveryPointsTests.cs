using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class DeliveryPointsTests : ConstVariablesBase
    {
        [Test, Description("Получить список пунктов самовывоза")]
        public void DeliveryPointsTest()
        {
            var responseDeliveryPoints =
               (ApiResponse.ResponseDeliveryPoints)apiRequest.GET("api/v1/" + keyShopPublic + "/delivery_points.json");
            Assert.IsTrue(responseDeliveryPoints.Success);
        }
    }
}