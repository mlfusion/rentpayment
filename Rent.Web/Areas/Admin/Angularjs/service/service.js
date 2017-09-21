
rentPaymentApp.service('rentPaymentAppService', function ($http) {
    var api = '/api/rentpayment/';
    var url = '';

    return {
        //GET
        getList: function() {
            url = api + "GET/";
            return $http.get(url);
        },

        //GET/5
        get: function(id) {
            //url = api + "GET/" + id;
            return id; // $http.get(url);
        },

        //POST
        post: function(data) {
            url = api + 'POST/';
            return $http.post(url, data);
        },

        //PUT
        put: function(data) {
            url = api + 'PUT/';
            return $http.put(url, data);
        }
    };
})