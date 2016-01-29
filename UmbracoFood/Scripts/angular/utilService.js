var umbracoFood = angular.module("umbracoFoodApp");


umbracoFood.factory('utilService', ['growl', function (growl) {
        return{
            growl: function (response) {
                if (response.status == 200 || response.status == 204) {
                    growlSuccess(response.message);
                } else {
                    growlFailure(response.message);
                }
            },
            growlSuccess: function(message) {
                growl.success(message);
            },
            growlFailure: function (message) {
                growl.error(message);
            },
            getDateWithoutOffset: function (dateWithOffset) {
                var date = new Date(dateWithOffset);
                return new Date(date.getTime() + date.getTimezoneOffset() * 60000);
            }
        }
}]);