package core.pagesControls;

import core.systemControls.HtmlControl;
import core.systemControls.HtmlControlContainer;
import core.systemControls.Link;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

/**
 * Created by Виктория on 23.11.2015.
 */
public class OrderPayment extends HtmlControl
{
    public OrderPayment(By xpath)
    {
        super(xpath);

        final String xpathLocator = "//div[@data-name='route.order-create']";
        final String xpathLocator2 = "//div[@class='panel payment']";
        HtmlControl container =  new HtmlControlContainer(By.xpath(xpathLocator));

        setSaveDraft(new Link(By.xpath(xpathLocator2 + "//button[@data-name='btn.save.draft']"), container));
        setSave(new Link(By.xpath(xpathLocator2 + "//button[@data-name='form.save']"), container));
    }
    private Link SaveDraft;
    @Step
    public final Link getSaveDraft()
    {
        return SaveDraft;
    }
    public final void setSaveDraft(Link value)
    {
        SaveDraft = value;
    }
    private Link Save;
    @Step
    public final Link getSave()
    {
        return Save;
    }
    public final void setSave(Link value)
    {
        Save = value;
    }
}
