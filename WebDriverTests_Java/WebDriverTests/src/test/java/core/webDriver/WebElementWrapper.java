package core.webDriver;
import org.openqa.selenium.*;
import org.openqa.selenium.interactions.Actions;

import java.util.List;

public class WebElementWrapper implements WebElement {
    private WebElement nativeWebElement;
    private String description;
    private By locator;
    private SearchContext searchContext;

    public WebElementWrapper(SearchContext searchContext, By locator, String description) {
        this.searchContext = searchContext;
        this.locator = locator;
        this.description = description;
    }

    public final WebElement findElement(By by)
    {
        return Execute(() ->
        {
            List<WebElement> element = findNativeWebElement().findElements(by);
            WebElement result = element.get(0);
//            if (resulty == null) {
//                throw new NoSuchElementException(by.toString());
//            }
            return result;
        });
    }
    public final List<WebElement> findElements(By by)
    {
        return Execute(() -> findNativeWebElement().findElements(by));
    }

    @FunctionalInterface
    public interface Function<WebElement>
    {
        WebElement invoke();
    }
    public WebElement invoke()
    {
        return invoke();
    }
    private <T> T Execute(Function<T> func)
    {
        for (int i = 5; i >= 0; i--)
        {
            try
            {
                return func.invoke();
            } catch (ElementNotVisibleException e)
            {
                if (i == 0)
                {
                    throw new ElementNotVisibleException(String.format("Элемент '%1$s' невидимый", description), e);
                }
                TryFixPage();
            } catch (InvalidElementStateException e)
            {
                ClearCache();
            }
            catch (StaleElementReferenceException e2)
            {
                ClearCache();
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException(String.format("Не найден элемент '%1$s'", description), e);
            }
            catch (IllegalStateException exception)
            {
                if (exception.getMessage().indexOf("Element is not clickable at point") == -1)
                {
                    throw exception;
                }
                TryFixPage();
            }
        }
        return func.invoke();
    }
public void clear()
{
    Execute(() ->
            {
      findNativeWebElement().clear();
    return 0;
    });
}

    public void sendKeys(String text)
    {
        Execute(() ->
                {
        findNativeWebElement().sendKeys(text);
        return 0;
        });
    }

    public void submit()
    {
        Execute(() ->
                {
          findNativeWebElement().submit();
        return 0;
        });
    }

    public void sendKeys(CharSequence... charSequences) {
        findNativeWebElement().sendKeys(charSequences);
    }


    public void click()
    {
        Execute(() ->
                {
         findNativeWebElement().click();
        return 0;
        });
    }

    public String getAttribute(String attributeName)
    {
        return Execute(() -> findNativeWebElement().getAttribute(attributeName));
    }

    public String getCssValue(String propertyName)
    {
        return Execute(() -> findNativeWebElement().getCssValue(propertyName));
    }


    public String getTagName() {
        return findNativeWebElement().getTagName();
    }

    public String getText() {
        return findNativeWebElement().getText();
    }

    public boolean isEnabled() {
        return findNativeWebElement().isEnabled();
    }

    public boolean isSelected() {
        return findNativeWebElement().isSelected();
    }

    public Point getLocation() {
        return findNativeWebElement().getLocation();
    }

    public Dimension getSize() {
        return findNativeWebElement().getSize();
    }

    public boolean isDisplayed() {
        return findNativeWebElement().isDisplayed();
    }

    public <X> X getScreenshotAs(OutputType<X> outputType) throws WebDriverException {
        return findNativeWebElement().getScreenshotAs(outputType);
    }

    public void mouseover()
    {
        Execute(() ->
                {
        Actions actions = new Actions((WebDriver) GetRootSearchContext());
        actions.moveToElement(findNativeWebElement()).perform();
        return 0;
        });
    }

    public void sendKeysToBody(String text)
    {
        GetRootSearchContext().findElement(By.tagName("body")).sendKeys(text);
    }

    private SearchContext GetRootSearchContext()
    {
        WebElementWrapper wrapper = (WebElementWrapper)((searchContext instanceof WebElementWrapper) ? searchContext : null);
        if (wrapper != null)
            return wrapper.GetRootSearchContext();
        return searchContext;
    }
    public void ScrollDown()
    {
        WebDriverCache.getWebDriver().javascriptExecutor("arguments[0].scrollIntoView();", findNativeWebElement());
    }

    private void BlurCurrentActiveElement()
    {
        WebDriverCache.getWebDriver().ExecuteScript(
                "if (document.activeElement != null) {document.activeElement.blur();}");
    }

    private void TryFixPage()
    {
        ScrollDown();
        BlurCurrentActiveElement();
    }

    private void ClearCache()
    {
        nativeWebElement = null;
    }

    public final WebElement findNativeWebElement()
    {
        return (nativeWebElement != null) ? nativeWebElement : (nativeWebElement = FindNativeWebElementInternal());
    }
    private WebElement FindNativeWebElementInternal()
    {
        SearchContext context = searchContext;
        WebElementWrapper wrapper = (WebElementWrapper)((context instanceof WebElementWrapper) ? context : null);
        if (wrapper != null)
        {
            context = wrapper.FindNativeWebElementInternal();
        }
        return context.findElement(locator);
    }
}
