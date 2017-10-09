//package tests;
//
//import com.gargoylesoftware.htmlunit.ElementNotFoundException;
//import com.sun.jna.platform.unix.X11;
//import org.apache.http.util.Asserts;
//import org.junit.Test;
//import org.openqa.selenium.*;
//import org.openqa.selenium.chrome.ChromeDriver;
//import org.openqa.selenium.chrome.ChromeOptions;
//import org.openqa.selenium.firefox.FirefoxDriver;
//import org.openqa.selenium.remote.DesiredCapabilities;
//import org.slf4j.Logger;
//import org.slf4j.LoggerFactory;
//import org.testng.Assert;
//
//import java.io.File;
//import java.io.IOException;
//
//public class InterviewClass  {
//// имя класса InterviewClass с большой буквы в InterviewClass.class
//    private static final Logger LOGGER = LoggerFactory.getLogger(InterviewClass.class);
//    final boolean myCondition = false;
//
//    @Test
//    public void MainTest() {
////        WebDriver driver = new FirefoxDriver();
//		DesiredCapabilities capabilities = DesiredCapabilities.chrome();
//		String assembliesDirectory = FindAssembliesDirectory();
//		ChromeOptions chromeOptions = new ChromeOptions();
//		String OsType = System.getProperty("os.name" );
//		if (OsType.startsWith("Windows"))
//		{
//			String directory = combine(new String[]{assembliesDirectory, "//Selenium", "//chromedriver.exe"});
//			System.setProperty("webdriver.chrome.driver",directory);
//			String chromePath = combine(new String[]{assembliesDirectory, "//Chrome", "//chrome.exe"});
//			chromeOptions.setBinary(chromePath);
//
//			chromeOptions.addArguments("start-maximized");
//			chromeOptions.addArguments("--disable-webdriver-enable-native-events");
//			chromeOptions.addArguments("--disable-download.prompt-for-download");
//			chromeOptions.addArguments("--download.default-directory=", core.webDriver.WebDriverDownloadDirectory.DirectoryPath);
//			chromeOptions.addArguments("--enable-download.directory_upgrade");
//			chromeOptions.addArguments("--profile-content-settings.pattern_pairs=http://*,*");
//			capabilities.setCapability(ChromeOptions.CAPABILITY, chromeOptions);
//		}
//
//        WebDriver  driver = new ChromeDriver(chromeOptions);
//        driver.get("http://www.google.com");
//        WebElement element = null;
//        int i = 0;
//        if (myCondition)
//        {
//            throw new RuntimeException("The condition is true!");
//        }
//        try {
//            try {
////                xpath не корректный либо
////                element = driver.findElements(By.xpath("//tr/td/div[2]/span/div[@id='myElementId']")).get(1);
////                либо
////                element = driver.findElements(By.xpath("//tr/td/div[2]/span/div[3]")).get(1);
//                element = driver.findElements(By.xpath("//tr/td/div[2]/span/div[3][@id='myElementId']")).get(1);
//            }
//            catch (NullPointerException e) {
//                i++;
//                e.printStackTrace();
//            }
//            if (element == null) {
////                перенести слип после if, все равно ничего не изменится, вынести слип в отдельный метод
////                типа
////                        Waiter()
////                {
////                    try {
////                Thread.sleep(3000000);
////            }
////            catch (InterruptedException e1) {
////                e1.printStackTrace();
////            }
////                }
//                Thread.sleep(3000000);
//                try {
//                    element = driver.findElement(By.xpath("//tr/td/div[2]/span/div[3][@id='myElementId']"));
//                } catch (ElementNotFoundException e) {
//                    i++;
//                    e.printStackTrace();
//                }
//            }
//        }
//        catch (Exception e) {
////            бесполезный кусок
////            try {
////                Thread.sleep(3000000);
////            }
////            catch (InterruptedException e1) {
////                e1.printStackTrace();
////            }
//            try {
////                слип сюда и вынести в отдельный класс его вообще
//                Thread.sleep(3000000);
////                By.cssSelector не правильно описан.
////                element = driver.findElement(By.cssSelector("li > td > div:nth-child(3) > span > div:nth-child(3)")); или
////                element = driver.findElement(By.cssSelector("li > td > div:nth-child(3) > span > #\\30 myElementId"));
//                element = driver.findElement(By.cssSelector("//tr/td/div[2]/span/div[3][@id='myElementId']"));
//            } catch (ElementNotFoundException ex) {
//                i++;
//            } catch (InterruptedException e1) {
//                e1.printStackTrace();
//            }
//        }
////надо Assert вместо AssertS
//		Assert.assertTrue(check_Element(element, "My ELEMENT text !"));
//		Assert.assertTrue(CheckElementIsDisplayed(element));
//        checkAssignmentExitWarningMessageIsShown(false);
//        element.click();
//        checkAssignmentExitWarningMessageIsShown(true);
//    }
//
//
//
//    /**
//     	     * Checks that element is displayed
//     	     */
//    private Boolean CheckElementIsDisplayed(WebElement element) {
//        if (element != null) {
//                return true;
//        } else if (element.isDisplayed()) {
//            return true;
//        }
//        return false;
//    }
//
//    	    /**
//    	     * Checks that given element is present and that the text is equal to given text
//    	     *
//    	     * @param element
//    	     * @param ExpectedText
//    	     * @return true is element is present and the text equals to expected value. Otherwise - false
//    	     */
//            	    public boolean check_Element(WebElement element, String ExpectedText) {
//       	        Boolean result = false;
//        	        inValidAmount(ExpectedText);
//        	        if (element.isDisplayed()) {
//        	            String string12 = element.getText();
//        	            try {
//        	                if (string12.equalsIgnoreCase(ExpectedText)) {
//        	                    LOGGER.info("PASS");
//        	                }
//        	            } catch (Exception e) {
//        	                e.printStackTrace();
//        	            }
//        	        } else {
//        	            LOGGER.info("FAIL");
//        	        }
//
//        	        LOGGER.info("PASS");
//        	        return result;
//        	    }
//
//            	static boolean inValidAmount(String amount) {
//        	    boolean status = false;
//        	    if (amount.startsWith("0")) {
//        	      status = false;
//        	    }
//        	    else {
//        	      status = Integer.parseInt(amount) > 0 ? true : false;
//        	    }
//        	    return status;
//        	  }
//
////            	    @Report("Check whether the assignment exit warning is displayed")
////    либо передовать веб драйвер как параметр либо сделать эту проверку медом веб драйвера
////    не надо возвращить InterviewClass. Надо возвращать void
//    	    public void checkAssignmentExitWarningMessageIsShown(boolean isWarningMessageDisplayed, WebDriver driver) {
//    	        try {
//    	            Alert alert = driver.switchTo().alert();
//            	            String alertText = alert.getText();
//            	            assertTrue(isWarningMessageDisplayed ? alertText != null : alertText == null, "Assignment Exit Alert Message is Shown: " + isWarningMessageDisplayed);
//            	        } catch (NoAlertPresentException ignored) {
//            	        }
//       	        return this;
//       	    }
//
////    зачем этот метод отдельный?
//    protected void assertTrue(boolean condition, String errorMessage) {
//        try {
//            Assert.assertTrue(condition, errorMessage);
//        } catch (AssertionError e) {
//            fail(e.getMessage());
//        }
//    }
//
//    public static String combine(String[] args) {
//        String result = "";
//        for (int i=0; i<args.length; i++ )
//        {
//            result = result.concat(args[i]);
//        }
//        return result;
//    }
//    private String FindAssembliesDirectory()
//    {
////        java.io.File currentDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory);
//        java.io.File currentDirectory = new File(".").getAbsoluteFile().getParentFile();
//        while (true)
//        {
//            if (currentDirectory == null)
//            {
//                throw new RuntimeException("The folder Assemblies not found");
//            }
//            java.io.File[] directories = currentDirectory.listFiles();
//            for (java.io.File directoryInfo : directories)
//            {
//                if (directoryInfo.getName().equals("Assemblies") || directoryInfo.getName().equals("assemblies"))
//                {
//                    return directoryInfo.getPath();
//                }
//            }
//            currentDirectory = currentDirectory.getParentFile();
//        }
//    }
//   }
