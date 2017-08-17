(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .controller('registerController', function ($rootScope, $scope, $state, $translate, authService) {

            $scope.registerData = {};
            $scope.existingUser = null;

           

            $scope.register = function () {
                $scope.registerInProgress = true;

                authService.register($scope.registerData).then(function (data) {
                  $state.go('confirmation');
                }, function (error) {
                    $scope.registerInProgress = false;
                    if (error && error.errorName === 'RecordAlreadyExisting') {
                        $scope.existingUser = $scope.registerData.login;
                    }
                });
            }
        });

})();;