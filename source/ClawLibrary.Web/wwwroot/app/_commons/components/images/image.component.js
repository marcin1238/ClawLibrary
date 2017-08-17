(function () {

  angular.module('clawlibrary.commons.components')
        .directive('httpSrc', function ($http, $cacheFactory) {

            return {
                restrict: 'A',
                link: function (scope, element, attributes) {

                    var requestConfig = {
                        method: 'GET',
                        responseType: 'arraybuffer',
                        cache: 'true'
                    };

                    scope.$on('image-changed', function (event, args) {
                        if (attributes.httpSrc === args.url) {
                            var httpCache = $cacheFactory.get('$http');
                            httpCache.remove(args.url);
                            attributes.$set('httpSrc', attributes.httpSrc);
                        }
                    });

                    function base64Img(data) {
                        var arr = new Uint8Array(data);
                        var raw = '';
                        var i, j, subArray, chunk = 5000;
                        for (i = 0, j = arr.length; i < j; i += chunk) {
                            subArray = arr.subarray(i, i + chunk);
                            raw += String.fromCharCode.apply(null, subArray);
                        }
                        return btoa(raw);
                    };

                    attributes.$observe('httpSrc', function (newValue) {
                        requestConfig.url = newValue;
                        $http(requestConfig)
                            .then(function (data) {
                                attributes.$set('src', "data:image/jpeg;base64," + base64Img(data.data));
                            }, function (error) {
                                attributes.$set('src', attributes.errSrc);
                            });
                    });
                }
            };
        });
})();