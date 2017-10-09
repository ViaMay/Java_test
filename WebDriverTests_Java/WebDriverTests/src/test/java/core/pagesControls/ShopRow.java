package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.StaticText;
import core.systemControls.TextInput;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 20.11.2015.
 */
public class ShopRow extends HtmlControl
{
    public ShopRow(int index)
    {
        super(By.xpath("//"));
        String locator = "//div[@data-name='shop.list']/div[@data-shop-id]";

        setNameShop(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div[@class='head']"), index)));
        setShopArrow(new Link(BY.ByNthOfBy(By.xpath(locator + "/div[@class='head']/div[@class='arrow']/i"), index)));
        setAdress(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div/div[@class='address']/div[1]"), index)));
        setStock(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div/div[@class='address']/div[2]"), index)));
        setKey(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div/div[@class='address']/div[3]"), index)));
        setPickupType(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div/div[@data-name='info.pickup']//span[@data-name='type_default']"), index)));
        setPickupCompany(new StaticText(BY.ByNthOfBy(By.xpath(locator + "/div/div[@data-name='info.pickup']//span[@data-name='company_default']"), index)));
        setSdkInfo(new StaticText(BY.ByNthOfBy(By.xpath("//div[@data-name='sdk.platform']/div"), index)));
        setShopEdit(new Link(BY.ByNthOfBy(By.xpath(locator + "/div/div/button[@data-action='form.edit']/img"), index)));
    }
    private StaticText NameShop;
    public final StaticText getNameShop()
    {
        return NameShop;
    }
    public final void setNameShop(StaticText value)
    {
        NameShop = value;
    }
    private Link ShopArrow;
    public final Link getShopArrow()
    {
        return ShopArrow;
    }
    public final void setShopArrow(Link value)
    {
        ShopArrow = value;
    }

    private StaticText Adress;
    public final StaticText getAdress()
    {
        return Adress;
    }
    public final void setAdress(StaticText value)
    {
        Adress = value;
    }
    private StaticText Stock;
    public final StaticText getStock()
    {
        return Stock;
    }
    public final void setStock(StaticText value)
    {
        Stock = value;
    }
    private StaticText Key;
    public final StaticText getKey()
    {
        return Key;
    }
    public final void setKey(StaticText value)
    {
        Key = value;
    }
    private StaticText PickupType;
    public final StaticText getPickupType()
    {
        return PickupType;
    }
    public final void setPickupType(StaticText value)
    {
        PickupType = value;
    }
    private StaticText PickupCompany;
    public final StaticText getPickupCompany()
    {
        return PickupCompany;
    }
    public final void setPickupCompany(StaticText value)
    {
        PickupCompany = value;
    }
    private StaticText SdkInfo;
    public final StaticText getSdkInfo()
    {
        return SdkInfo;
    }
    public final void setSdkInfo(StaticText value)
    {
        SdkInfo = value;
    }

    private Link ShopEdit;
    public final Link getShopEdit()
    {
        return ShopEdit;
    }
    public final void setShopEdit(Link value)
    {
        ShopEdit = value;
    }
}