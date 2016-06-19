module.exports = function (grunt) {

    grunt.initConfig({
        pkg: grunt.file.readJSON('package.json'),
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
            options: {
                banner: '/*! <%= pkg.name %> - v<%= pkg.version %> - ' +
                 '<%= grunt.template.today("yyyy-mm-dd") %> */'
            },
            dev: {
                files: {
                    'lib/qSynG-<%= pkg.version %>.js': ['src/*.js']
                },
                options: {
                    screwIE8: true,
                    beautify: true,
                    mangle: false
                }
            },
            min: {
                files: {
                    'lib/qSynG-<%= pkg.version %>.min.js': ['src/*.js']
                },
                options: {
                        screwIE8: true,
                        beautify: false,
                        mangle: true
                }
            }
        },
        copy: {
            main: {
                nonull:true,
                src: 'src/qSynG.js', 
                dest: 'lib/qSynG.debug.js' 
            }
        }
    });

    grunt.loadNpmTasks('grunt-jsdoc');
    grunt.loadNpmTasks('grunt-contrib-jshint');
    grunt.loadNpmTasks('grunt-contrib-watch');
    grunt.loadNpmTasks('grunt-contrib-uglify');
    grunt.loadNpmTasks('grunt-contrib-copy');

    grunt.registerTask('default', ['jshint']);
    grunt.registerTask('compile', ['jshint', 'jsdoc', 'copy', 'uglify:min']);

};