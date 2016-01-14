angular.module("umbraco").controller("Restaurants.RestaurantEditController",
	function ($scope, $routeParams, restaurantResource, notificationsService, navigationService, treeService) {

	    $scope.loaded = false;

        var restaurant = {};

	    if ($routeParams.id == -1) {
	        $scope.restaurant = restaurant;
	        $scope.loaded = true;
	    }
	    else {
	        restaurantResource.getById($routeParams.id).then(function (response) {
	            $scope.restaurant = response.data;

	            $scope.loaded = true;

	        });
	    }


	    $scope.save = function (restaurant) {
	        restaurantResource.save(restaurant)
                .then(function () {
                    $scope.restaurantForm.$dirty = false;
                    navigationService.syncTree({ tree: 'RestaurantsTree', path: [-1, -1], forceReload: true });
                    notificationsService.success("Success", restaurant.Name + " has been saved");
                });
	    };


	});