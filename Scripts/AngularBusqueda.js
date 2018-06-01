var app = angular.module("myApp", []);
app.controller("myCtrl", function ($scope, $http) {
    $scope.SendData = function (dat) {
        $http.post('WSLogin.asmx/GetUsers', { user: dat }).then(function (response) {
            $scope.User = response.data.d;
        });
    }

    $scope.Update = function (val, dat) {
        var Id = $scope.User[val].Id;
        var ID = Id.toString();
        $http.post('WSLogin.asmx/Updates', { user: ID }).then(function () { });
        $http.post('WSLogin.asmx/GetUsers', { user: dat }).then(function (response) {
            $scope.User = response.data.d;
        });
    }
});