/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);

    productCategoryAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService']

    function productCategoryAddController(apiService, $scope, notificationService, $state, commonService) {
        $scope.productCategory = {
            CreateDate: new Date(),
            Status: true,
            
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        $scope.AddProductCategory = AddProductCategory;
        function AddProductCategory() {
            apiService.post('api/productcategory/create', $scope.productCategory, function (result) {
                notificationService.Success(result.data.Name + ' đã được thêm mới.');
                $state.go('product_categories');
            }, function (err) { notificationService.Error('Thêm mới không thành công.'); });
        }
        $scope.parentCategories = [];

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) { $scope.parentCategories = result.data; }, function () { console.log('Can not get parent list'); });
        }

        loadParentCategory();
    }
})(angular.module('trainingshop.product_categories'));