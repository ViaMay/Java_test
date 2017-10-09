package core.systemControls;

import core.webDriver.WebDriverCache;
import core.webDriver.WebElementWrapper;
import core.supportClass.RefObject;
import org.openqa.selenium.By;
import org.openqa.selenium.SearchContext;
import org.openqa.selenium.WebElement;
import org.testng.Assert;
import ru.yandex.qatools.allure.annotations.Step;

public abstract class HtmlControl {
    private String controlDescription;
    public WebElementWrapper element;
    public By locator;
    public SearchContext searchContext;


    protected HtmlControl(By locator) {
        this(locator, null);
    }


    protected HtmlControl(By locator, HtmlControl container) {
        setContainer(container);
        setLocator(locator);
        Container = getContainer();
        this.locator = getLocator();
        controlDescription = FormatControlDescription(locator.toString(), container);
        searchContext = getContainer().getSearchContext();
        element = new WebElementWrapper(searchContext, locator, controlDescription);
    }

    private HtmlControl Container;
    public final HtmlControl getContainer()
    {
        return Container;
    }
    private void setContainer(HtmlControl value)
    {
        Container = value;
    }

    private By Locator;
    public final By getLocator()
    {
        return Locator;
    }
    private void setLocator(By value)
    {
        Locator = value;
    }
    protected HtmlControl(String idLocator) {
        this(idLocator, null);
    }

    protected HtmlControl(String idLocator, HtmlControl container) {
        this(By.id(idLocator), container);
    }

    public final String FormatControlDescription(String locatorString, HtmlControl container)
    {
        String description = String.format("%1$s (%2$s)", this.getClass().getSimpleName(), locatorString);
        if (container == null)
        {
            return description;
        }
        return String.format("%1$s в контексте элемента %2$s", description, container.controlDescription);
    }
    public static SearchContext getSearchContext(HtmlControl container)
    {
        return container != null ? container.element : WebDriverCache.getWebDriver().GetSearchContext();
    }
    public static SearchContext getSearchContext()
    {
        return WebDriverCache.getWebDriver().GetSearchContext();
    }

    public final void switchToFrame()
    {
        WebDriverCache.getWebDriver().switchToFrame(locator);
    }

    public final void javascriptExecutorClick()
    {
        WebElement button = searchContext.findElement(locator);
        WebDriverCache.getWebDriver().javascriptExecutor("arguments[0].click();", button);
    }

    public final Object javascriptExecuteWebElement(String script)
    {
        WebElement webElement = searchContext.findElement(locator);
        return WebDriverCache.getWebDriver().javascriptExecutor(script, webElement);
    }

    public final Object javascriptExecute(String script)
    {
        return WebDriverCache.getWebDriver().ExecuteScript(script);
    }

    public final void switchToDefaultContent()
    {
        WebDriverCache.getWebDriver().switchToDefaultContent();
    }

    public boolean getIsEnabled()
    {
        return !hasClass("disabled");
    }

    public final boolean getIsPresent()
    {
        return (getContainer() == null || getContainer().getIsPresent()) && searchContext.findElements(locator).size() > 0;
    }

    public boolean getIsVisible()
    {
        return element.isDisplayed();
    }

    public final SearchContext searchContext()
    {
        return searchContext;
    }
    public final void mouseover()
    {
        element.mouseover();
    }

    public final void sendKeysToBody(String text)
    {
        element.sendKeysToBody(text);
    }

    public final void sendKeys(String text)
    {
        element.sendKeys(text);
    }

    public String getText()
    {
        return element.getText().trim();
    }

    public String getTagName()
    {
        return element.getTagName().trim();
    }

    public String getValue()
    {
        return element.getAttribute("value");
    }

    public void AssertNessesary() throws InterruptedException {
        WaitClassPresenceWithRetries("field__incorrect");
        WaitAttributeValue("title", "Поле должно быть заполнено");
    }

    public final boolean hasClass(String className)
    {
        String actualClasses = null;
        RefObject<String> tempRef_actualClasses = new RefObject<String>(actualClasses);
        boolean tempVar = hasClass(className, tempRef_actualClasses);
        actualClasses = tempRef_actualClasses.argValue;
        return tempVar;
    }

    private boolean hasClass(String className, RefObject<String> actualClasses) {
        actualClasses.argValue = getAttributeValue("class");
        String[] actualClassesArray = actualClasses.argValue.split(String.valueOf(new String[]{" ", "\r", "\n", "\t"}), -1);
        String[] expectedClassesArray = className.split(String.valueOf(new String[] {" ", "\r", "\n", "\t"}), -1);
return true;
//        return expectedClassesArray.All(actualClassesArray.contains);
    }

    public String getAttributeValue(String attributeName)
    {
        return element.getAttribute(attributeName);
    }

    public String formatWithLocator(String text)
    {
        return String.format("'%1$s' '%2$s'", text, controlDescription);
    }

    @Step("Ждем видимости")
    public final void WaitVisible()
    {
        String description = formatWithLocator("Ожидание видимости элемента");
        Assert.assertEquals(getIsVisible(), true, description);
    }
    @Step("Ждем видимости")
    public final void WaitVisibleWithRetries()
    {
        WaitVisibleWithRetries(null);
    }
    @Step("Ожидание видимости элемента")
    public final void WaitVisibleWithRetries(Integer timeout)
    {
        String description = formatWithLocator("Ожидание видимости элемента");
        Waiter.Wait(()-> getIsVisible(), description, timeout);
    }
    @Step("Ожидание не видимости элемента")
    public final void WaitInvisible()
    {
        String description = formatWithLocator("Ожидание невидимости элемента");
        Assert.assertTrue(getIsVisible(), description);
    }

    @Step("Ожидание не видимости элемента")
    public final void WaitInvisibleWithRetries()
    {
        WaitInvisibleWithRetries(null);
    }

    @Step("Ожидание не видимости элемента")
    public final void WaitInvisibleWithRetries(Integer timeout) {
        String description = formatWithLocator("Ожидание невидимости элемента");
        Waiter.Wait(() -> !getIsVisible(), description, timeout);
    }

    @Step("Ожидание присутствие элемента")
    public final void WaitPresence()
    {
        String description = formatWithLocator("Ожидание присутствия элемента");
        Assert.assertTrue(getIsPresent(), description);
    }

    @Step("Ожидание присутствие элемента")
    public final void WaitPresenceWithRetries()
    {
        WaitPresenceWithRetries(null);
    }

    @Step("Ожидание присутствие элемента")
    public final void WaitPresenceWithRetries(Integer timeout)
    {
        String description = formatWithLocator("Ожидание присутствия элемента");
        Waiter.Wait(() -> getIsPresent(), description, timeout);
    }

    @Step("Ожидание отсутствия элемента")
    public final void WaitAbsence()
    {
        String description = formatWithLocator("Ожидание отсутствия элемента");
        Assert.assertFalse(getIsPresent(), description);
    }

    @Step("Ожидание отсутствия элемента")
    public final void WaitAbsenceWithRetries()
    {
        WaitAbsenceWithRetries(null);
    }

    @Step("Ожидание отсутствия элемента")
    public final void WaitAbsenceWithRetries(Integer timeout)
    {
        String description = formatWithLocator("Ожидание отсутствия элемента");
        Waiter.Wait(() -> !getIsPresent(), description, timeout);
    }

    @Step("Ожидание доступности элемента")
    public final void WaitEnabled()
    {
        String description = formatWithLocator("Ожидание доступности элемента");
        Assert.assertTrue(getIsEnabled(), description);
    }

    @Step("Ожидание доступности элемента")
    public final void WaitEnabledWithRetries()
    {
        WaitEnabledWithRetries(null);
    }

    @Step("Ожидание доступности элемента")
    public final void WaitEnabledWithRetries(Integer timeout)
    {
        String description = formatWithLocator("Ожидание доступности элемента");
        Waiter.Wait(() -> getIsEnabled(), description, timeout);
    }

    @Step("Ожидание недоступности элемента")
    public final void WaitDisabled()
    {
        String description = formatWithLocator("Ожидание недоступности элемента");
        Assert.assertFalse(getIsEnabled(), description);
    }

    @Step("Ожидание недоступности элемента")
    public final void WaitDisabledWithRetries()
    {
        WaitDisabledWithRetries(null);
    }

    @Step("Ожидание недоступности элемента")
    public final void WaitDisabledWithRetries(Integer timeout)
    {
        String description = formatWithLocator("Ожидание недоступности элемента");
        Waiter.Wait(() -> !getIsEnabled(), description, timeout);
    }
    @Step("Ожидание текста {0} в элемента")
    public void WaitText(String expectedText)
    {
        String description = formatWithLocator(String.format("Ожидание появления текста '%1$s' в элементе", expectedText));
        Waiter.Wait(() -> getText().equals(expectedText), description);
    }

    @Step("Ожидание value {0} в элементе")
    public final void WaitValue(String value)
    {
        String description = formatWithLocator(String.format("Ожидание value '%1$s' в элементе", value));
        Waiter.Wait(() -> getAttributeValue("value").equals(value), description);
    }

    @Step("Ожидание текста {0} в элементе")
    public final void WaitTextStartsWith(String expectedText)
    {
        String description = formatWithLocator(String.format("Ожидание текста '%1$s' в начале текста элемента", expectedText));
        Waiter.Wait(() -> getText().startsWith(expectedText), description);
    }

    @Step("Ожидание текста {0} в внутри текста элемента")
    public final void WaitTextContains(String expectedText)
    {
        String description = formatWithLocator(String.format("Ожидание текста '%1$s' внутри текста элемента", expectedText));
        Waiter.Wait(() -> getText().contains(expectedText), description);
    }

    @Step("Ожидание текста {0} в внутри текста элемента")
    public final void WaitTextContainsWithRetries(String expectedText)
    {
        WaitTextContainsWithRetries(expectedText, null);
    }

    @Step("Ожидание текста {0} в внутри текста элемента")
    public final void WaitTextContainsWithRetries(String expectedText, Integer timeout)
    {
        String description = formatWithLocator(String.format("Ожидание текста '%1$s' внутри текста элемента", expectedText));
        Waiter.Wait(() -> getText().contains(expectedText), description, timeout);
    }

    @Step("Ожидание класса {0}  у элемента")
    public void WaitClassPresence(String className)
    {
        String description = formatWithLocator(String.format("Ожидание класса '%1$s' у элемента", className));
        String actualClasses = null;
        RefObject<String> tempRef_actualClasses = new RefObject<String>(actualClasses);
        Assert.assertTrue(hasClass(className, tempRef_actualClasses), String.format("'%1$s', актуальный класс: '%2$s'", description, actualClasses));
        actualClasses = tempRef_actualClasses.argValue;
    }

    @Step("Ожидание класса {0} у элемента")
    public void WaitClassPresenceWithRetries(String className)
    {
        WaitClassPresenceWithRetries(className, null);
    }

    @Step("Ожидание класса {0} у элемента")
    public void WaitClassPresenceWithRetries(String className, Integer timeout)
    {
        String description = formatWithLocator(String.format("Ожидание появления класса '%1$s' у элемента", className));
        Waiter.Wait(() -> hasClass(className), description, timeout);
    }

    @Step("Ожидание отсутствия класса {0} у элемента")
    public final void WaitClassAbsence(String className)
    {
        String description = formatWithLocator(String.format("Ожидание отсутствия класса '%1$s' у элемента", className));
        String actualClasses = null;
        RefObject<String> tempRef_actualClasses = new RefObject<String>(actualClasses);
        Assert.assertFalse(hasClass(className, tempRef_actualClasses), String.format("'%1$s', актуальный класс: '%2$s'", description, actualClasses));
        actualClasses = tempRef_actualClasses.argValue;
    }

    @Step("Ожидание отсутствия класса {0} у элемента")
    public final void WaitClassAbsenceWithRetries(String className)
    {
        WaitClassAbsenceWithRetries(className, null);
    }

    @Step("Ожидание отсутствия класса {0} у элемента")
    public final void WaitClassAbsenceWithRetries(String className, Integer timeout) {
        String description = formatWithLocator(String.format("Ожидание отсутствия класса '%1$s' у элемента", className));
        Waiter.Wait(() -> !hasClass(className), description, timeout);
    }

    @Step("Ожидание отсутствия класса {0} у элемента")
    public final void WaitAttributeValue(String attributeName, String expectedText)
    {
        String description = formatWithLocator(String.format("Ожидание аттрибута '%1$s' со значением '%2$s' в элементе", attributeName, expectedText));
        Assert.assertEquals(expectedText, getAttributeValue(attributeName), description);
    }

    @Step("Ожидание отсутствия класса {0} у элемента")
    public final void WaitAttributeValueWithRetries(String attributeName, String expectedText)
    {
        WaitAttributeValueWithRetries(attributeName, expectedText, null);
    }

    public final void WaitAttributeValueWithRetries(String attributeName, String expectedText, Integer timeout)
    {
        String description = formatWithLocator(String.format("Ожидание аттрибута '%1$s' со значением '%2$s' в элементе", attributeName, expectedText));
        Waiter.Wait(() -> expectedText == getAttributeValue(attributeName), description, timeout);
    }

    @Step("Кликнуть на элемент")
    public void click() {
        Waiter.WaitExeption(() -> element.click(), "Не удалось кликнуть на элемент");
    }

    @Step("Кликнуть на элемент")
    public void WaitIsClicked() {
        Waiter.WaitExeption(() -> element.click(), "Не удалось кликнуть на элемент");
    }
}