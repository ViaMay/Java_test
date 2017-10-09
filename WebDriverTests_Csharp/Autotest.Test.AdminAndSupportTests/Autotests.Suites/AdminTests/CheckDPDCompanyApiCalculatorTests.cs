using System;
using Autotests.WebPages.Pages.PageAdmin;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.Tests.AdminTests
{
    public class CheckDPDCompanyApiCalculatorTests : SendOrdersBasePage
    {
        [Test, Description("Проверка компании ДПД. Роут Дзержинский-Дзержинский")]
        public void CalculatorTest()
        {
            LoginAsAdmin(adminName, adminPass);
            var calculatorPage = LoadPage<CalculatorPage>("admin/calculator");
            calculatorPage.LabelDirectory.WaitText("Маршрут");
            calculatorPage.RadioButtonList[1].Click();

            calculatorPage.CityFrom.SetFirstValueSelect("Дзержинский");
            calculatorPage.CityTo.SetFirstValueSelect("Дзержинский");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<CalculatorPage>();
            var row1 = calculatorPage.Table.FindRowByName("smartcourier");

//            var row2 = calculatorPage.Table.FindRowByName("dpd economy");
        }

        [Test, Description("Проверка компании ДПД. Роут Дзержинский-Дзержинский")]
        public void Calculator02Test()
        {
            LoginAsAdmin(adminName, adminPass);
            var calculatorPage = LoadPage<CalculatorPage>("admin/calculator");
            calculatorPage.LabelDirectory.WaitText("Маршрут");
            calculatorPage.RadioButtonList[0].Click();

            calculatorPage.CityFrom.SetFirstValueSelect("Дзержинский");
            calculatorPage.CityTo.SetFirstValueSelect("Дзержинский");

            calculatorPage.СountedButton.Click();
            calculatorPage = calculatorPage.GoTo<CalculatorPage>();

                var row1 = calculatorPage.Table.FindRowByName("smartcourier");
            
            
        }
    }
}