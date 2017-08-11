(function () {
    'use strict';

    angular.module('clawlibrary.modules.shell')
        .service('shellService', function (webapiService, shellRoutes) {

            this.getUser = function () {
                return webapiService.get(shellRoutes.getUser);
            }

            this.getUserPictureUrl = function () {
                return shellRoutes.getUserPicture;
            }

        })
        .constant('shellRoutes', (function () {
            var baseShellRoute = 'api';

            return {
                getUser: baseShellRoute + '/users',
                getUserPicture: baseShellRoute + '/users/picture'
            }
        })());
})();