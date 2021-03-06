﻿bandwebsite.home = (function($, undefined)
{
    var _isRendering = false;
    
    var _init = function(bandId)
    {
        $(window).scroll(function() {
            if ($(window).scrollTop() >= $(document).height() - $(window).height() - 100) {
                _render();
            }
        });
        
        $(window).resize(function () {
            if ($(document).height() <= $(window).height()) {
                _render();
            }
        });

        bandwebsite.blog.init(bandId, $('#blog'));
        _render();
    };

    var _renderMore = function(moreDataAvailable)
    {
        _isRendering = false;
        // Load more if there is more data and the window is not yet filled with data
        if (moreDataAvailable && $(document).height() <= $(window).height()) {
            _render();
        }
    };

    var _render = function()
    {
        if (!_isRendering) {
            _isRendering = true;
            bandwebsite.blog.render(_renderMore);
        }
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