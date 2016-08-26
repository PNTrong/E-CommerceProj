/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.filter('myStatus', function () {
        return function (status) {
            if (status)
                return 'Kích Hoạt';
            else
                return 'Khóa';
        }
    });
})(angular.module('trainingshop.common'));