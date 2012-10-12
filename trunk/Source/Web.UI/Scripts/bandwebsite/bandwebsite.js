/*
// Override the onload event
window.onload = function()
{
    // the page finished loading, do something here...
};
*/

// jQuery's version of window.onload function
$(document).ready(function ()
{
    bandwebsite.init();
});

/*
$(document).live('pageinit', function (event)
{
bandwebsite.init();
});
*/

var bandwebsite = (function ()
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
})();