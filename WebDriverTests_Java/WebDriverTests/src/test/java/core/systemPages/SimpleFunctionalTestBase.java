package core.systemPages;


import core.pages.LoginPage;
import core.webDriver.WebDriverCache;
import org.testng.annotations.AfterClass;
import org.testng.annotations.BeforeClass;
import org.testng.annotations.Listeners;

@Listeners({ElementScreenshot.class})
public class SimpleFunctionalTestBase extends WebDriverFunctionalTestBase
{
    @BeforeClass
    @Override
    public void SetUp() {
        super.SetUp();
        LoadPage(LoginPage.class, "user2");
    }

    @AfterClass
    @Override
    public void TearDown()  {
            super.TearDown();
    }

    protected final void ResetDownloadFilesState()
    {
        WebDriverCache.getWebDriver().CleanDownloadDirectory();
    }

    public final void WaitDocuments()  {
        WaitDocuments(90000);
    }

    public final void WaitDocuments(int value) {
        try {
            Thread.sleep(value);
        } catch (InterruptedException e) {
            e.printStackTrace();
        }
    }

    private core.pages.DefaultPage DefaultPage;
    protected final core.pages.DefaultPage getDefaultPage()
    {
        return DefaultPage;
    }
    private void setDefaultPage(core.pages.DefaultPage value)
    {
        DefaultPage = value;
    }
}
