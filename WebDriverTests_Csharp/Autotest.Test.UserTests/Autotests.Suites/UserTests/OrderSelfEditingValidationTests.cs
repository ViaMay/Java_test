﻿using System.Threading;
using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderSelfEditingValidationTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа, проверка валидации при редактировании")]
        public void OrderSelfEditingValidationTest()
        {
            var userPage = LoginAsUser(userNameAndPass, userNameAndPass);
            userPage.UseProfile.Click();
            userPage.UserShops.Click();
            var shopsListPage = userPage.GoTo<UserShopsPage>();
            shopsListPage.Table.FindRowByName(userShopName).OrdersCreateSelf.Click();
            var orderCreateSelfPage = shopsListPage.GoTo<OrderSelfCreatePage>();
            orderCreateSelfPage = orderCreateSelfPage.RefreshUntilMap();
            
            orderCreateSelfPage.BuyerName.SetValueAndWait("Фамилия Имя Отчество");
            orderCreateSelfPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait(userNameAndPass);
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("4");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("444");

            var rowArticle = orderCreateSelfPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateSelfPage.Width.SetValueAndWait("4");
            orderCreateSelfPage.Height.SetValueAndWait("4");
            orderCreateSelfPage.Length.SetValueAndWait("4");
            orderCreateSelfPage.Weight.SetValueAndWait("4");
            orderCreateSelfPage.OrderNumber.SetValue("14");

            orderCreateSelfPage.СountedButton.Click();
            orderCreateSelfPage.MapOrders.SwitchToFrame();

            orderCreateSelfPage.MapOrders.City.SelectValueFirst("Москва");
            orderCreateSelfPage.MapOrders.ImageLocator.Click();
            orderCreateSelfPage.MapOrders.TakeHere.Click();
            orderCreateSelfPage.MapOrders.SwitchToDefaultContent();

            orderCreateSelfPage.SaveDraftButton.Click();
            var orderPage = orderCreateSelfPage.GoTo<OrderPage>();
            var ordersId = GetOdrerIdTakeOutUrl();
            orderPage.BackOrders.Click();
            var ordersPage = orderPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);

            ordersPage.Table.GetRow(0).Edit.Click();
            var orderSelfEditingPage = ordersPage.GoTo<OrderSelfEditingPage>();

            orderSelfEditingPage.Width.SetValue("");
            orderSelfEditingPage.Height.SetValue("");
            orderSelfEditingPage.Length.SetValue("");
            orderSelfEditingPage.Weight.SetValue("");

            orderSelfEditingPage.BuyerName.SetValue("");
            orderSelfEditingPage.BuyerPhone.SetValueAndWait(" ");
            orderSelfEditingPage.BuyerEmail.SetValue("");
            orderSelfEditingPage.DeclaredPrice.SetValue("");
            orderSelfEditingPage.PaymentPrice.SetValue("");

            orderSelfEditingPage.SaveChangeButton.ClickAndWaitTextError(3);
            orderSelfEditingPage.ErrorText[0].WaitText("ФИО получателя обязательно к заполнению");
            orderSelfEditingPage.ErrorText[1].WaitText("Телефон получателя обязательно к заполнению");
            orderSelfEditingPage.ErrorText[2].WaitText("Значение поля «Сторона 1» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[3].WaitText("Значение поля «Сторона 2» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[4].WaitText("Значение поля «Сторона 3» должно быть положительным числом");
            orderSelfEditingPage.ErrorText[5].WaitText("Значение поля «Вес» должно быть положительным числом");

            orderSelfEditingPage.Width.SetValue("11.2");
            orderSelfEditingPage.Height.SetValue("12.2");
            orderSelfEditingPage.Length.SetValue("13.2");
            orderSelfEditingPage.Weight.SetValue("9.2");
            orderSelfEditingPage.DeclaredPrice.SetValue("0");
            orderSelfEditingPage.PaymentPrice.SetValue("0");

            orderSelfEditingPage.BuyerName.SetValue("Фамилия2 Имя2 Очество2");
            orderSelfEditingPage.BuyerPhone.SetValue("2222222222");

            orderSelfEditingPage.BuyerPhoneAdd.SetValueAndWait("+74444444444, +75555555555,  +75555555555");
            orderSelfEditingPage.BuyerEmail.SetValueAndWait("+7");
            orderSelfEditingPage.GoodsDescription.SetValue("");
            orderSelfEditingPage.SaveChangeButton.Click();

            orderSelfEditingPage.SaveChangeButton.ClickAndWaitTextError(4);
            orderSelfEditingPage.ErrorText[0].WaitText("Длина поля «Дополнительный телефон получателя» должна быть не более 30 символов");
            orderSelfEditingPage.ErrorText[1].WaitText("Email должно быть корректным адресом электронной почты");
            orderSelfEditingPage.ErrorText[2].WaitText("Описание посылки обязательно к заполнению");

            orderSelfEditingPage.BuyerPhoneAdd.SetValueAndWait("");
            orderSelfEditingPage.BuyerEmail.SetValueAndWait("");
            orderSelfEditingPage.GoodsDescription.SetValue("GoodsDescription");

            orderSelfEditingPage.СountedButton.Click();

            orderSelfEditingPage.SaveChangeButton.Click();
            Thread.Sleep(3000);
            orderPage = orderSelfEditingPage.GoTo<OrderPage>();
            orderPage.StatusOrder.WaitText("В обработке");
        }
    }
}