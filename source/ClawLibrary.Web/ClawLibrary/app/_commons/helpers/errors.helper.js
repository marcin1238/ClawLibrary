(function () {
    'use strict';

    angular.module('clawlibrary.commons.helpers')
        .service('errorsHelper', function ($translate, alertHelper) {

            this.handleError = function (error) {
                if (error && error.errorName) {
                    alertHelper.alert($translate.instant('ENUM.ErrorCode.' + error.errorName));
                }
            }
        });
})();