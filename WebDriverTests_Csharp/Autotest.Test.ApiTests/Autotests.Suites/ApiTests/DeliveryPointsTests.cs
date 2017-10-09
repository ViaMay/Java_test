﻿using System;
using System.Collections.Specialized;
using System.Linq;
using Autotests.Utilities.ApiTestCore;
using NUnit.Framework;

namespace Autotests.Tests.ApiTests
{
    public class DeliveryPointsTests : SendOrdersBasePage
    {
        [Test, Description("Получить список пунктов самовывоза")]
        public void DeliveryPointsTest()
        {
            string keyShopPublic = GetShopKeyByName(userShopName);

            var responseDeliveryPoints =
               (ApiResponse.ResponseDeliveryPoints)apiRequest.GET("api/v1/" + keyShopPublic + "/delivery_points.json");
            Assert.IsTrue(responseDeliveryPoints.Success);

            var responseRowDeliveryPoint = FindRowByName(deliveryPointName, responseDeliveryPoints);
            Assert.AreEqual(responseRowDeliveryPoint.City.Id, "151184");
            Assert.AreEqual(responseRowDeliveryPoint.City.Name, "Москва");
            Assert.IsTrue(responseRowDeliveryPoint.HasFittingRoom);
            Assert.IsTrue(responseRowDeliveryPoint.IsCard);
            Assert.IsTrue(responseRowDeliveryPoint.IsCash);
            Assert.AreEqual(responseRowDeliveryPoint.Address, deliveryPointAddress);
            Assert.AreEqual(responseRowDeliveryPoint.Latitude, deliveryPointLatitude);
            Assert.AreEqual(responseRowDeliveryPoint.Longitude, deliveryPointLongitude);
        }

        public ApiResponse.MessageDeliveryPoint FindRowByName(string name, ApiResponse.ResponseDeliveryPoints responseDeliveryPoints)
        {
            for (var i = 0; i < responseDeliveryPoints.Response.Count(); i++)
            {
                if (responseDeliveryPoints.Response[i].Name == name)
                    return responseDeliveryPoints.Response[i];
            }
            throw new Exception(string.Format("не найдена точка доставки с именем {0} в списке всех точек доставки", deliveryPointName));
        }
    }
}