var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('RestaurantsController', ['$scope', 'restaurantsService', function ($scope, restaurantsService) {
    $scope.restaurants = [];

    var loadData = function() {
        return restaurantsService.getRestaurants()
            .then(onRestaurantsFetched);
    }

    var onRestaurantsFetched = function(response) {
        $scope.restaurants = response.data;
    }

    loadData();
}]);