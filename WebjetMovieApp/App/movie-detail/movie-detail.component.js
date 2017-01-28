'use strict';

angular.
  module('movieDetail').
  component('movieDetail', {
    templateUrl: 'app/movie-detail/movie-detail.template.html',
    controller: ['$http', '$routeParams',
      function movieDetailController($http, $routeParams) {
          var self = this;
          self.loading = true;

          //$http.get('api/movies', { params: { movieId: $routeParams.movieId } }).then(function (response) {
          //    self.movie = response.data;
          //    self.loading = false;
          //});
          $http.get("api/movies/", { params: { movieId: $routeParams.movieId } })
            .success(function (data, status, headers, config) {
                self.movie = angular.fromJson(data);
                self.loading = false;
                console.log("Movies received");
            }).error(function (data, status, headers, config) {
                alert("error while getting movies");
                self.loading = false;
            });
      }
    ]
  });
