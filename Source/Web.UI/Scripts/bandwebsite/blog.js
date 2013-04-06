bandwebsite.blog = (function($, ko, undefined)
{
    var _bandId;
    var _viewModelInstance;

    var _paging = { page: 0, pageSize: 3 };

    var _init = function (bandId, $element)
    {
        _bandId = bandId;
        
        _viewModelInstance = new _viewModel();
        ko.applyBindings(_viewModelInstance, $element[0]);
    };

    var _destroy = function()
    {
    };

    var render = function(callback, errorCount)
    {
        $.getJSON('/api/Blog/' + _bandId + '/' + _paging.page + '/' + _paging.pageSize)
            .done(function (data) {
                _paging.page++;
                $.each(data, function (i, item) {
                    _viewModelInstance.items.push(item);
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

    var _viewModel = function()
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