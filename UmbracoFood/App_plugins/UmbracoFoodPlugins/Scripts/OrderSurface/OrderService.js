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
           //getRestaurant: function (id) {
           //    return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/GetRestaurant/' + id )
           //        .then(function (result) {
           //            return result;
           //        });
           //},
           createOrder: function (model) {
               return $http.post('/UmbracoFood/umbraco/UmbracoFoodApi/OrderApi/PostOrder', model)
                   .then(function (result) {
                       return result;
                   });
           },
           //editRestaurant: function (model) {
           //    return $http.put('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/PutRestaurant', model)
           //        .then(function (result) {
           //            return result;
           //        });
           //},
           //deleteRestaurant: function(id) {
           //    return $http.delete('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/DeleteRestaurant/' + id)
           //        .then(function(result) {
           //            return result;
           //        });
           //}
        }
    }
]);
