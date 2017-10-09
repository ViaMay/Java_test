package core.pagesControls;

import core.systemControls.*;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 20.11.2015.
 */
public class InfoModal extends HtmlControl
{
    public InfoModal(By className)
    {
        super(className);
        setTextInfo(new StaticText(By.xpath("//div[@class='body']/div/span[@class='bold']"), this));
        setOkButton(new Link(By.xpath("//button[@class='action ']"), this));
        setGotoOrdersListButton(new Link(By.xpath("//button[@class='action width50']"), this));
        setGotoEditOrderButton(new Link(By.xpath("//button[@class='width50']"), this));
        setCloseButton(new Link(By.xpath("//*[@data-modal-action='close']/img"),this));
    }

    private StaticText TextInfo;
    public final StaticText getTextInfo()
    {
        return TextInfo;
    }
    public final void setTextInfo(StaticText value)
    {
        TextInfo = value;
    }
    private Link OkButton;
    public final Link getOkButton()
    {
        return OkButton;
    }
    public final void setOkButton(Link value)
    {
        OkButton = value;
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

    private Link GotoOrdersListButton;
    public final Link getGotoOrdersListButton()
    {
        return GotoOrdersListButton;
    }
    public final void setGotoOrdersListButton(Link value)
    {
        GotoOrdersListButton = value;
    }

    private Link GotoEditOrderButton;
    public final Link getGotoEditOrderButton()
    {
        return GotoEditOrderButton;
    }
    public final void setGotoEditOrderButton(Link value)
    {
        GotoEditOrderButton = value;
    }
}