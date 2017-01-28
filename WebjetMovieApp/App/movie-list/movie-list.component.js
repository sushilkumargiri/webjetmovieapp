'use strict';

angular.
  module('movieList').
  component('movieList', {
      templateUrl: 'app/movie-list/movie-list.template.html',
      controller: function movieListController($http, $timeout) {
          var self = this;
          self.orderProp = 'Year';
          self.loading = true;
          self.timeExceeded = false;
          self.secondCount = 0;

          self.LoadList = function () {
              self.loading = true;
              $http.get("api/movies")
            .success(function (data, status, headers, config) {
                self.movies = angular.fromJson(data.Movies);
                self.loading = false;
                console.log("Movies received");
            }).error(function (data, status, headers, config) {
                alert("error while getting movies. Please Retry!");
                self.loading = false;
            });
          }
          self.FindCheapestMovie = function () {
              self.loading = true;
              self.secondCount = 0;
              var updateCounter = function () {
                  self.secondCount++;
                  $timeout(updateCounter, 1000);
              };
              $http.get("api/cheapestmovie")
            .success(function (data, status, headers, config) {
                self.movies = angular.fromJson(data);
                self.loading = false;
                self.secondCount = 0;
                console.log("Movies received");
            }).error(function (data, status, headers, config) {
                alert("error while getting movies. Please Retry!");
                self.loading = false;
                self.secondCount = 0;
            });
          }
      }
  });
