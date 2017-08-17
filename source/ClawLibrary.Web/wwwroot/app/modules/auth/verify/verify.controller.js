(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .controller('verifyController', function ($rootScope, $scope, $state, $stateParams, $translate, authService) {

            $scope.verifyData = {
                verificationCode: $stateParams.verificationCode
            };

            $scope.verify = function () {
                $scope.signingInProgress = true;

                authService.verify($scope.verifyData).then(function (data) {
                    $state.go('dashboard');
                }, function (error) { $scope.signingInProgress = false; });
            }
        });

})();