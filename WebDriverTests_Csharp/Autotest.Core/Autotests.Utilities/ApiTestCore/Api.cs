using System;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Autotests.Utilities.ApiTestCore
{
    public class Api
    {
        private readonly string ApplicationBaseUrl;

        public Api(string value)
        {
            ApplicationBaseUrl = value;
        }

        public ApiResponse.TResponse GET(string url)
        {
            return GET(url, new NameValueCollection {});
        }
        public ApiResponse.TResponse POST(string url)
        {
            return POST(url, new NameValueCollection {});
        }

        public ApiResponse.TResponse GET(string url, NameValueCollection data)
        {
            using (var client = new WebClient())
            {

                string dataString = "";
                foreach (string key in data.Keys)
                {
                    dataString = dataString + key + "=" + data[key] + "&";
                }
                if (dataString.Count() != 0) dataString = dataString.Remove(dataString.Count() - 1);

//                Авторизация на сервере если в имени урла есть @
                if (ApplicationBaseUrl.Contains("@"))
                {
                    string[] words = ApplicationBaseUrl.Split(new char[] {':', '@'});
                    client.Credentials = new NetworkCredential(words[0], words[1]);
                }
                
                var response = client.DownloadString("http://" + ApplicationBaseUrl + "/" + url + "?" + dataString);
                return JsonSerializer(response);

            }
        }

        public ApiResponse.TResponse POST(string url, NameValueCollection data)
        {
            var strings = data.GetValues("orders");
            if (strings != null)
            {
                var respons = SendPostPequestWhihtAttach(url, data);
                return JsonSerializer(respons);
            }
            
            using (var client = new WebClient())
            {
//                Авторизация на сервере если в имени урла есть @
                if (ApplicationBaseUrl.Contains("@"))
                {
                    string[] words = ApplicationBaseUrl.Split(new [] { ':', '@' });
                    client.Credentials = new NetworkCredential(words[0], words[1]);
                }
                byte[] responseJson = client.UploadValues("http://" + ApplicationBaseUrl + "/" + url, data);
                var respons = Encoding.UTF8.GetString(responseJson);
                return JsonSerializer(respons);
            }
        }

        private string SendPostPequestWhihtAttach(string url, NameValueCollection data)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x");
            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");

            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("http://" + ApplicationBaseUrl + "/" + url);
            webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
            webRequest.Method = "POST";
            webRequest.KeepAlive = true;
            webRequest.Credentials = CredentialCache.DefaultCredentials;

            Stream request = webRequest.GetRequestStream();

            string formdataTemplate = "Content-Disposition: form-data; name=\"{0}\"\r\n\r\n{1}";
            foreach (string key in data.Keys)
            {
                request.Write(boundarybytes, 0, boundarybytes.Length);
                string formitem = string.Format(formdataTemplate, key, data[key]);
                byte[] formitembytes = Encoding.UTF8.GetBytes(formitem);
                request.Write(formitembytes, 0, formitembytes.Length);
            }
            request.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n";
            string header = string.Format(headerTemplate, "orders", data.Get("orders"), "xls");
            byte[] headerbytes = Encoding.UTF8.GetBytes(header);
            request.Write(headerbytes, 0, headerbytes.Length);

            FileStream fileStream = new FileStream(data.Get("orders"), FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[4096];
            int bytesRead = 0;
            while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                request.Write(buffer, 0, bytesRead);
            }
            fileStream.Close();

            byte[] trailer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            request.Write(trailer, 0, trailer.Length);
            request.Close();

            WebResponse webResponse = null;
            webResponse = webRequest.GetResponse();
            Stream stream = webResponse.GetResponseStream();
            StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }
        private static ApiResponse.TResponse JsonSerializer(string value)
        {

//            ResponseEmailSend 
            if (value.Contains(@"freshdesk_url"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseFreshdesk));
                return (ApiResponse.ResponseFreshdesk)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseEmailSend 
            if (value.Contains(@"is_send"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseEmailSend));
                return (ApiResponse.ResponseEmailSend) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseOrdersList 
            if (value.Contains(@"rows"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseOrdersList));
                return (ApiResponse.ResponseOrdersList) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponsePaymentPriceFee если тег response есть percent_card то это ResponsePaymentPriceFee 
            if (value.Contains(@"percent_card") && value.Contains(@"percent"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponsePaymentPriceFee));
                return
                    (ApiResponse.ResponsePaymentPriceFee)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseSdk ответ на запрос
            if (value.Contains(@"sdk_token") || value.Contains(@"sdk2.ddelivery.ru") || value.Contains(@"sdk.ddelivery.ru") || value.Contains(@"sdk2.dev.ddelivery.ru") || value.Contains(@"insales2.ddelivery.ru") || value.Contains(@"dab.dev.ddelivery.ru"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseSdk));
                return (ApiResponse.ResponseSdk) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseDocumentsRequest если тег response есть completed  
            if (value.Contains(@"response"":{""completed"""))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDocumentsRequest));
                return
                    (ApiResponse.ResponseDocumentsRequest)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }

//            ResponseCalculation если тег response множественный то это идет калькулятора 
            if (value.Contains(@"response"":[") && value.Contains(@"delivery_company_name"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseCalculation));
                return
                    (ApiResponse.ResponseCalculation) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseDeliveryPoints если тег response множественный то это идет точки доставки
            if (value.Contains(@"response"":[") && value.Contains(@"has_fitting_room"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDeliveryPoints));
                return
                    (ApiResponse.ResponseDeliveryPoints)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseFailOrder
            if (value.Contains(@"{""success"":false,""response"":{""message"":{"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseFailObject));
                var responseFailObject =
                    (ApiResponse.ResponseFailObject) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
                try
                {
                    Console.WriteLine("выводим текст responseFailObject для ловли бага: "
                                      + responseFailObject.Response.Error.Address
                                      + responseFailObject.Response.Error.CalculateOrder
                                      + responseFailObject.Response.Error.ContactPerson
                                      + responseFailObject.Response.Error.DeliveryCompany
                                      + responseFailObject.Response.Error.ContactPhone
                                      + responseFailObject.Response.Error.DeliveryPoint
                                      + responseFailObject.Response.Error.DimensionSide1
                                      + responseFailObject.Response.Error.Email
                                      + responseFailObject.Response.Error.Flat
                                      + responseFailObject.Response.Error.ItemsCount
                                      + responseFailObject.Response.Error.Name
                                      + responseFailObject.Response.Error.OrderComment
                                      + responseFailObject.Response.Error.Street
                                      + responseFailObject.Response.Error.ToAddPhone
                                      + responseFailObject.Response.Error.ToCity
                                      + responseFailObject.Response.Error.Username
                                      + responseFailObject.Response.Error.Warehouse
                                      + responseFailObject.Response.Error.Weight
                        );
                }
                catch (Exception)
                {
                }
                return responseFailObject;
            }
//            ResponseAddOrder
            if (value.Contains(@"success"":true,""response"":{""order"":"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseAddOrder));
                return (ApiResponse.ResponseAddOrder) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseStatus
            if ((value.Contains(@"status") && value.Contains(@"status_description")) ||
                value.Contains(@"delivery_company_order_number"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseStatus));
                return (ApiResponse.ResponseStatus) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseOrderInfo
            if (value.Contains(@"to_house") && value.Contains(@"to_flat"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseOrderInfo));
                return (ApiResponse.ResponseOrderInfo) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseTrueCancel
            if (value.Contains(@"success"":true,""response"":{""order_id"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseTrueCancel));
                return (ApiResponse.ResponseTrueCancel) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseCompanyTerm
            if (value.Contains(@"success"":true,""response"":{") && value.Contains(@"term"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseCompanyTerm));
                return
                    (ApiResponse.ResponseCompanyTerm) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseInfoObject
            if (value.Contains(@"success"":true,""response"":{""_id") && value.Contains(@"name"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseInfoObject));
                return (ApiResponse.ResponseInfoObject) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseAddObject
            if (value.Contains(@"success"":true,""response"":{""_id"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseAddObject));
                return (ApiResponse.ResponseAddObject) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseDeamonСities
            if (value.Contains(@"success"":true,""options"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDeamonСities));
                return
                    (ApiResponse.ResponseDeamonСities)
                        json.ReadObject(new MemoryStream(Encoding.GetEncoding(1252).GetBytes(value)));
//                json.ReadObject(new MemoryStream(Encoding.GetEncoding("windows-1251").GetBytes(value)));
            }
//            ResponseDeamonСity
            if (value.Contains(@"success"":true,""result") || value.Contains(@"success"":false,""result"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDeamonСity));
                return
                    (ApiResponse.ResponseDeamonСity)
                        json.ReadObject(new MemoryStream(Encoding.GetEncoding(1252).GetBytes(value)));
//                        json.ReadObject(new MemoryStream(Encoding.GetEncoding("windows-1251").GetBytes(value)));
            }
//            ResponseDeamonPoints
            if (value.Contains(@"success"":true,""points"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDeamonPoints));
                return
                    (ApiResponse.ResponseDeamonPoints)

                        json.ReadObject(new MemoryStream(Encoding.GetEncoding(1252).GetBytes(value)));
//                json.ReadObject(new MemoryStream(Encoding.GetEncoding("windows-1251").GetBytes(value)));
            }
//            ResponseStatusConfirm
            if (value.Contains(@"success"":true,""response") && value.Contains(@"status") && value.Contains(@"message"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseStatusConfirm));
                return
                    (ApiResponse.ResponseStatusConfirm) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseDocumentDelivery
            if (value.Contains(@"success"":true,""response") && value.Contains(@"confirm"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDocumentPickup));
                return
                    (ApiResponse.ResponseDocumentPickup)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponsePickupOrders
            if (value.Contains(@"success"":true,""response") && value.Contains(@"delivery_company_id"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponsePickupOrders));
                return
                    (ApiResponse.ResponsePickupOrders) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseUserBarcodes
            if (value.Contains(@"success"":true") && value.Contains(@"barcodes"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseUserBarcodes));
                return
                    (ApiResponse.ResponseUserBarcodes) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseDocumentsList 
            if (value.Contains(@"success"":true,""response"":[]") ||
                (value.Contains(@"success"":true,""response"":[{""_id""") && value.Contains(@"""_create_user""")))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDocumentsList));
                return
                    (ApiResponse.ResponseDocumentsList) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }

            //            ResponseCompanies
            if (value.Contains(@"companies"":[") && value.Contains(@"""id"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseCompanies));
                return (ApiResponse.ResponseCompanies) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseCompaniesСonditions
            if (value.Contains(@"response"":[") && value.Contains(@"company_id") && value.Contains(@"npp_commission"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseCompaniesСonditions));
                return
                    (ApiResponse.ResponseCompaniesСonditions)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseUserInfo 
            if (value.Contains(@"username"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseUserInfo));
                return (ApiResponse.ResponseUserInfo) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseObjectsList
            if (value.Contains(@"{""success"":true,""response"":{") && value.Contains(@"_id") && value.Contains(@"name"))
            {
                string[] split = value.Split(new Char[] {'{', '}'});
                var dataString = "{" + split[1] + "[";
                for (int i = 2; i < split.Count() - 2; i++)
                {
                    if (i%2 != 0)
                        dataString = dataString + "{" + split[i] + "},";
                }
                dataString = dataString + "]}";
                value = dataString;
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseObjectsList));
                return
                    (ApiResponse.ResponseObjectsList) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseObjectsList
            if (value.Contains(@"{""success"":true,""response"":[{") && value.Contains(@"""name"":") && value.Contains(@"""_id"":"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseObjectsList));
                return (ApiResponse.ResponseObjectsList)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseBikData
            if (value.Contains(@"response"":") && value.Contains(@"""bik"":"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseBikData));
                return (ApiResponse.ResponseBikData) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponsePickupCompany
            if (value.Contains(@"response"":{") && value.Contains(@"id") && value.Contains(@"name"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponsePickupCompany));
                return
                    (ApiResponse.ResponsePickupCompany) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseBarcodeMessage
            if (value.Contains(@"{""success"":true,""response"":{""order_number"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseBarcodeMessage));
                return
                    (ApiResponse.ResponseBarcodeMessage)
                        json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }

//            ResponseLkAuth
            if (value.Contains(@"{""success"":true,""response"":{""ttl_token"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseLkAuth));
                return (ApiResponse.ResponseLkAuth) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponsePublicKey 
            if (value.Contains(@"public_key"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponsePublicKey));
                return (ApiResponse.ResponsePublicKey) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponsePickupCompanies 
            if (value.Contains(@"response"":[") &&  value.Contains(@"pickup_type_name"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponsePickupCompanies));
                return (ApiResponse.ResponsePickupCompanies)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponseCompaniesOrShops
            if (value.Contains(@"response"":[") && value.Contains(@"""id"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseCompaniesOrShops));
                return(ApiResponse.ResponseCompaniesOrShops)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponsePimPayStatus
            if (value.Contains(@"response"":") && value.Contains(@"user_title"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponsePimPayStatus));
                return(ApiResponse.ResponsePimPayStatus)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponsePickupRegistry
            if (value.Contains(@"success_create_for_orders") || value.Contains(@"not_found_orders"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponsePickupRegistry));
                return (ApiResponse.ResponsePickupRegistry)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
//            ResponseFail
            if (value.Contains(@"success"":false"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseFail));
                var responseFail =
                    (ApiResponse.ResponseFail)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
                try
                {
                    Console.WriteLine("выводим текст responseFail для ловли бага: " + responseFail.Response.ErrorText);
                }
                catch (Exception)
                {
                }
                return responseFail;
            }
			//            ResponseMessage
            if (value.Contains(@"{""success"":true,""response"":{""message") ||
                value.Contains(@"{""success"":true,""response"":{""file") ||
                value.Contains(@"{""success"":true,""response"":{""report") ||
                value.Contains(@"{""success"":true,""response"":{""id"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseMessage));
                return (ApiResponse.ResponseMessage)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponseDd247
            if (value.Contains(@"{""success"":true,""response"":{""shop_api_key"))
            {
                var json = new DataContractJsonSerializer(typeof (ApiResponse.ResponseDd247));
                return (ApiResponse.ResponseDd247) json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponseNewsList
            if (value.Contains(@"{""success"":true,""response"":[") && value.Contains(@"content") && value.Contains(@"type"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseNewsList));
                return (ApiResponse.ResponseNewsList)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            //            ResponseFmPackageList
            if (value.Contains(@"{""success"":true,""response"":[") && value.Contains(@"package_id") && value.Contains(@"bill_link"))
            {
                var json = new DataContractJsonSerializer(typeof(ApiResponse.ResponseFmPackageList));
                return (ApiResponse.ResponseFmPackageList)json.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
            }
            var json2 = new DataContractJsonSerializer(typeof (ApiResponse.TResponse));
            return (ApiResponse.TResponse) json2.ReadObject(new MemoryStream(Encoding.UTF8.GetBytes(value)));
        }
    }
}