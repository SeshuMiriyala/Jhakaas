'use strict';

/* Directives */


app.directive('popOver', function ($compile) {

   
    var itemsTemplate = "<div ng-repeat='item in items'>{{item}} <br/><hr/></div> ";
    var getTemplate = function (contentType) {
        var template = '';
        switch (contentType) {
            case 'items':
                template = itemsTemplate;
                break;
        }
        return template;
    }
    return {
        restrict: "A",
        transclude: true,
        template: "<span ng-transclude></span>",
        link: function (scope, element, attrs) {
            var popOverContent = "<div></div>";

            if (scope.items) {
                var html = getTemplate("items");
                popOverContent = $compile(html)(scope);
            }
            
            var options = {
                content: popOverContent,
                placement: "bottom",
                html: true,
                title: scope.title
            };
            $(element).popover(options);
            
            
        },
        scope: {
            items: '=',
            title: '@'
        }
    };
});
