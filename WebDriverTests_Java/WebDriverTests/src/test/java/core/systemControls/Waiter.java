package core.systemControls;

import core.supportClass.StopWatch;
import org.junit.Assert;
import org.openqa.selenium.ElementNotVisibleException;
import org.openqa.selenium.WebDriverException;

public final class Waiter
{
    private static final int waitTimeout = 100;
    private static final int defaultTimeout = 30000;
    private static final int firstTestTimeoutFactor = 3;
    private static String firstTestName;


    public static void Wait(Function<Boolean> tryFunc, String actionDescription) {
        Wait(tryFunc, actionDescription, null);
    }

    public static void Wait(Function<Boolean> tryFunc, String actionDescription, Integer timeout) {
        if (timeout==null) timeout=defaultTimeout;
        StopWatch w = new StopWatch();
        w.start();
        do
        {
            if (tryFunc.invoke()) {
                return;
            }
            try {
                Thread.sleep(waitTimeout);
            } catch (InterruptedException e) {
                e.printStackTrace();
            }
        } while (w.getElapsedTimeSecs()*1000 < GetActualTimeout(timeout));
        w.stop();
        Assert.fail(String.format("действие '%1$s' не выполнилось за '%2$s' мс", actionDescription, GetActualTimeout(timeout)));
    }



    @FunctionalInterface
    public interface Function<Boolean>
    {
        Boolean invoke();
    }
    public Boolean invoke()
    {
        return invoke();
    }

    @FunctionalInterface
    public interface Action
    {
        void invokeVoid();
    }

    public void invokeVoid()
    {
        invokeVoid();
    }



    private static int GetActualTimeout(Integer timeout)
    {
        return (timeout != null) ? timeout : defaultTimeout;
    }

    public static void WaitExeption(Action tryFunc    ) {
        WaitExeption(tryFunc, "WaitExeption", null);
    }
    public static void WaitExeption(Action tryFunc, String actionDescription) {
        WaitExeption(tryFunc, actionDescription, null);
    }

    public static void WaitExeption(Action tryFunc, String actionDescription, Integer timeout) {
        if (timeout==null) timeout=defaultTimeout;
        StopWatch w = new StopWatch();
        w.start();
        do
        {
            try{
                tryFunc.invokeVoid();
                return;
            }
            catch (ElementNotVisibleException e)
            {
                try {
                    Thread.sleep(100);
                } catch (InterruptedException e3) {
                    e3.printStackTrace();
                }
            }
            catch (WebDriverException e)
            {
                try {
                    Thread.sleep(100);
                } catch (InterruptedException e1) {
                    e1.printStackTrace();
                }
            }
        } while (w.getElapsedTimeSecs()*1000 < GetActualTimeout(timeout));
        w.stop();
        Assert.fail(String.format("действие '%1$s' не выполнилось за '%2$s' мс", actionDescription, GetActualTimeout(timeout)));
    }
}