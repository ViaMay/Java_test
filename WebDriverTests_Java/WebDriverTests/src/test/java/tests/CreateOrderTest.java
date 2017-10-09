package tests;
import core.SetUrlTestBase;
import core.pages.HomePage;
import core.pages.LoginPage;
import core.pages.OrderCreatePage;
import core.pagesControls.OrderCollectionInfoList;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

@Title("Тест на создания заказа")
public class CreateOrderTest extends SetUrlTestBase {

    @Test
    @Description("CreateOrderTest")
    public void CreateOrderTest_Test() {
        LoginPage userNewLoginPage = new LoginPage();
        HomePage userPage = userNewLoginPage.LoginAsUser(userNameAndPass, userNameAndPass);
        userPage.getMenu().getOrderCreate().click();
        OrderCreatePage orderCreatePage = userPage.GoTo(OrderCreatePage.class);
        orderCreatePage.getOrderPersonalInfo().getName().SetValue("Имя");
        orderCreatePage.getOrderPersonalInfo().getPhone().SetValue("1111111111");
        orderCreatePage.getOrderPersonalInfo().getEmail().SetValue(userNameAndPass);
        orderCreatePage.getOrderPersonalInfo().getPhoneAdd().SetValue("433");
        orderCreatePage.getOrderPersonalInfo().getPostalCode().SetValue("123123");
        orderCreatePage.getOrderPersonalInfo().getComment().SetValue("коммент");

        orderCreatePage.getOrderCollectionInfo().getShopRefnum().SetValue("ShopRefnum");
        orderCreatePage.getOrderCollectionInfo().getItemsCount().SetValue("1");
        OrderCollectionInfoList list = orderCreatePage.getOrderCollectionInfo().getArticleList();
        list.getItemRow(0).getArticle().SetValue("ArtName");
        list.getItemRow(0).getName().SetValue("Name");
        list.getItemRow(0).getCount().SetValue("3");

        orderCreatePage.getOrderLogistics().getDeclaredPrice().SetValueAndWait("15");
        orderCreatePage.getOrderLogistics().getWidth().SetValueAndWait("5");
        orderCreatePage.getOrderLogistics().getHeight().SetValueAndWait("3");
        orderCreatePage.getOrderLogistics().getLength().SetValueAndWait("4");
        orderCreatePage.getOrderLogistics().getWeight().SetValueAndWait("6");

        orderCreatePage.waitDocuments(8000);
        orderCreatePage.getOrderLogistics().getRecipient().switchToFrame();
        orderCreatePage.getOrderLogistics().getRecipient().getCityField().WaitIsClicked();
        orderCreatePage.getOrderLogistics().getRecipient().getCityField().SetValue("Москва");
        orderCreatePage.waitDocuments(5000);
        orderCreatePage.getOrderLogistics().getRecipient().getCitesList().WaitPresenceWithRetries();
        orderCreatePage.getOrderLogistics().getRecipient().getCitesList().findByName("г. Москва").click();
        orderCreatePage.getOrderLogistics().getRecipient().getСourierСompany().WaitPresenceWithRetries();
        orderCreatePage.getOrderLogistics().getRecipient().getСourierСompany().selectByTextContains("test_via");
        orderCreatePage.getOrderLogistics().getRecipient().getCourierStreet().SetValue("1");
        orderCreatePage.getOrderLogistics().getRecipient().getCourierHouse().SetValue("2");
        orderCreatePage.getOrderLogistics().getRecipient().getCourierFlat().SetValue("3");

        orderCreatePage.getOrderLogistics().getRecipient().switchToDefaultContent();
        list.getItemRow(0).getName().SetValueAndWait("Name");
        orderCreatePage.waitDocuments(100);
        orderCreatePage.getOrderPayment().getSaveDraft().WaitText("Cохранить как черновик");
        orderCreatePage.getOrderPayment().getSaveDraft().click();
        orderCreatePage.getOrderPayment().getSaveDraft().WaitTextStartsWith("Проверка заказа");
        orderCreatePage.getOrderPayment().getSaveDraft().WaitTextStartsWith("Готово");
        orderCreatePage.getInfoModal().getTextInfo().WaitTextStartsWith("Ваш заказ создан успешно. ID заказа:");
        orderCreatePage.getInfoModal().getCloseButton().click();
//        orderCreatePage.getInfoModal().getCloseButton().javascriptExecutorClick();
//        orderCreatePage.getOrderLogistics().getRecipient().getCourierFlat().
//                javascriptExecute("document.getElementsByName('optionsRadios')[0].click();");
//        javascriptExecute("document.getElementsByName('street')[0].setAttribute('value', '04064');");
    }
}
