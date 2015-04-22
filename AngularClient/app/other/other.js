(function () {
    'use strict';

    angular
        .module('app')
        .controller('other', other);

    other.$inject = ['$http', 'authService'];

    function other($http, authService) {
        /* jshint validthis:true */
        var vm = this;
        vm.data = '';

        activate();

        function activate() {
            authService.authorize();
            $http.get('/api/OtherData').then(function (response) {
                vm.data = response.data;
            });
        }
    }
})();
