/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);

    productCategoryEditController.$inject = ['apiService', '$scope', 'notificationService', '$state','$stateParams','commonService'];

    function productCategoryEditController(apiService, $scope, notificationService, $state,$stateParams,commonService) {
        $scope.productCategory = {
            CreateDate: new Date(),
            Status: true
        }

        $scope.UpdateProductCategory = UpdateProductCategory;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }

        function UpdateProductCategory() {
            apiService.put('api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.Success(result.data.Name + ' has updated!');
                    $state.go('product_categories');
                },
                function (err) {
                    notificationService.Error('Action is not success!');
                });
        }

        function loadProductCategoryDetail() {
            apiService.get('api/productcategory/getbyid/' + $stateParams.id, null, function (result) { $scope.productCategory = result.data; },
                function (err) {
                    notificationService.Error(err.data);
                }
                );
        }

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) { $scope.parentCategories = result.data; }, function () { console.log('Can not get parent list'); });
        }

        loadParentCategory();
        loadProductCategoryDetail();

    }

})(angular.module('trainingshop.product_categories'));