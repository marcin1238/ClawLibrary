(function() {
    'use strict';

    angular.module('clawlibrary.modules.shell')
        .controller('shellController', function ($rootScope, $scope, $state, $translate, tokenStorage, shellService) {

//            $scope.username = null;
//            $scope.header = null;

            $rootScope.user = {};
            $scope.imageUserUrl = shellService.getUserPictureUrl();

            $scope.getUser = function () {
                shellService.getUser().then(function (data) {
                    $rootScope.user = data;
                }, function (error) { });
            }

            $scope.logout = function () {
                tokenStorage.clear();
                $state.go('dashboard');
            }

            $scope.login = function () {
                $state.go('login');
            }
            
            $scope.checkUser = function () {
              var token = tokenStorage.get();
              if (token === undefined || token === null) {
                  $scope.username = null;
                  $scope.header = null;
                  return true;
              }
              else {
                  $scope.username = token.name;
                  $scope.header = $rootScope.header;
                  return false;
              }
            }

            $scope.isAdmin = function () {
                return tokenStorage.isInRoles("Admin");
            }
            $scope.isRegularUser = function () {
              return tokenStorage.isInRoles("Regular");
            }

            $scope.checkUser();
            $scope.getUser();
            
        });

})();