using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class ShopDeliveryCompanyTests : SendOrdersBasePage
    {
        [Test, Description("Получить список доступных компаний доставки если указана компания забора и если не указана")]
        public void ShopDeliveryCompanyTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            var companyId = GetCompanyIdByName(companyName);

//           Проверка если компания забора указана
            var responseDeliveryCompanу = (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/" + keyShopPublic + "/shop_delivery_companies.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].Id, companyId);
            Assert.AreEqual(responseDeliveryCompanу.Response[0].Name, companyName);

//            проверка если нету компании забора
             //            удаление склад если он был до этого

            var userKey = GetUserKeyByName(userNameAndPass);
            DeleteWarehouseByName(userWarehouseName + "_ApiCompanies");
//            склад создаем не в москве
            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/warehouse_create.json",
                new NameValueCollection
                {
                    {"name", userWarehouseName + "_ApiCompanies"},
                    {"flat", "138"},
                    {"city", "416"},
                    {"contact_person", "contact_person"},
                    {"contact_phone", "contact_phone"},
                    {"contact_email", userNameAndPass},
                    {"schedule", "schedule"},
                    {"street", "street"},
                    {"house", "house"}
                }
                );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");
            DeleteShopByName(userShopName + "_ApiCompanies");
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiCompanies"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Квебек"}
                }
                );
            Assert.IsTrue(responseShop.Success, "Ожидался ответ true на отправленный запрос POST по API");

//            отправляем запрос так что в магазин с пустой компанией забора
            responseDeliveryCompanу = (ApiResponse.ResponseCompaniesOrShops)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json");
            Assert.IsTrue(responseDeliveryCompanу.Success);
            FindRowByName("DPD", responseDeliveryCompanу);
        }

        [Test, Description("Получить список доступных компаний доставки")]
        public void ShopDeliveryCompanyErrorTest()
        {
            var keyShopPublic = GetShopKeyByName(userShopName);
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminCompany");

            var responseWarehouse =
                (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/" + keyShopPublic + "/warehouse_create.json",
                    new NameValueCollection
                    {
                        {"name", userWarehouseName + "_ApiAdminCompany"},
                        {"flat", "138"},
                        {"city", "151184"},
                        {"contact_person", "contact_person"},
                        {"contact_phone", "contact_phone"},
                        {"contact_email", userNameAndPass},
                        {"schedule", "schedule"},
                        {"street", "street"},
                        {"house", "house"}
                    }
                    );
            Assert.IsTrue(responseWarehouse.Success, "Ожидался ответ true на отправленный запрос POST по API");
            DeleteShopByName(userShopName + "_ApiAdminCompany");

//            Создание магазина
            var userKey = GetUserKeyByName(userNameAndPass);
            var responseShop = (ApiResponse.ResponseAddObject)apiRequest.POST("api/v1/cabinet/" + userKey + "/shop_create.json",
                new NameValueCollection
                {
                    {"name", userShopName + "_ApiAdminCompany"},
                    {"warehouse", responseWarehouse.Response.Id},
                    {"address", "Москва"}
                }
                );
            Assert.IsTrue(responseShop.Success);

            //            удаление склада
            DeleteWarehouseByName(userWarehouseName + "_ApiAdminCompany");
            CacheFlush();

//            Отправляем запрос указываем магазин у которого удален склад.
            var responseDeliveryCompanу = (ApiResponse.ResponseFail)apiRequest.GET("api/v1/" + responseShop.Response.Key + "/shop_delivery_companies.json");
            Assert.IsFalse(responseDeliveryCompanу.Success, "Ожидался ответ false на отправленный запрос POST по API");
            Assert.AreEqual(responseDeliveryCompanу.Response.ErrorText, "Ни у одной из компаний Забора нет забора в городе, к которому привязан склад.");
        }

        void FindRowByName(string name, ApiResponse.ResponseCompaniesOrShops responseDeliveryCompanу)
        {
            for (var i = 0; i < responseDeliveryCompanу.Response.Count(); i++)
            {
                if (responseDeliveryCompanу.Response[i].Name.Contains(name))
                    return;
            }
            throw new Exception(string.Format("не найдена компания с именем {0} в списке всех компаний доставки", name));
        }
    }
}