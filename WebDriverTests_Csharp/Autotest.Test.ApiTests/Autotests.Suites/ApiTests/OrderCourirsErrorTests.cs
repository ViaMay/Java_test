using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderCourirsErrorTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа курьерской")]
        public void OrderCourirsErrorTest()
        {

            string keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

//            delivery_company = ""
            var responseCreateFailOrder = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", ""},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
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
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.ErrorText, "Company not found");
            
//            Вес пустой weight=0
            responseCreateFailOrder = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "0"},
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
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.ErrorText, "weight:weight должно быть в промежутке от 0.00099 до 10000;");
        
//            to_city=""
            responseCreateFailOrder = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                    new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "2"},
		        {"to_city", ""},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", userShopName},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
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
            Assert.IsFalse(responseCreateFailOrder.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFailOrder.Response.ErrorText, "to_city:to city должно быть в промежутке от 0 до 9223372036854775807;");
        }
    }
}