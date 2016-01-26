var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('EditRestaurantController', ['$scope', 'restaurantService', 'utilService', function ($scope, restaurantService, utilService) {
    $scope.restaurantId = null;


    $scope.init = function (id) {
        $scope.restaurantId = id;
        fetchRestaurant();
    }

    var fetchRestaurant = function() {
        return restaurantService.getRestaurant($scope.restaurantId)
                .then(onRestaurantFetched);
    }

    var onRestaurantFetched = function (response) {
        $scope.restaurant = response.data;
    }

    $scope.editRestaurant = function (restaurant) {
        return restaurantService.editRestaurant(restaurant)
            .then(onEditRestaurant);
    }

    var onEditRestaurant = function (response) {
        utilService.growlSuccess("Restauracja została zedytowana");
    }
}]);