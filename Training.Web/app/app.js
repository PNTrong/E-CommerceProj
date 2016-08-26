/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function () {
    angular.module('trainingshop',
        ['trainingshop.products', 'trainingshop.common', 'trainingshop.product_categories']).config(config);

    config.$inject = ['$stateProvider', '$urlRouterProvider'];

    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('home', {
            url: "/admin",
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        });
        $urlRouterProvider.otherwise('/admin');
    }
})();