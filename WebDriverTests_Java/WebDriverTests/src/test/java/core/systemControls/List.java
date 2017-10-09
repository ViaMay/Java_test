package core.systemControls;


import core.supportClass.BY;
import org.openqa.selenium.By;

public class List extends HtmlControl
{

    public List(By locator)
    {
        this(locator, null);
    }

    public List(By locator, HtmlControl container)
    {
        super(locator, container);
    }

    public List(String idLocator)
    {
        this(idLocator, null);
    }

    public List(String idLocator, HtmlControl container)
    {
        super(idLocator, container);
    }

    public final int getCount()
    {
        return element.findElements(By.xpath("//ul/li[@data-id]")).size();
    }

    public final List getItem(int index)
    {
        return new List(BY.ByNthOfBy(By.xpath("//ul/li[@data-id]"), index), this);
    }


    public final List findByName(String value)
    {
        for (int i = 0; i < getCount(); i++)
        {
            if (value.equals(this.getItem(i).getText()))
            {
                return this.getItem(i);
            }
        }
        throw new RuntimeException(String.format("Не найден текст '%1$s' в списке", value));
    }
}
