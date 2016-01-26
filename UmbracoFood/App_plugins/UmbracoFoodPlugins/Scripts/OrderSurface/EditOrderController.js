var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('EditOrderController', ['$scope', 'orderService', 'utilService', function ($scope, orderService, utilService) {
    $scope.orderId = null;
    $scope.estimatedDeliveryTimeDisabled = true;
    $scope.meal = {
        MealName: "",
        Price: 0,
        Count: 0,
        Person: ""
    }
    var mealOrig = angular.copy($scope.meal);

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
        if ($scope.meal.MealName && $scope.meal.Price >= 0 && $scope.meal.Count > 0 && $scope.meal.Person) {
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

    $scope.statusChanged = function () {
        if ($scope.order.StatusId == 2) {
            $scope.estimatedDeliveryTimeDisabled = false;
        } else {
            $scope.order.EstitmatedDeliveryTime = null;
            $scope.estimatedDeliveryTimeDisabled = true;
        }
    }
    
    $scope.changeStatus = function () {
        if ($scope.order.StatusId == 2 && ($scope.order.EstitmatedDeliveryTime == null || $scope.order.EstitmatedDeliveryTime < 0)) {
            utilService.growlFailure("Musisz uzupełnić przewidywany czas dostarczenia zamówienia");
            return;
        }

        return orderService.changeStatus({
            OrderId: $scope.order.RestaurantId,
            StatusId: $scope.order.StatusId,
            EstitmatedDeliveryTime: $scope.order.EstitmatedDeliveryTime
        });
    }





    
}]);


