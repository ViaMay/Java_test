package core.systemPages;
import core.webDriver.WebDriverCache;
import core.systemControls.PageLoadCounter;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import java.net.MalformedURLException;
import java.net.URL;

public abstract class WebDriverFunctionalTestBase
{
    @BeforeClass
    public void SetUp()
    {
        WebDriverCache.RestartIfNeed();
        WebDriverCache.getWebDriver().DeleteAllCookies();
        WebDriverCache.getWebDriver().CleanDownloadDirectory();
        PageLoadCounter.Reset();
    }

    @AfterClass
    public void TearDown()
    {
        try
        {
            if (WebDriverCache.getInitialized())
            {
                TearDownInternal();
            }
        }
        finally
        {
            WebDriverCache.getWebDriver().DeleteAllCookies();
            WebDriverCache.getWebDriver().ExecuteScript("localStorage.clear();");
            WebDriverCache.DestroyInstance();
        }
    }

    private void TearDownInternal()
    {
        try
        {
            PageLoadCounter.AssertPageNotLoaded();
        }
        finally
        {
        }
    }

    public final <TPage extends PageBase> TPage LoadPage(Class<TPage> pageClass, String localPath)  {
        URL g = null;
        try {
            g = new URL(new URL(String.format("http://%1$s/", getApplicationBaseUrl())), localPath);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
        return PageBase.GoToUri(pageClass,g);
    }

    public final <TPage extends PageBase> TPage LoadPageUrl(Class<TPage> pageClass, String localPath)  {
        try {
            return PageBase.<TPage>GoToUri(pageClass,new URL(localPath));
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }
        return null;
    }

    public String getApplicationBaseUrl()
    {
        return "stage.ddelivery.ru";
    }

    private static void CaptureJavascriptErrors()
    {
        Object tempVar = WebDriverCache.getWebDriver().ExecuteScript("return window.jsErrors");
        String errors = (String)((tempVar instanceof String) ? tempVar : null);
    }
}

