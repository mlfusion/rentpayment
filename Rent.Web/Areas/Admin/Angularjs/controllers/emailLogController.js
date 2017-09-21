
// module
//var app = angular.module('app', []);

// service
app.factory('emailLogService', function ($http) {
    var api = '/api/logEmail/';
    var url = '';

    return {
        ////GET
        //getList: function() {
        //    url = api + "GET/";
        //    return $http.get(url);
        //},

        //GET/5
        get: function (data) {
            url = api + "GET/" + id;
            return $http.get(url, data);
        },

        //POST
        post: function (data) {
            url = api + 'POST/';
            return $http.post(url, data);
        }//,

        ////PUT
        //put: function(data) {
        //    url = api + 'PUT/';
        //    return $http.put(url, data);
        //}
    };
});

// controller
app.controller('emailLogController', function ($scope, emailLogService) {
    $scope.emailLogs = [];
    $scope.emailLog = null;

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
        $('#tab3').hide();
        $('#tab3Loader').show();

        $scope.post(id);
    };

    //post
    $scope.post = function (id) {

        var _emailLog = {
            Id: id,
            CurrentPage: $scope.CurrentPage != 0 ? $scope.CurrentPage : 0,
            PageSize: 5
        };

       // $('#tab3').hide();
       // $('#tab3Loader').show();

        emailLogService.post(_emailLog).success(function (data) {
            $scope.emailLog = data;

            $.each(data, function (index, i) {
                if (index == 0) {
                    $scope.Uid = i.Uid;
                    $scope.CurrentPage = i.Paging.CurrentPage;
                    $scope.TotalCount = i.Paging.TotalCount;
                }
            });

            if ($scope.emailLog != '') {
                $('#tab3').show();
                $('#grpEmailLog').focus();

                var total = Math.round($scope.TotalCount / 5) <= 1.0 ? 0 : Math.round($scope.TotalCount / 5);

                $scope.PreviousButton = $scope.CurrentPage == 0 ? true : false;
                $scope.NextButton = total == $scope.CurrentPage ? true : false;
                $scope.Total = Math.round($scope.TotalCount / 5) - 1;
            } else {
                $('#tab3').html('No data found for user.').show();
            }
            $('#tab3Loader').hide();
        }).error(function (data) {
            $('#tab3').html('Error occured loading data. Please refresh page again.');
            $('#tab3Loader').hide();
        });
    };

    // open modal
    $scope.openModal = function (id) {

        //$scope.clear();
        //$scope.rentPaymentId = 0;
        //$scope.active = true;
        //$scope.Uid = id;
        //$scope.editMode = false;
        //$scope.title = "Add New";
        //// $('#btnSubmit').val('Add');
        //$('#rentPaymentModal').modal('show');
    };

});

