/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {UpdateProduct
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService','$scope']
    function productEditController() {
        $scope.product = {
            CreateDate: new Date(),
            Status: true
        }

        $scope.UpdateProduct = UpdateProduct;
        $scope.GetSeoTitle = GetSeoTitle;

        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        function UpdateProduct() {
            apiService.put('api/product/update', $scope.product,
                function (result) {
                    notificationService.Success(result.data.Name + ' has updated!');
                    $state.go('products');
                },
                function (err) {
                    notificationService.Error('Action is not success!');
                });
        }

        function loadProductDetail() {
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) { $scope.product = result.data; },
                function (err) {
                    notificationService.Error(err.data);
                }
                );
        }

        function loadParentCategory() {
            apiService.get('api/product/getallparents', null, function (result) { $scope.parentCategories = result.data; }, function () { console.log('Can not get parent list'); });
        }

        loadParentCategory();
        loadProductDetail();
    }
})(angular.module('trainingshop.products'));