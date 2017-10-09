package core.systemControls;

import org.openqa.selenium.By;
import org.openqa.selenium.Keys;
import ru.yandex.qatools.allure.annotations.Step;

public class TextInput extends HtmlControl
{

    public TextInput(By locator)
    {
        this(locator, null);
    }

    public TextInput(By locator, HtmlControl container)
    {
        super(locator, container);
    }

    @Step("Вводим зачение {0}")
    public void SetValueAndWait(String value)
    {
        Clear();
        element.sendKeys(value + Keys.TAB);
    }

    @Step("Вводим зачение {0}")
    public final void SetValue(String value)
    {
        Waiter.WaitExeption(() -> Clear());
        Waiter.WaitExeption(() -> element.sendKeys(value), "Не удалось ввести текст");
    }

    public final void Clear()
    {
        element.clear();
    }

    @Step("Copy зачение {0}")
    public final void Copy()
    {
        element.sendKeys(Keys.CONTROL + "a" + "c" + Keys.CONTROL + Keys.TAB);
    }

    @Step("Paste зачение {0}")
    public final void Paste()
    {
        Clear();
        element.sendKeys(Keys.CONTROL + "v" + Keys.CONTROL + Keys.TAB);
    }
}

