package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.MessageCalculation;
import core.api.jsonRespons.ResponseCalculation;
import core.api.jsonRespons.ResponseFail;
import org.junit.Ignore;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на CalculatorProTest")
public class CalculatorProTest extends GetConstantBasePage
{
    @Test
    @Ignore
    public void CalculatorCourirsTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);

        //            Все поля заполнены. Расчитать цену курьерки для Магазина у которого только один тип доставки  указана ТК забора
        ResponseCalculation responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseCalculation.class );
        Assert.assertEquals(responseCalculator.getResponse().size(), 1);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getDeliveryCompanyName(), companyName);

        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
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
        Assert.assertEquals(responseCalculator.getResponse().size(), 1);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getDeliveryCompanyName(), companyName);

        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
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
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
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
        for (MessageCalculation row: responseCalculator.getResponse()) {
            Assert.assertEquals(row.getPickupType(), "1");}

        String companyPickupNameId = GetCompanyIdByName(companyPickupName);
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"},
                                {"pickup_company", companyPickupNameId}
                        }),
                        ResponseCalculation.class );
        Assert.assertEquals(responseCalculator.getResponse().size(), 1);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getPickupCompany(), companyPickupNameId);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getPickupType(), "3");
    }

    @Test
    @Ignore
    public void CalculatorErrorTest_Test()
    {
        String keyShopPublic = GetShopKeyByName(userShopName);
// Возврат ошибки
        ResponseFail responseFailCalculator =
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