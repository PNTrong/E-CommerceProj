/// <reference path="/Assets/admin/libs/angular/angular.js" />
/// <reference path="../product_categories/productCategoryAddView.html" />
(function (app) {
    app.controller('productListController', productListController);

    productListController.$inject = ['apiService', '$scope', 'notificationService', '$ngBootbox', '$filter'];

    function productListController(apiService, $scope, notificationService, $ngBootbox, $filter) {
        $scope.products = [];
        $scope.getProducts = getProducts;
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.search = search;
        $scope.keyword = '';
        $scope.selectAll = selectAll;


        function search() {
            getProducts();
        }

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.products, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.products, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("products", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        $scope.deleteMultiple = deleteMultiple;
        function deleteMultiple() {
                var IDs = [];
                $.each($scope.selected, function (i, item) {
                    IDs.push(item);
                });
                var config = {
                    params: {
                        checkedProduct: JSON.stringify(IDs)
                    }
                }
                apiService.del('api/product/deletemulti',config, function (result) {
                    notificationService.Success('Xóa thành công ' + result.data + ' bản ghi.');
                    search();
                }, function (error) {
                    notificationService.Error('Xóa không thành công');
                });
        }

        $scope.deleteProduct = deleteProduct;
        function deleteProduct(id) {
            $ngBootbox.confirm('Do you want to delete this record?').then(function () {
                var config = {
                    params: {
                        id:id
                    }
                }

                apiService.del('api/product/delete', config, function () {
                    notificationService.Success('Delete sucessfully!');
                    search();
                }, function () { notificationService.Error('Delete it not complete!')});
            });
            
        }

        function getProducts(page) {
            page = page || 0;
            var config = {
                params: {
                    keyword:$scope.keyword,
                    page: page,
                    pageSize:2
                }
            }

            apiService.get('api/product/getall', config, function (result) {
                if (result.data.TotalRow == 0) {
                    notificationService.Warning("Không tìm thấy bảng ghi nào !");
                }
                $scope.products = result.data.Items;
                $scope.page = result.data.Page;
                $scope.pagesCount = result.data.TotalPage;
                $scope.totalCount = result.data.TotalRow;


            },
            function (err) { console.log(err.data); });
        }
        $scope.getProducts();
    }
    
})(angular.module('trainingshop.products'));