package core.supportClass;

import core.systemControls.HtmlControl;
import org.openqa.selenium.By;

public final class BY
{
    public static By Self(HtmlControl control)
    {
        return new BySelf(control);
    }


    public static By NthOfClass(String className, int index)
    {
        return new ByNthOfClass(className, index);
    }

    public static By ByNthOfBy(By byName, int index)
    {
        return new ByNthOfBy(byName, index);
    }


    public static By ByDataUnique(String byName)
    {
        return new ByDataUnique(byName);
    }
}
