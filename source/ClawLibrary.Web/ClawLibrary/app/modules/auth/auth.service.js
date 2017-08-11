(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth')
        .service('authService', function (webapiService, authRoutes, $q, tokenStorage) {

            this.getToken = function (loginData, appName) {
                var deferred = $q.defer();

                webapiService.post(authRoutes.getToken, loginData).then(function (data) {
                    tokenStorage.set(data)
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
                getToken: baseAuthRoute + '/token'
            }
        })());
})();