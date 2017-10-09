using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CalculatorPriceAllTests : SendOrdersBasePage
    {
        [Test, Description("Расчитать цену самовывоза")]
        public void CalculatorPriceAllTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            var responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                    new NameValueCollection
                    {
                        {"city_to", "151184"},
                        {"dimension_side1", "7,8"},
                        {"dimension_side2", "6,6"},
                        {"dimension_side3", "5,3"},
                        {"weight", "4,1"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 2);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Response[1].DeliveryCompanyName, companyName);

            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                    new NameValueCollection
                    {
                        {"city_to", "Москва"},
                        {"dimension_side1", "7,8"},
                        {"dimension_side2", "6,6"},
                        {"dimension_side3", "5,3"},
                        {"weight", "4,1"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "City not found (city to)");

//            Одна из сторон равна нулю. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                new NameValueCollection
                    {
                        {"city_to", "151184"},
                        {"dimension_side1", "0"},
                        {"dimension_side2", "6,6"},
                        {"dimension_side3", "5,3"},
                        {"weight", "4,1"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });

            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "dimension_side1:dimension side  должно быть в промежутке от 0.00099 до 100000;");

//           declared_price равна нулю. Возврат ошибки
            responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/calculator_all.json",
                new NameValueCollection
                    {
                        {"dimension_side1", "7,8"},
                        {"dimension_side2", "6,6"},
                        {"dimension_side3", "5,3"},
                        {"weight", "4,1"},
                        {"payment_price", "1000"}
                    });
            Assert.IsFalse(responseFailCalculator.Success);
            Assert.AreEqual(responseFailCalculator.Response.ErrorText, "declared price обязательно к заполнению");

        }
    }
}