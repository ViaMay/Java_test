using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupCompaniesTests : SendOrdersBasePage
    {
        [Test, Description("pickup_companies.json Получить список компаний забора")]
        public void UserPickupCompaniesTest()
        {
            var userKey = GetUserKeyByName(userNameAndPass);

//            получение шрикодов
            var responsePickupCompanies =
                (ApiResponse.ResponsePickupCompanies)apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_companies.json");

            var pickupCompanyRow = FindRowByName(companyPickupName, responsePickupCompanies);
            Assert.AreEqual(pickupCompanyRow.Name, companyPickupName);
            Assert.AreEqual(pickupCompanyRow.Id, GetCompanyIdByName(companyPickupName));
            Assert.AreEqual(pickupCompanyRow.PickupType, "3");
            Assert.AreEqual(pickupCompanyRow.PickupTypeName, "Единый забор Курьером");
            Assert.AreEqual(pickupCompanyRow.Warehouses, null);

            pickupCompanyRow = FindRowByName(companyPickupName + "_2", responsePickupCompanies);
            Assert.AreEqual(pickupCompanyRow.Name, companyPickupName + "_2");
            Assert.AreEqual(pickupCompanyRow.Id, GetCompanyIdByName(companyPickupName + "_2"));
            Assert.AreEqual(pickupCompanyRow.PickupType, "2");
            Assert.AreEqual(pickupCompanyRow.PickupTypeName, "Самостоятельный подвоз до склада Консолидации");
            Assert.AreEqual(pickupCompanyRow.Warehouses[0].Name, "test_Pickup_2_Warehouse");
            Assert.AreEqual(pickupCompanyRow.Warehouses[0].CityId, "434");
            Assert.AreEqual(pickupCompanyRow.Warehouses[0].CityName, "Челябинск");
        }

        private ApiResponse.MessagePickupCompanies FindRowByName(string pickupCompanyName, ApiResponse.ResponsePickupCompanies responsePickupCompanies)
        {
            for (var i = 0; i < responsePickupCompanies.Response.Count(); i++)
            {
                if (responsePickupCompanies.Response[i].Name == pickupCompanyName)
                    return responsePickupCompanies.Response[i];
            }
            throw new Exception(string.Format("не найден город с name {0} в списке всех городов", pickupCompanyName));
        }
    }
}