(function () {
    'use strict';

    angular.module('clawlibrary.modules.settings')
        .service('settingsService', function (webapiService, settingsRoutes) {

          
        })
        .constant('settingsRoutes', (function () {
            var baseSettingsRoute = '/api/settings';

            return {
                baseSettingsRoute
            }
        })());
})();