var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('AddRestaurantController', ['$scope', 'addRestaurantService', 'utilService', function ($scope, addRestaurantService, utilService)
{
    $scope.restaurant = {
        Name: "",
        Phone: "",
        Url: "",
        MenuUrl: ""
    }

    $scope.addRestaurant = function (restaurant) {

        return addRestaurantService.addRestaurant(restaurant)
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