/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', function ($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;

        function search() {
            getProductCategories();
        }
        function getProductCategories(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pageSize: 2
                }
            }

            apiService.get('/api/productcategory/getall', config, function (result) {
                if (result.data.TotalRow == 0) {
                    notificationService.Warning('Không có bảng ghi nào tìm thấy!');
                }
                $scope.productCategories = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalRow;
            }, function () { console.log('Load productcategory failed.') });
        }

        $scope.getProductCategories();
    }

    );

    //productCategoryListController.$inject = ['$scope', 'apiService'];

    //function productCategoryListController($scope, apiService) {
    //    $scope.productCategories = [];

    //    $scope.getProductCategories = getProductCategories;

    //    function getProductCategories() {
    //        apiService.get('/api/productcategory/getall', null, function (result) {
    //            $scope.productCategories = result.data;
    //        }, function (error) { console.log('Load productcategory failed.') });
    //    }

    //    $scope.getProductCategories();
    //}
})(angular.module('trainingshop.product_categories'));