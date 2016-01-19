var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.factory('restaurantsService', [
    '$http', function ($http) {
        return {
            getRestaurants: function () {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/GetRestaurant')
                    .then(function (result) {
                        return result;
                    });
            }
        }
    }
]);
