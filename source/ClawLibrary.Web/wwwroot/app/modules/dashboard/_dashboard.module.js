(function() {
    'use strict';

    angular.module('clawlibrary.modules.dashboard', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('dashboard', {
                    parent: 'shell',
                    url: '/dashboard',
                    allowAnonymous: true,
                    allowAuthorized: true,
                    templateUrl: '/app/modules/dashboard/dashboard.html',
                    controller: 'dashboardController',
                });
        });

})();
