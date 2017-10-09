package core.pagesControls;

import core.supportClass.BY;
import core.systemControls.Autocomplete;
import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.TextInput;
import org.openqa.selenium.By;

/**
 * Created by Anya on 23.12.2015.
 */
public class StockAddModal extends HtmlControl
{
    public StockAddModal(By className, HtmlControl container)
    {
        super(className);

        setStockName(new TextInput(BY.ByDataUnique("modal-warehouse-name"),this));
        setCity (new Autocomplete(BY.ByDataUnique("modal-warehouse-city_search"), this));
        setContactPerson(new TextInput(BY.ByDataUnique("modal-warehouse-contact_person"),this));
        setEmail(new TextInput(BY.ByDataUnique("modal-warehouse-contact_email"),this));
        setPhone(new TextInput(BY.ByDataUnique("modal-warehouse-contact_phone"),this));
        setStreet(new TextInput(BY.ByDataUnique("modal-warehouse-street"),this));
        setHouse(new TextInput(BY.ByDataUnique("modal-warehouse-house"),this));
        setFlat(new TextInput(BY.ByDataUnique("modal-warehouse-flat"),this));
        setPostalCode(new TextInput(BY.ByDataUnique("modal-warehouse-postal_code"), this));
        setSaveButton(new Link(By.xpath("//*[@data-view='app.modal-form.stock']//*[@data-name='btn.save']/img"),this));
        setCloseButton(new Link(By.xpath("//*[@data-closest='.modal-block.add-stock']/img"), this));
    }

    private TextInput ContactPerson;
    public final TextInput getContactPerson(){ return ContactPerson; }
    public final void setContactPerson(TextInput value) {ContactPerson = value; }

    private TextInput Email;
    public final TextInput getEmail(){ return Email;}
    public final void setEmail(TextInput value) {Email = value; }

    private TextInput Phone;
    public final TextInput getPhone(){ return Phone;}
    public final void setPhone(TextInput value){Phone = value; }

    private TextInput Street;
    public final TextInput getStreet(){ return Street; }
    public final void setStreet(TextInput value){Street = value; }

    private TextInput House;
    public final TextInput getHouse(){ return House; }
    public final void setHouse(TextInput value){House = value; }

    private TextInput Flat;
    public final TextInput getFlat(){ return Flat; }
    public final void setFlat(TextInput value){Flat = value; }

    private TextInput PostalCode;
    public final TextInput getPostalCode(){ return PostalCode; }
    public final void setPostalCode(TextInput value) {PostalCode = value; }

    private TextInput StockName;
    public final TextInput getStockName() {return StockName;}
    public final void setStockName(TextInput value) {StockName = value; }

    private Autocomplete City;
    public final Autocomplete getCity() {return City;}
    public final void setCity(Autocomplete value) {City = value; }

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
