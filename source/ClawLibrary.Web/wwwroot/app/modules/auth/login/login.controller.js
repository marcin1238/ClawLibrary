(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .controller('loginController', function ($rootScope, $scope, $state, $translate, authService) {

            $scope.loginData = {};

            $scope.login = function () {
                $scope.loginInProgress = true;

                authService.getToken($scope.loginData).then(function (data) {
                    $state.go('dashboard');
                }, function (error) {
                    $scope.loginInProgress = false;

                    if (error && error.errorName === 'Unauthorized') {
                        $scope.errorMessage = $translate.instant('MODULES.Auth.InvalidLoginMessage');
                    }
                });
            }
        });

})();