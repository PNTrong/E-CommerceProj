/// <reference path="../plugins/angular/angular.js" />

var myApp = angular.module("myModule", []);

myApp.controller("myController", myController);

var myController = function ($scope) {
    $scope.message = "Training Angularjs";
}