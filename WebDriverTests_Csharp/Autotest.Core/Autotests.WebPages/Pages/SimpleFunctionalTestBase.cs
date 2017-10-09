using Autotests.Utilities.WebTestCore;
using Autotests.Utilities.WebTestCore.TestSystem;
using Autotests.WebPages.Pages.PageAdmin;
using Autotests.WebPages.Pages.PageUser;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages
{
    [TestFixture]
    public class SimpleFunctionalTestBase : WebDriverFunctionalTestBase
    {
        public override void SetUp()
        {
            base.SetUp();
            LoadPage<LoginPage>("auth/login");
        }

        public override void TearDown()
        {
//            base.TearDown();
            try
            {
                base.TearDown();
            }
            catch (UnhandledAlertException)
            {
                var alert = WebDriverCache.WebDriver.Alert();
                alert.Dismiss();
                base.TearDown();
            }
//            catch (NoSuchElementException)
//            {
//            }
//            finally
//            {
//                
//            }
        }

        public UserHomePage LoginAsUser(string login, string password)
        {
            var enterPage = LoadPage<LoginPage>("auth/login");
            return enterPage.LoginAsUser(login, password);
        }

        public AdminHomePage LoginAsAdmin(string login, string password)
        {
            var enterPage = LoadPage<LoginPage>("auth/login");
            return enterPage.LoginAsAdmin(login, password);
        }

        protected void ResetDownloadFilesState()
        {
            WebDriverCache.WebDriver.CleanDownloadDirectory();
        }

        public LoginPage LoginDefaultPage()
        {
            var partyCreationPage = LoadPage<LoginPage>("auth/login");
            return partyCreationPage;
        }

        public void WaitDocuments(int value = 90000)
        {
            Thread.Sleep(value);
        }

        protected LoginPage LoginPage { get; private set; }
    }
}