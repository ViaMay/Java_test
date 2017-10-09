package tests_api;

import core.GetConstantBasePage;
import core.api.jsonRespons.ResponseBikData;
import core.api.jsonRespons.ResponseFail;
import org.testng.Assert;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на BikDataTest")
public class BikDataTest extends GetConstantBasePage
{
    @Test
    @Description("BikDataTest")
    public void BikDataTest_Test() {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);
//        шлем апи запрос используя ключ магазина
        ResponseBikData responseBikData = (ResponseBikData)apiRequest.GET(
                "api/v1/" + keyShopPublic + "/bikdata.json",
                SetApiValue( new String[][] {
                        {"bik", "044525225"}}),
                ResponseBikData.class);

        Assert.assertTrue(responseBikData.getSuccess());
        Assert.assertEquals(responseBikData.getResponse().getBik(), "044525225");
        Assert.assertNotNull(responseBikData.getResponse().getId());
        Assert.assertEquals(responseBikData.getResponse().getName(), "ПАО СБЕРБАНК");
        Assert.assertEquals(responseBikData.getResponse().getKs(), "30101810400000000225");
    }

    @Test
    @Description("BikDataErrorTest")
    public void BikDataErrorTest_Test()
    {
//        получаем апи ключ магазина
        String keyShopPublic = GetShopKeyByName(userShopName);
//        шлем апи запрос используя ключ магазина
        ResponseFail responseFail = (ResponseFail)apiRequest.GET(
                "api/v1/" + keyShopPublic + "/bikdata.json",
                SetApiValue( new String[][] {
                        {"bik", "123"}}),
                ResponseFail.class);

        Assert.assertEquals(responseFail.getResponse().getMessage(), "bik:Длина поля bik должна быть равной 9 символа(ов);");

        responseFail = (ResponseFail)apiRequest.GET(
                "api/v1/" + keyShopPublic + "/bikdata.json",
                SetApiValue(new String[][] {
                        {"bik", "04452522а"}}),
                ResponseFail.class);

        Assert.assertEquals(responseFail.getResponse().getMessage(), "bik:bik должно быть целым числом;");
    }
}