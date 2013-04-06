$(document).ready(function ()
{
    bandwebsite.init();
});
var bandwebsite = (function ($, undefined)
{
    var init = function ()
    {
        if (!window.location.hash) {
            window.scrollTo(0, 1);
        }
        
        bandwebsite.header.init($('.page header'));

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