app.controller('postsController', function ($scope, postsService, $timeout) {

    $scope.posts = [];
    postsService.getAllPosts().then(function (posts) {
        $scope.posts = posts;
        $scope.lastPostDate = posts[0].CreatedOn;
    }, function (statusCode) {
        console.log(statusCode);
    });

    $scope.cnt = 0;
    $scope.showBubble = false;
    $scope.hovered = false;

    $scope.getNewPosts = function () {
        postsService.getAllPosts().then(function (posts) {
            $scope.showBubble = false;
            $scope.posts = posts;
            $scope.lastPostDate = posts[0].CreatedOn;
        }, function (statusCode) {
            console.log(statusCode);
        });
    };

    var getNewPostsCount = function () {
        postsService.getNewPostsCount($scope.lastPostDate).then(function (count){
            if (count > 0) {
                $scope.showBubble = true;
                $scope.newMessageCount = count;

            } else {
                $scope.showBubble = false;
            }
        }, function (statusCode) {
            console.log(statusCode);
        });
        $timeout(getNewPostsCount, 5000);
    };
    
    
    $timeout(getNewPostsCount, 5000);

});

