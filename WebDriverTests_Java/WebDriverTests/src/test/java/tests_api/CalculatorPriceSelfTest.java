package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.ResponseCalculation;
import core.api.jsonRespons.ResponseFail;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на CalculatorPriceAllTest")
public class CalculatorPriceSelfTest extends GetConstantBasePage
{
    @Test
    @Description("Расчитать цену самовывоза")
    public void CalculatorPriceSelfTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);
        String deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

//            Не заполен город city_to. Расчитать цену самомвывоза (по пункут выдачи)
        ResponseCalculation responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", ""},
                                {"delivery_point", deliveryPoinId},
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

//            Заполены все значения. Расчитать цену самомвывоза (по городу доставки)
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
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

//            delivery_point пустое. Расчитать цену самомвывоза (по городу доставки)
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", "151184"},
                                {"delivery_point", ""},
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

//            Одна из сторон равна нулю. Возврат ошибки
        ResponseFail responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
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

//           declared_price равна нулю. Возврат ошибки
        responseFail =
                (ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", "151184"},
                                {"delivery_point", deliveryPoinId},
                                {"dimension_side1", "4"},
                                {"dimension_side2", "4"},
                                {"dimension_side3", "4"},
                                {"weight", "4"},
                                {"payment_price", "1000"}
                        }),
                        ResponseFail.class );
        Assert.assertFalse(responseFail.getSuccess());
        Assert.assertEquals(responseFail.getResponse().getMessage(), "declared price обязательно к заполнению");

//            Заполены все значения. Город не верно. Расчитать цену самомвывоза (по точке)
        responseCalculator =
                (ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                        SetApiValue( new String[][] {
                                {"type", "1"},
                                {"city_to", "Оклахома"},
                                {"delivery_point", deliveryPoinId},
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
    }
}