package core.systemControls;

import org.openqa.selenium.By;


public class StaticText extends HtmlControl
{

    public StaticText(By locator)
    {
        this(locator, null);
    }

    //C# TO JAVA CONVERTER NOTE: Java does not support optional parameters. Overloaded method(s) are created above:
//ORIGINAL LINE: public StaticText(By locator, HtmlControl container = null)
    public StaticText(By locator, HtmlControl container)
    {
        super(locator, container);
    }


    public StaticText(String idLocator)
    {
        this(idLocator, null);
    }

    //C# TO JAVA CONVERTER NOTE: Java does not support optional parameters. Overloaded method(s) are created above:
//ORIGINAL LINE: public StaticText(string idLocator, HtmlControl container = null)
    public StaticText(String idLocator, HtmlControl container)
    {
        super(idLocator, container);
    }
}
