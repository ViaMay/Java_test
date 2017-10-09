package core.pagesControls;


import core.supportClass.BY;
import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.TextInput;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class OrderCollectionInfoListRow extends HtmlControl
{

    public OrderCollectionInfoListRow(int index)
    {
        super(By.xpath("//"));

        final String xpathLocator = "//div[@class='list-cont']/div[@class='list-item']/";
        setArticle(new TextInput(BY.ByNthOfBy(By.xpath(xpathLocator + "div/input[@name='article']"), index)));
        setName(new TextInput(BY.ByNthOfBy(By.xpath(xpathLocator + "div/input[@name='name']"), index)));
        setCount(new TextInput(BY.ByNthOfBy(By.xpath(xpathLocator + "div/input[@name='count']"), index)));
        setDelete(new Link(BY.ByNthOfBy(By.xpath(xpathLocator + "div[@class='delete']"), index)));
    }
    private TextInput Article;
    @Step
    public final TextInput getArticle()
    {
        return Article;
    }
    public final void setArticle(TextInput value)
    {
        Article = value;
    }
    private TextInput Name;
    @Step
    public final TextInput getName()
    {
        return Name;
    }
    public final void setName(TextInput value)
    {
        Name = value;
    }
    private TextInput Count;
    @Step
    public final TextInput getCount()
    {
        return Count;
    }
    public final void setCount(TextInput value)
    {
        Count = value;
    }
    private Link Delete;
    @Step
    public final Link getDelete()
    {
        return Delete;
    }
    public final void setDelete(Link value)
    {
        Delete = value;
    }


}
