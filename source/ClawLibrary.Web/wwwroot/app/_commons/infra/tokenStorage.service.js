(function () {
    'use strict';

    angular.module('clawlibrary.commons.helpers')
        .service('tokenStorage', function (webStorage) {

            var tokenKey = 'authorizationData';
            var nameTokenKey = 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name';
            var rolesTokenKey = 'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

            this.get = function () {
                var tokenData = webStorage.get(tokenKey)
                if (tokenData != null && verifyToken(tokenData)) {
                    return tokenData;
                }
                return null;
            }

            this.isInRoles = function (roles) {
                var token = this.get();
                if (token && token.roles) {
                    return roles.split(',').filter(function (el) {
                        return token.roles.indexOf(el.trim()) !== -1;
                    }).length > 0;
                }
                return false;
            }

            this.set = function (tokenData) {
                webStorage.set(tokenKey, prepareTokenData(tokenData));
            }

            this.clear = function () {
                webStorage.remove(tokenKey);
            }

            var verifyToken = function (tokenData) {
                var expirationDate = new Date(tokenData.expires_at);
                if (expirationDate <= new Date()) {
                    webStorage.remove(tokenKey);
                    return false;
                }
                return true;
            }

            var prepareTokenData = function (tokenData) {
                var decoded = jwt_decode(tokenData.access_token);
                tokenData.name = decoded[nameTokenKey];
                tokenData.roles = decoded[rolesTokenKey];
                return tokenData;
            }
        });
})();