package core.pages;

import core.systemControls.Link;
import core.systemPages.PageBase;
import org.openqa.selenium.By;

public class DefaultPage extends PageBase
{
    public DefaultPage()
    {
        setLoginButton(new Link(By.linkText("Вход в личный кабинет")));
    }

    private Link LoginButton;
    public Link getLoginButton()
    {
        return LoginButton;
    }
    public void setLoginButton(Link value)
    {
        LoginButton = value;
    }

    @Override
    public void BrowseWaitVisible()
    {
        getLoginButton().WaitVisibleWithRetries();
    }
}