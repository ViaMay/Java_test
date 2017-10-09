package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.HtmlControl;
import core.systemControls.TextInput;
import org.openqa.selenium.By;
import ru.yandex.qatools.allure.annotations.Step;

public class OrderPersonalInfo extends HtmlControl
{
    public OrderPersonalInfo(By xpath)
    {
        super(xpath);
        setName(new TextInput(BY.ByDataUnique("order-create-to_name")));
        setPhone(new TextInput(BY.ByDataUnique("order-create-to_phone")));
        setEmail(new TextInput(BY.ByDataUnique("order-create-to_email")));
        setPhoneAdd(new TextInput(BY.ByDataUnique("order-create-to_add_phone")));
        setPostalCode(new TextInput(BY.ByDataUnique("order-create-to_postal_code")));
        setComment(new TextInput(BY.ByDataUnique("order-create-order_comment")));
    }
    private TextInput Name;
    @Step
    public final TextInput getName()
    {
        return Name;
    }
    public final void setName(TextInput value)
{
    Name = value;
}
    private TextInput Phone;
    @Step
    public final TextInput getPhone()
    {
        return Phone;
    }
    public final void setPhone(TextInput value)
    {
        Phone = value;
    }
    private TextInput Email;
    @Step
    public final TextInput getEmail()
    {
        return Email;
    }
    public final void setEmail(TextInput value)
    {
        Email = value;
    }
    private TextInput PhoneAdd;
    @Step
    public final TextInput getPhoneAdd()
    {
        return PhoneAdd;
    }
    public final void setPhoneAdd(TextInput value)
    {
        PhoneAdd = value;
    }
    private TextInput PostalCode;
    @Step
    public final TextInput getPostalCode()
    {
        return PostalCode;
    }
    public final void setPostalCode(TextInput value)
    {
        PostalCode = value;
    }
    private TextInput Comment;
    @Step
    public final TextInput getComment()
    {
        return Comment;
    }
    public final void setComment(TextInput value)
    {
        Comment = value;
    }
}