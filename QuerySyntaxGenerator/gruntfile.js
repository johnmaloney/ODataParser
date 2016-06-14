module.exports = function (grunt) {

    grunt.initConfig({

        // https://github.com/krampstudio/grunt-jsdoc
        jsdoc : {
            dist : {
                src: ['src/**/*.js', 'src/README.md', 'test/*.js'],
                options: {
                    destination: 'doc'
                }
            }
        },
        
        jshint: {
            files: ['Gruntfile.js', 'src/**/*.js', 'test/**/*.js'],
            options: {
                globals: {
                    jQuery: true
                }
            }
        },
        
        watch: {
            files: ['<%= jshint.files %>', 'src/README.md'],
            tasks: ['compile']
        },
        
        // Compiles the scripts into minified files //
        // to RUN specific version (e.g. production or dev)
        // TYPE command "grunt uglify:dev" for the dev version
        uglify: {
            
            dev: {
                files: {
                    'lib/gSynG-0.1.0.js': ['src/*.js']
                },
                options: {
                    screwIE8: true,
                    beautify: false,
                    mangle: true
                    //, exportAll:true
                }
            }
        }
    });

    grunt.loadNpmTasks('grunt-jsdoc');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-uglify');

    grunt.registerTask('default', ['jshint']);
    grunt.registerTask('compile', ['jshint', 'jsdoc', 'uglify:dev']);

};