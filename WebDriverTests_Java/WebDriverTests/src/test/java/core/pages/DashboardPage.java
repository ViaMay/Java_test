package core.pages;

import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;


public class DashboardPage extends HomePage
{
    public DashboardPage()
    {
        final String xpathLocator = "//div[@data-name='route.dashboard']";
        HtmlControl container =  new HtmlControlContainer(By.xpath(xpathLocator));

        setOrderCreate(new Link(By.xpath("//div[@data-view='app.order.create.mini']/div"), container));
        setOrderShipment(new Link(By.xpath("//div[@data-view='app.order.shipment']/div"), container));
        setFinancesReport(new Link(By.xpath("//div[@data-view='app.finances.report.mini']/div"), container));
    }

    @Override
    public void BrowseWaitVisible()
    {
        getOrderCreate().WaitText("Создать заказ");
        getOrderShipment().WaitText("Заказы к отгрузке");
        getFinancesReport().WaitText("Финансовая информация");
    }

     Link OrderCreate;
    private Link getOrderCreate()
    {
        return OrderCreate;
    }
    private void setOrderCreate(Link value)
    {
        OrderCreate = value;
    }
    private Link OrderShipment;
    private Link getOrderShipment()
    {
        return OrderShipment;
    }
    private void setOrderShipment(Link value)
    {
        OrderShipment = value;
    }
    private Link FinancesReport;
    private Link getFinancesReport()
    {
        return FinancesReport;
    }
    private void setFinancesReport(Link value)
    {
        FinancesReport = value;
    }
}