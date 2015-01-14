var CCPNALASHAA_WEB_API_URL = 'http://192.168.4.148/ccp_nalashaa_api/api/'

var CCPNalashaaApp = angular.module("CCPNalashaaApp", ['ngRoute', 'ngResource', 'ngCookies']).//, 'angularUtils.directives.dirPagination']).
    config(function ($routeProvider) {
        $routeProvider.
            when('/', { controller: HomeCtrl, templateUrl: 'home.html' }).
           when('/mytrips', { controller: MyTripsCtrl, templateUrl: 'mytrips.html' }).
           when('/tripdetail/:id', { controller: TripDetailCtrl, templateUrl: 'tripdetail.html' }).
           when('/trips/new', { controller: AddTripCtrl, templateUrl: 'AddCP.html' }).
            when('/trips/edit/:id', { controller: AddTripCtrl, templateUrl: 'AddCP.html' }).
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
CCPNalashaaApp.factory('ccp_trips', function ($resource) {
    return $resource(CCPNALASHAA_WEB_API_URL + 'trips/:id', { id: '@id' }, { update: { method: 'PUT' } });
});

CCPNalashaaApp.filter('startFrom', function () {
    return function (input, start) {
        start = +start; //parse to int
        return input.slice(start);
    }
});
var MyTripsCtrl = function ($scope, $location, ccp_trips) {
    $scope.currentUser = function (trip) {
        return trip.UserId == 1;
    }
    var tripDetails = ccp_trips.query();

    $scope.trips = tripDetails;

    $scope.getTripStatus = function (statusId) {
        return statusId == 1 ? 'Open' : (statusId == 2 ? 'Closed' : 'Cancelled');
    }
};

var TripDetailCtrl = function ($scope, $routeParams, $location, ccp_trips) {
    $scope.trip = ccp_trips.get({ id: $routeParams.id });

    $scope.getTripStatus = function (statusId) {
        return statusId == 1 ? 'Open' : (statusId == 2 ? 'Closed' : 'Cancelled');
    }

    $scope.getFrameSrc = function (olat, oln, dlat, dln) {
        return 'direction.html?oll=' + olat + ',' + oln + '&dll=' + dlat + ',' + dln;
    }
};

var AddTripCtrl = function ($scope, $location, ccp_trips) {
    $scope.action = 'Add Trip';
    $scope.init = function () {
        var origintxtbox = (document.getElementById('txtOrigin'));
        var destinationtxt = (document.getElementById('txtDestination'));
        var autocomplete = new google.maps.places.Autocomplete(origintxtbox);
        var autocomplete1 = new google.maps.places.Autocomplete(destinationtxt);
    };
    $scope.save = function () {
        $scope.trips.UserId = 1;
        $scope.trips.TripStatus = 1;
        $scope.trips.CreatedDate = new Date;
        ccp_trips.save($scope.trips, function () {
            $location.path('/mytrips')
        })
    };
    $scope.clear = function () {
        $scope.trips = null;
    }
    $scope.init();

}
var HomeCtrl = function ($scope, $location, ccp_trips) {
    
        var TripDetails = ccp_trips.query(); 
        $scope.tripDetails = [];
        $scope.totaltrips = TripDetails.length;
        $scope.isDesc = false;
        $scope.from = 0;
        $scope.to = TripDetails.length; 
        $scope.tripDetails = ccp_trips.query();
        //$scope.tripDetails = $scope.tripDetails.concat(TripDetails.slice($scope.from, $scope.to));
        $scope.currentPage = 0;
        $scope.pageSize = 10;
        $scope.numberOfPages = function () {
            return Math.ceil($scope.tripDetails.length / $scope.pageSize);
        } 
        $scope.MoreDetails = function (tripid) {
        $scope.isDetailedTrip = true;
        $scope.selectedTrip = $scope.tripDetails.filter(function (item) {
            return item.TripId === tripid;
        });
    }; 
    $scope.numPages = function () {
        return Math.ceil($scope.todos.length / $scope.numPerPage);
    };


};

var TripCtrl = function ($scope, $location, $cookieStore, trips) {
    $scope.action = 'Add Trip';
    $scope.init = function () {
        var origintxtbox = (document.getElementById('txtOrigin'));
        var destinationtxt = (document.getElementById('txtDestination'));
        var autocomplete = new google.maps.places.Autocomplete(origintxtbox);
        var autocomplete1 = new google.maps.places.Autocomplete(destinationtxt);
        $scope.tripDetails = [];
        $scope.totaltrips = TripDetails.length;
        $scope.tripDetails.concat(TripDetails.slice($scope.from, $scope.to))

    }
    $scope.save = function () {
        if ($scope.trips) {
            trips.save = function () {

            }
            //$scope.trips.Origin = $('#txtOrigin').val();
            //$scope.trips.Destination = $('#txtDestination').val(); 
            //$scope.trips.TripStatus = 1;
            //$scope.tripDetails = TripDetails;
            ////TripDetails.push($scope.trips);
            //$scope.tripDetails.push($scope.trips)
            //$cookieStore.remove('alltrips');
            //$cookieStore.put('alltrips', $scope.tripDetails);
            $location.path('/');
        }
        else {
            return null;
        }

    };
    $scope.clear = function () {
        $scope.trips = null;
    }
    $scope.init();
};
