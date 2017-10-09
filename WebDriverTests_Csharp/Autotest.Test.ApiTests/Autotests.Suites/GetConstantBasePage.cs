using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests
{
    public class GetConstantBasePage : ConstVariablesBase
    {
        public string GetShopKeyByName(string shopName)
        {
            var responseLkAuth =
                (ApiResponse.ResponseLkAuth) apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });

            var response =
                (ApiResponse.ResponseObjectsList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                        new NameValueCollection
                        {
                            {"method", "get_shops"},
                        });
            Assert.IsTrue(response.Success);
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Name == shopName)
                    return response.Response[i].PublicKey;
            }
            return "";
        }

        public string GetDeliveryPointIdByName(string pointName)
        {
            var responseDeliveryPoints = (ApiResponse.ResponseDeamonPoints)apiRequest.GET("tmpdaemon/",
                   new NameValueCollection
                    {
                        {"_action", "delivery_points"},
                        {"companies", GetCompanyIdByName(companyName)},
                    });
            Assert.IsTrue(responseDeliveryPoints.Success);
            for (var i = 0; i < responseDeliveryPoints.Points.Count(); i++)
            {
                if (responseDeliveryPoints.Points[i].Name == pointName)
                    return responseDeliveryPoints.Points[i].Id;
            }
            return "";
        }

        public string GetWarehouseIdByName(string warehouseName)
        {
            var responseLkAuth =
                (ApiResponse.ResponseLkAuth) apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });

            var response =
                (ApiResponse.ResponseObjectsList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                        new NameValueCollection
                        {
                            {"method", "get_warehouses"},
                        });
            Assert.IsTrue(response.Success);
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Name == warehouseName)
                    return response.Response[i].Id;
            }
            return "";
        }

        public string GetShopIdByName(string shopName)
        {
            var responseLkAuth =
                (ApiResponse.ResponseLkAuth) apiRequest.GET("api/v1/cabinet/lk_auth.json",
                    new NameValueCollection
                    {
                        {"login", userNameAndPass},
                        {"password", userNameAndPass}
                    });

            var response =
                (ApiResponse.ResponseObjectsList)
                    apiRequest.GET("api/v1/cabinet/" + responseLkAuth.Response.Token + "/lk_request.json",
                        new NameValueCollection
                        {
                            {"method", "get_shops"},
                        });
            Assert.IsTrue(response.Success);
            for (var i = 0; i < response.Response.Count(); i++)
            {
                if (response.Response[i].Name == shopName)
                    return response.Response[i].Id;
            }
            return "";
        }

        public void CacheFlush()
        {
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/cache_flush.json");
            Assert.IsTrue(response.Success);
        }

        public void ProcessIOrders()
        {
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/process_i_orders.json");
            Assert.IsTrue(response.Success);
        }

        public string GetUserKeyByName(string userName)
        {
            var response =apiRequest.GET("admin/api/v1/" + adminKey + "/get_user_key_by_name.json",
                    new NameValueCollection
                    {
                        {"email", userName},
                    });
            if (response.Success)
            {
                var response2 = (ApiResponse.ResponsePublicKey)response;
                return response2.Response.PublicKey;
            }
            return "";
        }

        public string GetUserIdByName(string userName)
        {
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/get_user_key_by_name.json",
                    new NameValueCollection
                    {
                        {"email", userName},
                    });
            if (response.Success)
            {
                var response2 = (ApiResponse.ResponsePublicKey)response;
                return response2.Response.Id;
            }
            return "";
        }

        public string GetCompanyIdByName(string name)
        {
            var response =
                (ApiResponse.ResponseCompanies) apiRequest.GET("admin/api/v1/" + adminKey + "/get_companies_by_name.json",
                    new NameValueCollection
                    {
                        {"company_name", name},
                    });
            Assert.IsTrue(response.Success);
            return response.Response.Companies[0].Id;
        }

        public void SetCompanyEnabledBbarcodPull(string id, string value)
        {
            apiRequest.GET("admin/api/v1/" + adminKey + "/set_company_enabled_barcode_pull.json",
                    new NameValueCollection
                    {
                        {"id", id},
                        {"enable", value},
                    });
        }
        public void SetUserBarcodeLimit(string id, string value)
        {
            apiRequest.GET("admin/api/v1/" + adminKey + "/set_user_barcode_limit.json",
                    new NameValueCollection
                    {
                        {"id", id},
                        {"limit", value},
                    });
        }

        public void DeleteShopByName(string shopName)
        {
            var id = GetShopIdByName(shopName);
            if (id != "")
            {
                var responseShopDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/shop_delete/" + id + ".json");
                Assert.IsTrue(responseShopDelete.Success);
            }
        }

        public void DeleteWarehouseByName(string name)
        {
            var id = GetWarehouseIdByName(name);
            if (id != "")
            {
                var responseDelete =
                    (ApiResponse.ResponseMessage)
                        apiRequest.POST("api/v1/testing/" + adminKey + "/warehouse_delete/" + id + ".json");
                Assert.IsTrue(responseDelete.Success);
            }
        }

        public string GetCompanyWarehouseByName(string name)
        {
            //           Получаем id склада компании
            var responseObjectsList = (ApiResponse.ResponseMessage)apiRequest.POST("admin/api/v1/" + adminKey + "/get_companywarehouse_by_name.json",
                new NameValueCollection
                {
                {"name", name}
                });
            return responseObjectsList.Response.Id;
        }

        public string GetCompanyWarehouseByIdCompany(string id)
        {
            //           Получаем id склада компании
            var responseObjectsList = (ApiResponse.ResponseDocumentsList)apiRequest.POST("admin/api/v1/" + adminKey + "/get_companywarehouses_by_idcompany.json",
                new NameValueCollection
                {
                {"id", id}
                });
            return responseObjectsList.Response[0].Id;
        }


        public void SetOutordersNumber(string id)
        {
            var response = apiRequest.GET("admin/api/v1/" + adminKey + "/set_order_status_in_warehouse.json",
                   new NameValueCollection
                    {
                        {"id", id},
                    });
            Assert.IsTrue(response.Success);
        }
    }
}