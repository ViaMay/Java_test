package tests;

import core.SetUrlTestBase;
import core.pages.*;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на прокликивание всех страниц в ЛК пользователя 2")
public class ClickAllPageTest extends SetUrlTestBase {

    @Test
    @Description("ClickAllPageTest")
    public void ClickAllPageTest_Test() {

        LoginPage userNewLoginPage = new LoginPage();
        HomePage userPage = userNewLoginPage.LoginAsUser(userNameAndPass, userNameAndPass);

        userPage.getMenu().getOrderCreate().click();
        OrderCreatePage orderCreatePage = userPage.GoTo(OrderCreatePage.class);

        orderCreatePage.getMenu().getOrderList().click();
        OrderAndDocumentsListPage orderAndDocumentsListPage = orderCreatePage.GoTo(OrderAndDocumentsListPage.class);

        orderAndDocumentsListPage.getDocumentsLink().click();
        orderAndDocumentsListPage = orderAndDocumentsListPage.GoTo(OrderAndDocumentsListPage.class);

        orderAndDocumentsListPage.getMenu().getShopsAndStock().click();
        ShopsAndStocksPage shopsAndStocksPage = orderAndDocumentsListPage.GoTo(ShopsAndStocksPage.class);

        shopsAndStocksPage.getStocksLink().click();
        shopsAndStocksPage = shopsAndStocksPage.GoTo(ShopsAndStocksPage.class);

        shopsAndStocksPage.getMenu().getIntegrations().click();
        MarketPage marketPage = shopsAndStocksPage.GoTo(MarketPage.class);

        marketPage.getMenu().getFinances().click();
        FinancesPage financesPage = marketPage.GoTo(FinancesPage.class);

        financesPage.getFinancesCommissionLink().click();
        financesPage = financesPage.GoTo(FinancesPage.class);
    }
}
