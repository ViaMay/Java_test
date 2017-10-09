package core.supportClass;

import core.systemControls.HtmlControl;
import org.openqa.selenium.By;
import org.openqa.selenium.SearchContext;
import org.openqa.selenium.WebElement;

import java.util.ArrayList;
import java.util.Arrays;

/**
 * Created by Виктория on 23.11.2015.
 */
public class BySelf extends By
{
    private HtmlControl control;

    public BySelf(HtmlControl control)
    {
        this.control = control;
    }

    @Override
    public WebElement findElement(SearchContext context)
    {
        return control.element;
    }

    @Override
    public ArrayList<WebElement> findElements(SearchContext context)
    {
        ArrayList<WebElement> list = control.element != null ? new ArrayList<WebElement>(Arrays.asList(new WebElement[]{control.element})) : new ArrayList<WebElement>();
        return new ArrayList(list);
    }


    @Override
    public String toString()
    {
        return "BySelf";
    }
}

