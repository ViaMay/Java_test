package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.CheckBox;
import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.TextInput;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

/**
 * Created by Виктория on 23.11.2015.
 */
public class OrderCollectionInfo extends HtmlControl {
    public OrderCollectionInfo(By xpath) {
        super(xpath);

        final String xpathLocator = "//div[@data-name='route.order-create']";
        final String xpathLocator2 = "//div[@class='panel collection-info']";
        HtmlControl container = new HtmlControlContainer(By.xpath(xpathLocator));

        setShopRefnum(new TextInput(BY.ByDataUnique("order-create-shop_refnum")));
        setItemsCount(new TextInput(BY.ByDataUnique("order-create-items_count")));
        setVolume(new CheckBox(By.xpath(xpathLocator2 + "//input[@data-name='form.already_paid']"), container));
        setArticleList(new OrderCollectionInfoList(By.xpath(xpathLocator2 + "//*[@data-name='goods.list']"), container));
}

    private TextInput ShopRefnum;
    @Step
    public final TextInput getShopRefnum() {
        return ShopRefnum;
    }
    public final void setShopRefnum(TextInput value) {
        ShopRefnum = value;
    }
    private TextInput ItemsCount;
    @Step
    public final TextInput getItemsCount() {
        return ItemsCount;
    }
    public final void setItemsCount(TextInput value) {
        ItemsCount = value;
    }
    private CheckBox Volume;
    @Step
    public final CheckBox getVolume() {
        return Volume;
    }
    public final void setVolume(CheckBox value) {
        Volume = value;
    }
    private OrderCollectionInfoList ArticleList;
    @Step
    public final OrderCollectionInfoList getArticleList()
    {
        return ArticleList;
    }
    public final void setArticleList(OrderCollectionInfoList value)
    {
        ArticleList = value;
    }
}