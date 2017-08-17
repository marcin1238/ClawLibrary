(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .controller('resetController', function ($rootScope, $scope, $state, $translate, authService) {

            $scope.email = null;

            $scope.reset = function () {
                $scope.resetInProgress = true;

                authService.reset($scope.email).then(function (data) {
                    $state.go('resetInformation');
                }, function (error) { $scope.resetInProgress = false; });
            }
        });

})();