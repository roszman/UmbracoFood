angular.module("umbraco.resources")
	.factory("restaurantResource", function ($http) {
	    return {
	        getById: function (id) {
	            return $http.get("backoffice/Restaurants/RestaurantsApi/GetById/" + id);
	        },
	        getAll: function () {
	            return $http.get("backoffice/Restaurants/RestaurantsApi/GetAll");
	        },
	        save
                : function (restaurant) {
	            return $http.post("backoffice/Restaurants/RestaurantsApi/PostSave", angular.toJson(restaurant));
	        },
	        create: function (restaurant) {
	            return $http.post("backoffice/Restaurants/RestaurantsApi/PostCreate", angular.toJson(restaurant));
	        },
	        deleteById: function (id) {
	            return $http.delete("backoffice/Restaurants/RestaurantsApi/DeleteById?id=" + id);
	        }
	    };
	});