var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.factory('addRestaurantService', [
    '$http', function ($http) {
        return {
            addRestaurant: function (model) {

                return $http.post('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/PostRestaurant', model)
                    .then(function (result) {
                        return result;
                    });
            },
            test: function () {
                return $http.get('/UmbracoFood/umbraco/UmbracoFoodApi/RestaurantApi/GetTest')
                    .then(function (result) {
                        return result;
                    });
            }
        }
    }
]);
