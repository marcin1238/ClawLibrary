

(function () {
    'use strict';

    angular.module('clawlibrary.commons.helpers')
        .service('translateHelper', function ($translate) {

            this.getEnumValues = function (key) {
                var translationTable = $translate.getTranslationTable();
                var values = [];

                for (var ttKey in translationTable) {
                    if (translationTable.hasOwnProperty(ttKey) && ttKey.startsWith('ENUM.' + key)) {
                        values.push({ value: ttKey.replace('ENUM.' + key + '.', ''), text: translationTable[ttKey] });
                    }
                }
                return values;
            }
        });
})();