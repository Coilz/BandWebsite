$(document).ready(function () {
    //infuser.defaults.templateUrl = "templates";
    //infuser.defaults.templatePrefix = "SomePrefix"
    infuser.defaults.templateSuffix = ".tmpl.html";

    bandwebsite.init('801d6185-36e2-4d6b-9363-7ec0c33dc14d');
});

var bandwebsite = (function($, ko, undefined)
{
    var _bandId;

    var _init = function(bandId)
    {
        _bandId = bandId;

        bandwebsite.blog.init(bandId);
        bandwebsite.blog.render($('#blog'));
    };

    var _destroy = function()
    {
    };

    // public API
    return {
        init: _init,
        destroy: _destroy
    };
})(jQuery, ko);