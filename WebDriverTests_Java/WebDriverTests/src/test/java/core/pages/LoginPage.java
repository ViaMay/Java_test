package core.pages;

import core.systemControls.Link;
import core.systemControls.TextInput;
import core.systemPages.PageBase;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class LoginPage extends PageBase
{
    public LoginPage()
    {
        setLoginInput(new TextInput(By.name("login")));
        setPasswordInput(new TextInput(By.name("pass")));
        setLoginButton(new Link(By.xpath("//button[@data-name='btn.send']")));
        setLoginButtonDownload(new Link(By.xpath("//button[@data-name='btn.send']/span")));
    }

    private TextInput LoginInput;
    @Step
    public final TextInput getLoginInput()
    {
        return LoginInput;
    }
    public final void setLoginInput(TextInput value)
    {
        LoginInput = value;
    }
    private Link LoginButton;
    @Step
    public final Link getLoginButton()
    {
        return LoginButton;
    }
    public final void setLoginButton(Link value)
    {
        LoginButton = value;
    }
    private Link LoginButtonDownload;
    public final Link getLoginButtonDownload()
    {
        return LoginButtonDownload;
    }
    public final void setLoginButtonDownload(Link value)
    {
        LoginButtonDownload = value;
    }
    private TextInput PasswordInput;
    @Step
    public final TextInput getPasswordInput()
    {
        return PasswordInput;
    }
    public final void setPasswordInput(TextInput value)
    {
        PasswordInput = value;
    }

    @Override
    @Step("Переходим на страницу LoginPage и проверяем видимость элементов")
    public void BrowseWaitVisible()
    {
        getLoginInput().WaitVisible();
        getPasswordInput().WaitVisible();
        getLoginButton().WaitVisible();
    }

    public final HomePage LoginAsUser(String login, String password)
    {
        getLoginInput().SetValue(login);
        getPasswordInput().SetValue(password);
        getLoginButton().WaitText("Вход");
        getLoginButton().click();
        getLoginButtonDownload().WaitVisibleWithRetries();
        getLoginButton().WaitVisibleWithRetries();
        waitDocuments(1000);
        return GoTo(HomePage.class);
    }

    public final LoginPage InvalidLogin()
    {
        getLoginButton().click();
        return GoTo(LoginPage.class);
    }
}