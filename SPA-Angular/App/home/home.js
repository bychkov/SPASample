(function () {
    'use strict';

    angular
        .module('app')
        .controller('home', home);

    home.$inject = ['$http'];

    function home($http) {
        /* jshint validthis:true */
        var vm = this;
        vm.data = '';

        activate();

        function activate() {
            $http.get('/api/HomeData').then(function (response) {
                vm.data = response.data;
            });
        }
    }
})();
