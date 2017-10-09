﻿using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderCourirsCheckСountedAndIndexTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка работы кнопки пересчета и работы пересчета индекса")]
        public void OrderCourirsCheckСountedAndIndexTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateCourier.Click();
            var orderCreateCourirsPage = shopsListPage.GoTo<OrderCourirsCreatePage>();

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Москва");
            orderCreateCourirsPage.DeclaredPrice.SetValueAndWait("10.1");
            orderCreateCourirsPage.Width.SetValueAndWait("10.1");
            orderCreateCourirsPage.Height.SetValueAndWait("11.1");
            orderCreateCourirsPage.Length.SetValueAndWait("12.1");
            orderCreateCourirsPage.Weight.SetValueAndWait("3.0");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 32.00 руб");
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");
            
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 53.00 руб");
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");

            orderCreateCourirsPage.Weight.SetValueAndWait("20.1");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
            
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("101000");

            orderCreateCourirsPage.Weight.SetValueAndWait("10");
            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Санкт-Петербург");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("190000");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Екатеринбург");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("620000");

            orderCreateCourirsPage.CityTo.SetFirstValueSelect("Питеримка");
            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.DeliveryList[0].WaitAbsence();
            orderCreateCourirsPage.BuyerPostalCode.WaitValue("162035");
        }
    }
}