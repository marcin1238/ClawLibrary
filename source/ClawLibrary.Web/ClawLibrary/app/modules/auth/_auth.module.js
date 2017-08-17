(function () {
    'use strict';

    angular.module('clawlibrary.modules.auth', ['ui.router'])
        .config(function ($stateProvider) {
            $stateProvider
                .state('login',
                    {
                        url: '/login',
                        allowAnonymous: true,
                        allowAuthorized: true,
                        templateUrl: '/app/modules/auth/login/login.html',
                        controller: 'loginController'
                    })
                .state('register',
                    {
                        url: '/register',
                        allowAnonymous: true,
                        allowAuthorized: false,
                        templateUrl: '/app/modules/auth/register/register.html',
                        controller: 'registerController'
                    })
              .state('resetInformation',
                    {
                      url: '/resetInformation',
                        allowAnonymous: true,
                        allowAuthorized: false,
                        templateUrl: '/app/modules/auth/reset/resetInformation.html',
                        controller: 'resetInformationController'
              })
                .state('reset',
                    {
                        url: '/reset',
                        allowAnonymous: true,
                        allowAuthorized: false,
                        templateUrl: '/app/modules/auth/reset/reset.html',
                        controller: 'resetController'
              })
              .state('newPassword',
                    {
                      url: '/newPassword',
                        allowAnonymous: true,
                        allowAuthorized: false,
                        templateUrl: '/app/modules/auth/setPassword/newPassword.html',
                        controller: 'newPasswordController'
              })
              .state('setPassword', {
                    url: '/password/set/:verificationCode',
                    allowAuthorized: false,
                    allowAnonymous: true,
                    templateUrl: '/app/modules/auth/setPassword/setPassword.html',
                    controller: 'setPasswordController'
              })
                .state('verify', {
                    url: '/verify/:verificationCode',
                    allowAuthorized: false,
                    allowAnonymous: true,
                    templateUrl: '/app/modules/auth/verify/verify.html',
                    controller: 'verifyController'
                })
                .state('confirmation',
                    {
                        url: '/confirmation',
                        allowAnonymous: true,
                        allowAuthorized: false,
                        templateUrl: '/app/modules/auth/register/confirmation.html',
                        controller: 'confirmationController'
                    });
        });

})();
