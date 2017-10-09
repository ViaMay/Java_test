package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.ResponseCalculation;
import core.api.jsonRespons.ResponseFail;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на CalculatorPriceAllTest")
public class CalculatorPriceAllTest extends GetConstantBasePage
{
    @Test
    @Description("Расчитать цену")
    public void CalculatorPriceAllTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);
        String deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

        ResponseCalculation responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
                                {"dimension_side1", "7,8"},
                                {"dimension_side2", "6,6"},
                                {"dimension_side3", "5,3"},
                                {"weight", "4,1"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseCalculation.class );

        Assert.assertEquals(responseCalculator.getResponse().size(), 2);
        Assert.assertEquals(responseCalculator.getResponse().get(0).getDeliveryCompanyName(), companyName);
        Assert.assertEquals(responseCalculator.getResponse().get(1).getDeliveryCompanyName(), companyName);

        //            Город не корректен. Возврат ошибки,
        ResponseFail responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "Москва"},
                                {"delivery_point", deliveryPoinId},
                                {"dimension_side1", "7,8"},
                                {"dimension_side2", "6,6"},
                                {"dimension_side3", "5,3"},
                                {"weight", "4,1"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "City not found (city to)");

        //            Одна из сторон равна нулю. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
                                {"dimension_side1", "0"},
                                {"dimension_side2", "6,6"},
                                {"dimension_side3", "5,3"},
                                {"weight", "4,1"},
                                {"declared_price", "1000"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "dimension_side1:dimension side  должно быть в промежутке от 0.00099 до 100000;");

//           declared_price равна нулю. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                        SetApiValue( new String[][] {
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "6,6"},
                                {"dimension_side3", "5,3"},
                                {"weight", "4,1"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "declared price обязательно к заполнению");
    }
}