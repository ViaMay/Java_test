package core.pages;

import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;


public class OrderAndDocumentsListPage extends HomePage
{
    public OrderAndDocumentsListPage()
    {
        final String xpathLocator = "//div[@data-name='route.order-list']";
        HtmlControl container = new HtmlControlContainer(By.xpath(xpathLocator));

        setOrdersLink(new Link(By.linkText("Список заказов"), container));
        setDocumentsLink(new Link(By.linkText("Документы"), container));
    }

    @Override
    public void BrowseWaitVisible() {
        getOrdersLink().WaitText("Список заказов");
        getDocumentsLink().WaitText("Документы");
    }
    private Link OrdersLink;
    @Step
    public Link getOrdersLink() {
        return OrdersLink;
    }
    public void setOrdersLink(Link value) {
        OrdersLink = value;
    }
    private Link DocumentsLink;
    @Step
    public Link getDocumentsLink() {
        return DocumentsLink;
    }
    public void setDocumentsLink(Link value) {
        DocumentsLink = value;
    }
}
