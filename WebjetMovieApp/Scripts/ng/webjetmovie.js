angular.module('webjetMovieApp', ['ngRoute', 'movieList', 'movieDetail'])
    .config(['$locationProvider', '$routeProvider', '$httpProvider', function ($locationProvider, $routeProvider, $httpProvider) {
        $locationProvider.hashPrefix('!');
        $routeProvider.
            when('/movies', {
                template: '<movie-list></movie-list>'
            }).
            when('/movies/:ID', {
                template: '<movie-detail></movie-detail>'
            }).
            otherwise('/movies');
    }
    ]);

angular.module('movieList')
.component('movieList', {
    templateUrl: 'html/movies.html',
    controller: function MovieController($http) {
        var self = this;
        self.orderProp = 'Year';

        $http.get('home/movies').then(function (response) {
            self.movies = response.data.Movies;
        });
    }
});

angular.module('movieDetail')
.component('movieDetail', {
    templateUrl: 'html/movie.html',
    controller: function MovieDetailController($http, $routeParams) {
        var self = this;
        self.orderProp = 'Year';

        $http.get('home/movie', $routeParams.ID).then(function (response) {
            self.movie = response.data.Movie;
        });
    }
});
