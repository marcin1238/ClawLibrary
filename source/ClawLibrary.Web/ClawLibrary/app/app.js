
(function () {
    'use strict';

    var appMdl = angular.module('clawlibrary', [
        //// Common
        'clawlibrary.commons.components',
        'clawlibrary.commons.helpers',
        'clawlibrary.commons.infra',

        //// Modules    		
        'clawlibrary.modules.shell',
        'clawlibrary.modules.auth',
        'clawlibrary.modules.dashboard',
        'clawlibrary.modules.settings',
        'clawlibrary.modules.user',

        //3rd party
        'ui.router',
        'angular.filter',
        'pascalprecht.translate',
        'webStorageModule',
        'ngMessages'
    ]);


    appConfig.$inject = ["$stateProvider", "$urlRouterProvider", "$translateProvider", "$httpProvider"];

    appRun.$inject = ["$rootScope", "webStorage", "$q", "$state", "$timeout", "tokenStorage"];

    appMdl
        .config(appConfig)
        .run(appRun);


    function appConfig($stateProvider, $urlRouterProvider, $translateProvider, $httpProvider) {

        //$stateProvider
        //    .state('error', {
        //        url: '/error',
        //        templateUrl: '/app/modules/errorPages/error.html'
        //    });

        $urlRouterProvider.otherwise('/dashboard');

        $translateProvider.useStaticFilesLoader({
            files: [{
                prefix: '/assets/resources/',
                suffix: '.json'
            }]
        });
        $translateProvider.preferredLanguage('PL');
        $translateProvider.useSanitizeValueStrategy('escape');

        $httpProvider.interceptors.push('authInterceptorService');
    };

    function appRun($rootScope, webStorage, $q, $state, $timeout, tokenStorage) {

        //support for redirectiong states
        $rootScope.$on('$stateChangeStart', function (event, to, params) {
            $timeout(function () { $.AdminLTE.layout.fix(); }, 150);

            if (to.allowAnonymous !== true && tokenStorage.get() === null) {
                event.preventDefault();
                $rootScope.bodyClass = 'login-page';
                $state.go('login');
            }
            if (to.allowAuthorized === false && tokenStorage.get() !== null) {
                event.preventDefault();
                $state.go('dashboard');
            }
            if (to.redirectTo) {
                event.preventDefault();
                $state.go(to.redirectTo, params, { location: 'replace' });
            }
        });

        //prevent from navigation when backspace is clicked on not focuesed input
        $(document).keydown(function (e) {
            if (e.keyCode === 8 && e.target.tagName !== 'INPUT' && e.target.tagName !== 'TEXTAREA') {
                e.preventDefault();
            }
        });

        $(document).ready(function () {
            $timeout(function () {
                $.AdminLTE.layout.fix();
            }, 100)
        })

        webStorage.prefix('clawlibrary');
    };

})();