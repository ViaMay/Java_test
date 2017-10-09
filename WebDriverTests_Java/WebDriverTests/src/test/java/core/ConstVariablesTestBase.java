package core;

import core.api.Api;
import org.codehaus.jackson.annotate.JsonAutoDetect;
import org.codehaus.jackson.annotate.JsonMethod;
import org.codehaus.jackson.map.DeserializationConfig;
import org.codehaus.jackson.map.ObjectMapper;

public class ConstVariablesTestBase
{
    public String getApplicationBaseUrl()
    {
//	return "dev.ddelivery.ru/";
        return "stage.ddelivery.ru/";
    }

    public String adminName = "v.e@ddelivery.ru";
    public String adminPass = "LbREWCnphA";
    public String adminKey = "9c205350bc5f193bc100a55bac2109f0";

    public String legalEntityName = "test_legalEntity";
    public String legalUserName = "test_legalUser";
    public String legalPickupName = "test_legalPickup";
    public String companyName = "test_via";
    public String companyPickupName = "test_Pickup";
    public String deliveryPointName = "test_deliverypoint";

    public String userNameAndPass = "tester@user.ru";
    public String pickupNameAndPass = "tester@pickup.ru";
    public String userWarehouseName = "test_userWarehouses_via";
    public String userShopName = "test_userShops_via";

    public String weightName = "test_via_Weight";
    public double weightMin = 2;
    public double weightMax = 16;

    public String sideName = "test_via_Side";
    public double side1Min = 1;
    public double side2Min = 2;
    public double side3Min = 3;
    public double side1Max = 40;
    public double side2Max = 50;
    public double side3Max = 60;

    public int marginsPickup = 11;
    public int marginsCourirs = 12;
    public int marginsSelf = 13;

    public String deliveryPointAddress = "Ленинский проспект 127";
    public String deliveryPointLongitude = "37.477079";
    public String deliveryPointLatitude = "55.645873";

    public String deliveryPointAddress2 = "ул. Салова, 27Литер АД, пом. 35";
    public String deliveryPointLongitude2 = "30.372815";
    public String deliveryPointLatitude2 = "59.895735";

    public Api apiRequest = new Api(getApplicationBaseUrl());
    public ObjectMapper mapper = new ObjectMapper().setVisibility(JsonMethod.FIELD, JsonAutoDetect.Visibility.ANY).
            configure(DeserializationConfig.Feature.FAIL_ON_UNKNOWN_PROPERTIES, false);
}


