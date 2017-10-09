package core.systemControls;


import org.openqa.selenium.By;
import org.openqa.selenium.NoSuchElementException;
import org.openqa.selenium.WebElement;
import org.openqa.selenium.support.ui.Quotes;

import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;
import java.util.StringTokenizer;

public class Select extends HtmlControl
{
    private StaticText selectedText;
    private StaticText selectedText02;
    private boolean isMulti;

    public Select(By locator)
    {
        this(locator, null);
    }

    public Select(By locator, HtmlControl container)
    {
        super(locator, container);
        HtmlControlContainer controlContainer = new HtmlControlContainer(locator, container);

        selectedText = new StaticText(By.xpath("option[@selected]"), controlContainer);
        selectedText02 = new StaticText(By.xpath("option"), controlContainer);

    }



    @Override
    public String getText()
    {
        return selectedText02.getText();
    }

    @Override
    public void WaitText(String expectedText)
    {
        String description = formatWithLocator(String.format("Ожидание появления текста '%1$s' в элементе", expectedText));
        Waiter.Wait(() -> selectedText02.getText().equals(expectedText), description);

    }

    public boolean isMultiple() {
        return this.isMulti;
    }


    public List<WebElement> getOptions() {
        return this.element.findElements(By.tagName("option"));
    }

    public List<WebElement> getAllSelectedOptions() {
        ArrayList toReturn = new ArrayList();
        Iterator var2 = this.getOptions().iterator();

        while(var2.hasNext()) {
            WebElement option = (WebElement)var2.next();
            if(option.isSelected()) {
                toReturn.add(option);
            }
        }

        return toReturn;
    }


    public void selectByValue(String value) {
        List options = this.element.findElements(By.xpath(".//option[@value = " + Quotes.escape(value) + "]"));
        boolean matched = false;

        for(Iterator var4 = options.iterator(); var4.hasNext(); matched = true) {
            WebElement option = (WebElement)var4.next();
            this.setSelected(option);
            if(!this.isMultiple()) {
                return;
            }
        }

        if(!matched) {
            throw new NoSuchElementException("Cannot locate option with value: " + value);
        }
    }
    public void selectByText(String text)
    {
        Waiter.WaitExeption(() -> element.click(), "Ждем кликабельности элемента");
//        List options = this.element.findElements(By.xpath(".//option[normalize-space(.) = " + Quotes.escape(text) + "]"));
        WebElement options = this.element.findElement(By.xpath(".//option[normalize-space(.) = " + Quotes.escape(text) + "]"));
        options.click();
//        String value = (String) javascriptExecute("return $('[data-unique=\"modal-shop-warehouse\"]').val();");
        String value = (String) javascriptExecuteWebElement("return arguments[0].value;");
        WebElement optionsValue = this.element.findElement(By.xpath(".//option[@value = " + Quotes.escape(value) + "]"));
        if (!optionsValue.getText().equals(text))
            throw new NoSuchElementException("Cannot locate element with text: " + text);
    }
    public void selectByTextContains(String text)
    {
        Waiter.WaitExeption(() -> element.click(), "Ждем кликабельности элемента");

        List options = this.element.findElements(By.xpath(".//option"));
        for (int i= 0; i < options.size(); i++ )
        {
            WebElement option = (WebElement) options.get(i);
            if (option.getText().contains(text))
            {
                option.click();
                String value = (String) javascriptExecuteWebElement("return arguments[0].value;");
                WebElement optionsValue = this.element.findElement(By.xpath(".//option[@value = " + Quotes.escape(value) + "]"));
                if (!optionsValue.getText().contains(text))
                    throw new NoSuchElementException("Cannot locate element with text: " + text);
                break;
            }
            throw new NoSuchElementException("Cannot locate element with text: " + text);
        }

    }
    private void setSelected(WebElement option) {
        if(!option.isSelected()) {
            option.click();
        }}


    private String getLongestSubstringWithoutSpace(String s) {
        String result = "";
        StringTokenizer st = new StringTokenizer(s, " ");

        while(st.hasMoreTokens()) {
            String t = st.nextToken();
            if(t.length() > result.length()) {
                result = t;
            }
        }

        return result;
    }


}

