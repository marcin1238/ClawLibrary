(function () {
    'use strict';

    angular.module('clawlibrary.commons.infra')
        .service('webapiService', function ($http, $q) {

            function buildUrl(relativeUrl) {
                return relativeUrl;
                //return CONFIG.apiBasePath + relativeUrl;
            }

            this.put = function (relativeUrl, params, config) {
                var promise = $q.defer();

                $http.put(buildUrl(relativeUrl), params, config)
                    .then(function (response) {
                        promise.resolve(response.data);
                    }, function (error) {
                        promise.reject(error.data);
                    });
                return promise.promise;
            }

            this.post = function (relativeUrl, params, config) {
                var promise = $q.defer();

                $http.post(buildUrl(relativeUrl), params, config)
                    .then(function (response) {
                        promise.resolve(response.data);
                    }, function (error) {
                        promise.reject(error.data);
                    });
                return promise.promise;
            }

            this.patch = function (relativeUrl, params, config) {
                var promise = $q.defer();

                $http.patch(buildUrl(relativeUrl), params, config)
                    .then(function (response) {
                        promise.resolve(response.data);
                    }, function (error) {
                        promise.reject(error.data);
                    });
                return promise.promise;
            }

            this.get = function (relativeUrl, config) {
                var promise = $q.defer();

                $http.get(buildUrl(relativeUrl), config)
                    .then(function (response) {
                        promise.resolve(response.data);
                    }, function (error) {
                        promise.reject(error.data);
                    });
                return promise.promise;
            }

            this.delete = function (relativeUrl, config) {
                var promise = $q.defer();

                $http.delete(buildUrl(relativeUrl), config)
                    .then(function (response) {
                        promise.resolve(response.data);
                    }, function (error) {
                        promise.reject(error.data);
                    });

                return promise.promise;
            }
        });
})();