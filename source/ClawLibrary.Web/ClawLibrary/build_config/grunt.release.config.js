//module.exports = {

//    copy: {
//        release: {
//            files: [{
//                src: ['app/**/*.html'],
//                dest: './wwwroot/',
//                cwd: './ClientApp/',
//                nonull: true,
//                expand: true
//            }, {
//                src: 'assets/**/*',
//                dest: './wwwroot/',
//                cwd: './ClientApp/',
//                nonull: true,
//                expand: true
//            }]
//        },
//    },

//    injector: {
//        release: {
//            options: {
//                parentDirFirst: false,
//                relative: false,
//                transform: function (filePath) {
//                    var ext = filePath.split('.').pop();
//                    filePath = filePath.replace('/wwwroot', '');

//                    switch (ext) {
//                        case 'css': return '<link rel="stylesheet" href="' + filePath + '">';
//                        case 'js': return '<script src="' + filePath + '"></script>';
//                        case 'html': return '<link rel="import" href="' + filePath + '">';
//                    }
//                }
//            },
//            files: {
//                './Views/Angular/Index.cshtml': [

//                    'wwwroot/libs/jquery/jquery.js',
//                    'wwwroot/libs/angular/angular.js',
//                    'wwwroot/libs/kendo-ui/kendo.ui.core.js',
//                    'wwwroot/libs/angular-filter/angular-filter.js',
//                    'wwwroot/libs/angular-translate/angular-translate.js',
//                    'wwwroot/libs/**/*.js',
//                    'wwwroot/app.js',
//                    'wwwroot/assets/css/bootstrap.css',
//                    'wwwroot/**/*.css'
//                ],
//            }
//        }
//    },

//    uglify: {
//        release: {
//            options: {
//                mangle: false
//            },
//            files: {
//                './wwwroot/app.js': [
//                    'ClientApp/app/**/*.module.js',
//                    'ClientApp/app/**/*.service.js',
//                    'ClientApp/app/**/*.helper.js',
//                    'ClientApp/app/**/*.controller.js',
//                    'ClientApp/app/**/*.component.js',
//                    'ClientApp/app/**/*.js',
//                ]
//            }
//        }
//    },

//    less: {
//        release: {
//            src: [
//                './ClientApp/**/*.less'
//            ],
//            dest: './wwwroot/assets/css/style.css',
//            options: {
//                compile: true,
//                compress: true,
//                noUnderscores: false,
//                noIDs: false,
//                zeroUnits: false
//            }
//        }
//    },

//    build: {
//        release: {
//            tasks: ['clean:wwwroot', 'copy:libs', 'copy:release', 'less:release', 'uglify:release', 'injector:release']
//        }
//    }
//};
