package core.pagesControls;

import core.systemControls.HtmlControl;
import core.systemControls.Link;
import core.systemControls.StaticText;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 20.11.2015.
 */
public class NavigationBlock extends HtmlControl
{
    public NavigationBlock(By className)
    {
        super(className);

        setSupportLink(new Link(By.xpath("//a[@data-view='app.freshdesk.link']/span"),this));
        setProfileLink(new Link(By.xpath("//a[@data-event='app.modal-form.profile:show']/span"),this));
        setExitLink(new Link(By.xpath("//a[@data-global-action='exit']/span"),this));
        setUserId(new StaticText(By.xpath("//span[@data-store='user.profile.id']"),this));
    }

    private Link SupportLink;
    public final Link getSupportLink()
    {
        return SupportLink;
    }
    public final void setSupportLink(Link value)
    {
        SupportLink = value;
    }
    private Link ProfileLink;
    public final Link getProfileLink()
    {
        return ProfileLink;
    }
    public final void setProfileLink(Link value)
    {
        ProfileLink = value;
    }
    private Link ExitLink;
    public final Link getExitLink()
    {
        return ExitLink;
    }
    public final void setExitLink(Link value)
    {
        ExitLink = value;
    }
    private StaticText UserId;
    public final StaticText getUserId()
    {
        return UserId;
    }
    public final void setUserId(StaticText value)
    {
        UserId = value;
    }
}
