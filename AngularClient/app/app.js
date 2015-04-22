(function () {
    'use strict';

    var app = angular.module('app', [
        // Angular modules 
        'ngAnimate',
        'ngRoute'

        // Custom modules 

        // 3rd Party Modules
        
    ]);

    app.config(['$httpProvider', '$routeProvider', function ($httpProvider, $routeProvider) {
        $routeProvider.when('/', { templateUrl: '/app/home/home.html' });
        $routeProvider.when('/other', { templateUrl: '/app/other/other.html' });
        $routeProvider.when('/admin', { templateUrl: '/app/admin/admin.html' });
        $routeProvider.otherwise({ redirectTo: '/' });

    }]);

    app.run(['$route', 'authService', function ($route, authService) {
        // bootstrap router
        authService.setAuthContext();
    }]);
})();