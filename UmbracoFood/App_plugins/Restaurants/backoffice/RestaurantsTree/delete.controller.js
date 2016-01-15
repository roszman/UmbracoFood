angular.module("umbraco")
.controller("Restaurants.RestaurantDeleteController",
	function ($scope, restaurantResource, navigationService, treeService, $location) {
	    $scope.delete = function (id) {
	        restaurantResource.deleteById(id).then(function () {
	            treeService.removeNode($scope.currentNode);
	            navigationService.hideNavigation();
	            $location.path("/Restaurants");
	        });

	    };
	    $scope.cancelDelete = function () {
	        navigationService.hideNavigation();
	    };
	});