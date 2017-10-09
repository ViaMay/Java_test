package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.ResponseCalculation;
import core.api.jsonRespons.ResponseFail;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на CalculatorPriceAllTest")
public class CalculatorPriceCourirsTest extends GetConstantBasePage
{
    @Test
    @Description("Расчитать цену курьерская")
    public void CalculatorPriceCourirsTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);

//            Все поля заполнены. Расчитать цену курьерки
        ResponseCalculation responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
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

//          city_to заполнен некорректно. Возврат ошибки
        ResponseFail responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "33030303030"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "City not found (city to)");

//            Одна из сторон равна нулю. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "0"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "dimension_side1:dimension side  должно быть в промежутке от 0.00099 до 100000;");

//          Превышение веса. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "411111111111111"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "weight:weight должно быть в промежутке от 0.00099 до 10000;");

//           declared_price равна нулю. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "2"},
                                {"city_to", "151184"},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "declared price обязательно к заполнению");
    }
}