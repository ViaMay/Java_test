using System.Collections.Specialized;
using System.Globalization;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserPickupCreateTests : SendOrdersBasePage
    {
        [Test, Description("pickup_create.json Создать забор")]
        public void UserPickupCreateTest()
        {
            var idPickupWarehouse = GetCompanyWarehouseByName(companyPickupNameWarehouse);
            var userKey = GetUserKeyByName(userNameAndPass);
            var pickupCompanyId = GetCompanyIdByName(companyPickupName + "_2");
            var pickupDate = nowDate.AddDays(2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";

            var responseCreate =
                (ApiResponse.ResponseAddOrder)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.IsTrue(responseCreate.Success, "Ожидался ответ true на отправленный запрос GET по API");
            Assert.AreEqual(responseCreate.Response.Message, "Успешно создана заявка на забор №" + responseCreate.Response.OrderId);

            //  запрос списка заказов забора проверяем что закрыт и создан новый
            var responseOrderListRequest =
                (ApiResponse.ResponseOrdersList)
                    apiRequest.GET("api/v1/cabinet/" + userKey + "/pickup_list.json",
                        new NameValueCollection
                        {
                            {"limit", "10"},
                        });
            Assert.IsTrue(responseOrderListRequest.Success, "Ожидался ответ true на отправленный запрос GET по API");

            foreach (var row in responseOrderListRequest.Response.Rows)
            {
                if (row.PickupId == responseCreate.Response.OrderId)
                {
                    Assert.AreEqual(row.PickupDate, pickupDate);
                    Assert.AreEqual(row.Status, "10");
                    Assert.AreEqual(row.Creator, "user");
                    Assert.AreEqual(row.PickupMode, "Подвоз");
                    Assert.AreEqual(row.PickupDate, pickupDate);
                    Assert.AreEqual(row.PickupTimeFrom, "0:01");
                    Assert.AreEqual(row.PickupTimeTo, "23:59");
                    Assert.AreEqual(row.PickupCompanyId, pickupCompanyId);
                    Assert.AreEqual(row.PickupCompanyName, companyPickupName + "_2");
                    Assert.AreEqual(row.PickupWarehouseId, idPickupWarehouse);
                    Assert.AreEqual(row.PickupWarehouseName, "test_Pickup_2_Warehouse");
                    Assert.AreEqual(row.PickupWarehouseAddress, "ул.Улица, дом Дом, офис(квартира) 123");
                    Assert.AreEqual(row.PickupWarehouseType, "2");
                    Assert.AreEqual(row.PickupPenalty, "0");
                    Assert.AreEqual(row.PickupPrice, "");
                }
            }

//           проверяем валидации
            var responseCreateFail =
                (ApiResponse.ResponseFail)
                    apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
                        new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", pickupDate}, 
                        });
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "warehouse:warehouse обязательно к заполнению;");

             responseCreateFail =
               (ApiResponse.ResponseFail)
       apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
           new NameValueCollection
                        {
                            {"warehouse", idPickupWarehouse},
                        });
             Assert.AreEqual(responseCreateFail.Response.ErrorText, "pickup_company:pickup company обязательно к заполнению;pickup_date:pickup date обязательно к заполнению;");
            
            responseCreateFail =
               (ApiResponse.ResponseFail)
       apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
           new NameValueCollection
                        {
                            {"pickup_company", "a"},
                            {"pickup_date", "a"},
                            {"warehouse", "a"},
                        });
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "pickup_company:pickup company должно быть в промежутке от 0 до 9223372036854775807;warehouse:warehouse должно быть в промежутке от 0 до 9223372036854775807;");

            pickupDate = nowDate.AddDays(-2).ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) + " 11:21:31";
            responseCreateFail =
              (ApiResponse.ResponseFail)
      apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
          new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date",pickupDate},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "Не соблюден 3х часовой интервал или компания забора не работает в этот день");
            
            responseCreateFail =
              (ApiResponse.ResponseFail)
      apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
          new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"pickup_date", "в"},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "pickup_date:pickup date должно быть датой;");
 
            responseCreateFail =
              (ApiResponse.ResponseFail)
      apiRequest.POST("api/v1/cabinet/" + userKey + "/pickup_create.json",
          new NameValueCollection
                        {
                            {"pickup_company", pickupCompanyId},
                            {"warehouse", idPickupWarehouse},
                        });
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "pickup_date:pickup date обязательно к заполнению;");
        }
    }
}