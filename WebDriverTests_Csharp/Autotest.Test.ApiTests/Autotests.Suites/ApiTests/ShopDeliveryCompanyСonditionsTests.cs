using System;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class ShopDeliveryCompanyСonditionsTests : SendOrdersBasePage
    {
        [Test, Description("Получить информацию о переводе наличных денежных средств")]
        public void ShopDeliveryCompanyСonditionsTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyId = GetCompanyIdByName(companyName);

//           Получить информацию о переводе наличных денежных средств
            var responseDeliveryCompanу = (ApiResponse.ResponseCompaniesСonditions)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_delivery_companies_conditions.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].CompanyId, companyId);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].PaymentTime, "0");
            Assert.AreEqual(responseDeliveryCompanу.Response[0].NppCommission, "-");
            Assert.AreEqual(responseDeliveryCompanу.Response[0].CardCommission, "-");
            Assert.AreEqual(responseDeliveryCompanу.Response[0].DeclaredCommission, "-");
            Assert.AreEqual(responseDeliveryCompanу.Response[0].AppliedFromAmount, "0");

            responseDeliveryCompanу = (ApiResponse.ResponseCompaniesСonditions)apiRequest.GET("api/v1/" + "33665aec58c959e411eea7d3e070f727" + "/shop_delivery_companies_conditions.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
            var row = FindRowByName("DPD Consumer", responseDeliveryCompanу);
            Assert.AreEqual(row.CompanyId, "23");
            Assert.AreEqual(row.CompanyName, "DPD Consumer");
//            Assert.AreEqual(responseDeliveryCompanу.Response[1].PaymentTime, "4");
            Assert.IsTrue(row.NppCommission.Contains("%"));
//            Assert.AreEqual(responseDeliveryCompanу.Response[1].CardCommission, "-");
            Assert.IsTrue(row.DeclaredCommission.Contains("%"));
//            Assert.AreEqual(responseDeliveryCompanу.Response[1].AppliedFromAmount, "0");
//            Assert.AreEqual(responseDeliveryCompanу.Response[1].MinAmount, "57.2");
        }


        private ApiResponse.MessageCompaniesСonditions FindRowByName(string name, ApiResponse.ResponseCompaniesСonditions responseCompaniesСonditions)
        {
            for (var i = 0; i < responseCompaniesСonditions.Response.Count(); i++)
            {
                if (responseCompaniesСonditions.Response[i].CompanyName == name)
                    return responseCompaniesСonditions.Response[i];
            }
            throw new Exception(string.Format("не найден город с Id {0} в списке всех городов", name));
        }
    }
}