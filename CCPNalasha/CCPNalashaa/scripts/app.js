var CCPNALASHAA_WEB_API_URL = 'http://192.168.4.148/ccp_nalashaa_api/api/'

var CCPNalashaaApp = angular.module("CCPNalashaaApp", ['ngRoute', 'ngResource']).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: HomeCtrl, templateUrl: 'home.html' }).
            when('/trips', { controller: TripCtrl, templateUrl: 'AddCP.html' }).
            otherwise({ redirectTo: '/' });
    });

CCPNalashaaApp.directive("datepicker", function () {
    return {
        restrict: "A",
        require: "ngModel",
        link: function (scope, elem, attrs, ngModelCtrl) {
            var updateModel = function (dateText) {
                scope.$apply(function () {
                    ngModelCtrl.$setViewValue(dateText);
                });
            };
            var options = {
                dateFormat: "mm/dd/yy",
                onSelect: function (dateText) {
                    updateModel(dateText);
                }
            };
            elem.datepicker(options);
        }
    }
});

CCPNalashaaApp.factory('ccp_home', function ($resource) {
    return $resource(CCPNALASHAA_WEB_API_URL + 'users', { id: '@id' }, { update: { method: 'PUT' } });
});

CCPNalashaaApp.factory('CCPNalashaa', function ($resource) {
    return $resource(CCPNALASHAA_WEB_API_URL + 'users/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

 
var HomeCtrl = function ($scope, $location, ccp_home) {
    $scope.init = function () {
        $scope.tripDetails = [];
        $scope.totaltrips = TripDetails.length;
        $scope.isDesc = false;
        $scope.from = 0;
        $scope.to = TripDetails.length;
        $scope.isMore = true;

        $scope.tripDetails = $scope.tripDetails.concat(TripDetails.slice($scope.from, $scope.to));
    }

    $scope.init();

    $scope.MoreDetails = function (slno) {
        $scope.isDetailedTrip = true;
        $scope.selectedTrip = $scope.tripDetails.filter(function (item) {
            return item.SlNo === slno;
        });
    };
};

var TripCtrl = function ($scope, $location) {
    $scope.action = 'Add Trip';    
    $scope.init = function () {
        var origintxtbox = (document.getElementById('txtOrigin'));
        var destinationtxt = (document.getElementById('txtDestination'));
        var autocomplete = new google.maps.places.Autocomplete(origintxtbox);
        var autocomplete1 = new google.maps.places.Autocomplete(destinationtxt);

    }
    $scope.save = function () {
        if ($scope.trips)

        { }
        else {
            return null;
        }

    };
    $scope.clear = function () {
        $scope.trips = null;
    }
    $scope.init();
};
