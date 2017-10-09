package core;

import com.gargoylesoftware.htmlunit.util.NameValuePair;
import core.api.jsonRespons.*;
import org.testng.Assert;
import ru.yandex.qatools.allure.annotations.Step;

import java.util.ArrayList;

public class GetConstantBasePage extends ConstVariablesTestBase
{
    @Step()
    public String GetCompanyIdByName(String name) {
        ArrayList<NameValuePair> nameValuePairs = new ArrayList<>(2);
        nameValuePairs.add(new NameValuePair("company_name", name));
        ResponseCompanies responseCompanies =
                (ResponseCompanies)apiRequest.GET("admin/api/v1/" + adminKey + "/get_companies_by_name.json",
                        nameValuePairs ,ResponseCompanies.class );

        Assert.assertTrue(responseCompanies.getSuccess());
    return String.valueOf(responseCompanies.getResponse().getСompanies().get(0).getId());
    }

    @Step()
    public String GetDeliveryPointIdByName(String pointName)
    {
        ArrayList<NameValuePair> nameValuePairs = new ArrayList<>(2);
        nameValuePairs.add(new NameValuePair("_action", "delivery_points"));
        nameValuePairs.add(new NameValuePair("companies", GetCompanyIdByName(companyName)));
        ResponseDeamonPoints responseDeliveryPoints =
                (ResponseDeamonPoints)apiRequest.GET("tmpdaemon/",
                        nameValuePairs ,ResponseDeamonPoints.class );

        Assert.assertTrue(responseDeliveryPoints.getSuccess());
        for (MessageOptionsPoints point : responseDeliveryPoints.getPoints())
        {
            if (point.getName().equals(pointName))
                return String.valueOf((point.getId()));
        }
        return "";
    }

    @Step()
    public String GetShopKeyByName(String shopName)
    {
        ResponseShops responseShops = (ResponseShops)
                apiRequest.GET("api/v1/cabinet/" + GetToken()
                    + "/lk_request.json?method=get_shops", ResponseShops.class);

        for (MessageShop shop : responseShops.getResponse()) {
            if (shop.getName().equals(shopName)) {
                return shop.getPublicKey();
            }
        }
        return "";
    }

    @Step()
    public String GetShopIdByName(String shopName)
    {
        ResponseShops responseShops = (ResponseShops)
                apiRequest.GET("api/v1/cabinet/" + GetToken()
        + "/lk_request.json?method=get_shops", ResponseShops.class);

        for (MessageShop shop : responseShops.getResponse()) {
             if (shop.getName().contains(shopName)) {
                return String.valueOf((shop.getId()));
            }
        }
        return "";
    }

    @Step()
    public String GetStockIdByName(String stockName)
    {
        ResponseWarehouses ResponseWarehouses = (ResponseWarehouses)
                apiRequest.GET("api/v1/cabinet/" + GetToken()
                        + "/lk_request.json?method=get_warehouses", ResponseWarehouses.class);

        for (MessageWarehouse Warehouse : ResponseWarehouses.getResponse()) {
            if (Warehouse.getName().contains(stockName)) {
                return String.valueOf((Warehouse.getId()));
            }
        }
        return "";
    }

    @Step("удаляем магазин с именем{0}")
    public final void DeleteShopByName(String shopName) {
        String id = GetShopIdByName(shopName);
        if (!id.equals(""))
        {
            ResponseDeleteObject deleteResponse = (ResponseDeleteObject) apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + id + ".json", ResponseDeleteObject.class);
            if (!deleteResponse.getSuccess())
                apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + id + ".json");
        }
    }

    @Step("удаляем склад с именем{0}")
    public final void DeleteStockByName(String stockName)
    {
        String id = GetStockIdByName(stockName);
        if (!id.equals(""))
        {
            ResponseDeleteObject responseStockDelete = (ResponseDeleteObject) apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + id + ".json", ResponseDeleteObject.class);
            if (!responseStockDelete.getSuccess())
                apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + id + ".json");
        }
    }

    @Step()
    private final String GetToken()
    {
        ResponseLkAuth responseLkAuth = (ResponseLkAuth) apiRequest.GET("api/v1/cabinet/lk_auth.json?login="
                + userNameAndPass+ "&password=" + userNameAndPass, ResponseLkAuth.class);
        return responseLkAuth.getResponse().getTokenTtl();
    }

    public ArrayList<NameValuePair> SetApiValue (String[][] valuePairs)
    {
        int count = valuePairs.length;
        ArrayList<NameValuePair> nameValuePairs = new ArrayList<>(count);
        for (int i=0; i<count; i++)
        {
            nameValuePairs.add(new NameValuePair(valuePairs[i][0], valuePairs[i][1]));
        }
        return nameValuePairs;
    }
}

