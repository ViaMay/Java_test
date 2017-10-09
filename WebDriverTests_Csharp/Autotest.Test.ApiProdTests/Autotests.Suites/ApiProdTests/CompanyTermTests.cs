using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiProdTests
{
    public class CompanyTermTests : ConstVariablesBase
    {
        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermTest()
        {
//            Term  = 0,  и false
            var responseCompanyTerm =
               (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/" + keyShopPublic + "/company_term.json",
                   new NameValueCollection
                    {
                        {"company", companyId}
                    });
            Assert.IsTrue(responseCompanyTerm.Success);
        }

        [Test, Description("Получить информацию о сроке хранения на ПВЗ для определенной компании")]
        public void CompanyTermErrorTest()
        {
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