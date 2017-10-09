package core.pages;

import core.systemControls.Link;
import core.systemControls.TextInput;
import core.systemPages.PageBase;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class Login2Page extends PageBase
{
    public Login2Page()
    {
        ButtonDown = new Link(By.id("to_down"));
        LoginButton = new Link(By.xpath("//*[@id='Fit']/div[2]/div[1]/div[5]/a/img"));
    }

    public Link ButtonDown;
    public Link LoginButton;


    @Override
    public void BrowseWaitVisible()
    {
        ButtonDown.WaitVisible();
    }
}