var umbracoFood = angular.module("umbracoFoodApp");

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

        }
    }
]);
