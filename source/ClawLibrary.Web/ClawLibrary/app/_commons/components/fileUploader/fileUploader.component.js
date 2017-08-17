(function () {

  angular.module('clawlibrary.commons.components')
        .directive('fileUploader', function () {

            return {
                restrict: 'E',
                scope: {
                    src: '=',
                    onSelect: '&',
                    defaultSrc: '@'
                },
                controller: function ($scope, $timeout, $element) {
                    $scope.hasImage = true;

                    $scope.imageChanged = function (element) {
                        if (element.files[0] !== undefined) {
                            $scope.onSelect({ file: element.files[0] })
                        }
                    }

                    $scope.imageLoadError = function (element) {
                        $timeout(function () { $scope.hasImage = false; });
                    }

                    $scope.imageLoaded = function (element) {
                        $timeout(function () { $scope.hasImage = true; });
                    }

                    $scope.openFileDialog = function () {
                        $element.find('input').click();
                    }
                },
                templateUrl: '/app/_commons/components/fileUploader/fileUploader.html',
            }
        });
})();