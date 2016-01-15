var umbracoFood = angular.module("umbracoFoodApp", ['angular-growl']);


umbracoFood.config(['growlProvider', function (growlProvider) {
    growlProvider.globalPosition('top-center');
}]);