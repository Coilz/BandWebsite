bandwebsite.header = (function($, undefined)
{
    var _header = null;

    var _init = function(header)
    {
        _header = header;
        bandwebsite.header.picturebar.init($('#pictureBar'));
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