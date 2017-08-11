(function () {
    'use strict';

    angular.module('clawlibrary.modules.shell', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('shell', {
                    url: '',
                    abstract: true,
                    allowAnonymous: true,
                    allowAuthorized: true,
                    templateUrl: '/app/modules/_shell/shell.html',
                    controller: 'shellController'
                });
        });

})();
