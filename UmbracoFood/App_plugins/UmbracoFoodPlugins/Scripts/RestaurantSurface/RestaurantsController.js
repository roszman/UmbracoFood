var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('RestaurantsController', ['$scope', 'restaurantService', 'utilService', function ($scope, restaurantService, utilService) {
    $scope.restaurants = [];

    var loadData = function() {
        return restaurantService.getRestaurants()
            .then(onRestaurantsFetched);
    }

    var onRestaurantsFetched = function(response) {
        $scope.restaurants = response.data.Restaurants;
    }

    $scope.delete = function(id) {
        return restaurantService.deleteRestaurant(id)
            .then(function() {
                utilService.growlSuccess("Restauracja została usunięta");
            })
            .then(onRestaurantDeleted);
    };

    var onRestaurantDeleted = function() {
        loadData();
    }


    loadData();
}]);