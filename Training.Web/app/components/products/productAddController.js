/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productAddController', productAddController);

    productAddController.$inject = ['apiService', '$scope', 'notificationService', '$state','commonService']

    function productAddController(apiService, $scope, notificationService, $state, commonService) {

        $scope.product = {
            CreateDate: new Date(),
            Status: true
        }

        $scope.ckeditorOptions = {
            language: 'vi',
            height:'200px'
        }

        $scope.ChooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl){
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }

        $scope.moreImages = [];
        $scope.ChooseMoreImages = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function(fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                });
            }
            finder.popup();
        }

        $scope.GetSeoTitle = GetSeoTitle;
        function GetSeoTitle() {
            $scope.product.Alias = commonService.getSeoTitle($scope.product.Name);
        }

        $scope.AddProduct = AddProduct;

        function AddProduct() {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('api/product/create', $scope.product, function (result) {
                notificationService.Success(result.data.Name + ' đã được thêm mới!');
                $state.go('products');
            }, function (err) {
                notificationService.Error('Thêm mới không thành công!');
            });
        }

        $scope.productCategories = [];

        function loadParentCategory() {
            apiService.get('api/productcategory/getallparents', null, function (result) {
                $scope.productCategories = result.data;
            }, function () {
                console.log('Can not get parent list');
            });
        }
        loadParentCategory();
    }
})(angular.module('trainingshop.products'));