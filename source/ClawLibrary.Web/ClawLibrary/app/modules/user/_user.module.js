(function() {
    'use strict';

    angular.module('clawlibrary.modules.user', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('user', {
                    parent: 'shell',
                    url: '/user',
                    allowAnonymous: false,
                    allowAuthorized: true,
                    templateUrl: '/app/modules/user/user.html',
                    controller: 'userController'
                });
        });

})();
