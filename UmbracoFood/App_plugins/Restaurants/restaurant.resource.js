angular.module("umbraco.resources")
	.factory("restaurantResource", function ($http) {
	    return {
	        getById: function (id) {
	            return $http.get("backoffice/Restaurants/RestaurantsApi/GetById/" + id);
	        },
	        getAll: function (id) {
	            return $http.get("backoffice/Restaurants/RestaurantsApi/GetAll");
	        },
	        save: function (person) {
	            return $http.post("backoffice/Restaurants/RestaurantsApi/PostSave", angular.toJson(person));
	        },
	        deleteById: function (id) {
	            return $http.delete("backoffice/Restaurants/RestaurantsApi/DeleteById?id=" + id);
	        }
	    };
	});