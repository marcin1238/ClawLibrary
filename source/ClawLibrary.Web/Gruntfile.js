
var sharedConfig = require('./ClawLibrary/build_config/grunt.shared.config.js');
var localConfig = require('./ClawLibrary/build_config/grunt.local.config.js');
//var releaseConfig = require('./ClientApp/build_config/grunt.release.config.js');

module.exports = function (grunt) {

    grunt.loadNpmTasks('grunt-contrib-clean');
    grunt.loadNpmTasks('grunt-contrib-copy');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-less');
    grunt.loadNpmTasks('grunt-injector');
    grunt.loadNpmTasks("grunt-extend-config");


    grunt.initConfig(sharedConfig);

    grunt.extendConfig(localConfig);
    //grunt.extendConfig(releaseConfig);


    grunt.registerMultiTask('build', 'Builds project', function () {
        grunt.task.run(this.data.tasks);
    });

    grunt.registerMultiTask('serve', 'Builds project and starts Watch', function () {
        grunt.task.run(this.data.tasks);
    });

    grunt.registerTask('_build_project', 'Builds project', ['build:local']);

    grunt.registerTask('_serve_project', 'Builds project and starts Watch', ['serve:local']);
};