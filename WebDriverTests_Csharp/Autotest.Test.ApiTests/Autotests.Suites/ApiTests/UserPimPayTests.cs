using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPimPayTests : GetConstantBasePage
    {
        [Test, Description("PimPay")]
        public void UserPimPayTest()
        {
            var userKey = GetUserKeyByName("bryleva12@mailforspam.com");
            //            Настройка системы в стартовую позицю
            apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_enable.json");
            apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_activate.json");
            apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_disable.json");
            apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_deactivate.json");

            var responseInfoFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_disable.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Отключение финансирования возможно только при статусе Финансирование подключено");
            
            responseInfoFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_deactivate.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Финансирование возможно отключить только при статусе Финансирование приостановлено");

            responseInfoFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_enable.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Данный ИНН уже был активирован в PimPay");

            var responseInfo = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_activate.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Финансирование включено");

            var responseStatus = (ApiResponse.ResponsePimPayStatus)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_pimpay_status.json");
            Assert.IsTrue(responseStatus.Success);
            Assert.AreEqual(responseStatus.Response.UserTitle, "Финансирование подключено");
            
            responseInfoFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_deactivate.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Финансирование возможно отключить только при статусе Финансирование приостановлено");
            
            responseInfo = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_disable.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Финансирование приостановлено. Финансирование через PimPay будет отключено только после того, как будут завершены все текущие финансовые операции по данной учетной записи. Статус финансирования будет обновлен автоматически позже.");
            
            responseStatus = (ApiResponse.ResponsePimPayStatus)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_pimpay_status.json");
            Assert.IsTrue(responseStatus.Success);
            Assert.AreEqual(responseStatus.Response.UserTitle, "Финансирование приостановлено");

            responseInfoFail = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_activate.json");
            Assert.IsFalse(responseInfoFail.Success);
            Assert.AreEqual(responseInfoFail.Response.ErrorText, "Включение финансирования возможно только при статусе Финансирование отключено");

            responseInfo = (ApiResponse.ResponseMessage)apiRequest.GET("api/v1/cabinet/" + userKey + "/pimpay_deactivate.json");
            Assert.IsTrue(responseInfo.Success);
            Assert.AreEqual(responseInfo.Response.Message, "Финансирование отключено");

            responseStatus = (ApiResponse.ResponsePimPayStatus)apiRequest.GET("api/v1/cabinet/" + userKey + "/get_pimpay_status.json");
            Assert.IsTrue(responseStatus.Success);
            Assert.AreEqual(responseStatus.Response.UserTitle, "Финансирование отключено");
        }
    }
}