﻿using Autotests.WebPages;
using Autotests.WebPages.Pages.PageUser;
using NUnit.Framework;

namespace Autotests.Tests.UserTests
{
    public class OrderCourirsCreateDraftTests : ConstVariablesTestBase
    {
        [Test, Description("Создание черновика заказа и проверка введенных значений")]
        public void OrderCreateCourirsCheckDraftTest()
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
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();

            orderCreateCourirsPage.DeliveryList[0].WaitTextContains("test_via, цена: 53.00 руб");
            orderCreateCourirsPage.DeliveryList[0].Click();
            
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerStreet.WaitValue("Улица");
            orderCreateCourirsPage.BuyerHouse.WaitValue("Дом");
            orderCreateCourirsPage.BuyerFlat.WaitValue("Квартира");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            orderCreateCourirsPage.BuyerPhoneAdd.SetValueAndWait("+71234567890, +71234567890");
            orderCreateCourirsPage.BuyerEmail.SetValueAndWait(userNameAndPass);

            orderCreateCourirsPage.PaymentPrice.SetValueAndWait("1500");
            orderCreateCourirsPage.OrderNumber.SetValueAndWait("OrderNumber");
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("Хороший товар,годный");
            orderCreateCourirsPage.OrderComment.SetValueAndWait("OrderNumber");
            orderCreateCourirsPage.IsCargoVolume.CheckAndWait();

            var rowArticle = orderCreateCourirsPage.GetArticleRow(0);
                rowArticle.Name.SetValueAndWait("Имя1");
                rowArticle.Article.SetValueAndWait("Article1");
                rowArticle.Count.SetValueAndWait("6");

            orderCreateCourirsPage.SaveDraftButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();

            orderCourirsPage.TableSender.City.WaitText("Москва");
            orderCourirsPage.TableSender.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableSender.Name.WaitText(legalEntityName);
            orderCourirsPage.TableSender.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableSender.Delivery.WaitText("Курьерская");
            orderCourirsPage.TableSender.OrderComment.WaitText("OrderNumber");
            orderCourirsPage.TableSender.IsCargoVolume.WaitText("да");

            orderCourirsPage.TableRecipient.City.WaitText("Москва");
            orderCourirsPage.TableRecipient.PostCode.WaitText("123123");
            orderCourirsPage.TableRecipient.Address.WaitText("ул.Улица, дом Дом, офис(квартира) Квартира");
            orderCourirsPage.TableRecipient.Name.WaitText("Фамилия Имя Очество");
            orderCourirsPage.TableRecipient.Email.WaitText(userNameAndPass);
            orderCourirsPage.TableRecipient.Phone.WaitText("+7 (111)111-1111");
            orderCourirsPage.TableRecipient.PhoneAdd.WaitText("+71234567890, +71234567890");
            orderCourirsPage.TableRecipient.Issue.WaitText("Ручная");
            orderCourirsPage.TableRecipient.PickupCompany.WaitText(companyPickupName);
            orderCourirsPage.TableRecipient.DeliveryCompany.WaitText(companyName);

            orderCourirsPage.TablePrice.PaymentPrice.WaitText("1500.00 руб.");
            orderCourirsPage.TablePrice.DeclaredPrice.WaitText("10.10 руб.");
            orderCourirsPage.TablePrice.Insurance.WaitText("0.00 руб.");
            orderCourirsPage.TablePrice.DeliveryPrice.WaitText("53.00 руб.");
            orderCourirsPage.TablePrice.PickupPrice.WaitText("21.00 руб.");

            orderCourirsPage.TableSize.Width.WaitText("10 см");
            orderCourirsPage.TableSize.Height.WaitText("11 см");
            orderCourirsPage.TableSize.Length.WaitText("12 см");
            orderCourirsPage.TableSize.Weight.WaitText("9.10 кг");
            orderCourirsPage.TableSize.Count.WaitText("1");

            orderCourirsPage.TableArticle.GetRow(0).Name.WaitText("Имя1");
            orderCourirsPage.TableArticle.GetRow(0).Article.WaitText("Article1");
            orderCourirsPage.TableArticle.GetRow(0).Count.WaitText("6");
        }

        [Test, Description("Проверка изменения статусов")]
        public void OrderCreateCourirsCheckStatusTest()
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
            orderCreateCourirsPage.Weight.SetValueAndWait("9.1");

            orderCreateCourirsPage.СountedButton.Click();
            orderCreateCourirsPage.WaitCounted();
            
            orderCreateCourirsPage.BuyerStreet.SetValueAndWait("Улица");
            orderCreateCourirsPage.BuyerHouse.SetValueAndWait("Дом");
            orderCreateCourirsPage.BuyerFlat.SetValueAndWait("Квартира");
            orderCreateCourirsPage.BuyerPostalCode.SetValueAndWait("123123");
            orderCreateCourirsPage.BuyerName.SetValueAndWait("Фамилия Имя Очество");
            orderCreateCourirsPage.BuyerPhone.SetValueAndWait("1111111111");
            
            orderCreateCourirsPage.GoodsDescription.SetValueAndWait("okk");
            orderCreateCourirsPage.SaveDraftButton.Click();
            var orderCourirsPage = orderCreateCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("В обработке");

            var ordersId = GetOdrerIdTakeOutUrl();
            orderCourirsPage.BackOrders.Click();
            var ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).Delete.WaitText("Удалить");
            ordersPage.Table.GetRow(0).ID.Click();
            orderCourirsPage = ordersPage.GoTo<OrderPage>();

            orderCourirsPage.СonfirmButton.Click();
            orderCourirsPage = orderCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("Подтверждена");

            orderCourirsPage.Orders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);
            ordersPage.Table.GetRow(0).Status.WaitText("Подтверждена");
            ordersPage.Table.GetRow(0).Undo.WaitText("Отменить");
            ordersPage.Table.GetRow(0).Delete.WaitAbsence();

            var orderId = ordersPage.Table.GetRow(0).ID.GetText();
            ordersPage.Table.GetRow(0).ID.Click();
            orderCourirsPage = ordersPage.GoTo<OrderPage>();

            orderCourirsPage.UndoButton.Click();
            orderCourirsPage = orderCourirsPage.GoTo<OrderPage>();
            orderCourirsPage.StatusOrder.WaitText("В обработке");

            orderCourirsPage.BackOrders.Click();
            ordersPage = orderCourirsPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);
            ordersPage.Table.GetRow(0).Status.WaitText("В обработке");
            ordersPage.Table.GetRow(0).Сonfirm.WaitText("Подтвердить");
            ordersPage.Table.GetRow(0).Delete.WaitText("Удалить");

            ordersPage.Table.GetRow(0).Delete.Click();
            ordersPage.Aletr.WaitText("Удалить заказ?");
            ordersPage.Aletr.Accept();

            ordersPage = ordersPage.GoTo<OrdersListPage>();
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId);
            ordersPage.Table.GetRow(0).ID.WaitAbsence();
            
            ordersPage = LoadPage<OrdersListPage>("user/?search=" + ordersId + "&deleted=1");
            ordersPage.Table.GetRow(0).ID.WaitText(ordersId);
        }
    }
}