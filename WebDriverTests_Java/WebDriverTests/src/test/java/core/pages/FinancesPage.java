package core.pages;

import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 23.11.2015.
 */
public class FinancesPage extends HomePage
{
    public FinancesPage()
    {
        final String xpathLocator = "//div[@data-name='route.finances']";
        HtmlControl container =  new HtmlControlContainer(By.xpath(xpathLocator));

        setFinancesLink(new Link(By.linkText("Счета и отчеты"),container));
        setFinancesCommissionLink(new Link(By.linkText("Комиссии и сроки перечисления"),container));
    }

    @Override
    public void BrowseWaitVisible()
    {
        getFinancesLink().WaitVisibleWithRetries();
        getFinancesCommissionLink().WaitVisibleWithRetries();
    }

    private Link FinancesLink;
    public Link getFinancesLink()
    {
        return FinancesLink;
    }
    public void setFinancesLink(Link value)
    {
        FinancesLink = value;
    }
    private Link FinancesCommissionLink;
    public Link getFinancesCommissionLink()
    {
        return FinancesCommissionLink;
    }
    public void setFinancesCommissionLink(Link value)
    {
        FinancesCommissionLink = value;
    }
}
