﻿using Autotests.Utilities.WebTestCore.SystemControls;
using Autotests.WebPages.Pages.PageUser.Controls;
using OpenQA.Selenium;

namespace Autotests.WebPages.Pages.PageUser
{
    public class OrderSelfEditingPage : UserPageBase
    {
        public OrderSelfEditingPage()
        {
            BuyerName = new TextInput(By.Name("to_name"));
            BuyerPhone = new TextInput(By.Name("to_phone"));
            BuyerPhoneAdd = new TextInput(By.Name("to_add_phone"));
            BuyerEmail = new TextInput(By.Name("to_email"));
            DeclaredPrice = new TextInput(By.Name("declared_price"));
            PaymentPrice = new TextInput(By.Name("payment_price"));
            GoodsDescription = new TextInput(By.Name("goods_description"));
            OrderComment = new TextInput(By.Name("order_comment"));
            IsCargoVolume = new CheckBox(By.Name("is_cargo_volume"));
            DeliveryDate = new TextInput(By.Name("delivery_date_new"));
            ItemsCount = new TextInput(By.Name("items_count"));
            
            Weight = new TextInput(By.Name("weight"));
            Width = new TextInput(By.Name("dimension_side1"));
            Height = new TextInput(By.Name("dimension_side2"));
            Length = new TextInput(By.Name("dimension_side3"));
            OrderNumber = new TextInput(By.Name("shop_refnum"));

            CanceledButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.btn-success"));
            SaveChangeButton = new ButtonInput(By.CssSelector("div.form-actions > input.btn.btn-primary.pull-right"));

            ActionErrorText = new ErrorActionTextControl(By.ClassName("form-horizontal"));
            ErrorText = new ErrorTextControl(By.ClassName("form-horizontal"));
            AletrError = new AlertControl();

            MapOrders = new MapControl(By.Id("ddelivery"));
            СountedButton = new ButtonInput(By.Name("recalc"));
        }

        public OrderArticleStaticRowControl GetArticleRow(int index)
        {
            var row = new OrderArticleStaticRowControl(index);
            return row;
        }

        public override void BrowseWaitVisible()
        {
            base.BrowseWaitVisible();
            OrderNumber.WaitVisible();
        }

        public TextInput BuyerName { get; set; }
        public TextInput BuyerPhone { get; set; }
        public TextInput BuyerPhoneAdd { get; set; }
        public TextInput BuyerEmail { get; set; }
        public TextInput PaymentPrice { get; set; }
        public TextInput DeclaredPrice { get; set; }
        public TextInput GoodsDescription { get; set; }
        public TextInput OrderComment { get; set; }
        public CheckBox IsCargoVolume { get; set; }
        public TextInput DeliveryDate { get; set; }
        public TextInput ItemsCount { get; set; }

        public TextInput Weight { get; set; }
        public TextInput Width { get; set; }
        public TextInput Height { get; set; }
        public TextInput Length { get; set; }
        public TextInput OrderNumber { get; set; }
        public ButtonInput CanceledButton { get; set; }
        public ButtonInput SaveChangeButton { get; set; }

        public ErrorActionTextControl ActionErrorText { get; set; }
        public ErrorTextControl ErrorText { get; set; }
        public AlertControl AletrError { get; set; }

        public MapControl MapOrders { get; set; }
        public ButtonInput СountedButton { get; set; }
    }
}