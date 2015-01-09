var CCPNALASHAA_WEB_API_URL = 'http://192.168.4.148/ccp_nalashaa_api/api/users/'

var CCPNalashaaApp = angular.module("CCPNalashaaApp", ['ngRoute', 'ngResource']).
    config(function ($routeProvider)
    {
        $routeProvider.
            when('/', { controller: ListCtrl, templateUrl: 'list.html' }).
            when('/newTrip/:id', { controller: TripCtrl, templateUrl: 'AddCP.html' }).
            otherwise({ redirectTo: '/' });
    });

CCPNalashaaApp.factory('CCPNalashaa', function ($resource)
{
    return $resource(CCPNALASHAA_WEB_API_URL + ':id', { id: '@id' }, { update: { method: 'PUT' } });
});

var ListCtrl = function ($scope, $location, CCPNalashaa)
{
    $scope.init = function ()
    {
        $scope.users = CCPNalashaa.query();
    }
    $scope.showTrip = function () {
        $location.path('/newTrip/' + 1);
    }

    $scope.init();
};

var TripCtrl = function ($scope, $location, $routeParams) {
    $scope.action = 'Trip';

    $scope.save = function () {
        $scope.contact.SlNo = Contacts.length + 1;
        $scope.contact.ContactId = createGuid();
        Contacts.push($scope.contact);
        $location.path('#/');
    };
};
