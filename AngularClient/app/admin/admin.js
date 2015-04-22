(function () {
    'use strict';

    angular
        .module('app')
        .controller('admin', admin);

    admin.$inject = ['$http', 'authService']; 

    function admin($http, authService) {
        /* jshint validthis:true */
        var vm = this;
        vm.data = '';

        activate();

        function activate() {
            authService.authorize();
            $http.get('/api/AdminData').then(function (response) {
                vm.data = response.data;
            });
        }
    }
})();
