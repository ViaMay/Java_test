package core.systemPages;

import core.webDriver.WebDriverCache;
import org.testng.*;

import ru.yandex.qatools.allure.annotations.Attachment;

public class ElementScreenshot extends TestListenerAdapter {

    @Override
    public void onTestFailure(ITestResult tr){
        saveImageAttach();
    }

    @Attachment(type = "image/png")
    public byte[] saveImageAttach() {
        return WebDriverCache.getWebDriver().CaptureScreenshot();
    }
}
