app.controller("pushAlertController", [
    '$scope', 'postsService', '$timeout', function ($scope, postService, $timeout) {

        var now = new Date();
        $scope.items = [];
        $scope.notificationIds = [];
        $scope.newAlertCount = 0;
        var fullDate = now.getUTCFullYear() + "-" + now.getUTCMonth() + "-" + now.getUTCDate() + " " + now.getUTCHours() + ":" + now.getUTCMinutes() + ":" + now.getUTCSeconds();
        console.log(fullDate);

        $scope.reset = function () {
            //resetBubble();
            postService.updatePostsStatus($scope.notificationIds.join(',')).then(function (result) {
                if (result > 0) {
                    $scope.notificationIds = [];
                    $scope.newAlertCount = 0;
                }
            }, function (statusCode) {
                console.log(statusCode);
            });
            
        };

        var populateBubble = function() {
            $timeout(populateBubble, 10000);
            var count = 0;
            postService.getNewPosts().then(function (data) {
                if (data) {

                    _.each(data, function (element) {
                        if (element.IsNotificationEnabled && (!element.IsPostRead)) {
                            $scope.notificationIds.push(element.Id);
                            count = count + 1;
                            $scope.bubbleTitle = "New Notifications";
                            $scope.bubbleMessage = "You have " + $scope.newAlertCount + " new Pencils Create requests";
                            $scope.items.push(count + ') ' + element.ActivityName + ' ' + element.LeadFirstName);
                        }
                    });
                }
                $scope.newAlertCount = count;
            });
            
        };

        var resetBubble = function () {
            $scope.notificationIds = [];
            //$scope.items = [];

        };
        populateBubble();
    }
]);