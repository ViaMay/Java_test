package core.pagesControls;

import core.systemControls.HtmlControl;
import core.systemControls.Link;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

/**
 * Created by Виктория on 20.11.2015.
 */
public class Menu extends HtmlControl
{
    public Menu(By className)
    {
        super(className);
        setDashboard(new Link(By.xpath("//li[@data-route='dashboard']/a")));
        setOrderCreate(new Link(By.xpath("//li[@data-route='order-create']/a")));
        setOrderList(new Link(By.xpath("//li[@data-route='order-list']/a")));
        setShopsAndWarehouse(new Link(By.xpath("//li[@data-route='shops']/a")));
        setIntegrations(new Link(By.xpath("//li[@data-route='market']/a")));
        setFinances(new Link(By.xpath("//li[@data-route='finances']/a")));
    }

    private Link Dashboard;
    @Step
    public final Link getDashboard()
    {
        return Dashboard;
    }
    public final void setDashboard(Link value)
    {
        Dashboard = value;
    }
    private Link OrderCreate;
    @Step
    public final Link getOrderCreate()
    {
        return OrderCreate;
    }
    public final void setOrderCreate(Link value)
    {
        OrderCreate = value;
    }
    private Link OrderList;
    @Step
    public final Link getOrderList()
    {
        return OrderList;
    }
    public final void setOrderList(Link value)
    {
        OrderList = value;
    }
    private Link ShopsAndWarehouse;
    @Step
    public final Link getShopsAndStock()
    {
        return ShopsAndWarehouse;
    }
    public final void setShopsAndWarehouse(Link value)
    {
        ShopsAndWarehouse = value;
    }
    private Link Integrations;
    @Step
    public final Link getIntegrations()
    {
        return Integrations;
    }
    public final void setIntegrations(Link value)
    {
        Integrations = value;
    }
    private Link Finances;
    @Step
    public final Link getFinances()
    {
        return Finances;
    }
    public final void setFinances(Link value)
    {
        Finances = value;
    }
}