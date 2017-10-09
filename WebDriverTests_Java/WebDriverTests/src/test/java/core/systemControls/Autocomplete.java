package core.systemControls;


import core.supportClass.StopWatch;
import org.junit.Assert;
import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebElement;

public class Autocomplete extends  TextInput
{

    public Autocomplete(By locator)
    {
        this(locator, null);
    }

    public Autocomplete(By locator, HtmlControl container)
    {
        super(locator, container);

    }
    public void SetValueSelect(String value)
    {
        SetValueSelect(value, null);
    }

    public void SetValueSelect(String value, String valueExpected)
    {
        SetValue(value);
        if (valueExpected == null) valueExpected = value;

        StaticText list = new StaticText(By.xpath("//ul[@class='list-menu']"));
        list.getIsPresent();
        list.getIsVisible();

        StaticText element1 = new StaticText(By.xpath("//ul[@class='list-menu']/li[@data-value]"));
        element1.WaitPresenceWithRetries();
        element1.WaitVisibleWithRetries();

        StopWatch w = new StopWatch();
        w.start();
        do
        {
            java.util.List<WebElement> elements = list.element.findElements(By.xpath("//li[@data-value]"));
            for (WebElement element:
                    elements) {
                if (element.getText().contains(valueExpected))
                {
                    element.click();
                    return;
                }
            }
        } while (w.getElapsedTimeSecs()*1000 < 30000);
        w.stop();
        Assert.fail(String.format("Не наден элемент '%1$s' в выпадающем списке, по запросу '%2$s'",valueExpected, value ));
    }
}

