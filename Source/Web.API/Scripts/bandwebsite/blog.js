bandwebsite.blog = (function($, ko, undefined)
{
    var _bandId;
    var _viewModel;
    var _page = 0;
    var _pageSize = 3;

    var _init = function (bandId, $element)
    {
        _bandId = bandId;
        
        _viewModel = new viewModel();
        ko.applyBindings(_viewModel, $element[0]);
    };

    var _destroy = function()
    {
    };

    var render = function(callback, errorCount)
    {
        $.getJSON('/api/Blog/' + _bandId + '/' + _page + '/' + _pageSize)
            .done(function (data) {
                _page++;
                $.each(data, function (i, item) {
                    _viewModel.items.push(item);
                });

                if (callback !== undefined) {
                    callback(data.length > 0);
                }
            })
            .fail(function (jqxhr, textStatus, error) {
                if (errorCount === undefined) {
                    errorCount = 1;
                }
                
                var err = textStatus + ', ' + error;
                console.log("Request failed: " + err);
                
                if (errorCount < 5) {
                    render(callback, errorCount);
                }
            })
            .always(function() {
            });
    };

    var viewModel = function()
    {
        this.items = ko.observableArray();
    };

    // public API
    return {
        init: _init,
        destroy: _destroy,
        render: render
    };
})(jQuery, ko);