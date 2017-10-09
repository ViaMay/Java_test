package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.*;
import org.openqa.selenium.By;

public class OrderLogistics extends HtmlControl
{
    public OrderLogistics(By xpath)
    {
        super(xpath);
        setShop(new Select(BY.ByDataUnique("order-create-api_key")));
        setDeclaredPrice(new TextInput(BY.ByDataUnique("order-create-declared_price")));
        setWeight(new TextInput(BY.ByDataUnique("order-create-weight")));
        setWidth(new TextInput(BY.ByDataUnique("order-create-dimension_side1")));
        setHeight(new TextInput(BY.ByDataUnique("order-create-dimension_side2")));
        setLength(new TextInput(BY.ByDataUnique("order-create-dimension_side3")));

        setRecipient(new OrderLogisticsRecipientFrame(By.tagName("iframe")));
    }

    private Select Shop;
    public final Select getShop()
    {
        return Shop;
    }
    public final void setShop(Select value)
    {
        Shop = value;
    }
    private TextInput Weight;
    public final TextInput getWeight()
    {
        return Weight;
    }
    public final void setWeight(TextInput value)
    {
        Weight = value;
    }
    private TextInput Width;
    public final TextInput getWidth()
    {
        return Width;
    }
    public final void setWidth(TextInput value)
    {
        Width = value;
    }
    private TextInput Height;
    public final TextInput getHeight()
    {
        return Height;
    }
    public final void setHeight(TextInput value)
    {
        Height = value;
    }
    private TextInput Length;
    public final TextInput getLength()
    {
        return Length;
    }
    public final void setLength(TextInput value)
    {
        Length = value;
    }
    private TextInput DeclaredPrice;
    public final TextInput getDeclaredPrice()
    {
        return DeclaredPrice;
    }
    public final void setDeclaredPrice(TextInput value)
    {
        DeclaredPrice = value;
    }

    private OrderLogisticsRecipientFrame OrderLogisticsRecipient;
    public final OrderLogisticsRecipientFrame getRecipient()
    {
        return OrderLogisticsRecipient;
    }
    public final void setRecipient(OrderLogisticsRecipientFrame value)
    {
        OrderLogisticsRecipient = value;
    }


}