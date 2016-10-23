/// <reference path="/Assets/admin/libs/angular/angular.js" />

(function (app) {
    app.factory('apiService', apiService);

    apiService.$inject = ['$http', 'notificationService'];

    function apiService($http) {
        return {
            get: get,
            post: post,
            put: put,
            del:del
        }

        function del(url, data, success, fail) {
            $http.delete(url, data).then(function (result) { success(result) }, function (err) {
                console.log(err.status);
                if (err.status === 401) {
                    notificationService.Error('Authenticate is required.');
                }
                else if (fail != null) {
                    fail(err);
                }
            });
        }

        function put(url, data, success, fail) {
            $http.put(url, data).then(function (result) { success(result); }, function (err) {
                console.log(err.status);
                if (err.status === 401) {
                    notificationService.Error('Authenticate is required.');
                }
                else if (fail != null) {
                    fail(err);
                }
            });
        }

        function post(url, data, success, fail) {
            $http.post(url, data).then(function (result) { success(result) }, function (err) {
                console.log(err.status);
                if (err.status === 401) {
                    notificationService.Error('Authenticate is required.');
                }
                else if(fail != null){
                    fail(err);
                }
            });
        }

        function get(url, params, success, fail) {
            $http.get(url, params).then(function (result) { success(result); }, function (error) { fail(error);});
        }
        
    }
})(angular.module('trainingshop.common'));