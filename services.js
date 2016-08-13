/// <reference path="../assets/angular-1.2.9/i18n/angular-locale_en-us.js" />
'use strict';

// http://stackoverflow.com/questions/15033195/showing-spinner-gif-during-http-request-in-angular

angular.module('SharedServices', [])

.config(function ($httpProvider) {
    $httpProvider.responseInterceptors.push('tcmsHttpInterceptor');
    var spinnerFunction = function (data, headersGetter) {
        // todo start the spinner here
        //alert('start spinner');
        //utils.blockUI();
        return data;
    };
    $httpProvider.defaults.transformRequest.push(spinnerFunction);
})

.factory('tcmsHttpInterceptor', function ($q, $window) {
    return function (promise) {
        return promise.then(function (response) {
            // do something on success
            // todo hide the spinner
            //alert('stop spinner');
            //utils.unblockUI();
            return response;

        }, function (response) {
            // do something on error
            // todo hide the spinner
            //alert('stop spinner');
            //utils.unblockUI();
            return $q.reject(response);
        });
    };
})
    .factory(
            "transformRequestAsFormPost",
            function () {
                // I prepare the request data for the form post.
                function transformRequest(data, getHeaders) {
                    var headers = getHeaders();
                    headers["Content-type"] = "multipart/form-data";
                    return (serializeData(data));
                }
                // Return the factory value.
                return (transformRequest);
                // ---
                // PRVIATE METHODS.
                // ---
                // I serialize the given Object into a key-value pair string. This
                // method expects an object and will default to the toString() method.
                // --
                // NOTE: This is an atered version of the jQuery.param() method which
                // will serialize a data collection for Form posting.
                // --
                // https://github.com/jquery/jquery/blob/master/src/serialize.js#L45
                function serializeData(data) {
                    // If this is not an object, defer to native stringification.
                    if (!angular.isObject(data)) {
                        return ((data == null) ? "" : data.toString());
                    }
                    var buffer = [];
                    // Serialize each key in the object.
                    for (var name in data) {
                        if (!data.hasOwnProperty(name)) {
                            continue;
                        }
                        var value = data[name];
                        buffer.push(
                            encodeURIComponent(name) +
                            "=" +
                            encodeURIComponent((value == null) ? "" : value)
                        );
                    }
                    // Serialize the buffer and clean it up for transportation.
                    var source = buffer
                        .join("&")
                        .replace(/%20/g, "+")
                    ;
                    return (source);
                }
            }
        )

.directive('money', function () {
    'use strict';

    var NUMBER_REGEXP = /^\s*(\-|\+)?(\d+|(\d*(\.\d*)))\s*$/;

    function link(scope, el, attrs, ngModelCtrl) {
        var min = parseFloat(attrs.min || 0);
        var precision = parseFloat(attrs.precision || 2);
        var lastValidValue;

        function round(num) {
            var d = Math.pow(10, precision);
            return Math.round(num * d) / d;
        }

        function formatPrecision(value) {
            return parseFloat(value).toFixed(precision);
        }

        function formatViewValue(value) {
            return ngModelCtrl.$isEmpty(value) ? '' : '' + value;
        }


        ngModelCtrl.$parsers.push(function (value) {
            if (angular.isUndefined(value)) {
                value = '';
            }

            // Handle leading decimal point, like ".5"
            if (value.indexOf('.') === 0) {
                value = '0' + value;
            }

            // Allow "-" inputs only when min < 0
            if (value.indexOf('-') === 0) {
                if (min >= 0) {
                    value = null;
                    ngModelCtrl.$setViewValue('');
                    ngModelCtrl.$render();
                } else if (value === '-') {
                    value = '';
                }
            }

            var empty = ngModelCtrl.$isEmpty(value);
            if (empty || NUMBER_REGEXP.test(value)) {
                lastValidValue = (value === '')
                  ? null
                  : (empty ? value : parseFloat(value));
            } else {
                // Render the last valid input in the field
                ngModelCtrl.$setViewValue(formatViewValue(lastValidValue));
                ngModelCtrl.$render();
            }

            ngModelCtrl.$setValidity('number', true);
            return lastValidValue;
        });
        ngModelCtrl.$formatters.push(formatViewValue);

        var minValidator = function (value) {
            if (!ngModelCtrl.$isEmpty(value) && value < min) {
                ngModelCtrl.$setValidity('min', false);
                return undefined;
            } else {
                ngModelCtrl.$setValidity('min', true);
                return value;
            }
        };
        ngModelCtrl.$parsers.push(minValidator);
        ngModelCtrl.$formatters.push(minValidator);

        if (attrs.max) {
            var max = parseFloat(attrs.max);
            var maxValidator = function (value) {
                if (!ngModelCtrl.$isEmpty(value) && value > max) {
                    ngModelCtrl.$setValidity('max', false);
                    return undefined;
                } else {
                    ngModelCtrl.$setValidity('max', true);
                    return value;
                }
            };

            ngModelCtrl.$parsers.push(maxValidator);
            ngModelCtrl.$formatters.push(maxValidator);
        }

        // Round off
        if (precision > -1) {
            ngModelCtrl.$parsers.push(function (value) {
                return value ? round(value) : value;
            });
            ngModelCtrl.$formatters.push(function (value) {
                return value ? formatPrecision(value) : value;
            });
        }

        el.bind('blur', function () {
            var value = ngModelCtrl.$modelValue;
            if (value) {
                ngModelCtrl.$viewValue = formatPrecision(value);
                ngModelCtrl.$render();
            }
        });
    }

    return {
        restrict: 'A',
        require: 'ngModel',
        link: link
    };
})

    .directive('numbersOnly', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
                modelCtrl.$parsers.push(function (inputValue) {
                    if (inputValue == undefined) return ''
                    var transformedInput = inputValue.replace(/[^0-9]/g, '');
                    if (transformedInput != inputValue) {
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }

                    return transformedInput;
                });
            }
        };
    })

//.directive('numbersOnly', function () {
//    return {
//        require: 'ngModel',
//        link: function (scope, element, attr, ngModelCtrl) {
//            function fromUser(text) {
//                if (text) {
//                    var transformedInput = text.replace(/[^0-9]/g, '');

//                    if (transformedInput !== text) {
//                        ngModelCtrl.$setViewValue(transformedInput);
//                        ngModelCtrl.$render();
//                    }
//                    return transformedInput;
//                }
//                return undefined;
//            }
//            ngModelCtrl.$parsers.push(fromUser);
//        }
//    };
//})
 //.directive('numbersOnly1', function () {
 //    return {
 //        require: 'ngModel',
 //        link: function (scope, element, attrs, modelCtrl) {
 //            modelCtrl.$parsers.push(function (inputValue) {
 //                var str = inputValue;
 //                var a = isNaN(str);
 //                if (a == true) {
 //                    if (str.length == 2) {
 //                        return str.substring(0, str.length - 2);
 //                    }
 //                    else {
 //                        var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
 //                        if (transformedInput != inputValue) {
 //                            modelCtrl.$setViewValue(transformedInput);
 //                            modelCtrl.$render();
 //                        }
 //                        return transformedInput;
 //                    }
 //                }
 //                if (inputValue == undefined) return;
 //                if (str.length == 8 && str.indexOf('.') < 0) {
 //                    inputValue = str.substring(0, 7);
 //                    modelCtrl.$setViewValue(inputValue);
 //                    modelCtrl.$render();
 //                    return inputValue;
 //                }
 //                if (str.indexOf('.') > 7) {
 //                    inputValue = str.substring(0, 7);
 //                    modelCtrl.$setViewValue(inputValue);
 //                    modelCtrl.$render();

 //                    return inputValue;
 //                }
 //                if (str.indexOf('.') > 0) {
 //                    if (str.length > 10) {
 //                        inputValue = str.substring(0, 10);
 //                        modelCtrl.$setViewValue(inputValue);
 //                        modelCtrl.$render();
 //                        return inputValue;
 //                    }
 //                    var i;
 //                    i = str.length - str.indexOf('.');
 //                    if (i == 4 || str.indexOf('.') != str.lastIndexOf('.')) {
 //                        inputValue = str.substring(0, str.length - 1);
 //                        modelCtrl.$setViewValue(inputValue);
 //                        modelCtrl.$render();
 //                        return inputValue;
 //                    }
 //                    else {
 //                        return inputValue;
 //                    }
 //                }
 //                if (str.length > 10) {
 //                    inputValue = str.substring(0, 10);
 //                    modelCtrl.$setViewValue(inputValue);
 //                    modelCtrl.$render();
 //                    return inputValue;
 //                }
 //                var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
 //                if (transformedInput != inputValue) {
 //                    modelCtrl.$setViewValue(transformedInput);
 //                    modelCtrl.$render();
 //                }
 //                return transformedInput;
 //            });
 //        }
 //    }
 //})

//.directive('numbersOnly', function () {
//    return {
//        require: 'ngModel',
//        link: function (scope, element, attrs, modelCtrl) {
//            modelCtrl.$parsers.push(function (inputValue) {
//                var str = inputValue;
//                var a = isNaN(str);
//                if (a == true) {
//                    if (str.length == 2) {
//                        return str.substring(0, str.length - 2);
//                    }
//                    else {
//                        var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
//                        if (transformedInput != inputValue) {
//                            modelCtrl.$setViewValue(transformedInput);
//                            modelCtrl.$render();
//                        }
//                        return transformedInput;
//                    }
//                }
//                if (inputValue == undefined) return;
//                if (str.length == 17 && str.indexOf('.') < 0) {
//                    inputValue = str.substring(0, 16);
//                    modelCtrl.$setViewValue(inputValue);
//                    modelCtrl.$render();
//                    return inputValue;
//                }
//                if (str.indexOf('.') > 16) {
//                    inputValue = str.substring(0, 16);
//                    modelCtrl.$setViewValue(inputValue);
//                    modelCtrl.$render();
//                    return inputValue;
//                }
//                if (str.indexOf('.') > 0) {
//                    if (str.length > 18) {
//                        inputValue = str.substring(0, 18);
//                        modelCtrl.$setViewValue(inputValue);
//                        modelCtrl.$render();
//                        return inputValue;
//                    }
//                    var i;
//                    i = str.length - str.indexOf('.');
//                    if (i == 4 || str.indexOf('.') != str.lastIndexOf('.')) {
//                        inputValue = str.substring(0, str.length - 1);
//                        modelCtrl.$setViewValue(inputValue);
//                        modelCtrl.$render();
//                        return inputValue;
//                    }
//                    else {
//                        return inputValue;
//                    }
//                }
//                if (str.length > 18) {
//                    inputValue = str.substring(0, 18);
//                    modelCtrl.$setViewValue(inputValue);
//                    modelCtrl.$render();
//                    return inputValue;
//                }
//                var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
//                if (transformedInput != inputValue) {
//                    modelCtrl.$setViewValue(transformedInput);
//                    modelCtrl.$render();
//                }
//                return transformedInput;
//            });
//        }
//    }
//})

    .directive('numbersOnly100', function () {
        return {
            require: 'ngModel',
            link: function (scope, element, attrs, modelCtrl) {
                modelCtrl.$parsers.push(function (inputValue) {
                    var str = inputValue;
                    var a = isNaN(str);
                    if (a == true) {
                        if (str.length == 2) {
                            return str.substring(0, 2);
                        }
                        else {
                            var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
                            if (transformedInput != inputValue) {
                                modelCtrl.$setViewValue(transformedInput);
                                modelCtrl.$render();
                            }
                            return transformedInput;
                        }
                    }
                    if (inputValue == undefined) return;
                    if (str.length > 4) {
                        return str.substring(0, 3);
                        modelCtrl.$setViewValue(inputValue);
                        modelCtrl.$render();
                        return inputValue;
                    }
                    var transformedInput = inputValue.replace(/[^0-9]/g, '');
                    if (transformedInput != inputValue) {
                        modelCtrl.$setViewValue(transformedInput);
                        modelCtrl.$render();
                    }
                    return transformedInput;
                });
            }
        }
    })

.directive('numbersOnly18', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                var str = inputValue;
                var a = isNaN(str);
                if (a == true) {
                    if (str.length == 2) {
                        return str.substring(0, str.length - 2);
                    }
                    else {
                        var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
                        if (transformedInput != inputValue) {
                            modelCtrl.$setViewValue(transformedInput);
                            modelCtrl.$render();
                        }
                        return transformedInput;
                    }
                }
                if (inputValue == undefined) return;
                if (str.length > 18) {
                    inputValue = str.substring(0, 18);
                    modelCtrl.$setViewValue(inputValue);
                    modelCtrl.$render();
                    return inputValue;
                }
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }
                return transformedInput;
            });
        }
    }
})

.directive('numbersOnlyint', function () {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, modelCtrl) {
            modelCtrl.$parsers.push(function (inputValue) {
                var str = inputValue;
                var a = isNaN(str);
                if (a == true) {
                    if (str.length == 2) {
                        return str.substring(0, str.length - 2);
                    }
                    else {
                        var transformedInput = inputValue.replace(/[^0-9{1,7}\.\^0-9{1,2}]/, '');
                        if (transformedInput != inputValue) {
                            modelCtrl.$setViewValue(transformedInput);
                            modelCtrl.$render();
                        }
                        return transformedInput;
                    }
                }
                if (inputValue == undefined) return;
                if (str.length > 8) {
                    inputValue = str.substring(0, 8);
                    modelCtrl.$setViewValue(inputValue);
                    modelCtrl.$render();
                    return inputValue;
                }
                var transformedInput = inputValue.replace(/[^0-9]/g, '');
                if (transformedInput != inputValue) {
                    modelCtrl.$setViewValue(transformedInput);
                    modelCtrl.$render();
                }
                return transformedInput;
            });
        }
    }
})

.directive('checklistModel', function ($parse, $compile) {
    // contains
    function contains(arr, item) {
        if (angular.isArray(arr)) {
            for (var i = 0; i < arr.length; i++) {
                if (angular.equals(arr[i], item)) {
                    return true;
                }
            }
        }
        return false;
    }

    // add
    function add(arr, item) {
        arr = angular.isArray(arr) ? arr : [];
        for (var i = 0; i < arr.length; i++) {
            if (angular.equals(arr[i], item)) {
                return arr;
            }
        }
        arr.push(item);
        return arr;
    }

    // remove
    function remove(arr, item) {
        if (angular.isArray(arr)) {
            for (var i = 0; i < arr.length; i++) {
                if (angular.equals(arr[i], item)) {
                    arr.splice(i, 1);
                    break;
                }
            }
        }
        return arr;
    }

    // http://stackoverflow.com/a/19228302/1458162
    function postLinkFn(scope, elem, attrs) {
        // compile with `ng-model` pointing to `checked`
        $compile(elem)(scope);

        // getter / setter for original model
        var getter = $parse(attrs.checklistModel);
        var setter = getter.assign;

        // value added to list
        var value = $parse(attrs.checklistValue)(scope.$parent);

        // watch UI checked change
        scope.$watch('checked', function (newValue, oldValue) {
            if (newValue === oldValue) {
                return;
            }
            var current = getter(scope.$parent);
            if (newValue === true) {
                setter(scope.$parent, add(current, value));
            } else {
                setter(scope.$parent, remove(current, value));
            }
        });

        // watch original model change
        scope.$parent.$watch(attrs.checklistModel, function (newArr, oldArr) {
            scope.checked = contains(newArr, value);
        }, true);
    }

    return {
        restrict: 'A',
        priority: 1000,
        terminal: true,
        scope: true,
        compile: function (tElement, tAttrs) {
            if (tElement[0].tagName !== 'INPUT' || !tElement.attr('type', 'checkbox')) {
                throw 'checklist-model should be applied to `input[type="checkbox"]`.';
            }

            if (!tAttrs.checklistValue) {
                throw 'You should provide `checklist-value`.';
            }

            // exclude recursion
            tElement.removeAttr('checklist-model');

            // local scope var storing individual checkbox model
            tElement.attr('ng-model', 'checked');

            return postLinkFn;
        }
    };
})

.filter('startFrom', function () {
    return function (input, start) {
        if (input) {
            start = +start;
            return input.slice(start);
        }
        return [];
    };
})

.directive('errSrc', function () {
    return {
        link: function (scope, element, attrs) {
            element.bind('error', function () {
                if (attrs.src != attrs.errSrc) {
                    attrs.$set('src', attrs.errSrc);
                }
            });
        }
    }
})

.directive('ngEnter', function () {
    return function (scope, element, attrs) {
        element.bind("keydown keypress", function (event) {
            if (event.which === 13) {
                scope.$apply(function () {
                    scope.$eval(attrs.ngEnter);
                });

                event.preventDefault();
            }
        });
    };
})

.directive('nxEqualEx', function () {
    return {
        require: 'ngModel',
        link: function (scope, elem, attrs, model) {
            if (!attrs.nxEqualEx) {
                console.error('nxEqualEx expects a model as an argument!');
                return;
            }
            scope.$watch(attrs.nxEqualEx, function (value) {
                // Only compare values if the second ctrl has a value.
                if (model.$viewValue !== undefined && model.$viewValue !== '') {
                    model.$setValidity('nxEqualEx', value === model.$viewValue);
                }
            });
            model.$parsers.push(function (value) {
                // Mute the nxEqual error if the second ctrl is empty.
                if (value === undefined || value === '') {
                    model.$setValidity('nxEqualEx', true);
                    return value;
                }
                var isValid = value === scope.$eval(attrs.nxEqualEx);
                model.$setValidity('nxEqualEx', isValid);
                return isValid ? value : undefined;
            });
        }
    };
})
.directive('autoComplete', function ($timeout) {
    return function (scope, iElement, iAttrs) {
        iElement.autocomplete({
            source: scope[iAttrs.uiItems],
            select: function () {
                $timeout(function () {
                    iElement.trigger('input');
                }, 0);
            }
        });
    };
})

.filter('slice', function () {
    return function (arr, start, end) {
        return arr.slice(start, end);
    };
})

.factory('BankImageUploadService', function ($http, $q) { // explained abour controller and service in part 2

    var fac = {};
    fac.UploadFile = function (file, objBank) {
        var formData = new FormData();
        formData.append("file", file);
        //We can send more data to server using append                
        formData.append("obj", JSON.stringify(objBank));
        var defer = $q.defer();
        $http.post("/Admin/Bank/CreateBank", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });

        return defer.promise;

    }
    return fac;

})
//.directive('ckEditor', function () {
//    return {
//        require: '?ngModel',
//        link: function ($scope, elm, attr, ngModel) {
//            //CKEDITOR.replace('Description', {
//            //    "extraPlugins": 'imagebrowser',
//            //    "imageBrowser_listUrl": "/Image.json",
//            //});            
//            var ck = CKEDITOR.replace(elm[0], {
//                "extraPlugins": 'imagebrowser',
//                "imageBrowser_listUrl": "/Image.json",
//            });


//            ck.on('pasteState', function () {
//                $scope.$apply(function () {
//                    ngModel.$setViewValue(ck.getData());
//                });
//            });

//            ngModel.$render = function (value) {
//                ck.setData(ngModel.$modelValue);
//            };
//        }
//    };
//})

.factory('FileUploadService', function ($http, $q) { // explained abour controller and service in part 2

    var fac = {};
    fac.UploadFile = function (file, objDeposit) {
        console.log(objDeposit);
        var formData = new FormData();
        formData.append("file", file);
        //We can send more data to server using append                
        formData.append("obj", JSON.stringify(objDeposit));
        var defer = $q.defer();
        $http.post("/CustomerDeposit/CreateDeposit", formData,
            {
                withCredentials: true,
                headers: { 'Content-Type': undefined },
                transformRequest: angular.identity
            })
        .success(function (d) {
            defer.resolve(d);
        })
        .error(function () {
            defer.reject("File Upload Failed!");
        });

        return defer.promise;

    }
    return fac;

})

.directive('ckEditor', function () {
    return {
        require: '?ngModel',
        link: function (scope, elm, attr, ngModel) {
            var ck = CKEDITOR.replace(elm[0], {
                "extraPlugins": 'imagebrowser',
                "imageBrowser_listUrl": "/Image.json",
            });

            if (!ngModel) return;

            ck.on('instanceReady', function () {
                ck.setData(ngModel.$viewValue);
            });

            function updateModel() {
                scope.$apply(function () {
                    ngModel.$setViewValue(ck.getData());
                });
            }

            ck.on('change', updateModel);
            ck.on('key', updateModel);
            ck.on('dataReady', updateModel);

            ngModel.$render = function (value) {
                ck.setData(ngModel.$viewValue);
            };
        }
    };
})

.directive('rotate', function () {
    return {
        restrict: 'A',
        link: function (scope, element, attrs) {
            scope.$watch(attrs.degrees, function (rotateDegrees) {
                console.log(rotateDegrees);
                var r = 'rotate(' + rotateDegrees + 'deg)';
                element.css({
                    '-moz-transform': r,
                    '-webkit-transform': r,
                    '-o-transform': r,
                    '-ms-transform': r
                });
            });
        }
    }
})

.factory('LocalizeConfig', ['$http', function ($http) {
    return {

        /**
         * @type {Array.<String>}
         */
        supportedLocales: [],

        /**
         *
         * @param {Array.<String>} locales
         */
        setSupportedLocales: function (locales) {
            this.supportedLocales = locales;
        },

        /**
         * Load localization data for the specified locale
         * @param {String} locale
         */
        initialize: function (locale) {
            var url, config;

            if (locale) {
                if (this.supportedLocales.indexOf(locale) !== -1) {
                    currentLocale = locale;
                } else {
                    throw new Error("Unsupported locale");
                }
            }

            url = 'l10n/' + currentLocale + '/messages.json';
            config = {
                dataType: 'json'
            };

            $http.get(url, config)
                .then(function (response) {
                    var messages = response.data;
                    Globalize.addCultureInfo(currentLocale, {
                        messages: messages
                    });
                    console.log(_.keys(messages).length + ' locale messages added for ' + currentLocale);
                });
        }
    };
}])
    .filter('localize', function () {
        return function (key) {
            return Globalize.localize(key, currentLocale);
        };
    });

function isNumberKey(evt) {
    var charCode = (evt.which) ? evt.which : event.keyCode
    if (charCode > 31 && (charCode < 48 || charCode > 57)) {
        return false;
    }
    return true;
};

