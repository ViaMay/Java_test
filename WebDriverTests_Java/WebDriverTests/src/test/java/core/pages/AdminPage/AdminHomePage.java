package core.pages.adminPage;

import core.pages.HomePage;
import core.systemControls.Link;
import core.systemControls.TextInput;
import core.systemPages.PageBase;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 19.11.2015.
 */
public class AdminHomePage extends PageBase
{
    public AdminHomePage()
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
        getLoginInput().WaitVisible();
        getPasswordInput().WaitVisible();
        getLoginButton().WaitVisible();
    }

    public final HomePage LoginAsAdmin(String login, String password)
    {
        getLoginInput().SetValue(login);
        getPasswordInput().SetValue(password);
        getLoginButton().click();
        return GoTo(HomePage.class);
    }


    public final AdminHomePage InvalidLogin()
    {
        getLoginButton().click();
        return GoTo(AdminHomePage.class);
    }
}

