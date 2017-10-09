package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.Select;
import core.systemControls.TextInput;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 20.11.2015.
 */
public class ShopAddModal extends HtmlControl
{
    public ShopAddModal(By className, HtmlControl container)
    {
        super(className);
        setName(new TextInput(BY.ByDataUnique("modal-shop-name"),this));
        setWarehouse(new Select(BY.ByDataUnique("modal-shop-warehouse"),this));
        setAddress(new TextInput(BY.ByDataUnique("modal-shop-address"),this));
        setSaveButton(new Link(By.xpath("//*[@data-name='btn.save']/img"),this));
        setCloseButton(new Link(By.xpath("//*[@data-closest='.modal-block.add-shop']/img"),this));
    }

    private TextInput setName;
    public final TextInput getName()
    {
        return setName;
    }
    public final void setName(TextInput value)
    {
        setName = value;
    }
    private Select Warehouse;
    public final Select getWarehouse()
    {
        return Warehouse;
    }
    public final void setWarehouse(Select value)
    {
        Warehouse = value;
    }
    private TextInput Address;
    public final TextInput getAddress()
    {
        return Address;
    }
    public final void setAddress(TextInput value)
    {
        Address = value;
    }
    private Link SaveButton;
    public final Link getSaveButton()
    {
        return SaveButton;
    }
    public final void setSaveButton(Link value)
    {
        SaveButton = value;
    }
    private Link CloseButton;
    public final Link getCloseButton()
    {
        return CloseButton;
    }
    public final void setCloseButton(Link value)
    {
        CloseButton = value;
    }
}