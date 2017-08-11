
module.exports = {

    copy: {
        local: {
            files: [{
                src: ['app/**/*.js', 'app/**/*.html'],
                dest: './wwwroot/',
                cwd: './ClawLibrary/',
                nonull: true,
                expand: true
            }, {
                src: './ClawLibrary/index.html',
                dest: './wwwroot/',
                nonull: true,
                flatten: true,
                expand: true
            }, {
                src: 'assets/**/*',
                dest: './wwwroot/',
                cwd: './ClawLibrary/',
                nonull: true,
                expand: true
            }]
        },
    },

    injector: {
        local: {
            options: {
                parentDirFirst: false,
                relative: false,
                transform: function (filePath) {
                    var ext = filePath.split('.').pop();
                    filePath = filePath.replace('/wwwroot', '');

                    switch (ext) {
                    case 'css': return '<link rel="stylesheet" href="' + filePath + '">';
                    case 'js': return '<script src="' + filePath + '"></script>';
                    case 'html': return '<link rel="import" href="' + filePath + '">';
                    }
                }
            },
            files: {
                './wwwroot/index.html': [

                    'wwwroot/libs/jquery/jquery.js',
                    'wwwroot/libs/angular/angular.js',
                    'wwwroot/libs/kendo-ui/kendo.ui.core.js',
                    'wwwroot/libs/angular-filter/angular-filter.js',
                    'wwwroot/libs/angular-translate/angular-translate.js',
                    'wwwroot/libs/**/*.js',
                    'wwwroot/app/**/*.module.js',
                    'wwwroot/app/**/*.service.js',
                    'wwwroot/app/**/*.helper.js',
                    'wwwroot/app/**/*.controller.js',
                    'wwwroot/app/**/*.component.js',
                    'wwwroot/app/**/*.js',
                    'wwwroot/assets/css/bootstrap.css',
                    'wwwroot/**/*.css'
                ],
            }
        }
    },

    less: {
        local: {
            src: [
                './ClawLibrary/**/*.less'
            ],
            dest: './wwwroot/assets/css/style.css',
            options: {
                compile: true,
                compress: false,
                noUnderscores: false,
                noIDs: false,
                zeroUnits: false
            }
        }
    },

    build: {
        local: {
            tasks: ['clean:wwwroot', 'copy:libs', 'copy:local', 'less:local', 'injector:local']
        },
    },

    serve: {
        local: {
            tasks: ['build:local', 'watch:local']
        }
    },

    watch: {
        local: {
            files: [
                './ClawLibrary/**/*'
            ],
            options: {
                spawn: false,
                livereload: true
            },
            tasks: ['build:local']
        }
    }
};
