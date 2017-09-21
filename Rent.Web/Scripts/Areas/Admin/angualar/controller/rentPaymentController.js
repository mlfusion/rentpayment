
// module
//var app = angular.module('app', []);

// service
app.factory('rentPaymentService', function ($http) {
    var api = '/api/rentpayment/';
    var url = '';

    return {
        ////GET
        //getList: function() {
        //    url = api + "GET/";
        //    return $http.get(url);
        //},

        //GET/5
        get: function(id) {
            url = api + "GET/" + id;
            return $http.get(url);
        },

        //GETUsers/5
        getUsers: function (id) {
            url = "/api/users/GETUsers/" + id;
            return $http.get(url);
        },

        //POST
        post: function(data) {
            url = api + 'POST/';
            return $http.post(url, data);
        },

        ////PUT
        put: function(data) {
            url = api + 'PUT/';
            return $http.put(url, data);
        },

        //DELETE/5
        delete: function(id) {
        url = api + "DELETE/" + id;
        return $http.delete(url);
    },
    };
});

app.controller('paymentsController', function ($scope, rentPaymentService) {

    $scope.name = 'beau'

    $scope.loading = function (id) {
         alert('TEST ' + id)

        //show loader...
        $scope.post(id);
 
    };

    //post
    $scope.post = function (id) {

        $scope.Id = id;

        var rentPayment = {
            Id: id,
            CurrentPage: $scope.CurrentPage != 0 ? $scope.CurrentPage : 0,
            PageSize: 5
        };

        rentPaymentService.post(rentPayment).success(function (data) {
            $scope.rentPayment = data;

            $.each(data, function (index, i) {
                if (index == 0) {
                    $scope.Uid = i.Uid;
                    $scope.CurrentPage = i.Paging.CurrentPage;
                    $scope.TotalCount = i.Paging.TotalCount;
                }
            });

            if ($scope.rentPayment != '') {
               // $('#tab1').show();
                // $('#grpRentPayment').focus();

                //hide loader...

                var total = Math.round($scope.TotalCount / 5) <= 1.0 ? 0 : Math.round($scope.TotalCount / 5);

              
                $scope.PreviousButton = $scope.CurrentPage == 0 ? true : false;
                $scope.NextButton = total == $scope.CurrentPage ? true : false;

                //Show model
                $('#rentPaymentModal').modal('show');
            } else {
                // $('#tab1').html('No data found for user.').show();
            }
           // $('#tab1Loader').hide();
        }).error(function (data) {
            $('#tab1').html('Error occured loading data. Please refresh page again.');
          //  $('#tab1Loader').hide();
        });
    };

});

// controller
app.controller('rentPaymentController', function($scope, rentPaymentService) {
    //$scope.rentPayments = [];
    $scope.rentPayment = null;
    $scope.rentPayments = null;
    $scope.editMode = false;

    $scope.main = {
        page: 1,
        take: 5
    };

    $scope.nextPage = function() {
        if ($scope.CurrentPage <= $scope.TotalCount) {
            $scope.CurrentPage++;
            $scope.post($scope.Id);
        }
    };

    $scope.previousPage = function() {
        if ($scope.CurrentPage >= 1) {
            $scope.CurrentPage--;
            $scope.post($scope.Id);
        }
    };

    //put
    $scope.put = function() {

       // alert($('input[name="active"]:checked').val());
        $('#spanSaving').show();
        $scope.alertType = true;
        $scope.alertMessage = 'Updating...';

        var obj = {
            Active: $('input[name="active"]:checked').val(),
            PaymentDate: SubtractDate($scope.paymentDate),
            Uid: $scope.Uid,
            Payment: $scope.payment,
            RentPaymentId: $scope.rentPaymentId
        };

        rentPaymentService.put(obj).success(function(data) {
            $scope.post($scope.Id);
           // $('#spanSaving').hide();
            $scope.alertMessage = ($scope.rentPaymentId == 0 ? 'New payment was added successfully.' : ' Payment was updated successfully.');
        }).error(function (data) {
           // $('#spanSaving').hide();
            $scope.alertType = false;
            $scope.alertMessage = data.Message;
           // alert('error: ' + data);
        });
    };

    // close model popup
    $scope.close = function() {
        $('#spanSaving').hide();
    };

    // load data on grid
    $scope.loading = function(id) {
        $('#tab1').hide();
        $('#tab1Loader').show();

        $scope.post(id);
    };

    //post
    $scope.post = function (id) {

        $scope.Id = id;

        var rentPayment = {
            Id: id,
            CurrentPage: $scope.CurrentPage != 0 ? $scope.CurrentPage : 0,
            PageSize: 5
        };

        rentPaymentService.post(rentPayment).success(function(data) {
            $scope.rentPayment = data;

            $.each(data, function(index, i) {
                if (index == 0) {
                    $scope.Uid = i.Uid;
                    $scope.CurrentPage = i.Paging.CurrentPage;
                    $scope.TotalCount = i.Paging.TotalCount;
                }
            });

            if ($scope.rentPayment != '') {
                $('#tab1').show();
                $('#grpRentPayment').focus();

                var total = Math.round($scope.TotalCount / 5) <= 1.0 ? 0 : Math.round($scope.TotalCount / 5);

               // $scope.lol = total + ' OK';

                $scope.PreviousButton = $scope.CurrentPage == 0 ? true : false;
                $scope.NextButton = (total == $scope.CurrentPage) ? true : false;
            } else {
               // $('#tab1').html('No data found for user.').show();
            }
            $('#tab1Loader').hide();
        }).error(function(data) {
            $('#tab1').html('Error occured loading data. Please refresh page again.');
            $('#tab1Loader').hide();
        });
    };

    //delete
    $scope.remove = function(payment) {
        rentPaymentService.delete(payment.RentPaymentId).success(function(data) {
            $scope.post($scope.Id);
        }).error(function(data) {
            alert(data.Message);
        });
    };

    //edit
    $scope.edit = function(data) {
        //alert(data.Uid);
        //$scope.rentPayments = data;
        $scope.Uid = data.Uid;
        $scope.rentPaymentId = data.RentPaymentId;
        $scope.active = data.Active;
        $scope.payment = data.Payment;
        $scope.paymentDate = AddDate(data.PaymentDate); // $filter('date')(new Date(data.PaymentDate), "MM/dd/yyyy");
        $scope.editMode = true;
        $scope.title = "Edit";
        $scope.close();
        // $('#btnSubmit').val('Update');

        // bind dropdownlist
        $scope.getUsers($scope.Id);
        $('#rentPaymentModal').modal('show');
    };

    //insert
    $scope.insert = function (id) {
        //$scope.rentPayments = null;
    };

    // open modal
    $scope.openModal = function (id) {

        $scope.clear();
        $scope.rentPayment = null;
        $scope.rentPaymentId = 0;
        $scope.active = true;
        $scope.Uid = id;
        $scope.editMode = false;
        $scope.title = "Add New";
        $scope.close();
        $('#btnSubmit').val('Add');

        // bind dropdownlist
        $scope.getUsers(id);

        $('#rentPaymentModal').modal('show');
    };

    //clear data fields
    $scope.clear = function () {
       // $scope.rentPayment = null;
        $('.payment').val('');
    };

    // Get Users
    $scope.getUsers = function(id) {
        rentPaymentService.getUsers(id).success(function(data) {
            $scope.users = data;
        });
    };

});

//format date
function SubtractDate(date) {
    var result = new Date(date);
    result.setDate(result.getDate());
    return result;
}

//format date
function AddDate(date) {
    var result = new Date(date);
    result.setDate(result.getDate());
    return result;
}

