package core.webDriver;
import com.google.common.base.Stopwatch;
import com.sun.jna.platform.unix.X11;
import org.openqa.selenium.*;
import org.openqa.selenium.chrome.ChromeDriver;
import org.openqa.selenium.chrome.ChromeOptions;
import org.openqa.selenium.remote.DesiredCapabilities;
import org.openqa.selenium.remote.RemoteWebDriver;
import org.openqa.selenium.support.ui.ExpectedConditions;
import org.openqa.selenium.support.ui.WebDriverWait;

import java.io.*;
import java.net.MalformedURLException;
import java.net.URL;
import java.util.concurrent.TimeUnit;

public class WebDriverWrapper
{
    private WebDriver driver;
    private X11.Display display;

    public WebDriverWrapper()
    {
        DesiredCapabilities capabilities = DesiredCapabilities.chrome();
        String assembliesDirectory = FindAssembliesDirectory();
        ChromeOptions chromeOptions = new ChromeOptions();
        String OsType = System.getProperty("os.name" );
        if (OsType.startsWith("Windows"))
        {
            String directory = combine(new String[]{assembliesDirectory, "//Selenium", "//chromedriver.exe"});
            System.setProperty("webdriver.chrome.driver",directory);
            String chromePath = combine(new String[]{assembliesDirectory, "//Chrome", "//chrome.exe"});
            chromeOptions.setBinary(chromePath);

            chromeOptions.addArguments("start-maximized");
            chromeOptions.addArguments("--disable-webdriver-enable-native-events");
            chromeOptions.addArguments("--disable-download.prompt-for-download");
            chromeOptions.addArguments("--download.default-directory=", WebDriverDownloadDirectory.DirectoryPath);
            chromeOptions.addArguments("--enable-download.directory_upgrade");
            chromeOptions.addArguments("--profile-content-settings.pattern_pairs=http://*,*");
            capabilities.setCapability(ChromeOptions.CAPABILITY, chromeOptions);
        }
        else  if (OsType.startsWith("Linux")) {

                final X11 x11 = X11.INSTANCE;
                X11.Display display = x11.XOpenDisplay(null);
                X11.Window window = new X11.Window();

//            String directory = combine(new String[]{assembliesDirectory, "//Selenium", "//usr//bin//google-chrome"});
//            try {
//                Runtime.getRuntime().exec("chmod +x"+ directory);
//            } catch (IOException e) {
//                e.printStackTrace();
//            }
//            System.setProperty("webdriver.chrome.driver", directory);
            System.setProperty("webdriver.chrome.driver", "//usr//local//sbin//chromedriver");

//            String chromePath = combine(new String[]{assembliesDirectory, "//Chrome", "//usr//bin//google-chrome"});
//            try {
//                Runtime.getRuntime().exec("chmod +x"+ chromePath);
//            } catch (IOException e) {
//                e.printStackTrace();
//            }
//            chromeOptions.setBinary(chromePath);

            try {
                Runtime.getRuntime().exec("Xvfb :0 -ac -screen 0 1024x768x24 &");
            } catch (IOException e) {
                e.printStackTrace();
            }
            chromeOptions.setBinary("//usr//bin//chromium");
            chromeOptions.addArguments("--privileged");
            chromeOptions.addArguments("--no-sandbox");
            String chromePath = combine(new String[]{assembliesDirectory, "//Chrome", "//google-chrome-stable_current_x86_64.rpm"});
            chromeOptions.setBinary(chromePath);
        }
//        driver = new ChromeDriver(chromeOptions);

        String USERNAME = "andreiarnautov1";
        String AUTOMATE_KEY = "yiFysr9R61GJTRg1TDzt";
        String URL = "https://" + USERNAME + ":" + AUTOMATE_KEY + "@hub-cloud.browserstack.com/wd/hub";
        DesiredCapabilities caps = new DesiredCapabilities();
        caps.setCapability("browser", "IE");
        caps.setCapability("browser_version", "7.0");
        caps.setCapability("os", "Windows");
        caps.setCapability("os_version", "XP");
        caps.setCapability("browserstack.debug", "true");

        try {
            driver = new RemoteWebDriver(new URL(URL), caps);
        } catch (MalformedURLException e) {
            e.printStackTrace();
        }

    }

    public static String combine(String[] args) {
        String result = "";
        for (int i=0; i<args.length; i++ )
        {
            result = result.concat(args[i]);
        }
        return result;
    }

    public final String getUrl()
    {
        return driver.getCurrentUrl();
    }

    public final void GoToUri(URL uri)
    {
        driver.navigate().to(uri);
    }

    public final void Refresh()
    {
        driver.navigate().refresh();
    }

    public final SearchContext GetSearchContext()
    {
        return driver;
    }

    public final void DeleteAllCookies()
    {
        try
        {
            driver.manage().deleteAllCookies();
        }
        catch (UnhandledAlertException e)
        {
            Alert alert = driver.switchTo().alert();
            alert.dismiss();
            driver.manage().deleteAllCookies();
        }

    }

    public final void SetCookie(String cookieName, String cookieValue)
    {
        driver.manage().addCookie(new Cookie(cookieName, cookieValue, "/"));
    }

        public String FindCookie(String cookieName)
        {
            Cookie cookie = driver.manage().getCookieNamed(cookieName);
            if (cookie == null)
                return null;
            return cookie.getValue();
        }

    public static final class DotNetToJavaStringHelper
    {
        public static String substring(String string, int start, int length)
        {
            if (length < 0)
                throw new IndexOutOfBoundsException("Parameter length cannot be negative.");

            return string.substring(start, start + length);
        }
    }

    public final Object ExecuteScript(String script)
    {
        JavascriptExecutor js = (JavascriptExecutor) driver;
        return js.executeScript(script);
    }

    public final Object javascriptExecutor(String script, WebElement webElement)
    {
        JavascriptExecutor jse = (JavascriptExecutor) driver;
        return jse.executeScript(script, webElement);
    }
    public final void WaitForAjax()
    {
        while (true) // Handle timeout somewhere
        {
            JavascriptExecutor js = (JavascriptExecutor) driver;
            boolean ajaxIsComplete = (boolean) js.executeScript("return jQuery.active == 0");
            if (ajaxIsComplete)
            {
                break;
            }
        }
        while (true) // Handle timeout somewhere
        {
            JavascriptExecutor js = (JavascriptExecutor) driver;
            boolean ajaxIsComplete = (boolean) js.executeScript("return typeof(Ajax) != 'function' || Ajax.activeRequestCount == 0;");
            if (ajaxIsComplete)
            {
                break;
            }
        }
        while (true) // Handle timeout somewhere
        {JavascriptExecutor js = (JavascriptExecutor) driver;
            boolean ajaxIsComplete = (boolean) js.executeScript("return typeof(dojo) != 'function' ||  dojo.io.XMLHTTPTransport.inFlight.length == 0;");
            if (ajaxIsComplete)
            {
                break;
            }
        }
    }

    public final Alert Alert(int timeout) {
        return Alert(timeout, 100);
    }

    public final Alert Alert() {
        return Alert(6000, 100);
    }

    public final Alert Alert(int timeout, int waitTimeout) {
        Stopwatch w = Stopwatch.createStarted();
        do
        {
            try
            {
                return driver.switchTo().alert();
            }
            catch (RuntimeException e)
            {
                try {
                    Thread.sleep(waitTimeout);
                } catch (InterruptedException e1) {
                    e1.printStackTrace();
                }
            }
        } while (w.elapsed(TimeUnit.NANOSECONDS) < timeout);
        return driver.switchTo().alert();
    }
    public final WebDriver switchToFrame(By locator)
    {
                return (new WebDriverWait(driver, 10))
            .until(ExpectedConditions.frameToBeAvailableAndSwitchToIt(locator));
//        return driver.switchTo().frame(driver.findElement(By.tagName("iframe")));
    }

    public final WebDriver switchToDefaultContent()
    {
        return driver.switchTo().defaultContent();
    }

    public final void Quit()
    {
        try
        {
            try
            {
                driver.close();
            }
            finally
            {
                driver.quit();
            }
        }
        catch (RuntimeException exception)
        {
            System.out.printf("Ошибка при остановке ChromeDriver:\r\n%1$s" + "\r\n", exception);
        }
    }



    public final void CleanDownloadDirectory()
    {
        WebDriverDownloadDirectory.Clean();
    }

    public final String[] GetDownloadedFileNames()
    {
        return WebDriverDownloadDirectory.GetDownloadedFileNames();
    }

    public final void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime) throws InterruptedException {
        WaitDownloadFiles(expectedCountFiles, maximalWaitTime, 100);
    }

    public final void WaitDownloadFiles(int expectedCountFiles) throws InterruptedException {
        WaitDownloadFiles(expectedCountFiles, 15000, 100);
    }

    public final void WaitDownloadFiles(int expectedCountFiles, int maximalWaitTime, int sleepInterval) throws InterruptedException {
        WebDriverDownloadDirectory.WaitDownloadFiles(expectedCountFiles, maximalWaitTime, sleepInterval);
    }

//    private static void SetChromeVersionToRegistry(String chromePath)
//    {
//        String chromeVersion = fileVersionInfo.GetVersionInfo(chromePath).ProductVersion;
//        RegistryKey key = Registry.currentUser.CreateSubKey("Software\\Google\\Update\\Clients\\{8A69D345-D564-463c-AFF1-A69D9E530F96}");
//        key.setValue("pv", chromeVersion);
//        key.close();
//    }

    private String FindAssembliesDirectory()
    {
//        java.io.File currentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
        java.io.File currentDirectory = new File(".").getAbsoluteFile().getParentFile();
        while (true)
        {
            if (currentDirectory == null)
            {
                throw new RuntimeException("The folder Assemblies not found");
            }
            java.io.File[] directories = currentDirectory.listFiles();
            for (java.io.File directoryInfo : directories)
            {
                if (directoryInfo.getName().equals("Assemblies") || directoryInfo.getName().equals("assemblies"))
                {
                    return directoryInfo.getPath();
                }
            }
            currentDirectory = currentDirectory.getParentFile();
        }
    }

    public byte[] CaptureScreenshot()
    {
        return ((TakesScreenshot) driver).getScreenshotAs(OutputType.BYTES);
//        Random random = new Random();
//        int num = random.nextInt();
//        try {
//            WebDriver returned = new Augmenter().augment(driver);
//            if (returned != null) {
//                File f = ((TakesScreenshot) returned)
//                        .getScreenshotAs(OutputType.FILE);
//                try {
//                    FileUtils.copyFile(f, new File("target\\allure-results" + name + ".jpg"));
//                } catch (IOException e) {
//                    e.printStackTrace();
//                }
//            }
//        } catch (ScreenshotException se) {
//            se.printStackTrace();
//        }
    }
}


