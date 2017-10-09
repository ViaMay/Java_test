package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.MessageCalculation;
import core.api.jsonRespons.ResponseCalculation;
import core.api.jsonRespons.ResponseFail;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на CalculatorPriceAllTest")
public class CalculatorProAllTest extends GetConstantBasePage
{
    @Test
    public void CalculatorProAllTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);

        ResponseCalculation responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseCalculation.class );

        Assert.assertEquals(responseCalculator.getResponse().size(), 2);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getDeliveryCompanyName(), companyName);
        Assert.assertEquals(responseCalculator.getResponse().get(1).getDeliveryCompanyName(), companyName);

        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"},
                                {"pickup_type", "3"}
                        }),
                        ResponseCalculation.class );

        Assert.assertEquals(responseCalculator.getResponse().size(), 2);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getDeliveryCompanyName(), companyName);
        Assert.assertEquals(responseCalculator.getResponse().get(1).getDeliveryCompanyName(), companyName);

        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"},
                                {"pickup_type", "1"}
                        }),
                        ResponseCalculation.class );

        Assert.assertEquals(responseCalculator.getResponse().size(), 0);

        keyShopPublic = GetShopKeyByName("test_userShops_via_Pro");
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"},
                                {"pickup_type", "1"}}),
                        ResponseCalculation.class );
        for (MessageCalculation row: responseCalculator.getResponse()) {
            Assert.assertEquals(row.getPickupType(), "1");}

        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][]
        {
            {"type", "2"},
            {"city_to", "151184"},
            {"dimension_side1", "4"},
            {"dimension_side2", "4"},
            {"dimension_side3", "4"},
            {"weight", "4"},
            {"declared_price", "1000"},
            {"payment_price", "1000"},
            {"pickup_company", GetCompanyIdByName(companyPickupName)}
        }),
                        ResponseCalculation.class );
        Assert.assertEquals(responseCalculator.getResponse().size(), 2);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getPickupCompany(), GetCompanyIdByName(companyPickupName));
        Assert.assertEquals(responseCalculator.getResponse().get(1).getPickupCompany(), GetCompanyIdByName(companyPickupName));
        Assert.assertEquals(responseCalculator.getResponse().get(0).getPickupType(), "3");
        Assert.assertEquals(responseCalculator.getResponse().get(1).getPickupType(), "3");

//            запрос за подключенный тип без явно указанной ТК забора в настройках магазина
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][]
        {
            {"city_to", "151184"},
            {"dimension_side1", "4"},
            {"dimension_side2", "4"},
            {"dimension_side3", "4"},
            {"weight", "4"},
            {"declared_price", "1000"},
            {"payment_price", "1000"},
            {"pickup_type", "4"}
        }),
                        ResponseCalculation.class );
        for (MessageCalculation row: responseCalculator.getResponse()) {
            Assert.assertEquals(row.getPickupType(), "4");}

//            тот же запрос только теперь указываем ТК забора в запросе
        ResponseCalculation responseCalculator2 =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][]
        {
            {"city_to", "151184"},
            {"dimension_side1", "4"},
            {"dimension_side2", "4"},
            {"dimension_side3", "4"},
            {"weight", "4"},
            {"declared_price", "1000"},
            {"payment_price", "1000"},
            {"pickup_type", "4"},
            {"pickup_company", responseCalculator.getResponse().get(0).getPickupCompany()}
        }),
                        ResponseCalculation.class );
        for (MessageCalculation row: responseCalculator2.getResponse()) {
            Assert.assertEquals(row.getPickupCompany(), responseCalculator.getResponse().get(0).getPickupCompany());}
    }

    @Test
    public void CalculatorErrorTest_Test()
    {
        String keyShopPublic = GetShopKeyByName(userShopName);
// Возврат ошибки
        ResponseFail responseFailCalculator =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][]        {
            {"city_to", "151184"},
            {"dimension_side1", "4"},
            {"dimension_side2", "4"},
            {"dimension_side3", "4"},
            {"weight", "4"},
            {"declared_price", "1000"},
            {"payment_price", "1000"},
            {"pickup_type", "0"}
        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFailCalculator.getSuccess());
        Assert.assertEquals(responseFailCalculator.getResponse().getMessage(), "pickup_type:Неправильный тип забора;");

        responseFailCalculator =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                        SetApiValue( new String[][]
        {
            {"city_to", "151184"},
            {"dimension_side1", "4"},
            {"dimension_side2", "4"},
            {"dimension_side3", "4"},
            {"weight", "4"},
            {"declared_price", "1000"},
            {"payment_price", "1000"},
            {"pickup_type", "7"}
        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFailCalculator.getSuccess());
        Assert.assertEquals(responseFailCalculator.getResponse().getMessage(), "pickup_type:Неправильный тип забора;");
    }
}