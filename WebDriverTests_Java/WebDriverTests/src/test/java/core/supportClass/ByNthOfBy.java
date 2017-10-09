package core.supportClass;

import org.openqa.selenium.By;
import org.openqa.selenium.SearchContext;
import org.openqa.selenium.WebElement;

import java.util.Arrays;
import java.util.List;
import java.util.NoSuchElementException;

public class ByNthOfBy extends By
{
    private By by;
    private int index;

    public ByNthOfBy(By by, int index)
    {
        this.by = by;
        this.index = index;
    }

    @Override
    public WebElement findElement(SearchContext context)
    {
        List<WebElement> elements = by.findElements(context);
        if (index < 0 || index >= elements.size())
        {
            throw new NoSuchElementException(String.format("Element with by '%1$s' and index '%2$s' not found (actual count='%3$s')", by, index, elements.size()));
        }
        return elements.get(index);
    }

    @Override
    public List<WebElement> findElements(SearchContext context)
    {
        List<WebElement> elements = by.findElements(context);
        if (index < 0 || index >= elements.size())
        {
            return Arrays.asList(new WebElement[0]);
        }
        return Arrays.asList( new WebElement[] {elements.get(index)});
    }

    @Override
    public String toString()
    {
        return String.format("ByNthOfBy: <%1$s>[%2$s]", by, index);
    }
}

