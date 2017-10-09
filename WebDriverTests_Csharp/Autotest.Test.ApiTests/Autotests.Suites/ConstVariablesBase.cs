using System;
using System.Threading;
using Autotests.Utilities.ApiTestCore;

namespace Autotests.Tests
{
    public class ConstVariablesBase
    {
//    public string ApplicationBaseUrl { get { return "dev:0DShabby7maiden&0Edata@dev.ddelivery.ru"; } }
    public string ApplicationBaseUrl { get { return "dev.ddelivery.ru"; } }
//      public string ApplicationBaseUrl { get { return "stage.ddelivery.ru"; } }

        public string adminName { get { return "v.e@ddelivery.ru"; } }
        public string adminPass { get { return "LbREWCnphA"; } }
        public string adminKey { get { return "9c205350bc5f193bc100a55bac2109f0"; } }

        public string userNameAndPass{ get { return "tester@user.ru"; } }
        public string pickupNameAndPass { get { return "tester@pickup.ru"; } }
        public string userWarehouseName { get { return "test_userWarehouses_via"; } }
        public string userShopName { get { return "test_userShops_via"; } }

        public string companyName { get { return "test_via"; } }
        public string companyPickupName { get { return "test_Pickup"; } }
        public string companyPickupNameWarehouse { get { return "test_Pickup_2_Warehouse"; } }
        public string deliveryPointName { get { return "test_deliverypoint"; } }
        
        public string deliveryPointAddress { get { return "Ленинский проспект 127"; } }
        public string deliveryPointLongitude { get { return "37.477079"; } }
        public string deliveryPointLatitude { get { return "55.645873"; } }

        public Api apiRequest { get { return new Api(ApplicationBaseUrl); } }

        static TimeZoneInfo moscowTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        DateTime moscowDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, moscowTimeZone);

        public DateTime nowDate { get { return moscowDateTime; } }


        public void WaitDocuments(int value = 90000)
        {
            Thread.Sleep(value);
        }
    }
}