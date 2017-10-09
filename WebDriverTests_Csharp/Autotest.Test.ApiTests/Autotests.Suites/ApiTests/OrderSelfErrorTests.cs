using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class OrderSelfErrorTests : SendOrdersBasePage
    {
        [Test, Description("Создание заказа на самовывоз,ошибки при создании")]
        public void OrderSelfErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);
            var deliveryPoinId = GetDeliveryPointIdByName(deliveryPointName);

            // ошибка в delivery_point
            var responseCreateFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", "123456789"},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "4"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                });
            Assert.IsFalse(responseCreateFail.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "Город получения обязательно к заполнению");

            // ошибка в dimension_side1 = 0
            responseCreateFail = (ApiResponse.ResponseFail)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
                new NameValueCollection
                {
                {"api_key", keyShopPublic},
		        {"type", "1"},
		        {"delivery_point", deliveryPoinId},
		        {"to_city", "151184"},
		        {"delivery_company", "" + deliveryCompanyId},
		        {"shop_refnum", ""},
		        {"dimension_side1", "0"},
		        {"dimension_side2", "4"},
		        {"dimension_side3", "4"},
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_phone", "9999999999"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"}
                });
            Assert.IsFalse(responseCreateFail.Success, "Ожидался ответ Fail на отправленный запрос POST по API");
            Assert.AreEqual(responseCreateFail.Response.ErrorText, "dimension_side1:dimension side  должно быть в промежутке от 0.00099 до 100000;");
        }
    }
}