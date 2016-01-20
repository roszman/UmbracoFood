var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('RestaurantsController', ['$scope', 'restaurantService', function ($scope, restaurantService) {
    $scope.restaurants = [];

    var loadData = function() {
        return restaurantService.getRestaurants()
            .then(onRestaurantsFetched);
    }

    var onRestaurantsFetched = function(response) {
        $scope.restaurants = response.data.Restaurants;
    }

    loadData();
}]);