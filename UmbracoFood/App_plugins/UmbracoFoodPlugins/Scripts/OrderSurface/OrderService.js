﻿var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.factory('orderService', [
    '$http', function ($http) {
        return {
            getOrders: function () {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/GetOrders')
                    .then(function (result) {
                        return result;
                    });
            },
            createOrder: function (model) {
                return $http.post('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/PostOrder', model)
                    .then(function (result) {
                        return result;
                    });
            },
            getOrder: function(id) {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/GetOrder/' + id)
                    .then(function (result) {
                        return result;
                    });
            },
            addMeal: function (model) {
                return $http.put('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/PutOrderMeal', model)
                    .then(function (result) {
                        return result;
                    });
            },
            editOrder: function (model) {
                return $http.put('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/PutEditOrder', model)
                    .then(function (result) {
                        return result;
                    });
            },
            deleteOrder: function (id) {
                return $http.delete('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/DeleteOrder/' + id)
                    .then(function (result) {
                        return result;
                    });
            }
        }
    }
]);
