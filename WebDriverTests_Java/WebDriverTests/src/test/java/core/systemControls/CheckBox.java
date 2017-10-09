package core.systemControls;

import org.openqa.selenium.By;

/**
 * Created by Виктория on 03.12.2015.
 */
public class CheckBox  extends HtmlControl
{
    static
    {
    }


    public CheckBox(By locator)
    {
        this(locator, null);
    }

    public CheckBox(By locator, HtmlControl container)
    {
        super(locator, container);
    }
//
//    public final boolean getChecked()
//    {
//        return (boolean)element.getAttribute("checked");
//    }

//    public final void CheckAndWait()
//    {
//        if (!getChecked())
//        {
//            element.click();
//        }
//    }
//
//    public final void UncheckAndWait()
//    {
//        if (getChecked())
//        {
//            element.click();
//        }
//    }
//
//    public void WaitChecked()
//    {
//        String description = formatWithLocator(String.format("Ожидание Checked в элементе"));
//        Waiter.Wait(() -> getChecked() == true, description);
//    }
//
//    public void WaitUnchecked()
//    {
//        String description = formatWithLocator(String.format("Ожидание Checked в элементе"));
//        Waiter.Wait(() -> getChecked() == false, description);
//    }
}

