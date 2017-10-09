package core;

import core.api.Api;
import core.systemPages.SimpleFunctionalTestBase;
import org.codehaus.jackson.map.ObjectMapper;
import org.testng.annotations.BeforeClass;

public class SetUrlTestBase extends SimpleFunctionalTestBase
{
    @Override
    public String getApplicationBaseUrl()
    {
        return getConstantBasePage.getApplicationBaseUrl();
    }
    @Override
    @BeforeClass
    public void SetUp()
    {
        super.SetUp();
        adminName = getConstantBasePage.adminName;
        adminPass = getConstantBasePage.adminPass;
        adminKey = getConstantBasePage.adminKey;

        legalEntityName = getConstantBasePage.legalEntityName;
        legalUserName = getConstantBasePage.legalUserName;
        legalPickupName = getConstantBasePage.legalPickupName;
        companyName = getConstantBasePage.companyName;
        companyPickupName = getConstantBasePage.companyPickupName;
        deliveryPointName = getConstantBasePage.deliveryPointName;

        userNameAndPass = getConstantBasePage.userNameAndPass;
        pickupNameAndPass = getConstantBasePage.pickupNameAndPass;
        userWarehouseName = getConstantBasePage.userWarehouseName;
        userShopName = getConstantBasePage.userShopName;

        weightName = getConstantBasePage.weightName;
        weightMin = getConstantBasePage.weightMin;
        weightMax = getConstantBasePage.weightMax;

        sideName = getConstantBasePage.sideName;
        side1Min = getConstantBasePage.side1Min;
        side2Min = getConstantBasePage.side2Min;
        side3Min = getConstantBasePage.side3Min;
        side1Max = getConstantBasePage.side1Max;
        side2Max = getConstantBasePage.side2Max;
        side3Max = getConstantBasePage.side3Max;

        marginsPickup = getConstantBasePage.marginsPickup;
        marginsCourirs = getConstantBasePage.marginsCourirs;
        marginsSelf = getConstantBasePage.marginsSelf;

        deliveryPointAddress = getConstantBasePage.deliveryPointAddress;
        deliveryPointLongitude = getConstantBasePage.deliveryPointLongitude;
        deliveryPointLatitude = getConstantBasePage.deliveryPointLatitude;

        deliveryPointAddress2 = getConstantBasePage.deliveryPointAddress2;
        deliveryPointLongitude2 = getConstantBasePage.deliveryPointLongitude2;
        deliveryPointLatitude2 = getConstantBasePage.deliveryPointLatitude2;

        apiRequest = getConstantBasePage.apiRequest;
        mapper = getConstantBasePage.mapper;
    }

    public GetConstantBasePage getConstantBasePage = new GetConstantBasePage() ;
    public String adminName;
    public String adminPass;
    public String adminKey;

    public String legalEntityName;
    public String legalUserName;
    public String legalPickupName;
    public String companyName;
    public String companyPickupName;
    public String deliveryPointName;

    public String userNameAndPass;
    public String pickupNameAndPass;
    public String userWarehouseName;
    public String userShopName;

    public String weightName;
    public double weightMin;
    public double weightMax;

    public String sideName;
    public double side1Min;
    public double side2Min;
    public double side3Min;
    public double side1Max;
    public double side2Max;
    public double side3Max;

    public int marginsPickup;
    public int marginsCourirs;
    public int marginsSelf;

    public String deliveryPointAddress;
    public String deliveryPointLongitude;
    public String deliveryPointLatitude;

    public String deliveryPointAddress2;
    public String deliveryPointLongitude2;
    public String deliveryPointLatitude2;

    public Api apiRequest;
    public ObjectMapper mapper;
}
