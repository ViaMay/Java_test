package core.systemControls;


import org.openqa.selenium.By;

public class Link extends HtmlControl
{

    public Link(By locator)
    {
        this(locator, null);
    }

    public Link(By locator, HtmlControl container)
    {
        super(locator, container);
    }

    @Override
    public boolean getIsEnabled()
    {
        return !hasClass("disabled");
    }

    public final String getHref()
    {
        return getAttributeValue("href");
    }


    public static Link ByLinkText(String linkText)
    {
        return ByLinkText(linkText, null);
    }

    public static Link ByLinkText(String linkText, HtmlControl container)
    {
        return new Link(By.linkText(linkText), container);
    }
}