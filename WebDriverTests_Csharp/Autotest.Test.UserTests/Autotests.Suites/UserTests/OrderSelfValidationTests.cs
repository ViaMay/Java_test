﻿using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderSelfValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Проверка валидаций перерасчета")]
        public void TestAlertValidationСounted()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage = orderCreateSelfPage.RefreshUntilMap();

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.Aletr.WaitText("Сначала нужно указать размеры, вес и оценочную стоимость");
            orderCreateSelfPage.Aletr.Сancel();

            orderCreateSelfPage.Width.SetValueAndWait("10.1");
            orderCreateSelfPage.Height.SetValueAndWait("11.1");
            orderCreateSelfPage.Length.SetValueAndWait("20.1");
            orderCreateSelfPage.Weight.SetValueAndWait("9.1");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.Aletr.WaitText("Сначала нужно указать размеры, вес и оценочную стоимость");
            orderCreateSelfPage.Aletr.Accept();

            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("100");
            orderCreateSelfPage.Weight.SetValueAndWait("");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.Aletr.WaitText("Сначала нужно указать размеры, вес и оценочную стоимость");
            orderCreateSelfPage.Aletr.Accept();

            orderCreateSelfPage.Weight.SetValueAndWait("3");
            orderCreateSelfPage.Height.SetValueAndWait("");
            orderCreateSelfPage.Length.SetValueAndWait("");
            orderCreateSelfPage.Width.SetValueAndWait("");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.Aletr.WaitText("Сначала нужно указать размеры, вес и оценочную стоимость");
            orderCreateSelfPage.Aletr.Accept();

            orderCreateSelfPage.Height.SetValueAndWait("0");
            orderCreateSelfPage.Length.SetValueAndWait("0");
            orderCreateSelfPage.Width.SetValueAndWait("0");
            orderCreateSelfPage.СountedButton.Click();

            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();
 }

        [Test, Description("Проверка валидации при отправке")]
        public void TestValidationSend()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage = orderCreateSelfPage.RefreshUntilMap();

            orderCreateSelfPage.BuyerPhoneAdd.SetValueAndWait("+74444444444, +75555555555,  +75555555555");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait("+7");

            orderCreateSelfPage.SendOrderButton.ClickAndWaitTextError(3);
            orderCreateSelfPage.ErrorText[0].WaitText("ФИО получателя обязательно к заполнению");
            orderCreateSelfPage.ErrorText[1].WaitText("Телефон получателя обязательно к заполнению");
            orderCreateSelfPage.ErrorText[2].WaitText("Длина поля «Дополнительный телефон получателя» должна быть не более 30 символов");
            orderCreateSelfPage.ErrorText[3].WaitText("Email должно быть корректным адресом электронной почты"); 
            orderCreateSelfPage.ErrorText[4].WaitText("Описание посылки обязательно к заполнению");
            orderCreateSelfPage.ErrorText[5].WaitText("Сторона 1 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[6].WaitText("Сторона 2 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[7].WaitText("Сторона 3 обязательно к заполнению");
            orderCreateSelfPage.ErrorText[8].WaitText("Вес обязательно к заполнению");

            orderCreateSelfPage.ActionErrorText[0].WaitText("Компания доставки обязательно к заполнению");
            orderCreateSelfPage.ActionErrorText[1].WaitText("Не выбран пункт выдачи");
            orderCreateSelfPage.ActionErrorText[2].WaitText("Город получения обязательно к заполнению");

            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("okk");
            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.BuyerPhoneAdd.SetValueAndWait("");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait("");
            orderCreateSelfPage.СountedButton.Click();

            orderCreateSelfPage.SendOrderButton.ClickAndWaitTextError(3);
            orderCreateSelfPage.ActionErrorText[0].WaitText("Компания доставки обязательно к заполнению");
            orderCreateSelfPage.ActionErrorText[1].WaitText("Не выбран пункт выдачи");
            orderCreateSelfPage.ActionErrorText[2].WaitText("Город получения обязательно к заполнению");
            orderCreateSelfPage.ErrorText[0].WaitText("( Если по вашим оценкам ваше отправление превышает 0,5 м3)");
            orderCreateSelfPage.ErrorText[1].WaitText("Внимание! Калькулятор произвел расчет по параметрам, не учитывающим кол-во мест в заказе");

            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();
            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateSelfPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
        }
    }
}