package core.supportClass;

import org.openqa.selenium.By;
import org.openqa.selenium.SearchContext;
import org.openqa.selenium.WebElement;

import java.util.Arrays;
import java.util.List;
import java.util.NoSuchElementException;
/**
 * Created by Виктория on 03.12.2015.
 */
public class ByNthOfClass extends By
{
    private String className;
    private int index;

    public ByNthOfClass(String className, int index)
    {
        this.className = className;
        this.index = index;
    }

    @Override
    public WebElement findElement(SearchContext context)
    {
        List<WebElement> elements = className(className).findElements(context);
        if (index < 0 || index >= elements.size())
        {
            throw new NoSuchElementException(String.format("Element with class '%1$s' and index '%2$s' not found (actual count='%3$s')", className, index, elements.size()));
        }
        return elements.get(index);
    }



    @Override
    public List<WebElement> findElements(SearchContext context)
    {
        List<WebElement> elements = className(className).findElements(context);
        if (index < 0 || index >= elements.size())
        {
            return Arrays.asList(new WebElement[0]);
        }
        WebElement[] e = new WebElement[] {elements.get(index)};
        return Arrays.asList( new WebElement[] {elements.get(index)});
    }


    @Override
    public String toString()
    {
        return String.format("ByNthOfClass: .%1$s[%2$s]", className, index);
    }
}
