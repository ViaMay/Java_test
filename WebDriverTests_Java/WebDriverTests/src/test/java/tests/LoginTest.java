package tests;

import core.SetUrlTestBase;
import core.pages.HomePage;
import core.pages.Login2Page;
import core.pages.LoginPage;
import org.testng.annotations.Test;
import ru.yandex.qatools.allure.annotations.Description;
import ru.yandex.qatools.allure.annotations.Title;

import java.net.MalformedURLException;
import java.net.URL;

@Title("Тест на вход в ЛК пользователя 2")
public class LoginTest extends SetUrlTestBase
{
    @Test
    @Description("LoginTest")
    public void LoginTest_Test() {

        LoginPage userNewLoginPage = new LoginPage();
        URL url = null;
        try {
             url = new URL("https://growfood.pro/");
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
        Login2Page page = userNewLoginPage.GoToUri(Login2Page.class, url);
        page.ButtonDown.click();
        page.LoginButton.click();
//        userNewLoginPage.getPasswordInput().SetValue(userNameAndPass);
//
//        userNewLoginPage.getLoginButton().WaitText("Вход");
//        userNewLoginPage.getLoginButton().click();
//        userNewLoginPage.getLoginButtonDownload().WaitVisibleWithRetries();
//        userNewLoginPage.getLoginButton().WaitVisibleWithRetries();
//        HomePage userNewHomePage = userNewLoginPage.GoTo(HomePage.class);


//        driver = webdriver.Chrome("путь до драйвера")
//        driver.implicitly_wait(10)
//
//        driver.get("https://growfood.pro/")
//
//        button_todown = driver.find_element_by_id('to_down')
//        button_todown.click()
//
//        driver.find_element_by_xpath('//*[@id="Fit"]/div[2]/div[1]/div[5]/a/img').click()
    }
}