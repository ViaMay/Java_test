using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class CalculatorProAllTests : ConstVariablesBase
    {
        [Test]
        public void CalculatorTest()
        {
            var responseCalculator =
                (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                    new NameValueCollection
                    {
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

        [Test]
        public void CalculatorErrorTest()
        {
            var responseFailCalculator =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                new NameValueCollection
                    {
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
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                new NameValueCollection
                    {
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
 
    }
}