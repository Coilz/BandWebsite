$(document).ready(function ()
{
    bandwebsite.init();
});
    
var bandwebsite = (function ($, undefined)
{
    var init = function ()
    {
        infuser.defaults.templateUrl = "templates";
        //infuser.defaults.templatePrefix = "SomePrefix"
        //infuser.defaults.templateSuffix = ".tmpl.html";

        if (!window.location.hash) {
            window.scrollTo(0, 1);
        }
        
        bandwebsite.header.init($('.page header'));
        bandwebsite.home.init('801d6185-36e2-4d6b-9363-7ec0c33dc14d');

        $('.datepicker').datepicker({
            dateFormat: 'dd MM yy',
            showWeek: true,
            firstDay: 1
        });
    };

    var destroy = function ()
    {
    };

    // public API
    return {
        init: init,
        destroy: destroy
    };
})(jQuery);