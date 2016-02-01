var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('HomePageController', ['$scope', 'utilService', function ($scope, utilService)
{
    $scope.showMessage = function (message) {
        if (message) {
            utilService.growlSuccess(message);
        }
    }
}]);


