var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('AddRestaurantController', ['$scope', 'restaurantService', 'utilService', function ($scope, restaurantService, utilService)
{
    $scope.restaurant = {
        Name: "",
        Phone: "",
        Url: "",
        MenuUrl: ""
    }

    $scope.addRestaurant = function (restaurant) {

        return restaurantService.addRestaurant(restaurant)
            .then(clearForm)
            .then(onAddRestaurant);
    }

    var onAddRestaurant = function(response) {
        utilService.growlSuccess("Restauracja została dodana");
    }

    var clearForm = function () {
        $scope.restaurant.Name = "";
        $scope.restaurant.Phone = "";
        $scope.restaurant.Url = "";
        $scope.restaurant.MenuUrl = "";
    }
}]);