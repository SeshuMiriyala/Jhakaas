app.service('postsService', function ($http,$q) {

    //var self = this;

    
    return {
        getAllPosts: function () {

            var deferred = $q.defer();

            $http({ method: 'GET', url: 'http://por-vd-miriyals.dslab.ad.adp.com/api/post/GetNewPosts?lastPostCreatedOn=NULL', headers: { 'Content-Type': 'application/JsonP' } }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;

        },
        getPosts: function () {
            return posts;
        },

        getNewPostsCount: function (lastDateTime) {

            var deferred = $q.defer();

            var serviceUrl = 'http://por-vd-miriyals.dslab.ad.adp.com/api/post/GetNewPostsCount?lastPostCreatedOn=' + lastDateTime;
            $http({ method: 'GET', url: serviceUrl, headers: { 'Content-Type': 'application/JsonP' } }).
                success(function (data, status, headers, config) {
                    deferred.resolve(data);
                }).
                error(function (data, status, headers, config) {
                    deferred.reject(status);
                });
            return deferred.promise;

        },

        getNewPosts: function (lastDateTime) {

        var deferred = $q.defer();

            $http({ method: 'GET', url: 'http://por-vd-miriyals.dslab.ad.adp.com/api/post/GetNewPosts?lastPostCreatedOn=' + lastDateTime, headers: { 'Content-Type': 'application/JsonP' } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            }).
            error(function (data, status, headers, config) {
                deferred.reject(status);
            });
        return deferred.promise;

        },
        updatePostsStatus: function (postIds) {
            var deferred = $q.defer();

            $http({ method: 'GET', url: 'http://por-vd-miriyals.dslab.ad.adp.com/api/post/updatepostsstatus?postIds=' + postIds, headers: { 'Content-Type': 'application/JsonP' } }).
            success(function (data, status, headers, config) {
                deferred.resolve(data);
            }).
            error(function (data, status, headers, config) {
                deferred.reject(status);
            });
            return deferred.promise;

        }

    };

    
    var posts = [
        {
            "ActivityName": "Test drive",
            "SalesPersonName": "Seshu Miriyala",
            "SalesPersonId": 12345,
            "LeadName": "Raghavendra Phani",
            "LeadID": 12345,
            "DateTime": "03/27/2014 3:04 PM",
            "Text": "completed for "
        },
        {
            "ActivityName": "Credit Check",
            "SalesPersonName": "Seshu Miriyala",
            "SalesPersonId": 12345,
            "LeadName": "Nishant Kabra",
            "LeadID": 12345,
            "DateTime": "03/27/2014 2:30 PM",
            "Text": "done for "
        },
        {
            "ActivityName": "First Pencil",
            "SalesPersonName": "Seshu Miriyala",
            "SalesPersonId": 12345,
            "LeadName": "Nishant Kabra",
            "LeadID": 12345,
            "DateTime": "03/27/2014 2:30 PM",
            "Text": "created for "
        }
    ];

});