/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.filter('changeColor',function(){
        return function(status){
            if(status) return 'blue';
            else return 'red';
        } 
    });

})(angular.module('trainingshop.common'));