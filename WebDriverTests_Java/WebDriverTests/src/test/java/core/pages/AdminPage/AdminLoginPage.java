package core.pages.adminPage;

import core.systemControls.Link;
import core.systemControls.TextInput;
import core.systemPages.PageBase;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 19.11.2015.
 */
public class AdminLoginPage extends PageBase
{
    public AdminLoginPage()
    {
        setLoginInput(new TextInput(By.name("username")));
        setPasswordInput(new TextInput(By.name("password")));
        setLoginButton(new Link(By.className("btn")));
    }

    private TextInput LoginInput;
    public final TextInput getLoginInput()
    {
        return LoginInput;
    }
    public final void setLoginInput(TextInput value)
    {
        LoginInput = value;
    }
    private Link LoginButton;
    public final Link getLoginButton()
    {
        return LoginButton;
    }
    public final void setLoginButton(Link value)
    {
        LoginButton = value;
    }
    private TextInput PasswordInput;
    public final TextInput getPasswordInput()
    {
        return PasswordInput;
    }
    public final void setPasswordInput(TextInput value)
    {
        PasswordInput = value;
    }

    @Override
    public void BrowseWaitVisible()
    {
        getLoginInput().WaitVisibleWithRetries(5000);
        getPasswordInput().WaitVisible();
        getLoginButton().WaitVisible();
    }

    public final AdminHomePage LoginAsAdmin(String login, String password)
    {
        getLoginInput().SetValue(login);
        getPasswordInput().SetValue(password);
        getLoginButton().click();
        return GoTo(AdminHomePage.class);
    }


    public final AdminLoginPage InvalidLogin()
    {
        getLoginButton().click();
        return GoTo(AdminLoginPage.class);
    }
}

