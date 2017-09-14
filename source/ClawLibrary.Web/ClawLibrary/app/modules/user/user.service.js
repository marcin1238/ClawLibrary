(function() {
    'use strict';

    angular.module('clawlibrary.modules.user')
        .service('userService', function (webapiService, userRoutes) {

            this.getUser = function () {
                return webapiService.get(userRoutes.getUser);
            }

            this.updateUser = function (user) {
                return webapiService.put(userRoutes.updateUser, user);
            }

            this.getUserImageUrl = function () {
                return userRoutes.getUserImage;
            }

            this.updateUserImage = function (pictureBase64) {
                return webapiService.patch(userRoutes.updateUserImage, { pictureBase64: pictureBase64 });
            }

        })
        .constant('userRoutes', (function () {
            var baseUsersRoute = 'api/users';

            return {
                getUser: baseUsersRoute,
                updateUser: baseUsersRoute,
                getUserImage: baseUsersRoute + '/user/picture',
                updateUserImage: baseUsersRoute + '/user/picture'
            }
        })());

})();