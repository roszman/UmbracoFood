var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('OrdersController', ['$scope', 'orderService', 'utilService', function ($scope, orderService, utilService) {
    $scope.orders = [];

    var loadData = function() {
        return orderService.getOrders()
            .then(onOrdersFetched);
    }

    var onOrdersFetched = function (response) {
        $scope.orders = response.data.Orders;

        angular.forEach($scope.orders, function(order, key) {
            order.Deadline = utilService.getDateWithouOffset(order.Deadline);

            if (order.EstimatedDeliveryTime) {
                order.EstimatedDeliveryTime = utilService.getDateWithouOffset(order.EstimatedDeliveryTime);
            }
        });

    }
    loadData();
}]);