angular.module("umbraco").controller("Restaurants.RestaurantCreateController",
	function ($scope, restaurantResource, notificationsService, navigationService, $location) {
	    $scope.restaurant = {};
	    $scope.create = function (restaurant) {
	        restaurantResource.create(restaurant).then(function (response) {
	            $scope.restaurant = response.data;
	            $scope.restaurantForm.$dirty = false;
	            navigationService.syncTree({ tree: 'RestaurantsTree', path: [-1, -1], forceReload: true });
	            navigationService.hideDialog();
	            $location.path("/Restaurants/RestaurantsTree/edit/" + response.data);
	            notificationsService.success("Success", restaurant.Name + " has been saved");
	        });
	    };
	});