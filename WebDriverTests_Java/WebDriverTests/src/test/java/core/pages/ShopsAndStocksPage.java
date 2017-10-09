package core.pages;

import core.pagesControls.*;
import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class ShopsAndStocksPage extends HomePage
{
    public ShopsAndStocksPage()
    {
        final String xpathLocator = "//div[@data-name='route.shops']";
        HtmlControl container =  new HtmlControlContainer(By.xpath(xpathLocator));

        setShopsLink(new Link(By.linkText("Магазины"), container));
        setShopAdd(new Link(By.xpath("//*[@data-event='app.modal-form.shop:openToAdd']"), container));
        setShopAddModal(new ShopAddModal(By.xpath("//*[@data-event='app.modal-form.shop:openToAdd']"), container));

        setInfoModal(new InfoModal(By.xpath("//*[@data-name='modal.info']")));

        setStocksLink(new Link(By.linkText("Склады"), container));
        setStockAdd(new Link(By.xpath("//*[@data-event='app.modal-form.stock:openToAdd']"), container));
        setStockAddModal(new StockAddModal(By.xpath("//*[@data-event='app.modal-form.stock:openToAdd']"), container));
    }

    public final ShopRow getShopRow(int index)
    {
        ShopRow row = new ShopRow(index);
        return row;
    }
 public final ShopRow findRowByName(String name)
    {
        waitDocuments(2000);
        int count = getCountRow();
        for (int i = 0; i < count; i++)
        {
            if (getShopRow(i).getNameShop().getText().equals(name))
                return getShopRow(i);
        }
        try {
            throw new Exception(String.format("не найдена строка с именем", name));
        } catch (Exception e) {}
        return null;
    }
    public final StockRow getStockRow(int index)
    {
        StockRow row = new StockRow(index);
        return row;
    }
    public final StockRow findRowByStockName(String name)
    {
        waitDocuments(2000);
        int count = getCountRowStock();
        for (int i = 0; i < count; i++)
        {
            if (getStockRow(i).getNameStock().getText().equals(name))
                return getStockRow(i);
        }
        try {
            throw new Exception(String.format("не найдена строка с именем", name));
        } catch (Exception e) {}
        return null;
    }

    private int getCountRow()
    {
        HtmlControl container =  new HtmlControlContainer(By.xpath("//div[@data-name='route.shops']"));
        Integer count = container.element.findElements(By.xpath("//*[@data-shop-id]")).size();
        return count;
    }
    private int getCountRowStock()
    {
        HtmlControl container =  new HtmlControlContainer(By.xpath("//div[@data-name='stock.list']"));
        Integer count = container.element.findElements(By.xpath("//div[@data-name='stock.list']/div[@data-unique]")).size();
        return count;
    }

    @Override
    public void BrowseWaitVisible()
    {
        getShopsLink().WaitText("Магазины");
        getStocksLink().WaitText("Склады");
    }
    private Link ShopsLink;
    @Step
    public Link getShopsLink()
    {
        return ShopsLink;
    }
    public void setShopsLink(Link value)
    {
        ShopsLink = value;
    }
    private Link ShopAdd;
    @Step
    public Link getShopAdd()
    {
        return ShopAdd;
    }
    public void setShopAdd(Link value)
    {
        ShopAdd = value;
    }
    private ShopAddModal ShopAddModal;
    @Step
    public ShopAddModal getShopAddModal()
    {
        return ShopAddModal;
    }
    public void setShopAddModal(ShopAddModal value)
    {
        ShopAddModal = value;
    }
    private StockAddModal StockAddModal;
    @Step
    public StockAddModal getStockAddModal() { return StockAddModal; }
    public void setStockAddModal(StockAddModal value) { StockAddModal = value;}
    private InfoModal InfoModal;
    @Step
    public InfoModal getInfoModal()
    {
        return InfoModal;
    }
    public void setInfoModal(InfoModal value)
    {
        InfoModal = value;
    }
    private Link StocksLink;
    @Step
    public Link getStocksLink()
    {
        return StocksLink;
    }
    public void setStocksLink(Link value)
    {
        StocksLink = value;
    }
    private Link StockAdd;
    @Step
    public Link getStockAdd()
    {
        return StockAdd;
    }
    public void setStockAdd(Link value)
    {
        StockAdd = value;
    }
}
