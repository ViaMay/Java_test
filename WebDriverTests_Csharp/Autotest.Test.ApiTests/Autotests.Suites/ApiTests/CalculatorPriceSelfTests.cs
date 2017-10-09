﻿using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CalculatorPriceSelfTests : SendOrdersBasePage
    {
        [Test, Description("Расчитать цену самовывоза")]
        public void CalculatorPriceSelfTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            string deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

 //            Не заполен город city_to. Расчитать цену самомвывоза (по пункут выдачи)
            var responseCalculator =
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
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 1);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);

//            Заполены все значения. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
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

//            delivery_point пустое. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", ""},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 1);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);

//            Заполены все значения, Город заполнен текстом. Расчитать цену самомвывоза (по городу доставки)
            responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                    new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "Москва"},
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

//            Одна из сторон равна нулю. Возврат ошибки
            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveryPoinId},
                        {"dimension_side1", "0"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });

            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "dimension_side1:dimension side  должно быть в промежутке от 0.00099 до 100000;");

//           declared_price равна нулю. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
                new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "151184"},
                        {"delivery_point", deliveryPoinId},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "declared price обязательно к заполнению");

 //           payment_price равна нулю. Расчитываем стоимость.
            responseCalculator =
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
                        {"declared_price", ""},
                        {"payment_price", "1000"}
                    });
           Assert.AreEqual(responseCalculator.Response.Count(), 1);
           Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);

//           Проверка корректного to_city, не корректый город. Возврат ошибки
           responseCalculator =
               (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator.json",
               new NameValueCollection
                    {
                        {"type", "1"},
                        {"city_to", "Оклахома"},
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
        }
    }
}