function RegisterAccountCtrl($scope, $http, $location, $anchorScroll, $window, $timeout, $filter) {

    $scope.init = function () {
        $scope.initModel();
    };


    //window.location.href = window.location.href.substring(0, str.IndexOf('/'));
    $scope.initModel = function () {
        $scope.model = {
            id: '',
            idUser: '',
            FirstName: '',
            LastName: '',
            PhoneNumber: '',
            //Gender: 'Male',
            DOB: '',
            CreatedBy: '',
            CreatedDatetime: '',
            ModifiedBy: '',
            ModifiedDatetime: '',
            IsNotify: '',
            IsActive: '',
            IsCompleted: ''

        };

        $scope.DOBModel = {
            month: '0',
            year: '0',
            day: '0'
        }

        $scope.submitted = false;
        $scope.GetMonthName();
        $scope.GetAllDay();
        $scope.GetAllYear();
    }


    $scope.ResetPassword = function () {
        $.blockUI({ message: $('<h1><img src="/BigChoySunAssets/images/Loading-ball.gif" /></h1>'), css: { '-webkit-border-radius': '5px', '-moz-border-radius': '5px', } });
        var params = {
            HandphoneNumber: $scope.ForgotModel.HandphoneNumber
        };
        $http.get(route.Lookup.ResetPassword, { params: params }).success(function (data) {
            if (data.success == 1) {
                $scope.ForgotModel.idUser = data.idUser;
                $scope.ResetEnable = false;
                $scope.OTPEnable = true;
                $scope.NewPasswordEnable = false;
                toastr.success(data.message);
                //window.location.href = '/Home/';
            }
            else {
                $scope.ForgotModel.HandphoneNumber = '';
                $scope.ForgotData = data;
                $scope.ForgotModel.flgErrorForgot = true;

                setTimeout(function () {
                    $('#AlertSuccessMessageForgot').fadeOut('slow');
                    $scope.ForgotModel.flgErrorForgot = false;
                }, 5000);
            }
            $.unblockUI();
        });
    }

    $scope.cngsubmitted = false;
    $scope.ChangePassword = function (formNewpass) {
        if (formNewpass.$valid) {
            $.blockUI({ message: $('<h1><img src="/BigChoySunAssets/images/Loading-ball.gif" /></h1>'), css: { '-webkit-border-radius': '5px', '-moz-border-radius': '5px', } });
            var params = {
                idUser: $scope.ForgotModel.idUser,
                NewPassword: $scope.ForgotModel.NewPassword,
            };
            $http.get(route.Lookup.ChangePasswordUser, { params: params }).success(function (data) {
                if (data.success == 1) {
                    toastr.success(data.message);
                    $scope.init();
                    //$('#ForgotPassword').modal('hide');
                }
                else {
                    $scope.ChangeData = data;
                    $scope.flgErrorChange = true;
                    setTimeout(function () {
                        $('#AlertSuccessMessageChange').fadeOut('slow');
                        $scope.ForgotModel.flgErrorChange = false;
                    }, 5000);
                }
                $scope.Reset();
                $.unblockUI();
            });
        }
        else {
            $scope.cngsubmitted = true;
        }
    }

    $scope.Reset = function () {
        $scope.init();
    }



    //Register

    //For Dropdown Date
    $scope.DaysinMonth = function () {
        var y = $scope.DOBModel.year, d = $scope.DOBModel.day, m = $scope.DOBModel.month;
        if (m === '2') {
            var mlength = 28 + (!(y & 3) && ((y % 100) !== 0 || !(y & 15)));
        } else {
            var mlength = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][m - 1];
        }
        $scope.myArr = [];
        for (var i = 1; i <= mlength; i++) {
            $scope.myArr.push(i);
        }
        $scope.lstDay = $scope.myArr;
    }

    $scope.GetAllDay = function () {
        $scope.myArr = [];
        for (var i = 1; i <= 31 ; i++) {
            $scope.myArr.push(i);
        }
        $scope.lstDay = $scope.myArr;
    }

    $scope.GetMonthName = function () {
        $scope.lstMonth = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
    }

    $scope.GetAllYear = function () {
        $scope.myYear = [];
        var date = new Date();
        var currentYear = date.getFullYear();
        for (var i = currentYear; i >= 1950; i--) {
            $scope.myYear.push(i);
        }
        $scope.lstYear = $scope.myYear;
    }
    //End Dropdown Date


    $scope.submitted = false;
    $scope.CreateMember = function (form, o) {
        console.log(o);
        if (form.$valid) {
            o.Phone = $scope.model.PhoneNumber;
            $http.post(route.Lookup.CreateMemberRegister, o).success(function (data) {
                if (data.success == 1) {
                    toastr.error(data.message);
                    window.location.href = '/Home';
                }
                else if (data.error == 2) {
                    toastr.error(data.message);
                }
            });
        }
        else {
            $scope.submitted = true;
        }
    }
    //End Register

    $scope.init();
}