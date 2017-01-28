'use strict';

angular.
  module('webjetMovieApp').
  config(['$locationProvider' ,'$routeProvider',
    function config($locationProvider, $routeProvider) {
      $locationProvider.hashPrefix('!');

      $routeProvider.
        when('/movies', {
          template: '<movie-list></movie-list>'
        }).
        when('/movies/:movieId', {
          template: '<movie-detail></movie-detail>'
        }).
        otherwise('/movies');
    }
  ]);
