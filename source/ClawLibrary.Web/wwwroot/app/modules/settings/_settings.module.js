(function() {
    'use strict';

    angular.module('clawlibrary.modules.settings', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('settings', {
                    parent: 'shell',
                    url: '/settings',
                    allowAnonymous: false,
                    allowAuthorized: true,
                    templateUrl: '/app/modules/settings/settings.html',
                    controller: 'settingsController',
                });
        });

})();