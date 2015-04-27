(function () {
    'use strict';

    angular
        .module('app')
        .factory('authService', authService);

    authService.$inject = ['$http'];

    function authService($http) {
        var service = {
            getAccessToken: getAccessToken,
            authorize: authorize,
            setAuthContext: setAuthContext
        };

        return service;

        function getData() { }

        function setAccessToken(accessToken) {
            sessionStorage.setItem("accessToken", accessToken);
        };

        function getAccessToken() {
            return sessionStorage.getItem("accessToken");
        }
        function setAuthContext() {
            var fragment = getFragment();

            if (fragment.access_token) {
                // returning with access token, restore old hash, or at least hide token
                window.location.hash = fragment.state || '';
                setAccessToken(fragment.access_token);
                $http.defaults.headers.common.Authorization = "Bearer " + fragment.access_token;

            }

        }

        function authorize() {
            var token = getAccessToken();
            if (!token) {
                // no token - so bounce to Authorize endpoint in AccountController to sign in or register
                window.location = "/Account/Authorize?client_id=web&response_type=token&state=" + encodeURIComponent(window.location.hash);
            }
            else {
                $http.defaults.headers.common.Authorization = "Bearer " + token;
            }
        }

        function getFragment() {
            if (window.location.hash.indexOf("#") === 0) {
                return parseQueryString(window.location.hash.substr(2));
            } else {
                return {};
            }
        };

        function parseQueryString(queryString) {
            var data = {},
                pairs, pair, separatorIndex, escapedKey, escapedValue, key, value;

            if (queryString === null) {
                return data;
            }

            pairs = queryString.split("&");

            for (var i = 0; i < pairs.length; i++) {
                pair = pairs[i];
                separatorIndex = pair.indexOf("=");

                if (separatorIndex === -1) {
                    escapedKey = pair;
                    escapedValue = null;
                } else {
                    escapedKey = pair.substr(0, separatorIndex);
                    escapedValue = pair.substr(separatorIndex + 1);
                }

                key = decodeURIComponent(escapedKey);
                value = decodeURIComponent(escapedValue);

                data[key] = value;
            }

            return data;
        }

    }
})();