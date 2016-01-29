var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.controller('CreateOrderController', ['$scope', 'orderService', 'restaurantService', 'utilService', function ($scope, orderService, restaurantService, utilService)
{
    $scope.restaurants = [];
    $scope.meal = {
        Name: "",
        Price: "",
        Count: 0
    }
    $scope.mealOrig = angular.copy($scope.meal);

    $scope.minDateTime = new Date();
    var deadline = new Date();
    deadline.setHours(deadline.getHours() + 1);

    $scope.order = {
        Owner: "",
        SelectedRestaurantId: 0,
        Deadline: deadline,
        AccountNumber: "",
        Meals: []
    }
    
    $scope.addMeal = function () {
        if (!$scope.isMealValid()) {
            utilService.growlFailure("Uzupełnij poprawnie zamówienie");
            return;
        }
        $scope.order.Meals.push($scope.meal);
        clearAddedMeal();
    }



    $scope.isMealValid = function () {
        if ($scope.meal.Name && $scope.meal.Price >= 0 && $scope.meal.Count > 0) {
            return true;
        }
        return false;
    }

    var clearAddedMeal = function () {
        $scope.meal = angular.copy($scope.mealOrig);
    }

    $scope.summary = function() {
        var sum = 0;
        angular.forEach($scope.order.Meals, function (item, key) {
            sum += item.Price * item.Count;
        });
        return sum;
    }
    
    $scope.createOrder = function () {
        return orderService.createOrder($scope.order)
            .then(onOrderCreated);
    }

    var onOrderCreated = function () {
        utilService.growlSuccess("Zamówienie zostało utworzone.");
    }
    
    var loadRestaurants = function () {
        return restaurantService.getSelectRestaurantsItems()
            .then(onSelectRestaurantsItemsFetched);
    }

    var onSelectRestaurantsItemsFetched = function (response) {
        $scope.restaurants = response.data.Restaurants;
    }
    
    var init = function () {
        loadRestaurants();
    }

    init();
}]);


