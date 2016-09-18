'use strict';

innovetricApp.config(['$locationProvider', '$routeProvider',
    function config($locationProvider, $routeProvider) {
        $locationProvider.hashPrefix('!');

        $routeProvider.
          when('/categories', {
              template: '<category-list></category-list>'
          }).
          when('/products/:selection/:selectionId', {
              template: '<product-list></product-list>'
          }).
          //when('/login', {
          //    template: '<login></login>'
          //}).
          //when('/logout', {
          //    template: '<logout></logout>'
          //}).
          otherwise('/categories');
    }
]);