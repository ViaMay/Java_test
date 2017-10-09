using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CalculatorProAllTests : SendOrdersBasePage
    {
        [Test]
        public void CalculatorTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);

//            Все поля заполнены. Расчитать цену курьерки для Магазина у которого только один тип доставки  указана ТК забора
            var responseCalculator =
                (ApiResponse.ResponseCalculation) apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                    new NameValueCollection
                    {
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 2);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Response[1].DeliveryCompanyName, companyName);

            responseCalculator =
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
            Assert.AreEqual(responseCalculator.Response.Count(), 2);
            Assert.AreEqual(responseCalculator.Response[0].DeliveryCompanyName, companyName);
            Assert.AreEqual(responseCalculator.Response[1].DeliveryCompanyName, companyName);

            var responseCalculator1 =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
                    new NameValueCollection
                    {
                        {"city_to", "151184"},
                        {"dimension_side1", "4"},
                        {"dimension_side2", "4"},
                        {"dimension_side3", "4"},
                        {"weight", "4"},
                        {"declared_price", "1000"},
                        {"payment_price", "1000"},
                        {"pickup_type", "1"}
                    });
            Assert.AreEqual(responseCalculator1.Response.Count(), 0);

//            Магазин у которого 4 типов доставки. у двух типов прописаны ТКзабора
//            поиск по типу который есть и у которого явно указана ТК забора

            keyShopPublic = GetShopKeyByName("test_userShops_via_Pro");
            responseCalculator =
               (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
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
                        {"pickup_type", "1"}
                    });

            foreach (var row in responseCalculator.Response)
            {
                Assert.AreEqual(row.PickupType, "1");
            }
            
            responseCalculator =
               (ApiResponse.ResponseCalculation)apiRequest.GET("api/v1/" + keyShopPublic + "/pro_calculator_all.json",
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
                        {"pickup_company", GetCompanyIdByName(companyPickupName)}
                    });
            Assert.AreEqual(responseCalculator.Response.Count(), 2);
            Assert.AreEqual(responseCalculator.Response[0].PickupCompany, GetCompanyIdByName(companyPickupName));
            Assert.AreEqual(responseCalculator.Response[1].PickupCompany, GetCompanyIdByName(companyPickupName));
            Assert.AreEqual(responseCalculator.Response[0].PickupType, "3");
            Assert.AreEqual(responseCalculator.Response[1].PickupType, "3");
            
//            запрос за подключенный тип без явно указанной ТК забора в настройках магазина
            try
            {
                responseCalculator =
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
                        {"pickup_type", "4"}
                    });
                foreach (var row in responseCalculator.Response)
                {
                    Assert.AreEqual(row.PickupType, "4");
                }

//            тот же запрос только теперь указываем ТК забора в запросе
                var responseCalculator2 =
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
                        {"pickup_type", "4"},
                        {"pickup_company", responseCalculator.Response[0].PickupCompany}
                    });
                foreach (var row in responseCalculator2.Response)
                {
                    Assert.AreEqual(row.PickupCompany, responseCalculator.Response[0].PickupCompany);
                }
            }
            catch (System.InvalidCastException) {}
        }

        [Test]
        public void CalculatorErrorTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
// Возврат ошибки
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