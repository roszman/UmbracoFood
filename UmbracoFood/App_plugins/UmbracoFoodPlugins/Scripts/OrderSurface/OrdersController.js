var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('OrdersController', ['$scope', 'orderService', function ($scope, orderService) {
    $scope.orders = [];

    var loadData = function() {
        return orderService.getOrders()
            .then(onOrdersFetched);
    }

    var onOrdersFetched = function (response) {
        $scope.orders = response.data.Orders;
    }
    loadData();
}]);