var umbracoFood = angular.module("umbracoFoodApp", ['angular-growl', 'ui.bootstrap']);


umbracoFood.config(['growlProvider', '$httpProvider', function (growlProvider, $httpProvider) {

    growlProvider.globalPosition('top-center');

    $httpProvider.interceptors.push(function($q,growl) {
        return {
            'responseError': function (response) {
                var message =  "Operacja nie udała się. Spróbuj ponownie lub skontaktuj się z administratorem.";
                growl.error(message);

                return $q.reject();

            }
        };
    });
}]);