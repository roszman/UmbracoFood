var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('OrdersController', ['$scope', 'orderService', 'utilService', function ($scope, orderService, utilService) {
    $scope.orders = [];
    $scope.status = {
        InProgress: 1,
        InDelivery: 2,
        InKitchen: 3
    }

    var loadData = function() {
        return orderService.getOrders()
            .then(onOrdersFetched);
    }

    var onOrdersFetched = function (response) {
        $scope.orders = response.data.Orders;

        angular.forEach($scope.orders, function(order, key) {
            order.Deadline = utilService.getDateWithoutOffset(order.Deadline);

            if (order.EstimatedDeliveryTime) {
                order.EstimatedDeliveryTime = utilService.getDateWithoutOffset(order.EstimatedDeliveryTime);
            }


            var diffMs = order.Deadline.getTime() - new Date().getTime();

            var diffS = diffMs / 1000;
            console.log(diffS)
            order.Countdown = diffS > 0 ? diffS : null;

        });
    }
    loadData();
}]);