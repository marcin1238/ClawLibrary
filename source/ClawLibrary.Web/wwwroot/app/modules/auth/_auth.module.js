(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('login', {
                    url: '/login',
                    allowAnonymous: true,
                    allowAuthorized: true,
                    templateUrl: '/app/modules/auth/login/login.html',
                    controller: 'loginController'
                });
        });

})();
