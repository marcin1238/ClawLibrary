(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .controller('setPasswordController', function ($rootScope, $scope, $state, $stateParams, $translate, authService) {

            $scope.setPasswordData = {
                verificationCode: $stateParams.verificationCode
            };

            $scope.setPassword = function () {
                $scope.settingInProgress = true;

                authService.setPassword($scope.setPasswordData).then(function (data) {
                  $state.go('newPassword');
                }, function (error) { $scope.settingInProgress = false; });
            }
        });

})();