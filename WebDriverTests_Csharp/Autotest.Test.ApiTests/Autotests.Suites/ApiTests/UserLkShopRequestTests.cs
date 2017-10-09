using System.Collections.Specialized;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class UserLkShopRequestTests : SendOrdersBasePage
    {
        [Test, Description("логирование под пользователем")]
        public void Test()
        {
            var companyId = GetCompanyIdByName(companyName);

            var responseLkAuth01 =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });
            
            var response =
                (ApiResponse.ResponseObjectsList) apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_request.json",
                new NameValueCollection
                {
                    {"method", "get_shops"},
                });
            Assert.IsTrue(response.Success);

            var responseCompanyTerm =
                (ApiResponse.ResponseCompanyTerm)apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_shop_request.json",
                new NameValueCollection
                {
                    {"method", "company_term"},
                    {"api_shop_id", response.Response[0].Id},
                    {"company", companyId},
                });
            Assert.IsTrue(response.Success);
            Assert.IsTrue(responseCompanyTerm.Success);
            Assert.AreEqual(responseCompanyTerm.Response.Id, companyId);
        }

        [Test, Description("логирование под пользователем")]
        public void TestInfo()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var idShop = GetShopIdByName(userShopName);
            var deliveryCompanyId = GetCompanyIdByName(companyName);

            var responseCreateOrders = (ApiResponse.ResponseAddOrder)apiRequest.POST("api/v1/" + keyShopPublic + "/order_create.json",
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
		        {"confirmed", "false"},
		        {"weight", "4"},
                {"declared_price", "100"},
		        {"payment_price", "300"},
		        {"to_name", "Ургудан Рабат Мантов"},
		        {"to_street", "Барна"},
		        {"to_house", "3a"},
		        {"to_flat", "12"},
		        {"to_phone", "79999999999"},
		        {"to_add_phone", "71234567890"},
		        {"to_email", userNameAndPass},
		        {"goods_description", "Памперс"},
		        {"metadata", "[{\"name\": \"Описание\", \"article\": \"Артикул\", \"count\": 1}]"},
		        {"items_count", "2"},
		        {"is_cargo_volume", "true"},
		        {"order_comment", "order_comment"}
                });
            Assert.IsTrue(responseCreateOrders.Success, "Ожидался ответ true на отправленный запрос POST по API");
            
            var responseLkAuth01 =
                (ApiResponse.ResponseLkAuth)apiRequest.GET("api/v1/cabinet/lk_auth.json",
                new NameValueCollection
                {
                    {"login", userNameAndPass},
                    {"password", userNameAndPass}
                });

            var responseOrderInfo =
                (ApiResponse.ResponseOrderInfo)apiRequest.GET("api/v1/cabinet/" + responseLkAuth01.Response.Token + "/lk_shop_request.json",
                new NameValueCollection
                {
                    {"api_shop_id", idShop},
                    {"method", "order_info"},
                    {"id_param", responseCreateOrders.Response.OrderId},
                });
            Assert.IsTrue(responseOrderInfo.Success);
            Assert.AreEqual(responseOrderInfo.Response.ShopRefnum, userShopName);
        }
    }
}