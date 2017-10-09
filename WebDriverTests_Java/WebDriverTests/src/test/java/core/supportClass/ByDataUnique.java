package core.supportClass;

import org.openqa.selenium.By;
import org.openqa.selenium.SearchContext;
import org.openqa.selenium.WebElement;

import java.util.Arrays;
import java.util.List;

public class ByDataUnique extends By
{
    private By by;

    public ByDataUnique(String ByString)
    {
        this.by = by.xpath(String.format("//*[@data-unique='%1$s']", ByString));
    }

    @Override
    public List<WebElement> findElements(SearchContext searchContext) {
        List<WebElement> elements = by.findElements(searchContext);
        return elements;
    }
}

