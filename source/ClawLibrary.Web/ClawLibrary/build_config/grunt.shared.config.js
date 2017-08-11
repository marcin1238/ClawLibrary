
var path = require('path');

module.exports = {

    copy: {
        libs: {
            files: [
                {
                    src: [
                        '<%= build_settings.vendors.js %>',
                        '<%= build_settings.vendors.units %>',
                        '<%= build_settings.vendors.assets %>'
                    ],
                    dest: './wwwroot/libs/',
                    nonull: true,
                    rename: function (dest, src) {
                        var srcPathParts = src.split('/').slice(1);
                        var dirs = [srcPathParts[0], srcPathParts[srcPathParts.length - 1]];
                        return path.join(dest, dirs.join(path.sep)).replace('.min.js', '.js');
                    },
                    expand: true
                },
                {
                    src: [
                        '<%= build_settings.vendors.css %>'
                    ],
                    dest: './wwwroot/assets/css/',
                    nonull: true,
                    rename: function (dest, src) {
                        var fileName = path.basename(src).replace('.min.css', '.css');
                        return path.join(dest, fileName);
                    },
                    expand: true
                },
                //{
                //    src: [
                //        '<%= build_settings.vendors.kendoAssets %>'
                //    ],
                //    dest: './wwwroot/assets/css/',
                //    cwd: './bower_components/kendo-ui/styles/',
                //    nonull: true,
                //    expand: true
                //},
                {
                    src: [
                        '<%= build_settings.vendors.fonts %>'
                    ],
                    dest: './wwwroot/assets/fonts',
                    nonull: true,
                    flatten: true,
                    expand: true
                }
            ]
        },
        //local_config: {
        //    files: [
        //        {
        //            src: ['config/app.config.dev.js'],
        //            dest: 'app/app.config.js'
        //        }
        //    ]
        //}
    },

    clean: {
        wwwroot: {
            options: {
                force: true
            },
            src: ['./wwwroot/*']
        }
    },

    build_settings: {
      source_dir: 'ClawLibrary',
        build_dir: 'wwwroot',

        vendors: {
            units: [
                'bower_components/jquery/dist/jquery.js',
                'bower_components/angular-mocks/angular-mocks.js'
            ],
            js: [
                'bower_components/angular/angular.js',
                'bower_components/angular-bindonce/bindonce.js',
                'bower_components/angular-bootstrap/ui-bootstrap-tpls.min.js',
                'bower_components/angular-resource/angular-resource.js',
                'bower_components/angular-filter/dist/angular-filter.js',
                'bower_components/angular-ui-router/release/angular-ui-router.js',
                'bower_components/angular-messages/angular-messages.min.js',
                'bower_components/angular-ui-utils/modules/route/route.js',
                'bower_components/angular-webstorage/angular-webstorage.js',
                'bower_components/angular-translate/angular-translate.js',
                'bower_components/angular-translate-loader-static-files/angular-translate-loader-static-files.js',
                'bower_components/admin-lte/dist/js/app.js',
                'bower_components/admin-lte/plugins/slimScroll/jquery.slimscroll.js',
                'bower_components/jquery/dist/jquery.js',
                'bower_components/bootstrap/dist/js/bootstrap.js',
                'bower_components/angular-file-saver/dist/angular-file-saver.bundle.js',
                'bower_components/jwt-decode/build/jwt-decode.js'
            ],
            css: [
                'bower_components/bootstrap/dist/css/bootstrap.css',
                'bower_components/admin-lte/dist/css/AdminLTE.min.css',
                'bower_components/admin-lte/dist/css/skins/skin-blue.min.css',
                'bower_components/font-awesome/css/font-awesome.css',

            ],
            assets: [

            ],
            images: [

            ],
            fonts: [
                'bower_components/bootstrap/dist/fonts/*.*',
                'bower_components/font-awesome/fonts/*.*',

            ]

        }
    }
};
