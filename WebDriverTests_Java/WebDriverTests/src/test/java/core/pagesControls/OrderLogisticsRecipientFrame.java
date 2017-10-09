package core.pagesControls;

import core.systemControls.*;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

/**
 * Created by Виктория on 20.11.2015.
 */
public class OrderLogisticsRecipientFrame extends HtmlControl
{
    public OrderLogisticsRecipientFrame(By id)
    {
        super(id);

        setLoader(new TextInput(By.xpath("//*[@class='select-city show']")));

        setCityField(new TextInput(By.id("city-field")));
        setCitesList(new List(By.className("list")));
        setСourierСompany(new Select(By.id("courier_company")));
        setCourierStreet(new TextInput(By.xpath("//*[@id='courier_form']//*[@name='street']")));
        setCourierHouse(new TextInput(By.xpath("//*[@id='courier_form']//*[@name='house']")));
        setCourierFlat(new TextInput(By.xpath("//*[@id='courier_form']//*[@name='flat']")));

    }
    private TextInput Loader;
    public final TextInput getLoader()
    {
        return Loader;
    }
    public final void setLoader(TextInput value)
    {
        Loader = value;
    }
    private TextInput CityField;
    @Step
    public final TextInput getCityField()
    {
        return CityField;
    }
    public final void setCityField(TextInput value)
    {
        CityField = value;
    }
    private List CitesList;
    @Step
    public final List getCitesList()
    {
        return CitesList;
    }
    public final void setCitesList(List value)
    {
        CitesList = value;
    }
    private TextInput CourierStreet;
    @Step
    public final TextInput getCourierStreet()
    {
        return CourierStreet;
    }
    public final void setCourierStreet(TextInput value)
    {
        CourierStreet = value;
    }
    private TextInput CourierHouse;
    @Step
    public final TextInput getCourierHouse()
    {
        return CourierHouse;
    }
    public final void setCourierHouse(TextInput value)
    {
        CourierHouse = value;
    }
    private TextInput CourierFlat;
    @Step
    public final TextInput getCourierFlat()
    {
        return CourierFlat;
    }
    public final void setCourierFlat(TextInput value)
    {
        CourierFlat = value;
    }
    private Select СourierСompany;
    @Step
    public final Select getСourierСompany()
    {
        return СourierСompany;
    }
    public final void setСourierСompany(Select value)
    {
        СourierСompany = value;
    }


}