var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('AddRestaurantController', ['$scope', 'addRestaurantService', 'growl', function ($scope, addRestaurantService, growl)
{
    $scope.restaurant = {
        Name: "",
        Phone: "",
        Url: "",
        MenuUrl: ""
    }

    $scope.addRestaurant = function (restaurant) {
        return addRestaurantService.addRestaurant(restaurant)
            .then(onAddRestaurant);
    }

    var onAddRestaurant = function(response) {
        console.log(response);
    }
}]);