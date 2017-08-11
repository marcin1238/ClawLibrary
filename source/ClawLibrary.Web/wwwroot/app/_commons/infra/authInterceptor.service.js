(function () {
    'use strict';

    angular.module('clawlibrary.commons.infra')
        .service('authInterceptorService', function ($location, tokenStorage, $q) {

            this.request = function (config) {
                config.headers = config.headers || {};

                var authData = tokenStorage.get();
                if (authData) {
                    config.headers.Authorization = authData.token_type + ' ' + authData.access_token;
                }

                return config;
            }

            this.responseError = function (rejection) {
                if (rejection.status === 401) {
                    var authData = tokenStorage.get();
                    if (authData == null) {
                        $location.path('/login');
                    }

                }
                return $q.reject(rejection);
            }
        });
})();