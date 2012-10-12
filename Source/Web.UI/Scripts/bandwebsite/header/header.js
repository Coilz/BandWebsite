bandwebsite.header = (function()
{
    var _header = null;

    var init = function(header)
    {
        _header = header;
        bandwebsite.header.picturebar.init($('#pictureBar'));
    };

    var destroy = function()
    {
    };

    // public API
    return {
        init: init,
        destroy: destroy
    };
})();