$(document).ready(function () {
    infuser.defaults.templateUrl = "templates";
    //infuser.defaults.templatePrefix = "SomePrefix"
    //infuser.defaults.templateSuffix = ".tmpl.html";

    bandwebsite.init('801d6185-36e2-4d6b-9363-7ec0c33dc14d');
});

var bandwebsite = (function($, undefined)
{
    var _init = function(bandId)
    {
        $(window).scroll(function () {
            if ($(window).scrollTop() >= $(document).height() - $(window).height()) {
                _render();
            }
        });

        bandwebsite.blog.init(bandId, $('#blog'));
        _render();
    };

    var _renderMore = function(moreDataAvailable)
    {
        // Load more if there is more data and the window is not yet filled with data
        if (moreDataAvailable && $(document).height() <= $(window).height()) {
            _render();
        }
    };

    var _render = function()
    {
        bandwebsite.blog.render(_renderMore);
    };

    var _destroy = function()
    {
    };

    // public API
    return {
        init: _init,
        destroy: _destroy
    };
})(jQuery);