package core.pagesControls;

import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.StaticText;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 20.11.2015.
 */
public class StockRow extends HtmlControl
{
    public StockRow(int index)
    {
        super(By.xpath("//"));
        String locator = String.format("//*[@data-unique='warehouse_list_item_%1$s']/div", index + 1);

        setNameStock(new StaticText(By.xpath(locator)));
        setStockArrow(new Link(By.xpath(locator+ "/div/i")));

        setNameStockTwo(new StaticText(By.xpath(locator + "/div/div/div")));
        setContactName(new StaticText(By.xpath(locator + "/div/div/div[2]")));
        setStockPhone(new StaticText(By.xpath(locator + "/div/div/div[3]")));
        setStockEmail(new StaticText(By.xpath(locator + "/div/div/div[4]")));
        setStockCity(new StaticText(By.xpath(locator + "/div/div[2]/div[1]")));
        setStockStreet(new StaticText(By.xpath(locator + "/div/div[2]/div[2]")));
        setStockHome(new StaticText(By.xpath(locator + "/div/div[2]/div[3]")));
        setStockFlat(new StaticText(By.xpath(locator + "/div/div[2]/div[4]")));
        setStockTimetable(new StaticText(By.xpath(locator + "/div[2]")));

        setStockEdit(new Link(By.xpath(locator + "/div[4]/button")));
        setStockDelete(new Link(By.xpath(locator + "/div[3]/button")));
    }

    private StaticText NameStock;
    public final StaticText getNameStock() {
        return NameStock;
    }
    public final void setNameStock(StaticText value) {
        NameStock = value;
    }

    private StaticText NameStockTwo;
    public final StaticText getNameStockTwo() {
        return NameStockTwo;
    }
    public final void setNameStockTwo(StaticText value) {
        NameStockTwo = value;
    }

    private Link StockArrow;
    public final Link getStockArrow() {
        return StockArrow;
    }
    public final void setStockArrow(Link value) {
        StockArrow = value;
    }

    private StaticText ContactName;
    public final StaticText getContactName() {
        return ContactName;
    }
    public final void setContactName(StaticText value) {
        ContactName = value;
    }

    private StaticText StockPhone;
    public final StaticText getStockPhone() {
        return StockPhone;
    }
    public final void setStockPhone(StaticText value) {
        StockPhone = value;
    }

    private StaticText StockEmail;
    public final StaticText getStockEmail() {
        return StockEmail;
    }
    public final void setStockEmail(StaticText value) {
        StockEmail = value;
    }

    private StaticText StockCity;
    public final StaticText getStockCity() {
        return StockCity;
    }
    public final void setStockCity(StaticText value) {
        StockCity = value;
    }

    public StaticText StockStreet;
    public final StaticText getStockStreet() {
        return StockStreet;
    }
    public final void setStockStreet(StaticText value) {
        StockStreet = value;
    }

    public StaticText StockHome;
    public final StaticText getStockHome() {
        return StockHome;
    }
    public final void setStockHome(StaticText value) {
        StockHome = value;
    }

    public StaticText StockFlat;
    public final StaticText getStockFlat() {
        return StockFlat;
    }
    public final void setStockFlat(StaticText value) {
        StockFlat = value;
    }

    public StaticText StockTimetable;
    public final StaticText getStockTimetable() {
        return StockTimetable;
    }
    public final void setStockTimetable(StaticText value) {
        StockTimetable = value;
    }

    private Link StockEdit;
    public final Link getStockEdit() { return StockEdit; }
    public final void setStockEdit(Link value) { StockEdit = value; }

    private Link StockDelete;
    public final Link getStockDelete() { return StockDelete; }
    public final void setStockDelete(Link value) { StockDelete = value; }

}