package core.pagesControls;


import core.systemControls.HtmlControl;
import core.systemControls.Link;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class OrderCollectionInfoList extends HtmlControl
{

    public OrderCollectionInfoList(By locator)
    {
        this(locator, null);

    }

    public OrderCollectionInfoList(By locator, HtmlControl container)
    {
        super(locator, container);
        this.locator = locator;
        setAddItemButton(new Link(By.xpath("//select[@data-action='add.item']"), container));
    }
    private Link AddItemButton;
    public final Link getAddItemButton()
    {
        return AddItemButton;
    }
    public final void setAddItemButton(Link value)
    {
        AddItemButton = value;
    }


    public final int getCountRow()
    {
        return element.findElements(By.xpath("//div[@class='list-cont']/div[@class='list-item']")).size();
    }

    @Step("Строка с индексом {0}")
    public final OrderCollectionInfoListRow getItemRow(int index)
    {
        OrderCollectionInfoListRow row = new OrderCollectionInfoListRow(index);
        return row;
    }

    private By locator;
}
