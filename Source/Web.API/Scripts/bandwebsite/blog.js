bandwebsite.blog = (function($, ko, undefined)
{
    var _bandId;

    var _init = function(bandId)
    {
        _bandId = bandId;
    };

    var _destroy = function()
    {
    };

    var render = function($element)
    {
        $.getJSON('/api/Blog/' + _bandId)
            .done(function(data) {
                ko.applyBindings(new viewModel(data), $element[0]);
            });
    };
    
    var viewModel = function (array){
        this.items = ko.observableArray(array);
    };

    // public API
    return {
        init: _init,
        destroy: _destroy,
        render: render
    };
})(jQuery, ko);