package tests;

import core.SetUrlTestBase;
import core.pages.HomePage;
import core.pages.LoginPage;
import core.pages.ShopsAndStocksPage;
import core.pagesControls.StockRow;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на создание склада")
public class CreateStockTest extends SetUrlTestBase {

    @Test
    @Description("CreateStockTest")
    public void CreateStockTest_Test()
    {
        getConstantBasePage.DeleteStockByName(userWarehouseName + "user2");
        LoginPage userNewLoginPage = new LoginPage();
        HomePage userPage = userNewLoginPage.LoginAsUser(userNameAndPass, userNameAndPass);
        userPage.getMenu().getShopsAndStock().click();
        ShopsAndStocksPage shopsAndStocksPage = userPage.GoTo(ShopsAndStocksPage.class);
        shopsAndStocksPage.getStocksLink().click();
        shopsAndStocksPage = userPage.GoTo(ShopsAndStocksPage.class);

        shopsAndStocksPage.getStockAdd().click();
        shopsAndStocksPage.getStockAddModal().WaitVisibleWithRetries();
        shopsAndStocksPage.getStockAddModal().getStockName().SetValue(userWarehouseName + "user2");
        shopsAndStocksPage.getStockAddModal().getCity().SetValueSelect("Санкт-Петербург");

        shopsAndStocksPage.getStockAddModal().getContactPerson().SetValue("Иванов иван иванович");
        shopsAndStocksPage.getStockAddModal().getPhone().SetValue("{4567890111");
        shopsAndStocksPage.getStockAddModal().getEmail().SetValue("123@test.com");
        shopsAndStocksPage.getStockAddModal().getStreet().SetValue("тестовая");
        shopsAndStocksPage.getStockAddModal().getHouse().SetValue("123");
        shopsAndStocksPage.getStockAddModal().getFlat().SetValue("234");
        shopsAndStocksPage.getStockAddModal().getPostalCode().SetValue("123456");
        shopsAndStocksPage.getStockAddModal().getSaveButton().click();

        shopsAndStocksPage.getInfoModal().WaitPresenceWithRetries(8000);
        shopsAndStocksPage.getInfoModal().getTextInfo().WaitText("Склад создан!");
        shopsAndStocksPage.getInfoModal().getOkButton().click();
        shopsAndStocksPage.getInfoModal().WaitAbsenceWithRetries(5000);

        StockRow row = shopsAndStocksPage.findRowByStockName(userWarehouseName + "user2");
        row.getNameStock().WaitText(userWarehouseName + "user2");
        row.getStockArrow().click();
        row.getNameStockTwo().WaitText("Склад: "+ userWarehouseName + "user2");
        row.getContactName().WaitText("Контактное лицо: " + "Иванов иван иванович");
        row.getStockPhone().WaitText("Телефон: 7 (456) 789-0111");
        row.getStockEmail().WaitText("Email: 123@test.com");
        row.getStockCity().WaitText("Город: Санкт-Петербург");
        row.getStockStreet().WaitText("Улица: тестовая");
        row.getStockHome().WaitText("Дом/корпус: 123");
        row.getStockFlat().WaitText("Квартира/офис: 234");
        row.getStockTimetable().WaitText("График работы\n" +
                "Пн 09:00 18:00\n" +
                "Вт 09:00 18:00\n" +
                "Ср 09:00 18:00\n" +
                "Чт 09:00 18:00\n" +
                "Пт 09:00 18:00\n" +
                "Сб 09:00 18:00\n" +
                "Вс 09:00 18:00");

        getConstantBasePage.DeleteStockByName(userWarehouseName + "user2");
    }
}
