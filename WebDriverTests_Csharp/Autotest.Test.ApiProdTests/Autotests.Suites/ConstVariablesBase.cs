using Autotests.Utilities.ApiTestCore;

namespace Autotests.Tests
{
    public class ConstVariablesBase
    {
        public string ApplicationBaseUrl { get { return "cabinet.ddelivery.ru"; } }

        public string adminKey { get { return "9c205350bc5f193bc100a55bac2109f0"; } }
        public string keyShopPublic { get { return "9b552da5d889474caa8857d0d9965679"; } }
        public string shopId { get { return "73251"; } }
        public string deliveryPoinId { get { return "22169"; } }
        public string companyId { get { return "23"; } }
        public string userKey { get { return "de9fe3971aa18d5d809206d2f1679b7a"; } }
        public string warehouseId { get { return "118"; } }

        public Api apiRequest { get { return new Api(ApplicationBaseUrl); } }
    }
}