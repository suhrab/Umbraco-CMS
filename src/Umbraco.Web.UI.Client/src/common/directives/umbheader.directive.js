angular.module("umbraco.directives")
.directive('umbHeader', function($parse, $timeout){
    return {
        restrict: 'E',
        replace: true,
        transclude: 'true',
        templateUrl: 'views/directives/umb-header.html',
        //create a new isolated scope assigning a tabs property from the attribute 'tabs'
        //which is bound to the parent scope property passed in
        scope: {
            tabs: "="
        },
        link: function (scope, iElement, iAttrs) {

            var maxTabs = 4;

            function collectFromDom(activeTab){
                var $panes = $('div.tab-content');
                
                $panes.find('.tab-pane').each(function (index) {
                    var $this = angular.element(this);
                    var id = $this.attr("rel");
                    var label = $this.attr("label");
                    var tab = {id: id, label: label, active: false};
                    if(!activeTab){
                        tab.active = true;
                        activeTab = tab;
                    }

                    if ($this.attr("rel") === String(activeTab.id)) {
                        $this.addClass('active');
                    }
                    else {
                        $this.removeClass('active');
                    }
                    
                    if(label){
                        if(scope.visibleTabs.length < maxTabs || tab.id === 0){
                            scope.visibleTabs.push(tab);
                        }else{
                            scope.overflownTabs.push(tab);
                        }
                    }

                });

            }

            scope.showTabs = iAttrs.tabs ? true : false;
            scope.visibleTabs = [];
            scope.overflownTabs = [];

            $timeout(function () {
                collectFromDom(undefined);
            }, 500);

            //when the tabs change, we need to hack the planet a bit and force the first tab content to be active,
            //unfortunately twitter bootstrap tabs is not playing perfectly with angular.
            scope.$watch("tabs", function (newValue, oldValue) {

                $(newValue).each(function(i, val){
                        var tab = {id: val.id, label: val.label};
                        if(scope.visibleTabs.length < maxTabs || tab.id === 0){
                            scope.visibleTabs.push(tab);
                        }else{
                            scope.overflownTabs.push(tab);
                        }

                        //scope.visibleTabs.push({id: val.id, label: val.label});
                });
                
                //don't process if we cannot or have already done so
                if (!newValue) {return;}
                if (!newValue.length || newValue.length === 0){return;}
                
                var activeTab = _.find(newValue, function (item) {
                    return item.active;
                });

                //we need to do a timeout here so that the current sync operation can complete
                // and update the UI, then this will fire and the UI elements will be available.
                $timeout(function () {
                    collectFromDom(activeTab);
                }, 500);
                
            });
        }
    };
});