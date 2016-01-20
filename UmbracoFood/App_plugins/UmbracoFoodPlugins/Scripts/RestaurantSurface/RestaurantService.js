var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.factory('restaurantService', [
    '$http', function ($http) {
        return {
            getRestaurants: function () {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/GetRestaurants')
                    .then(function (result) {
                        return result;
                    });
            },
            getRestaurant: function (id) {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/GetRestaurant/' + id )
                    .then(function (result) {
                        return result;
                    });
            },
            addRestaurant: function (model) {
                return $http.post('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/PostRestaurant', model)
                    .then(function (result) {
                        return result;
                    });
            },
            editRestaurant: function (model) {
                return $http.put('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/PutRestaurant', model)
                    .then(function (result) {
                        return result;
                    });
            }
        }
    }
]);
