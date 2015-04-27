'use strict';

angular.module('myApp.auth')

.controller('RegistrationController', ['$scope', '$window', function ($scope, $window) {
    $scope.date = new Date();

    $scope.login = function () {
        var uri = URI('https://localhost:44306/core/connect/authorize')
            .addSearch('response_type', 'code id_token token')
            .addSearch('client_id', 'AngularClient')
            .addSearch('scope', 'openid profile localApi')
            .addSearch('redirect_uri', 'https://localhost:44307/#/loggedin?')
            .addSearch('nonce',  Math.floor( Math.random()*99999 ));
        $window.location.href = uri;
    };

    $scope.logout = function () {
        var uri = URI('https://localhost:44306/core/logout');
        $window.location.href = uri;
    };
}]);