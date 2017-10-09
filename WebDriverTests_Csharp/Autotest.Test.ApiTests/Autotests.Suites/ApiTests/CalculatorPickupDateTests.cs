﻿using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CalculatorPickupDateTests : SendOrdersBasePage
    {
        [Test, Description("Расчитать цену самовывоза c датой предполагаемого забора таска 732")]
        public void CalculatorPickupDateTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            string deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

//            поле pickup_date нет - считается что оно заполненно текущей датой
            var responseCalculator =
               (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                   new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveryPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 1);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);

//            подтвердить до сегоднешего вечера
            Assert.AreEqual(responseCalculator.Response[0].ConfirmDate,
                nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            зата забора следующий день
            Assert.AreEqual(responseCalculator.Response[0].PickupDate,
                nowDate.AddDays(1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//           дата доставки на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Response[0].DeliveryDate,
                nowDate.AddDays(3).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));

//            поле pickup_date - заполненно текущей датой
            var pickupDate = nowDate;
            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveryPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_date", pickupDate.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 1);
//            подтвердить до сегоднешего вечера
            Assert.AreEqual(responseCalculator.Response[0].ConfirmDate,
                nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            зата забора следующий день
            Assert.AreEqual(responseCalculator.Response[0].PickupDate,
                nowDate.AddDays(1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//           дата доставки на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Response[0].DeliveryDate,
                nowDate.AddDays(3).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));

//            поле pickup_date - заполненно на два дня вперед
            pickupDate = nowDate.AddDays(2);
            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", ""},
                        {"delivery_point", deliveryPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {
                            "pickup_date", pickupDate.ToString("dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture)
                        }
                    });

            Assert.AreEqual(responseCalculator.Response.Count(), 1);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);
//            на два дня вперед от PickupDate
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryDate,
                pickupDate.AddDays(2).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
//            близайщая от даты забора к PickupDate
            Assert.AreEqual(responseCalculator.Response[0].ConfirmDate,
                pickupDate.AddDays(-1).ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) + " 23:45");
//            возвращается текущая PickupDate
            Assert.AreEqual(responseCalculator.Response[0].PickupDate,
                pickupDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture));
        }
    }
}