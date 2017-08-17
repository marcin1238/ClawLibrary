(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .service('authService', function (webapiService, authRoutes, $q, tokenStorage) {

            this.getToken = function (loginData, appName) {
                var deferred = $q.defer();

                webapiService.post(authRoutes.getToken, loginData).then(function (data) {
                    tokenStorage.set(data);
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
          }
            this.register = function (registerData) {
              var url = authRoutes.register;
                return webapiService.post(url, registerData);
            }

            this.reset = function (email) {
              var url = authRoutes.reset;
              return webapiService.post(url, { email: email });
            }

            this.setPassword = function (data) {
                var url = authRoutes.setPassword;
                return webapiService.post(url, data);
            }

            this.verify = function (verifyData) {
                var deferred = $q.defer();

                var url = authRoutes.verify;
                webapiService.post(url, verifyData).then(function (data) {
                    tokenStorage.set(data);
                    deferred.resolve(data);
                }, function (error) {
                    deferred.reject(error);
                });

                return deferred.promise;
            }
        })
        .constant('authRoutes', (function () {
            var baseAuthRoute = '/api';

            return {
                getToken: baseAuthRoute + '/token',
                register: baseAuthRoute + '/register',
                verify: baseAuthRoute + '/verify',
                reset: baseAuthRoute + '/password/reset',
                setPassword: baseAuthRoute + '/password/set'
            }
        })());
})();