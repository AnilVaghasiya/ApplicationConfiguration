function ProductCtrl($scope, $http, $location, $anchorScroll, $window, $timeout, $filter, BankImageUploadService) {

    $scope.init = function () {
        $scope.initModel();
    };

    $scope.initModel = function () {
        $scope.model = {
            id: '',
            idUser: '',
            ProductName: '',
            ProductType: '',
            Price: ''
        };
        $scope.GetAllUser();
        $scope.GetAllProducts();
    };

    // Dynamic Search
    $scope.searchFilter1 = function (obj) {
        var re = new RegExp($scope.SearchText, 'i');
        return !$scope.SearchText || re.test(obj.ProductName) || re.test(obj.ProductType) || re.test(obj.Price);
    };
    $scope.filter = function (SearchText5) {
        $scope.SearchText = SearchText5;
        $timeout(function () {
            //$scope.currentPage = 1;
            $scope.totalItems = $scope.filtered.length;
            $scope.noOfPages = 10;
        }, 10);
    };
    // End of Dynamic Search
    $scope.GetAllUser = function () {
        $http.get(route.Lookup.GetAllUser).success(function (data) {
            console.log(data);
            if (data.success == 1) {
                $scope.lstUser = data.lstUser;
            } else if (data.error == 2) {
                toastr.error(data.message);
            }
        });
    }

    $scope.GetAllProducts = function () {
        $http.get(route.Lookup.GetAllProducts).success(function (data) {
            if (data.success == 1) {
                $scope.lstProducts = data.lstProducts;
                $scope.currentPage = 1;
                $scope.totalItems = $scope.lstProducts.length;
                $scope.entryLimit = 10; // items per page
                $scope.noOfPages = 10;
            } else if (data.error == 2) {
                toastr.error(data.message);
            }
        });
    }

    $scope.selectFileforUpload = function (file) {
        $scope.SelectedFileForUpload = file[0];
    }

    $scope.submitted = false;
    $scope.uploadImage = false;
    $scope.CreateProduct = function (form, o) {
        if (form.$valid) {
            console.log(o, $scope.model.idUser);
            //if (o.id != null && o.id != "") {
            //}
            //else {
            //    o.id = 0;
            //}
            //o.IsVisible = $scope.model.IsShowBank;
            //if ($scope.SelectedFileForUpload != null && $scope.SelectedFileForUpload != undefined) {
            //    BankImageUploadService.UploadFile($scope.SelectedFileForUpload, o).then(function (data) {
            //        if (data.success == 1) {
            //            $("#NewProduct").removeClass("active");
            //            $("#tab2").removeClass("active");
            //            $("#Products").addClass("active");
            //            $("#tab1").addClass("active");
            //            toastr.success(data.message);
            //            $scope.Reset();
            //        }
            //        else if (data.error == 2) {
            //            toastr.error(data.message);
            //        }
            //    }, function (e) {
            //        alert(e);
            //    });
            //}
            //else {
            $http.post(route.Lookup.CreateProductWithoutFile, o).success(function (data) {
                if (data.success == 1) {
                    $("#NewProduct").removeClass("active");
                    $("#tab2").removeClass("active");
                    $("#Products").addClass("active");
                    $("#tab1").addClass("active");
                    $.unblockUI();
                    toastr.success(data.message);
                    $scope.Reset();
                }
                else if (data.error == 2) {
                    $.unblockUI();
                    toastr.error(data.message);
                }
            });
            //}
        } else {
            $scope.submitted = true;
        }
    }

    $scope.FetchBankById = function (o) {
        console.log(o);
        $scope.model.id = o.id;
        $scope.model.idUser = o.idUser;
        $scope.model.ProductName = o.ProductName;
        $scope.model.ProductType = o.ProductType;
        $scope.model.Price = o.Price,
        //    $scope.model.ProductImage = o.ProductImage;
        $("#NewProduct").addClass("active");
        $("#tab2").addClass("active");
        $("#Products").removeClass("active");
        $("#tab1").removeClass("active");
    }

    $scope.DeleteProduct = function (id) {
        console.log(id);
        var params = {
            id: id
        }
        $http.get(route.Lookup.DeleteProduct, { params: params }).success(function (data) {
            if (data.success == 1) {
                $.unblockUI();
                toastr.success(data.message);
                $scope.Reset();
            }
            else {
                $.unblockUI();
                toastr.error(data.message);
            }
        });

    }


    $scope.AddNewProductTab = function () {
        $scope.Resetmodel();
        $("#Products").removeClass("active");
        $("#tab1").removeClass("active");
        $("#NewProduct").addClass("active");
        $("#tab2").addClass("active");
    }

    $scope.Resetmodel = function () {
        $scope.model.id = '';
        $scope.model.ProductName = '';
        $scope.model.ProductType = '';
        $scope.model.Price = '';
    }

    $scope.Reset = function () {
        $scope.model.id = 0;
        $("#NewProduct").removeClass("active");
        $("#tab2").removeClass("active");
        $("#Products").addClass("active");
        $("#tab1").addClass("active");
        $scope.init();
    }
    $scope.init();
}
