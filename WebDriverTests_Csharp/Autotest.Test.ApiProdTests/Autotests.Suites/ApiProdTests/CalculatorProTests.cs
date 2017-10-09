using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class CalculatorProTests : ConstVariablesBase
    {
        [Test, Description("Расчитать цену курьерская")]
        public void CalculatorCourirsTest()
        {
//            Все поля заполнены. Расчитать цену курьерки для Магазина у которого только один тип доставки  указана ТК забора
            var responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                    new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsTrue(responseCalculator.Success);

            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                    new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_type", "3"}
                    });
            Assert.IsTrue(responseCalculator.Success);
        }

        [Test, Description("Расчитать цену курьерская")]
        public void CalculatorCourirsErrorTest()
        {
// Возврат ошибки
            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_type", "0"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "pickup_type:Неправильный тип забора;");
            
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
                new NameValueCollection
                    {
                        {"type", "2"},
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_type", "7"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "pickup_type:Неправильный тип забора;");
        }

        [Test, Description("Расчитать цену курьерская")]
        public void CalculatorSelfTest()
        {
            //            Не заполен город city_to. Расчитать цену самомвывоза (по пункут выдачи)
            var responseCalculator =

                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
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
            Assert.IsTrue(responseCalculator.Success);

            responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
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
                        {"pickup_type", "3"}
                    });
            Assert.IsTrue(responseCalculator.Success);
        }

        [Test, Description("Расчитать цену самовывоза")]
        public void CalculatorSelfErrorTest()
        {
//          city_to заполнен некорректно. Возврат ошибки
            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
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
                        {"pickup_type", "0"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "pickup_type:Неправильный тип забора;");
            
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator.json",
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
                        {"pickup_type", "7"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "pickup_type:Неправильный тип забора;");
        }
    }
}