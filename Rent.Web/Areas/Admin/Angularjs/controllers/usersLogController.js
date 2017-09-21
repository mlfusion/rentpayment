
// module
//var app = angular.module('app', []);

// service
app.factory('usersLogService', function ($http) {
    var api = '/api/usersLog/';
    var url = '';

    return {
        ////GET
        //getList: function() {
        //    url = api + "GET/";
        //    return $http.get(url);
        //},

        //GET/5
        //get: function (data) {
        //    url = api + "GET/" + id;
        //    return $http.get(url, data);
        //},

        //POST
        post: function (data) {
            url = api + 'POST/';
            return $http.post(url, data);
        },

        // Test Post
        postOnActive: function (id, page) {
            url = '/Admin/User/Reactive';
            return $http.post(url, { id: id, page: page });
        },

        onActive: function(id, page) {
            var r = $http({
                method: 'POST',
                url: '/Admin/User/Reactive',
                data: { id: id, page: page }
            }).success(function(data) {
                return data.data;
            });
            return r;
        },

        onDeactive: function (id, page) {
            var r = $http({
                method: 'POST',
                url: '/Admin/User/Delete',
                data: { id: id, page: page }
            }).success(function (response) {
                return response.data;
            });

            return r;
        }


        ////PUT
        //put: function(data) {
        //    url = api + 'PUT/';
        //    return $http.put(url, data);
        //}
    };
});

// Users controller
app.controller('usersController', function($scope, usersLogService) {

    $scope.users = 'USERS';

    $scope.onActive = function(id, page) {
        var data = usersLogService.postOnActive(id, page);
       //('#result').html(data);
        $scope.lives = data;
    };


    $scope.onDeactive = function (id, page) {
        var data = usersLogService.onDeactive(id, page);
        $('#result').empty().html(data);
    };

});

// controller
app.controller('usersLogController', function ($scope, usersLogService) {
    $scope.usersLogs = [];
    $scope.usersLog = null;

    $scope.main = {
        page: 1,
        take: 5
    };

    $scope.nextPage = function () {
        if ($scope.CurrentPage <= $scope.TotalCount) {
            $scope.CurrentPage++;
            $scope.post($scope.Uid);
        }
    };

    $scope.previousPage = function () {
        if ($scope.CurrentPage >= 1) {
            $scope.CurrentPage--;
            $scope.post($scope.Uid);
        }
    };

    // load data on grid
    $scope.loading = function (id) {
        $('#tab2').hide();
        $('#tab2Loader').show();

        $scope.post(id);
    };

    //post
    $scope.post = function (id) {

        var _usersLog = {
            Id: id,
            CurrentPage: $scope.CurrentPage != 0 ? $scope.CurrentPage : 0,
            PageSize: 5
        };

       // $('#tab2').hide();
       // $('#tab2Loader').show();

        usersLogService.post(_usersLog).success(function (data) {
            $scope.usersLog = data;

            $.each(data, function (index, i) {
                if (index == 0) {
                    $scope.Uid = i.Uid;
                    $scope.CurrentPage = i.Paging.CurrentPage;
                    $scope.TotalCount = i.Paging.TotalCount;
                }
            });

            if ($scope.usersLog != '') {
                $('#tab2').show();
                $('#grpUsersLog').focus();

                var total = Math.round($scope.TotalCount / 5) <= 1.0 ? 0 : Math.round($scope.TotalCount / 5);

                $scope.PreviousButton = $scope.CurrentPage == 0 ? true : false;
                $scope.NextButton = total == $scope.CurrentPage ? true : false;
            } else {
                $('#tab2').html('No data found for user.').show();
            }
            $('#tab2Loader').hide();
        }).error(function (data) {
            $('#tab2').html('Error occured loading data. Please refresh page again.');
            $('#tab2Loader').hide();
        });
    };

});

