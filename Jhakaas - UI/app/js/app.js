'use strict';


// Declare app level module which depends on filters, and services
var app = angular.module('jhakaasApp', ['ngRoute', 'ui.bootstrap']).
config(['$routeProvider', function($routeProvider) {
  $routeProvider.when('/posts', {templateUrl: 'partials/Posts.html', controller: 'postsController'});
  $routeProvider.otherwise({redirectTo: '/posts'});
}]);
