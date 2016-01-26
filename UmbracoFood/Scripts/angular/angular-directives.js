var umbracoFood = angular.module("umbracoFoodApp");

umbracoFood.directive('ufConfirmation', ['$uibModal', function ($uibModal) {

    var modalInstanceCtrl = ["$scope", "$uibModalInstance", function ($scope, $uibModalInstance) {
        $scope.ok = function() {
            $uibModalInstance.close();
        };

        $scope.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        }
    }];

    return {
        restrict: 'A',
        scope: {
            ngRealClick: "&ngClick"
        },
        link: function(scope, element, attrs) {
            element.bind('click', function(e) {
                var message = attrs.ufConfirmation;

                if (message) {
                    e.stopImmediatePropagation();
                    e.preventDefault();
                    
                    var modalHtml = '<div class="modal-body">' + message + '</div>';
                    modalHtml += '<div class="modal-footer"><button class="btn btn-primary" ng-click="ok()">OK</button><button class="btn btn-warning" ng-click="cancel()">Anuluj</button></div>';

                    var modalInstance = $uibModal.open({
                        template: modalHtml,
                        controller: modalInstanceCtrl
                    });

                    modalInstance.result.then(function() {
                        scope.ngRealClick();
                    }, function () {
                        //modal dismissed
                    });

                }
            });
        }

    }

}]);