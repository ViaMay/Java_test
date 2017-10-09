package core.systemControls;


import com.sun.jna.platform.win32.Guid;
import core.webDriver.WebDriverCache;
import org.testng.Assert;

public final class PageLoadCounter
{
    public static void Reset()
    {
        lastPageLoadCounter = 0;
        cookieName = "pageLoadCounterValue_" + new Guid.GUID().toGuidString();
    }

    public static void AssertPageNotLoaded()
    {
        long expectedPageLoadCounter = lastPageLoadCounter;
//        Update();
        Assert.assertEquals(expectedPageLoadCounter, lastPageLoadCounter, "Был выполнен неожиданный переход на страницу");
    }

    private static void Update()
    {
        Long tempRef_lastPageLoadCounter = lastPageLoadCounter;
        lastPageLoadCounter = tempRef_lastPageLoadCounter + Long.parseLong(WebDriverCache.getWebDriver().FindCookie(cookieName));
    }

    public static void InitPageLoadCounterCookie()
    {
        if(WebDriverCache.getWebDriver().FindCookie(cookieName) == null)
            WebDriverCache.getWebDriver().SetCookie(cookieName, "1");
    }
    public static void WaitPageLoaded(int pageLoads)
    {
        WaitPageLoaded(pageLoads, null);
    }

    public static void WaitPageLoaded()
    {
        WaitPageLoaded(1, null);
    }

//    public static void WaitPageLoaded()
//    {
//        WaitPageLoaded(1);
//    }

//     public static void WaitPageLoaded(int pageLoads)
//    {
//        CheckServerException();
//    }


    public static void WaitPageLoaded(int pageLoads, Integer timeout)
    {
        long pageLoadCounter = lastPageLoadCounter + pageLoads;
        Waiter.Wait(() -> CheckPageLoaded(pageLoadCounter), "Ожидание завершения перехода на страницу", (timeout != null) ? timeout : 30000);
        CheckServerException();
        Assert.assertEquals(pageLoadCounter, lastPageLoadCounter, "Был выполнен неожиданный переход на страницу");
    }

    private static boolean CheckPageLoaded(long pageLoadCounter)
    {
        Update();
        if (lastPageLoadCounter >= pageLoadCounter)
        {
            return true;
        }
        CheckServerException();
        return false;
    }

    private static void CheckServerException()
    {
        StaticText errorNumber = new StaticText("err-info");
        if (errorNumber.getIsPresent())
        {
            errorNumber.sendKeysToBody("h");
            String exceptionText = (new StaticText("stackTrace")).getText();
            if (exceptionText.isEmpty())
            {
                throw new RuntimeException("Внутренняя ошибка сервера:" + System.lineSeparator() + exceptionText);
            }
        }
    }

    private static long lastPageLoadCounter;
    private static String cookieName;
}


