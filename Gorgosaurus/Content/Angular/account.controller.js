﻿(function () {
    angular.module('forumApp').controller('accountController', accountController);

    accountController.$inject = ['$scope', '$http', '$rootScope'];

    function accountController($scope, $http, $rootScope) {
        var self = this;

        self.loginInfo = { username: "Tester", password: "" };
        self.greetingText = "";
        self.isLoggedIn = false;

        $rootScope.loggedInUser = "";

        $scope.loginPopover = {
            templateUrl: 'myPopoverTemplate.html',
            title: 'Log in'
        };

        self.init = function(){
            self.getCurrentUser();
        };

        self.getCurrentUser = function () {
            $http.get("/account/current").success(function (resp) {
                if (!resp) {
                    $rootScope.loggedInUser = null;

                    self.isLoggedIn = false;
                }
                else {
                    $rootScope.loggedInUser = resp;

                    //console.log('changing', $rootScope.loggedInUsername);

                    self.isLoggedIn = true;                  
                }
                
                self.updateGreetingText();
            });
        };

        self.updateGreetingText = function () {
            if(self.isLoggedIn){
                self.greetingText = "Hello, " + $rootScope.loggedInUser.username;
            }
            else{
                 self.greetingText = "Hello, guest";
            }
        };

        self.login = function () {
            var loginData = { username: self.loginInfo.username, password: self.loginInfo.password };

            $http.post("/account/login", loginData)
                .success(function (res) {
                    console.log(res);
                    
                    self.getCurrentUser();
                });            
        };

        self.logout = function () {
            $http.post("/account/logout")
                .success(function (res) {
                    console.log(res);

                    self.getCurrentUser();
                });              
        };

        self.init();
    }
})();