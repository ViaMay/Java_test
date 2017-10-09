package core.pages;

import core.systemControls.TextInput;
import core.pagesControls.Menu;
import core.pagesControls.NavigationBlock;
import core.systemPages.PageBase;
import org.openqa.selenium.By;

/**
 * Created by Виктория on 19.11.2015.
 */
public class HomePage extends PageBase
{
    public HomePage()
    {
        setSearchBlock(new TextInput(By.className("search-block")));
        setNavigationBlock(new NavigationBlock(By.className("nav")));
        setMenu(new Menu(By.className("menu")));
    }
    @Override
    public void BrowseWaitVisible()
    {
        getSearchBlock().WaitVisibleWithRetries();
        getNavigationBlock().WaitVisibleWithRetries();

        getNavigationBlock().getSupportLink().WaitText("Поддержка");
        getNavigationBlock().getProfileLink().WaitText("Профиль");
        getNavigationBlock().getExitLink().WaitText("Выход");

        getMenu().getDashboard().WaitText("Контрольный центр");
        getMenu().getOrderCreate().WaitText("Новый заказ");
        getMenu().getOrderList().WaitText("Заказы и документы");
        getMenu().getShopsAndStock().WaitText("Склады и магазины");
        getMenu().getIntegrations().WaitText("Маркет");
        getMenu().getFinances().WaitText("Финансы");
    }

    private TextInput SearchBlock;
    public final TextInput getSearchBlock()
    {
        return SearchBlock;
    }
    public final void setSearchBlock(TextInput value)
    {
        SearchBlock = value;
    }
    private core.pagesControls.NavigationBlock NavigationBlock;
    public final core.pagesControls.NavigationBlock getNavigationBlock()
    {
        return NavigationBlock;
    }
    public final void setNavigationBlock(core.pagesControls.NavigationBlock value)
    {
        NavigationBlock = value;
    }
    private core.pagesControls.Menu Menu;
    public final core.pagesControls.Menu getMenu()
    {
        return Menu;
    }
    public final void setMenu(core.pagesControls.Menu value)
    {
        Menu = value;
    }
}

