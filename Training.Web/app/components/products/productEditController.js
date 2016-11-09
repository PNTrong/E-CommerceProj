/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productEditController', productEditController);

    productEditController.$inject = ['apiService', '$scope', 'notificationService', '$state', '$stateParams', 'commonService'];
    function productEditController(apiService, $scope, notificationService, $state, $stateParams, commonService) {
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

        $scope.ckeditorOptions = {
            language: 'vi',
            height: '200px'
        }

        $scope.ChooseImage = ChooseImage;
        function ChooseImage() {
            var finder = new CKFinder();
            finder.selectActionFunction = function(fileurl) {
                $scope.product.Image = fileurl;
            }

            finder.popup();
        }

        function loadProductDetail() {
            apiService.get('api/product/getbyid/' + $stateParams.id, null, function (result) { $scope.product = result.data; },
                function (err) {
                    notificationService.Error(err.data);
                }
                );
        }

        function loadParentCategory() {
            apiService.get('api/product/getallparents', null, function (result) { $scope.productCategories = result.data; }, function () { console.log('Can not get parent list'); });
        }

        loadParentCategory();
        loadProductDetail();
    }
})(angular.module('trainingshop.products'));