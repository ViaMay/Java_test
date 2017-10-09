using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserDocumentsListTests : SendOrdersBasePage
    {
        [Test, Description("Запрос документов на генерацию и проверка статуса")]
        public void DocumentsTest()
        {
            var pickupCompanyId = GetCompanyIdByName(companyPickupName);
            var userKey = GetUserKeyByName(userNameAndPass);
            var keyShopPublic = GetShopKeyByName(userShopName);
            var usersWarehousId = GetWarehouseIdByName(userWarehouseName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);


            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");

            var responseCreateOrders2 = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "true"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                });
            Assert.IsTrue(responseCreateOrders2.Success, "Ожидался ответ true на отправленный запрос POST по API");

            ProcessIOrders();

            SetOutordersNumber(responseCreateOrders.Response.OrderId);
            SetOutordersNumber(responseCreateOrders2.Response.OrderId);

//            генерация документов
            var responseDocumentsRequest =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_request.json",
                new NameValueCollection
                {
                {"order_id", responseCreateOrders.Response.OrderId + "," + responseCreateOrders2.Response.OrderId }
                });

            Assert.IsTrue(responseDocumentsRequest.Success, "Ожидался ответ true на отправленный запрос Get по API");
            Assert.IsFalse(responseDocumentsRequest.Response.Completed);

            responseDocumentsRequest =
                (ApiResponse.ResponseDocumentsRequest)apiRequest.GET("api/v1/" + keyShopPublic + "/documents_request.json",
                new NameValueCollection
                {
                {"order_id", responseCreateOrders.Response.OrderId + "," + responseCreateOrders2.Response.OrderId }
                });

            Assert.IsTrue(responseDocumentsRequest.Success, "Ожидался ответ true на отправленный запрос Get по API");
            Assert.IsFalse(responseDocumentsRequest.Response.Completed);

//            спим минуту ждем генерации
            WaitDocuments();

            var responseDocumentsList =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json", 
                 new NameValueCollection
                 {
                      {"_create_date", nowDate.ToString("dd.MM.yyyy", CultureInfo.InvariantCulture) },
                      {"warehouse_id", usersWarehousId }
                 });

            Assert.IsTrue(responseDocumentsList.Success, "Ожидался ответ true на отправленный запрос Get по API");
            foreach (var document in responseDocumentsList.Response)
            {
                Assert.AreEqual(document.Warehouse, usersWarehousId);
                Assert.That(document.CreateDate.Contains(nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture)));
                Assert.That(document.File.Contains("!Link:"));
                Assert.That(document.File.Contains(".pdf"));
                if (document.Type == "2")
                {
                    Assert.AreEqual(document.IdPickupCompany, pickupCompanyId);
                    Assert.AreNotEqual(document.IdDeliveryCompany, deliveryCompanyId);
                }
                if (document.Type == "3")
                {
                    Assert.AreEqual(document.IdDeliveryCompany, deliveryCompanyId);
                    Assert.AreNotEqual(document.IdPickupCompany, pickupCompanyId);
                }
            }

            responseDocumentsList =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                      {"_create_date", nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                      {"warehouse_id", usersWarehousId },
                      {"type", "1" }
                 });

            Assert.IsTrue(responseDocumentsList.Success, "Ожидался ответ true на отправленный запрос Get по API");
            foreach (var document in responseDocumentsList.Response)
                Assert.That(document.Type.Contains("1"));

            responseDocumentsList =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                      {"_create_date", nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                      {"warehouse_id", usersWarehousId },
                      {"type", "2" }
                 });

            Assert.IsTrue(responseDocumentsList.Success, "Ожидался ответ true на отправленный запрос Get по API");
            foreach (var document in responseDocumentsList.Response)
                Assert.That(document.Type.Contains("2"));

            responseDocumentsList =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                      {"_create_date", nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                      {"warehouse_id", usersWarehousId },
                      {"type", "3" }
                 });

            Assert.IsTrue(responseDocumentsList.Success, "Ожидался ответ true на отправленный запрос Get по API");
            foreach (var document in responseDocumentsList.Response)
                Assert.That(document.Type.Contains("3"));

            responseDocumentsList =
                (ApiResponse.ResponseDocumentsList)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                     { "_create_date", "12.12.2011" },
                     {"warehouse_id", usersWarehousId },
                 });
            Assert.AreEqual(responseDocumentsList.Response.Count(), 0);

            var responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                     { "type", "5" } ,
                     {"warehouse_id", usersWarehousId },
                 });
            Assert.AreEqual(responseFail.Response.ErrorText, "type:type может содержать один из доступных вариантов;");
            

            responseFail =
                (ApiResponse.ResponseFail)apiRequest.GET("api/v1/cabinet/" + userKey + "/documents_list.json",
                 new NameValueCollection
                 {
                     { "type", "1" } ,
                     {"_create_date", nowDate.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture) },
                 });
            Assert.AreEqual(responseFail.Response.ErrorText, "warehouse_id:warehouse id обязательно к заполнению;");
        }
    }
}