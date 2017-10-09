package tests;

import core.SetUrlTestBase;
import core.pages.HomePage;
import core.pages.LoginPage;
import core.pages.ShopsAndStocksPage;
import core.pagesControls.ShopRow;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на создания магазина")
public class CreateShopTest extends SetUrlTestBase {

    @Test
    @Description("CreateShopTest")
    public void CreateShopTest_Test()
    {
        getConstantBasePage.DeleteShopByName(userShopName + "user2");
        LoginPage userNewLoginPage = new LoginPage();

        HomePage userPage = userNewLoginPage.LoginAsUser(userNameAndPass, userNameAndPass);

        userPage.getMenu().getShopsAndStock().click();
        ShopsAndStocksPage shopsAndStocksPage = userPage.GoTo(ShopsAndStocksPage.class);
        shopsAndStocksPage.getShopsLink().click();
        shopsAndStocksPage = userPage.GoTo(ShopsAndStocksPage.class);

        shopsAndStocksPage.getShopAdd().click();
        shopsAndStocksPage.getShopAddModal().WaitVisibleWithRetries();
        shopsAndStocksPage.getShopAddModal().getName().SetValue(userShopName + "user2");
        shopsAndStocksPage.getShopAddModal().getAddress().SetValue("Москва");
        shopsAndStocksPage.getShopAddModal().getWarehouse().selectByText(userWarehouseName);
        shopsAndStocksPage.getShopAddModal().getSaveButton().click();

        shopsAndStocksPage.getInfoModal().WaitPresenceWithRetries(5000);
        shopsAndStocksPage.getInfoModal().getTextInfo().WaitText("Магазин создан!");
        shopsAndStocksPage.getInfoModal().getOkButton().click();
        shopsAndStocksPage.getInfoModal().WaitAbsenceWithRetries(5000);

        ShopRow row = shopsAndStocksPage.findRowByName(userShopName + "user2");
        row.getNameShop().WaitText(userShopName + "user2");
        row.getShopArrow().click();
        row.getAdress().WaitText("Адрес: Москва");
        row.getStock().WaitText("Склад: " + userWarehouseName);
        row.getKey().WaitTextContains("Api-ключ: ");

        getConstantBasePage.DeleteShopByName(userShopName + "user2");
    }
}
