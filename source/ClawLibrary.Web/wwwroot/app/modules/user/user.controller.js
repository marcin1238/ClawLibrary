(function() {
    'use strict';

    angular.module('clawlibrary.modules.user')
        .controller('userController', function ($rootScope, $scope, $state, $translate, userService, filesHelper) {

            $scope.imageUrl = userService.getUserImageUrl();;

            userService.getUser().then(function (data) {
                $scope.editableUser = data;
            }, function (error) { $state.go('dashboard') });

            $scope.saveUserDetails = function () {
                userService.updateUser($scope.editableUser).then(function (data) {
                    $scope.editableUser = data;
                    $rootScope.user = angular.copy(data);
                }, function (error) { });
            }

            $scope.uploadUserPicture = function (file) {

                filesHelper.convertToBase64(file).then(function (data) {
                    var base64result = data.split(',').pop();
                    userService.updateUserImage(base64result).then(function (data) {
                        $rootScope.$broadcast('image-changed', { url: $scope.imageUrl });
                    }, function (error) { });
                }, function (error) { });
            }
        });

})();