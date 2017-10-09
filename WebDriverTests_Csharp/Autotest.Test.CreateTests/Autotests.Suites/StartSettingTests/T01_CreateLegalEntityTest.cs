using Autotests.WebPages;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;

namespace Autotests.Tests.StartSettingTests
{
    public class T01CreateLegalEntityTest : ConstVariablesTestBase
    {
        [Test, Description("Создания юредического лица для Компании")]
        public void CreateLegalEntityTest()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.LegalEntities.Click();
            var legalEntitiesPage = adminPage.GoTo<AdminBaseListPage>();
            legalEntitiesPage.LabelDirectory.WaitText(@"Справочник ""Юридическое лицо""");
            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();

            while (legalEntitiesPage.Table.GetRow(0).Name.IsPresent)
            {
                legalEntitiesPage.Table.GetRow(0).ActionsDelete.Click();
                legalEntitiesPage.Aletr.Accept();
                legalEntitiesPage = legalEntitiesPage.GoTo<AdminBaseListPage>();
                legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
                legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            }
            legalEntitiesPage.Create.Click();
            var legalEntityCreatePage = legalEntitiesPage.GoTo<LegalEntityCreatePage>();
            legalEntityCreatePage.NameEntity.SetValueAndWait(legalEntityName);
            legalEntityCreatePage.SaveButton.Click();
            legalEntitiesPage = legalEntityCreatePage.GoTo<AdminBaseListPage>();
            legalEntitiesPage.Table.RowSearch.Name.SetValue(legalEntityName);
            legalEntitiesPage = legalEntitiesPage.SeachButtonRowClickAndGo();
            legalEntitiesPage.Table.GetRow(0).Name.WaitText(legalEntityName);
        }

        [Test, Description("Создания юредического лица для Компании")]
        public void CreateNewsTest01()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.News.Click();
            var newsPage = adminPage.GoTo<AdminBaseListPage>();
            newsPage.LabelDirectory.WaitText(@"Справочник ""Новости и уведомления""");
            newsPage.Table.RowSearch.Content.SetValue("<p>test_новость</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();

            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p>test_новость</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_новость</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_новость</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Create.Click();
            var newsCreatePage = newsPage.GoTo<NewsCreatePage>();
            newsCreatePage.Text.SetValueAndWait("test_новость");
            newsCreatePage.Type.SelectValue("Новость");
            newsCreatePage.Active.CheckAndWait();
            newsCreatePage.Email.UncheckAndWait();
            newsCreatePage.SaveButton.Click();
            newsPage = newsCreatePage.GoTo<AdminBaseListPage>();
        }

        [Test, Description("Создания юредического лица для Компании")]
        public void CreateNewsTest02()
        {
            var adminPage = LoginAsAdmin(adminName, adminPass);
            adminPage.DirectoryList.Click();
            adminPage.News.Click();
            var newsPage = adminPage.GoTo<AdminBaseListPage>();
            newsPage.LabelDirectory.WaitText(@"Справочник ""Новости и уведомления""");
            newsPage.Table.RowSearch.Content.SetValue("<p>test_уведомление</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();

            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p>test_уведомление</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_уведомление</p>");
            newsPage = newsPage.SeachButtonRowClickAndGo();
            while (newsPage.Table.GetRow(0).Name.IsPresent)
            {
                newsPage.Table.GetRow(0).ActionsDelete.Click();
                newsPage.Aletr.Accept();
                newsPage = newsPage.GoTo<AdminBaseListPage>();
                newsPage.Table.RowSearch.Content.SetValue("<p style=\"margin-left: 20px;\">test_уведомление</p>");
                newsPage = newsPage.SeachButtonRowClickAndGo();
            }
            newsPage.Create.Click();
            var newsCreatePage = newsPage.GoTo<NewsCreatePage>();
            newsCreatePage.Text.SetValueAndWait("test_уведомление");
            newsCreatePage.Type.SelectValue("Уведомление");
            newsCreatePage.Active.UncheckAndWait();
            newsCreatePage.Email.UncheckAndWait();
            newsCreatePage.SaveButton.Click();
            newsPage = newsCreatePage.GoTo<AdminBaseListPage>();
        }
    }
}