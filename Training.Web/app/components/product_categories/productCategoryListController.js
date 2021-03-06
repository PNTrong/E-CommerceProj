﻿/// <reference path="/Assets/admin/libs/angular/angular.js" />
(function (app) {
    app.controller('productCategoryListController', function ($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pagesCount = 0;
        $scope.keyword = '';
        $scope.getProductCategories = getProductCategories;
        $scope.search = search;
        $scope.deleteProductCategory = deleteProductCategory;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;

        function deleteMultiple() {
            var IDs = [];
            $.each($scope.selected, function (i, item) {
                IDs.push(item.ID);
            });
            var config = {
                params: {
                    checkedProductCategories: JSON.stringify(IDs)
                }
            }
            apiService.del('api/productcategory/deletemulti', config, function (result) {
                notificationService.Success('Xóa thành công ' + result.data + ' bản ghi.');
                search();
            }, function (error) {
                notificationService.Error('Xóa không thành công');
            });
        }
        
        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            } else {
                angular.forEach($scope.productCategories, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        $scope.$watch("productCategories", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            } else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);


        function deleteProductCategory(id) {
            $ngBootbox.confirm('Do you want to delete it?').then(function () {
                var config = {
                    params: {
                        id:id
                    }
                }

                apiService.del('api/productcategory/delete', config, function () {
                    notificationService.Success('Delete successfuly!');
                    search();
                },
                function () {
                    notificationService.Error('Delete it not complete!');
                });
            });
        }

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