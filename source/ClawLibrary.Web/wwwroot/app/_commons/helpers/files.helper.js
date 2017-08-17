(function () {
    'use strict';

    angular.module('clawlibrary.commons.helpers')
        .service('filesHelper', function ($http, $q) {

            this.convertToBase64 = function (file) {
                var promise = $q.defer();

                var reader = new FileReader();
                reader.onload = function (e) {
                    promise.resolve(e.target.result);
                };
                reader.onerror = function (e) {
                    promise.reject(e);
                };
                reader.readAsDataURL(file);

                return promise.promise;
            }

        });
})();