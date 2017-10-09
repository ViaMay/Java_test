package core.systemPages;

import core.webDriver.WebDriverCache;
import core.systemControls.PageLoadCounter;
import ru.yandex.qatools.allure.annotations.Step;

import java.net.URL;

public abstract class PageBase
{

    public PageBase()
    {
    }

    public abstract void BrowseWaitVisible();

    public <TPage extends PageBase>  TPage  GoTo(Class<TPage> pageClass)
    {
        return GoTo(pageClass, 1);
    }

    @Step ("Переход на страницу {0}")
    public <TPage extends PageBase>  TPage  GoTo(Class<TPage> pageClass, int pageLoads) {
        VerifyPageIsAlive();
        PageLoadCounter.WaitPageLoaded(pageLoads);
        CleanFields(this);
        TPage newPage = null;
        try {
            newPage = pageClass.newInstance();
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        newPage.BrowseWaitVisible();
        WebDriverCache.getWebDriver().WaitForAjax();
        return newPage;
    }

    public final <TPage extends PageBase> TPage ChangePageType(Class<TPage> pageClass) {
    VerifyPageIsAlive();
    CleanFields(this);
        TPage newPage = null;
        try {
            newPage = pageClass.newInstance();
        } catch (InstantiationException e) {
            e.printStackTrace();
        } catch (IllegalAccessException e) {
            e.printStackTrace();
        }
        newPage.BrowseWaitVisible();
    return newPage;
}

   public static <TPage extends PageBase> TPage GoToUri(Class<TPage> pageClass, URL uri)  {
        WebDriverCache.getWebDriver().GoToUri(uri);
        PageLoadCounter.InitPageLoadCounterCookie();
        PageLoadCounter.WaitPageLoaded();
        WebDriverCache.getWebDriver().SetCookie("testingMode", "1");

       TPage newPage = null;
       try {
           newPage = pageClass.newInstance();
       } catch (InstantiationException e) {
           e.printStackTrace();
       } catch (IllegalAccessException e) {
           e.printStackTrace();
       }
       newPage.BrowseWaitVisible();
//      InitPage();
        return newPage;
}

    public final String GetUrlParameter(String parameterName)
    {
        return WebDriverCache.getWebDriver().getUrl();
    }

    public final String GetUrl()
    {
        return WebDriverCache.getWebDriver().getUrl();
    }



   public static <TPage extends PageBase> TPage RefreshPage(Class<TPage> pageClass,TPage page) throws InstantiationException, IllegalAccessException {
    return PageBase.<TPage, TPage>RefreshPage(pageClass, pageClass,page);
}


    public static <TPage extends PageBase, TPage1 extends PageBase> TPage1 RefreshPage(Class<TPage> pageClass,Class<TPage1> pageClass1, TPage page) throws IllegalAccessException, InstantiationException {
        TPage newPage = pageClass.newInstance();
        newPage.VerifyPageIsAlive();
        WebDriverCache.getWebDriver().Refresh();
        PageLoadCounter.WaitPageLoaded();
        CleanFields(page);
        TPage1 newPage1 = pageClass1.newInstance();
        newPage.BrowseWaitVisible();
//      InitPage();
        return newPage1;
    }

    private static void InitPage()
    {
        WebDriverCache.getWebDriver().ExecuteScript("$(document.body).addClass('testingMode')");
    }

    private static void CleanFields(PageBase from)
    {
//        FieldsCleanerCache.Clean(from);
    }

    void VerifyPageIsAlive()
    {
        if (alive == null)
        {
            throw new IllegalStateException("Данная страница уже закрыта");
        }
    }

    private final Object alive = new Object();

    public void waitDocuments() {
        waitDocuments(90000);
    }
    @Step
    public void waitDocuments(int value)
    {
        try {
            Thread.sleep(value);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }
}
