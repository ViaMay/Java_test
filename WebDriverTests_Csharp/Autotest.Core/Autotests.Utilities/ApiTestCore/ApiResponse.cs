using System.Runtime.Serialization;

namespace Autotests.Utilities.ApiTestCore
{
    public class ApiResponse
    {
        [DataContract]
        public class AddMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "key")]
            public string Key { get; set; }
        }

        [DataContract]
        public class InfoObjectMessage
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "public_key")]
            public string PublicKey { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "house")]
            public string House { get; set; }

            [DataMember(Name = "flat")]
            public string Flat { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "contact_person")]
            public string ContactPerson { get; set; }

            [DataMember(Name = "contact_phone")]
            public string ContactPhone { get; set; }

            [DataMember(Name = "contact_email")]
            public string ContactEmail { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }

            [DataMember(Name = "postal_code")]
            public string PostalCode { get; set; }

            [DataMember(Name = "pickup_info")]
            public PickupInfo PickupInfo { get; set; }
        }

        [DataContract]
        public class PickupInfo
        {
            [DataMember(Name = "default_company")]
            public string DefaultCompany { get; set; }

            [DataMember(Name = "default_type")]
            public string DefaultType { get; set; }

            [DataMember(Name = "pickup_types")]
            public PickupTypes[] PickupType { get; set; }
        }

        [DataContract]
        public class PickupTypes
        {
            [DataMember(Name = "company")]
            public string Company { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "type_name")]
            public string TypeName { get; set; }

            [DataMember(Name = "available_companies_list")]
            public AvailableCompaniesList[] AvailableCompaniesList { get; set; }
        }

        [DataContract]
        public class AvailableCompaniesList
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }


        [DataContract]
        public class AddOrderMessage
        {
            [DataMember(Name = "order")]
            public string OrderId { get; set; }
			
            [DataMember(Name = "message")]
            public string Message { get; set; }

            [DataMember(Name = "barcodes")]
            public string[] Barcodes { get; set; }
        }

        [DataContract]
        public class ErrorMessage
        {
            [DataMember(Name = "to_city")]
            public string ToCity { get; set; }

            [DataMember(Name = "delivery_point")]
            public string DeliveryPoint { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "dimension_side1")]
            public string DimensionSide1 { get; set; }

            [DataMember(Name = "calculate_order")]
            public string CalculateOrder { get; set; }

            [DataMember(Name = "weight")]
            public string Weight { get; set; }

            [DataMember(Name = "to_email")]
            public string Email { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "street")]
            public string Street { get; set; }

            [DataMember(Name = "house")]
            public string House { get; set; }

            [DataMember(Name = "flat")]
            public string Flat { get; set; }

            [DataMember(Name = "contact_person")]
            public string ContactPerson { get; set; }

            [DataMember(Name = "contact_phone")]
            public string ContactPhone { get; set; }

            [DataMember(Name = "username")]
            public string Username { get; set; }

            [DataMember(Name = "order_comment")]
            public string OrderComment { get; set; }

            [DataMember(Name = "to_add_phone")]
            public string ToAddPhone { get; set; }

            [DataMember(Name = "items_count")]
            public string ItemsCount { get; set; }
			
            [DataMember(Name = "inn")]
            public string Inn { get; set; }
        }

        [DataContract]
        public class FailMessage
        {
            [DataMember(Name = "message")]
            public string ErrorText { get; set; }
        }

        [DataContract]
        public class FailOrderMessage
        {
            [DataMember(Name = "message")]
            public ErrorMessage Error { get; set; }
        }

        [DataContract]
        public class MessageCalculation
        {
            [DataMember(Name = "delivery_company_name")]
            public string DeliveryCompanyName { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "delivery_company_driver_version")]
            public string DeliveryCompanyDriverVersion { get; set; }

            [DataMember(Name = "pickup_company_driver_version")]
            public string PickupCompanyDriverVersion { get; set; }

            [DataMember(Name = "pickup_price")]
            public string PickupPrice { get; set; }

            [DataMember(Name = "delivery_price")]
            public string DeliveryPrice { get; set; }

            [DataMember(Name = "delivery_price_fee")]
            public string DeliveryPriceFee { get; set; }

            [DataMember(Name = "declared_price_fee")]
            public string DeclaredPriceFee { get; set; }

            [DataMember(Name = "delivery_time_min")]
            public string DeliveryTimeMin { get; set; }

            [DataMember(Name = "delivery_time_max")]
            public string DeliveryTimeMax { get; set; }

            [DataMember(Name = "delivery_time_avg")]
            public string DeliveryTimeAvg { get; set; }

            [DataMember(Name = "return_price")]
            public string ReturnPrice { get; set; }

            [DataMember(Name = "return_client_price")]
            public string ReturnClientPrice { get; set; }

            [DataMember(Name = "return_partial_price")]
            public string ReturnPartialPrice { get; set; }

            [DataMember(Name = "total_price")]
            public string TotalPrice { get; set; }

            [DataMember(Name = "payment_price_fee")]
            public string Paymentpricefee { get; set; }

            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }

            [DataMember(Name = "confirm_date")]
            public string ConfirmDate { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }

            [DataMember(Name = "pickup_company")]
            public string PickupCompany { get; set; }

            [DataMember(Name = "pickup_type")]
            public string PickupType { get; set; }

            [DataMember(Name = "pickup_company_name")]
            public string PickupCompanyName { get; set; }
        }

        [DataContract]
        public class MessageCompanyTerm
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "term")]
            public string Term { get; set; }

            [DataMember(Name = "prolongation")]
            public bool Prolongation { get; set; }
        }

        [DataContract]
        public class MessageDeliveryPoint
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "city")]
            public City City { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }

            [DataMember(Name = "has_fitting_room")]
            public bool HasFittingRoom { get; set; }

            [DataMember(Name = "is_cash")]
            public bool IsCash { get; set; }

            [DataMember(Name = "is_card")]
            public bool IsCard { get; set; }

            [DataMember(Name = "longitude")]
            public string Longitude { get; set; }

            [DataMember(Name = "latitude")]
            public string Latitude { get; set; }

            [DataMember(Name = "metro")]
            public string Metro { get; set; }

            [DataMember(Name = "description_in")]
            public string DescriptionIn { get; set; }

            [DataMember(Name = "description_out")]
            public string DescriptionOut { get; set; }

            [DataMember(Name = "indoor_place")]
            public string IndoorPlace { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "company")]
            public string Company { get; set; }

            [DataMember(Name = "company_code")]
            public string CompanyCode { get; set; }
        }

        [DataContract]
        public class OptionsCity
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "name_index")]
            public string NameIndex { get; set; }

            [DataMember(Name = "city_id")]
            public string CityId { get; set; }

            [DataMember(Name = "country")]
            public string Country { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "region")]
            public string Region { get; set; }

            [DataMember(Name = "region_id")]
            public string RegionId { get; set; }

            [DataMember(Name = "postal_code")]
            public string PostalCode { get; set; }

            [DataMember(Name = "area")]
            public string Area { get; set; }

            [DataMember(Name = "kladr")]
            public string Kladr { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "importance")]
            public string Importance { get; set; }
			
            [DataMember(Name = "message")]
            public string Message { get; set; }
        }

        [DataContract]
        public class OptionsPoints
        { 
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "city_id")]
            public string CityId { get; set; }

            [DataMember(Name = "city")]
            public string City { get; set; }

            [DataMember(Name = "region")]
            public string Region { get; set; }

            [DataMember(Name = "region_id")]
            public string RegionId { get; set; }

            [DataMember(Name = "city_type")]
            public string CityType { get; set; }

            [DataMember(Name = "postal_code")]
            public string PostalCode { get; set; }

            [DataMember(Name = "area")]
            public string Area { get; set; }

            [DataMember(Name = "kladr")]
            public string Kladr { get; set; }

            [DataMember(Name = "company")]
            public string Company { get; set; }

            [DataMember(Name = "company_id")]
            public string CompanyId { get; set; }

            [DataMember(Name = "company_code")]
            public string CompanyCode { get; set; }

            [DataMember(Name = "metro")]
            public string Metro { get; set; }

            [DataMember(Name = "description_in")]
            public string DescriptionIn { get; set; }

            [DataMember(Name = "description_out")]
            public string DescriptionOut { get; set; }

            [DataMember(Name = "indoor_place")]
            public string IndoorPlace { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "schedule")]
            public string Schedule { get; set; }

            [DataMember(Name = "longitude")]
            public string Longitude { get; set; }

            [DataMember(Name = "latitude")]
            public string Latitude { get; set; }

            [DataMember(Name = "has_fitting_room")]
            public bool HasFittingRoom { get; set; }

            [DataMember(Name = "is_cash")]
            public bool IsCash { get; set; }

            [DataMember(Name = "is_card")]
            public bool IsCard { get; set; }
            
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "status")]
            public string Status { get; set; }
        }

        [DataContract]
        public class City
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }

        [DataContract]
        public class MessageErrore
        {
            [DataMember(Name = "city")]
            public string City { get; set; }
        }

        [DataContract]
        public class MessageOrderInfo
        {
            [DataMember(Name = "declared_price")]
            public string DeclaredPice { get; set; }

            [DataMember(Name = "payment_price")]
            public string PaymentPrice { get; set; }

            [DataMember(Name = "city_to")]
            public string ToCity { get; set; }

            [DataMember(Name = "to_name")]
            public string ToName { get; set; }

            [DataMember(Name = "to_phone")]
            public string ToPhone { get; set; }

            [DataMember(Name = "to_add_phone")]
            public string ToAddPhone { get; set; }

            [DataMember(Name = "to_email")]
            public string ToEmail { get; set; }

            [DataMember(Name = "to_street")]
            public string ToStreet { get; set; }

            [DataMember(Name = "to_house")]
            public string ToHouse { get; set; }

            [DataMember(Name = "to_flat")]
            public string ToFlat { get; set; }

            [DataMember(Name = "to_postal_code")]
            public string ToPostalCode { get; set; }

            [DataMember(Name = "metadata")]
            public string MetaData { get; set; }

            [DataMember(Name = "shop_refnum")]
            public string ShopRefnum { get; set; }

            [DataMember(Name = "weight")]
            public string Weight { get; set; }

            [DataMember(Name = "dimension_side1")]
            public string DimensionSide1 { get; set; }

            [DataMember(Name = "dimension_side2")]
            public string DimensionSide2 { get; set; }

            [DataMember(Name = "dimension_side3")]
            public string DimensionSide3 { get; set; }

            [DataMember(Name = "goods_description")]
            public string GoodsDescription { get; set; }

            [DataMember(Name = "order_comment")]
            public string OrderComment { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "delivery_company_name")]
            public string DeliveryCompanyName { get; set; }

            [DataMember(Name = "delivery_point_id")]
            public string DeliveryPointid { get; set; }

            [DataMember(Name = "packing")]
            public string Packing { get; set; }

            [DataMember(Name = "price_info")]
            public PriceInfo PriceInfo { get; set; }

            [DataMember(Name = "order_id")]
            public string OrderId { get; set; }

            [DataMember(Name = "order_num")]
            public string OrderNum { get; set; }

            [DataMember(Name = "phone")]
            public string Phone { get; set; }

            [DataMember(Name = "delivery_city")]
            public string DeliveryCity { get; set; }

            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }

            [DataMember(Name = "formate_delivery_date")]
            public string FormateDeliveryDate { get; set; }

            [DataMember(Name = "stage_status")]
            public string StageStatus { get; set; }
			
            [DataMember(Name = "delivery_prediction")]
            public string DeliveryPrediction { get; set; }
			
            [DataMember(Name = "tracking_url")]
            public string TrackingUrl { get; set; }
			
            [DataMember(Name = "company_order_number")]
            public string CompanyOrderNumber { get; set; }
        }

        [DataContract]
        public class PriceInfo
        {
            [DataMember(Name = "pickup_price")]
            public string PickupPrice { get; set; }

            [DataMember(Name = "delivery_price")]
            public string DeliveryPrice { get; set; }

            [DataMember(Name = "sorting_price")]
            public string SortingPrice { get; set; }

            [DataMember(Name = "total_price")]
            public string TotalPrice { get; set; }

            [DataMember(Name = "packing_price")]
            public string PackingPrice { get; set; }
        }

        [DataContract]
        public class MessageStatus
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "status_description")]
            public string StatusDescription { get; set; }

            [DataMember(Name = "status_message")]
            public string StatusMessage { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }

            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }

            [DataMember(Name = "delivery_company_order_number")]
            public string DeliveryCompanyOderNumber { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "post_track")]
            public string PostTrack { get; set; }

            [DataMember(Name = "order_status")]
            public OrderStatus[] OrderStatus { get; set; }
        }

        [DataContract]
        public class MessageStatusConfirm
        {
            [DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "message")]
            public string Message { get; set; }
        }

        [DataContract]
        public class MessageObject
        {
            [DataMember(Name = "message")]
            public string Message { get; set; }
			
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "file")]
            public string File { get; set; }

            [DataMember(Name = "report")]
            public string Report { get; set; }
        }

        [DataContract]
        public class MessageLkAuth
        {
            [DataMember(Name = "ttl_token")]
            public string Token { get; set; }
        }

        [DataContract]
        public class MessageTrueCancal
        {
            [DataMember(Name = "order_id")]
            public string OrderId { get; set; }
        }

        [DataContract]
        public class ResponseDd247 : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDd247 Response { get; set; }
        }

        [DataContract]
        public class MessageDd247
        {
            [DataMember(Name = "shop_api_key")]
            public string ShopApiKey { get; set; }

            [DataMember(Name = "order_id")]
            public string OrderId { get; set; }
        }

        [DataContract]
        public class MessagePaymentPriceFee
        {
            [DataMember(Name = "from")]
            public string From { get; set; }

            [DataMember(Name = "min")]
            public string Min { get; set; }

            [DataMember(Name = "percent")]
            public string Percent { get; set; }

            [DataMember(Name = "percent_card")]
            public string PercentCard { get; set; }
        }

        [DataContract]
        public class MessagePickupOrders
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "delivery_company_id")]
            public string DeliveryCompanyId { get; set; }
        }

        [DataContract]
        public class MessageUserBarcodes
        {
            [DataMember(Name = "barcodes")]
            public string[] Barcodes { get; set; }
        }
        
        [DataContract]
        public class MessageDocumentDelivery
        {
            [DataMember(Name = "view")]
            public string View { get; set; }

            [DataMember(Name = "confirm")]
            public string Confirm { get; set; }
        }
        
        [DataContract]
        public class MessageDocumentsRequest
        {
            [DataMember(Name = "completed")]
            public bool Completed { get; set; }

            [DataMember(Name = "request_id")]
            public string RequestId { get; set; }

            [DataMember(Name = "documents")]
            public Documents[] Documents { get; set; }
        }
                
        [DataContract]
        public class MessageDocumentsList
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "_create_user")]
            public string CreateUser { get; set; }

            [DataMember(Name = "_create_date")]
            public string CreateDate { get; set; }

            [DataMember(Name = "_modify_user")]
            public string ModifyUser { get; set; }

            [DataMember(Name = "_modify_date")]
            public string ModifyDate { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "pickup_company")]
            public string IdPickupCompany { get; set; }

            [DataMember(Name = "delivery_company")]
            public string IdDeliveryCompany { get; set; }

            [DataMember(Name = "file")]
            public string File { get; set; }
        }
      
        [DataContract]
        public class MessageCompaniesOrShops
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }
        }
        
        [DataContract]
        public class MessageCompaniesСonditions
        {
            [DataMember(Name = "company_id")]
            public string CompanyId { get; set; }

            [DataMember(Name = "company_name")]
            public string CompanyName { get; set; }

            [DataMember(Name = "payment_time")]
            public string PaymentTime { get; set; }

            [DataMember(Name = "npp_commission")]
            public string NppCommission { get; set; }

            [DataMember(Name = "card_commission")]
            public string CardCommission { get; set; }

            [DataMember(Name = "declared_commission")]
            public string DeclaredCommission { get; set; }

            [DataMember(Name = "applied_from_amount")]
            public string AppliedFromAmount { get; set; }

            [DataMember(Name = "min_amount")]
            public string MinAmount { get; set; }
        }
        
        [DataContract]
        public class OrderStatus
        {
            [DataMember(Name = "date")]
            public string Date { get; set; }

            [DataMember(Name = "company_status")]
            public string CompanStatus { get; set; }

            [DataMember(Name = "company_status_description")]
            public string CompanyStatusDescription { get; set; }
        }
        
        [DataContract]
        public class Documents
        {
            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }

            [DataMember(Name = "pickup_company")]
            public string PickupCompany { get; set; }

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }

            [DataMember(Name = "url")]
            public string Url { get; set; }
        }


        [DataContract]
        public class MessageBarcodeMessage
        {
            [DataMember(Name = "order_number")]
            public string OrderNumber { get; set; }

            [DataMember(Name = "track_number")]
            public string TrackNumber { get; set; }

            [DataMember(Name = "package_number")]
            public string PackageNumber { get; set; }
        }


        [DataContract]
        public class ResponseDeamonPoints : TResponse
        {
            [DataMember(Name = "points")]
            public OptionsPoints[] Points { get; set; }
        }

        [DataContract]
        public class ResponseDeamonСities : TResponse
        {
            [DataMember(Name = "options")]
            public OptionsCity[] Options { get; set; }
        }

        [DataContract]
        public class ResponseDeamonСity : TResponse
        {
            [DataMember(Name = "result")]
            public OptionsCity Result { get; set; }
        }

        [DataContract]
        public class ResponseAddObject : TResponse
        {
            [DataMember(Name = "response")]
            public AddMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseInfoObject : TResponse
        {
            [DataMember(Name = "response")]
            public InfoObjectMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseAddOrder : TResponse
        {
            [DataMember(Name = "response")]
            public AddOrderMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseCalculation : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCalculation[] Response { get; set; }
        }

        [DataContract]
        public class ResponseCompanyTerm : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompanyTerm Response { get; set; }
        }

        [DataContract]
        public class ResponseCompaniesOrShops : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompaniesOrShops[] Response { get; set; }
        }

        [DataContract]
        public class ResponseCompanies : TResponse
        {
            [DataMember(Name = "response")]
            public MessegeCompanies Response { get; set; }
        }

        [DataContract]
        public class MessegeCompanies
        {
            [DataMember(Name = "companies")]
            public MessageCompaniesOrShops[] Companies { get; set; }
        }

        [DataContract]
        public class ResponseCompaniesСonditions : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompaniesСonditions[] Response { get; set; }
        }

        [DataContract]
        public class ResponsePickupCompany : TResponse
        {
            [DataMember(Name = "response")]
            public MessageCompaniesOrShops Response { get; set; }
        }

        [DataContract]
        public class ResponseDeliveryPoints : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDeliveryPoint[] Response { get; set; }
        }

        [DataContract]
        public class ResponseFail : TResponse
        {
            [DataMember(Name = "response")]
            public FailMessage Response { get; set; }

            [DataMember(Name = "error")]
            public string Error { get; set; }
        }

        [DataContract]
        public class ResponseFailObject : TResponse
        {
            [DataMember(Name = "response")]
            public FailOrderMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseOrderInfo : TResponse
        {
            [DataMember(Name = "response")]
            public MessageOrderInfo Response { get; set; }
        }

        [DataContract]
        public class ResponseStatus : TResponse
        {
            [DataMember(Name = "response")]
            public MessageStatus Response { get; set; }
        }

        [DataContract]
        public class ResponseStatusConfirm : TResponse
        {
            [DataMember(Name = "response")]
            public MessageStatusConfirm Response { get; set; }
        }

        [DataContract]
        public class ResponseMessage : TResponse
        {
            [DataMember(Name = "response")]
            public MessageObject Response { get; set; }
        }

        [DataContract]
        public class ResponseObjectsList : TResponse
        {
            [DataMember(Name = "response")]
            public InfoObjectMessage[] Response { get; set; }
        }

        [DataContract]
        public class ResponseLkAuth : TResponse
        {
            [DataMember(Name = "response")]
            public MessageLkAuth Response { get; set; }
        }

        [DataContract]
        public class ResponsePickupOrders : TResponse
        {
            [DataMember(Name = "response")]
            public MessagePickupOrders[] Response { get; set; }
        }

        [DataContract]
        public class ResponseUserBarcodes : TResponse
        {
            [DataMember(Name = "response")]
            public MessageUserBarcodes Response { get; set; }
        }

        [DataContract]
        public class ResponseTrueCancel : TResponse
        {
            [DataMember(Name = "response")]
            public MessageTrueCancal Response { get; set; }
        }
        
        [DataContract]
        public class ResponseDocumentsRequest : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDocumentsRequest Response { get; set; }
        }
          
        [DataContract]
        public class ResponseDocumentsList : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDocumentsList[] Response { get; set; }
        }
     
 
        [DataContract]
        public class ResponsePaymentPriceFee : TResponse
        {
            [DataMember(Name = "response")]
            public MessagePaymentPriceFee Response { get; set; }
        }

        [DataContract]
        public class ResponseDocumentPickup : TResponse
        {
            [DataMember(Name = "response")]
            public MessageDocumentDelivery Response { get; set; }
        }

        [DataContract]
        public class ResponseBarcodeMessage : TResponse
        {
            [DataMember(Name = "response")]
            public MessageBarcodeMessage Response { get; set; }
        }

        [DataContract]
        public class ResponseSdk : TResponse
        {
            [DataMember(Name = "response")]
            public MessageSdk Response { get; set; }
        }

        [DataContract]
        public class ResponseEmailSend : TResponse
        {
            [DataMember(Name = "response")]
            public MessageEmailSend Response { get; set; }
        }

        [DataContract]
        public class MessageEmailSend
        {
            [DataMember(Name = "is_send")]
            public string IsSend { get; set; }
        }

        [DataContract]
        public class MessageSdk
        {
            [DataMember(Name = "sdk_token")]
            public string SdkToken { get; set; }

            [DataMember(Name = "location")]
            public string Location { get; set; }

            [DataMember(Name = "info")]
            public MessegeInfoSdk Info { get; set; }
        }

        [DataContract]
        public class MessegeInfoSdk
        {
            [DataMember(Name = "cms")]
            public string Cms { get; set; }

            [DataMember(Name = "enter_point")]
            public string EnterPoint { get; set; }

            [DataMember(Name = "version")]
            public string Version { get; set; }
        }

        [DataContract]
        public class TResponse
        {
            [DataMember(Name = "success")]
            public bool Success { get; set; }
        }

        [DataContract]
        public class ResponseOrdersList : TResponse
        {
            [DataMember(Name = "response")]
            public MessageOrderList Response { get; set; }
        }

        [DataContract]
        public class ResponseFmPackageList : TResponse
        {
            [DataMember(Name = "response")]
            public MessageFmPackageList[] Response { get; set; }
        }

        [DataContract]
        public class ResponseNewsList : TResponse
        {
            [DataMember(Name = "response")]
            public MessageNewList[] Response { get; set; }
        }

        [DataContract]
        public class ResponsePublicKey : TResponse
        {
            [DataMember(Name = "response")]
            public MessagePublicKey Response { get; set; }
        }

        [DataContract]
        public class ResponseUserInfo : TResponse
        {
            [DataMember(Name = "response")]
            public MessageUserInfo Response { get; set; }
        }

        [DataContract]
        public class ResponseBikData : TResponse
        {
            [DataMember(Name = "response")]
            public MessageBikData Response { get; set; }
        }

        [DataContract]
        public class MessageBikData
        {
            [DataMember(Name = "id")]
            public string Id { get; set; }

            [DataMember(Name = "bik")]
            public string Bik { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "ks")]
            public string Ks { get; set; }
        }

        [DataContract]
        public class MessageFmPackageList
        {
            [DataMember(Name = "package_id")]
            public string PackageId { get; set; }

            [DataMember(Name = "bill_link")]
            public string BillLink { get; set; }

            [DataMember(Name = "agent_link")]
            public string AgentLink { get; set; }

            [DataMember(Name = "date")]
            public string Date { get; set; }
        }

        [DataContract]
        public class MessageUserInfo
        {
            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "phone")]
            public string Phone { get; set; }

            [DataMember(Name = "official_name")]
            public string OfficialName { get; set; }

            [DataMember(Name = "director")]
            public string Director { get; set; }

            [DataMember(Name = "on_basis")]
            public string OnBasis { get; set; }

            [DataMember(Name = "official_address")]
            public string OfficialAddress { get; set; }

            [DataMember(Name = "address")]
            public string Address { get; set; }

            [DataMember(Name = "inn")]
            public string Inn { get; set; }

            [DataMember(Name = "ogrn")]
            public string Ogrn { get; set; }

            [DataMember(Name = "bank_name")]
            public string BankName { get; set; }

            [DataMember(Name = "bank_bik")]
            public string BankBik { get; set; }

            [DataMember(Name = "bank_ks")]
            public string BankKs { get; set; }

            [DataMember(Name = "bank_rs")]
            public string BankRs { get; set; }

            [DataMember(Name = "username")]
            public string Username { get; set; }

            [DataMember(Name = "_id")]
            public string Id { get; set; }
        }

        [DataContract]
        public class MessagePublicKey
        {
            [DataMember(Name = "public_key")]
            public string PublicKey { get; set; }

            [DataMember(Name = "id")]
            public string Id { get; set; }
        }
        
        [DataContract]
        public class MessageOrderList
        {
            [DataMember(Name = "rows")]
            public Rows[] Rows { get; set; }

			[DataMember(Name = "count")]
			public string Count { get; set; }
        }

        [DataContract]
        public class Rows
        {
            [DataMember(Name = "_id")]
            public string Id { get; set; }

            [DataMember(Name = "creator")]
            public string Creator { get; set; }

			[DataMember(Name = "shop")]
			public string Shop { get; set;}

			[DataMember(Name = "type")]
			public string Type { get; set;}

            [DataMember(Name = "to_city")]
			public string CityTo { get; set;}

            [DataMember(Name = "from_city")]
            public string CityFrom { get; set; }

			[DataMember(Name = "delivery_company_name")]
			public string DeliveryCompanyName { get; set;}

            [DataMember(Name = "delivery_company")]
            public string DeliveryCompany { get; set; }
			
            [DataMember(Name = "delivery_date")]
            public string DeliveryDate { get; set; }
			
            [DataMember(Name = "delivery_date_new")]
            public string DeliveryDateNew { get; set; }
			
            [DataMember(Name = "delivery_time_from")]
            public string DeliveryTimeFrom { get; set; }
			
            [DataMember(Name = "delivery_time_to")]
            public string DeliveryTimeTo { get; set; }

			[DataMember(Name = "status")]
            public string Status { get; set; }

            [DataMember(Name = "shop_refnum")]
            public string ShopRefnum { get; set; }

            [DataMember(Name = "warehouse")]
            public string Warehouse { get; set; }
			
            [DataMember(Name = "managment_password")]
            public string ManagmentPassword { get; set; }
			
            [DataMember(Name = "to_phone")]
            public string ToPhone { get; set; }
			
            [DataMember(Name = "packing")]
            public string Packing { get; set; }

            [DataMember(Name = "id")]
            public string PickupId { get; set; }

            [DataMember(Name = "pickup_date")]
            public string PickupDate { get; set; }

            [DataMember(Name = "mode")]
            public string PickupMode { get; set; }

            [DataMember(Name = "pickup_time_to")]
            public string PickupTimeTo { get; set; }

            [DataMember(Name = "pickup_time_from")]
            public string PickupTimeFrom { get; set; }

            [DataMember(Name = "pickup_company_id")]
            public string PickupCompanyId { get; set; }

            [DataMember(Name = "pickup_company_name")]
            public string PickupCompanyName { get; set; }

            [DataMember(Name = "process_date")]
            public string PickupProcessDate { get; set; }

            [DataMember(Name = "warehouse_id")]
            public string PickupWarehouseId { get; set; }

            [DataMember(Name = "warehouse_name")]
            public string PickupWarehouseName { get; set; }

            [DataMember(Name = "warehouse_address")]
            public string PickupWarehouseAddress { get; set; }

            [DataMember(Name = "warehouse_type")]
            public string PickupWarehouseType { get; set; }

            [DataMember(Name = "penalty_applied")]
            public string PickupPenaltyApplied { get; set; }

            [DataMember(Name = "penalty")]
            public string PickupPenalty { get; set; }

            [DataMember(Name = "price")]
            public string PickupPrice { get; set; }
        }
        
        [DataContract]
        public class MessageNewList
        {
            [DataMember(Name = "_create_date")]
            public string CreateDate { get; set; }

            [DataMember(Name = "content")]
            public string Content { get; set; }

            [DataMember(Name = "type")]
            public string Type { get; set; }

            [DataMember(Name = "active")]
            public string Active { get; set; }

            [DataMember(Name = "email")]
            public string Email { get; set; }
        }

         [DataContract]
         public class ResponseFreshdesk : TResponse
         {
             [DataMember(Name = "response")]
             public MessageFreshdesk Response { get; set; }
         }

         [DataContract]
         public class ResponsePickupCompanies : TResponse
         {
             [DataMember(Name = "response")]
             public MessagePickupCompanies[] Response { get; set; }
         }

        [DataContract]
         public class ResponsePickupRegistry : TResponse
         {
             [DataMember(Name = "response")]
            public MessagePickupRegistry Response { get; set; }
         }

        [DataContract]
        public class MessagePickupRegistry
         {
             [DataMember(Name = "success_create_for_orders")]
            public string[] CreateForOrders { get; set; }

             [DataMember(Name = "not_found_orders")]
             public string[] NotFoundOrders { get; set; }
			 
             [DataMember(Name = "registry_ids")]
             public string[] RegistryIds { get; set; }
         }

        [DataContract]
         public class ResponsePimPayStatus : TResponse
         {
             [DataMember(Name = "response")]
            public MessagePimPayStatus Response { get; set; }
         }

         [DataContract]
        public class MessagePimPayStatus
         {
             [DataMember(Name = "user")]
             public string User { get; set; }

             [DataMember(Name = "inn")]
             public string Inn { get; set; }

             [DataMember(Name = "user_title")]
             public string UserTitle { get; set; }
         }

         [DataContract]
         public class MessageFreshdesk
         {
             [DataMember(Name = "freshdesk_url")]
             public string FreshdeskUrl { get; set; }
         }

         [DataContract]
         public class MessagePickupCompanies
         {
             [DataMember(Name = "id")]
             public string Id { get; set; }

             [DataMember(Name = "name")]
             public string Name { get; set; }

             [DataMember(Name = "pickup_type")]
             public string PickupType { get; set; }

             [DataMember(Name = "pickup_type_name")]
             public string PickupTypeName { get; set; }

             [DataMember(Name = "warehouses")]
             public Warehouses[] Warehouses { get; set; }
         }

         [DataContract]
         public class Warehouses
         {
             [DataMember(Name = "city_id")]
             public string CityId { get; set; }

             [DataMember(Name = "city_name")]
             public string CityName { get; set; }

             [DataMember(Name = "id")]
             public string Id { get; set; }

             [DataMember(Name = "name")]
             public string Name { get; set; }
         }
    }
}