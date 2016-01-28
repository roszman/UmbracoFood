var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('EditOrderController', ['$scope', 'orderService', 'utilService', function ($scope, orderService, utilService) {
    $scope.orderId = null;

    var mealOrig;
    var status = {
        InProgress: 1,
        InDelivery: 2,
        InKitchen: 3
    }

    $scope.estimatedMinutes = null;


    $scope.init = function (id) {
        $scope.orderId = id;
        fetchOrder();
    }

    var fetchOrder = function () {
        return orderService.getOrder($scope.orderId)
            .then(onOrderFetched);
    }

    var onOrderFetched = function (response) {
        $scope.order = response.data;

        var estimatedDeliveryTimeWithoutOffset = utilService.getDateWithouOffset($scope.order.EstimatedDeliveryTime);
        var now = new Date();

        var diffMs = (estimatedDeliveryTimeWithoutOffset - now);
        var diffMins = Math.round(((diffMs % 86400000) % 3600000) / 60000);

        $scope.estimatedMinutes = diffMins;


        $scope.meal = {
            MealName: "",
            Price: 0,
            Count: 0,
            Person: "",
            OrderId: $scope.order.OrderId
        }
        mealOrig = angular.copy($scope.meal);
    }

    $scope.addMeal = function () {
        if (!$scope.isMealValid()) {
            utilService.growlFailure("Uzupełnij poprawnie formularz");
            return;
        }
        return orderService.addMeal($scope.meal)
            .then(pushMealToList)
            .then(onMealAdded);
    }

    var onMealAdded = function () {
        clearAddedMeal();
        utilService.growlSuccess("Posilek został dołączony do zamówienia");
    }

    var pushMealToList = function () {
        $scope.order.Meals.push($scope.meal);
    }

    $scope.isMealValid = function () {
        if (angular.isDefined($scope.meal) && $scope.meal.MealName && $scope.meal.Price >= 0 && $scope.meal.Count > 0 && $scope.meal.Person) {
            return true;
        }
        return false;
    }

    var clearAddedMeal = function () {
        $scope.meal = angular.copy(mealOrig);
    }

    $scope.summary = function() {
        var sum = 0;
        if ($scope.order) {
            angular.forEach($scope.order.Meals, function (item, key) {
                sum += item.Price * item.Count;
            });
        }
        return sum;
    }

    $scope.estimatedDeliveryTimeMessage = function () {
        if(angular.isUndefined($scope.order) || $scope.order.Status == status.InDelivery) {
            return null;
        }
        else if ($scope.order.Status == status.InProgress) {
            return "Jeszcze nie zamówione!";
        }
        else {
            return "W kuchni! :)";
        }
    }

    $scope.estimatedDeliveryTimeVisible = function () {
        return angular.isDefined($scope.order) && $scope.order.Status == status.InDelivery;
    }

    $scope.editOrder = function () {
        if ($scope.order.Status == status.InDelivery && ($scope.estimatedMinutes == null || $scope.estimatedMinutes < 0)) {
            utilService.growlFailure("Musisz uzupełnić przewidywany czas dostarczenia zamówienia");
            return;
        }


        var estimatedDelieryTime = new Date();
        estimatedDelieryTime.setMinutes(estimatedDelieryTime.getMinutes() + $scope.estimatedMinutes);

        var editOrderModel = {
            OrderId: $scope.order.OrderId,
            Status: $scope.order.Status,
            EstitmatedDeliveryTime: estimatedDelieryTime
        }

        return orderService.editOrder(editOrderModel)
                .then(onOrderEdited);
    }

    var onOrderEdited = function (response) {
        utilService.growlSuccess("Twoje zmiany zostały zapisane");
    }
}]);


