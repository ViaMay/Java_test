﻿using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderSelfCreateSendTests : ConstVariablesTestBase
    {
        [Test, Description("Создание заяки и отправление")]
        public void OrderCreateAndSendTest()
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
            orderCreateSelfPage.BuyerPhoneAdd.SetValueAndWait("3333333333");
            orderCreateSelfPage.BuyerEmail.SetValueAndWait(userNameAndPass);
            orderCreateSelfPage.DeclaredPrice.SetValueAndWait("4");
            orderCreateSelfPage.PaymentPrice.SetValueAndWait("0");
            orderCreateSelfPage.GoodsDescription.SetValueAndWait("444");

            var rowArticle = orderCreateSelfPage.GetArticleRow(0);
            rowArticle.Name.SetValueAndWait("Имя1");
            rowArticle.Article.SetValueAndWait("Article1");
            rowArticle.Count.SetValueAndWait("6");

            orderCreateSelfPage.OrderComment.SetValueAndWait("OrderNumber");
            orderCreateSelfPage.IsCargoVolume.CheckAndWait();

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

            orderCreateSelfPage.PointDeliveryName.WaitText("Пункт выдачи: " + deliveryPointName);
            orderCreateSelfPage.PointDeliveryAddress.WaitText("Адрес: " + deliveryPointAddress);
            orderCreateSelfPage.PointDeliveryCompany.WaitText("Компания: " + companyName);
            orderCreateSelfPage.PointDeliveryPrice.WaitText("Цена: 41");

            orderCreateSelfPage.SendOrderButton.Click();
            var orderCourirsPage = orderCreateSelfPage.GoTo<OrderPage>();

            orderCourirsPage.StatusOrder.WaitText("Подтверждена");
            var ordersId = GetOdrerIdTakeOutUrl();
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Отменить");
        }
    }
}