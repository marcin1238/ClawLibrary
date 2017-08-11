
(function () {
    'use strict';

    angular.module('clawlibrary.commons.helpers')
        .service('urlHelper', function () {

            this.build = function (url, object) {
                return url.replace(/{([\w0-9]+)}/g, function (val, match) {
                    return object[match];
                })
            }
        });
})();