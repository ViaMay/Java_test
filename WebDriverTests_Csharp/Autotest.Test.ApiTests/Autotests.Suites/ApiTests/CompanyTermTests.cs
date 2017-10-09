using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class CompanyTermTests : SendOrdersBasePage
    {
        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);
            string companyId = GetCompanyIdByName(companyPickupName);

//            Term  = 0,  и false
            var responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", companyId}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
            Assert.AreEqual(responseCompanyTerm.Response.Term, "0");
            Assert.AreEqual(responseCompanyTerm.Response.Prolongation, false);
            
            //            Term  = 12 и true
            companyId = GetCompanyIdByName(companyName);
            responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", companyId}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
            Assert.AreEqual(responseCompanyTerm.Response.Term, "12");
            Assert.AreEqual(responseCompanyTerm.Response.Prolongation, true);
        }

        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermErrorTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);

            var responseFail =
               (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", "company"}
                    });
            Assert.IsFalse(responseFail.Success);
            Assert.AreEqual(responseFail.Response.ErrorText, "Company not found");

        }
    }
}