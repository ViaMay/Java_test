package core.pages;

import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 23.11.2015.
 */
public class MarketPage extends HomePage
{
    public MarketPage()
    {
        final String xpathLocator = "//div[@data-name='route.finances']";
        HtmlControl container = new HtmlControlContainer(By.xpath(xpathLocator));

        setAllLink(new Link(By.xpath("//*[@data-value='all']"), container));
        setFinancesServiceLink(new Link(By.xpath("//*[@data-value='finances_service']"), container));
        setStocksOfCustodyLink(new Link(By.xpath("//*[@data-value='stocks_of_custody']"), container));
    }

    @Override
    public void BrowseWaitVisible() {
        getAllLink().WaitText("Все");
        getFinancesServiceLink().WaitText("Финансовые сервисы");
        getStocksOfCustodyLink().WaitText("Склады ответственного хранения");
    }

    private Link AllLink;

    private Link getAllLink() {
        return AllLink;
    }

    private void setAllLink(Link value) {
        AllLink = value;
    }

    private Link FinancesServiceLink;

    private Link getFinancesServiceLink() {
        return FinancesServiceLink;
    }

    private void setFinancesServiceLink(Link value) {
        FinancesServiceLink = value;
    }
    private Link StocksOfCustodyLink;

    private Link getStocksOfCustodyLink() {
        return StocksOfCustodyLink;
    }

    private void setStocksOfCustodyLink(Link value) {
        StocksOfCustodyLink = value;
    }
}
