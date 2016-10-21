/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
*/

// does not work
// var project = require('./project.json');
var destPath = "wwwroot/";


var gulp = require("gulp"),
  rimraf = require("rimraf"),
  fs = require("fs"),
  sass = require("gulp-sass");

gulp.task("sass", function ()
{
  return gulp.src('Styles/main.scss')
    .pipe(sass())
    .pipe(gulp.dest(destPath + '/css'));
});