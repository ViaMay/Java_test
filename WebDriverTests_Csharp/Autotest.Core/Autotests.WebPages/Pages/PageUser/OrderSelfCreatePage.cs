﻿using System;
using System.Diagnostics;
using System.Threading;
using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using NUnit.Framework;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderSelfCreatePage : UserPageBase
    {
        public OrderSelfCreatePage()
        {
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));

            СountedButton = new ButtonInput(By.Name("recalc"));
            
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerPhoneAdd = new TextInput(By.Name("to_add_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));
            ItemsCount = new TextInput(By.Name("items_count"));
            DeclaredPrice = new TextInput(By.Name("declared_price"));
            PaymentPrice = new TextInput(By.Name("payment_price"));
            GoodsDescription = new TextInput(By.Name("goods_description"));
            OrderComment = new TextInput(By.Name("order_comment"));
            IsCargoVolume = new CheckBox(By.Name("is_cargo_volume"));

            PointDeliveryName = new StaticText(By.XPath("//div[@class='point_info_detail']/div"));
            PointDeliveryAddress = new StaticText(By.XPath("//div[@class='point_info_detail']/div[2]"));
            PointDeliveryCompany = new StaticText(By.XPath("//div[@class='point_info_detail']/div[3]"));
            PointDeliveryPrice = new StaticText(By.XPath("//div[@class='point_info_detail']/div[4]"));

            SaveDraftButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.btn-success"));
            SendOrderButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.pull-right"));

            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"));
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"));

            MapOrders = new MapControl(By.Id("ddelivery"));
         }

        public OrderArticleRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleRowControl(index);
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            Weight.WaitVisible();
        }

        public OrderSelfCreatePage RefreshPage()
        {
            return RefreshPage(this);
        }

        public OrderSelfCreatePage RefreshUntilMap(int timeout = 100000, int waitTimeout = 1000)
        {
            var w = Stopwatch.StartNew();
            var newPage = new OrderSelfCreatePage();
            try
            {
                newPage.MapOrders.SwitchToFrame();
                newPage.MapOrders.ImageZoom.WaiteVisible();
                newPage.MapOrders.SwitchToDefaultContent();
                return newPage;
            }
            catch (Exception)
            {
                do
                {
                    newPage = RefreshPage();
                    try
                    {
                        newPage.MapOrders.SwitchToFrame();
                        newPage.MapOrders.ImageZoom.WaiteVisible();
                        newPage.MapOrders.SwitchToDefaultContent();
                        return newPage;
                    }
                    catch (Exception)
                    {
                        Thread.Sleep(waitTimeout);
                    }
                } while (w.ElapsedMilliseconds < timeout);
            }
            Assert.Fail(string.Format("Не смогли дождаться видимости карты на странице создания заказов за {0} мс", timeout));
            return default(OrderSelfCreatePage);
        }

        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }
        public TextInput OrderNumber { get; set; }

        public ButtonInput СountedButton { get; set; }
        
        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerPhoneAdd { get; set; }
        public TextInput BuyerEmail { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public TextInput PaymentPrice { get; set; }
        public TextInput GoodsDescription { get; set; }

        public TextInput OrderComment { get; set; }
        public TextInput ItemsCount { get; set; }
        public CheckBox IsCargoVolume { get; set; }

        public ButtonInput SaveDraftButton { get; set; }
        public ButtonInput SendOrderButton { get; set; }

        public StaticText PointDeliveryName { get; set; }
        public StaticText PointDeliveryAddress { get; set; }
        public StaticText PointDeliveryCompany { get; set; }
        public StaticText PointDeliveryPrice { get; set; }

        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }

        public MapControl MapOrders { get; set; }
    }
}