bandwebsite.header.picturebar = (function ()
{
    var _picturebar = null;
    var _mouseIsDown = false;

    var _previousX;
    var _dragStartX;
    var _isDragging;

    var init = function (picturebar)
    {
        /*
        $('.picture').carouFredSel({
        items: {
        minimum: 1,
        start: 'random',
        width: 'variable'
        },
        scroll: {
        mousewheel: true,
        wipe: true,
        items: 2,
        easing: 'linear',
        pauseOnHover: true
        },
        pagination: {
        container: '#pictureBar',
        keys: true
        },
        next: {
        button: '.next_picture',
        key: 'right'
        },
        prev: {
        button: '.prev_picture',
        key: 'left'
        }
        });
        */

        _picturebar = picturebar;

        var pictures = _picturebar.find('#pictures');

        pictures[0].addEventListener('touchstart', function (e)
        {
            e.preventDefault();
            _mouseDown(e.touches[0]);
        }, false);

        pictures[0].addEventListener('touchend', function (e)
        {
            e.preventDefault();
            _mouseUp(e.touches[0]);
        }, false);

        pictures[0].addEventListener('touchmove', function (e)
        {
            e.preventDefault();
            _mouseMove(e.touches[0]);
        }, false);

        pictures.bind('mousedown', function (e)
        {
            e.preventDefault();
            _mouseDown(e);
        });

        pictures.bind('mouseup', function (e)
        {
            e.preventDefault();
            _mouseUp(e);
        });

        pictures.mouseenter(function (e)
        {
            //e.preventDefault();
            if (!e.button) _mouseIsDown = false;
        });

        pictures.bind('mousemove', function (e)
        {
            e.preventDefault();
            _mouseMove(e);
        });

        $('.picture').fancybox({
            openEffect: 'elastic',
            openEasing: 'easeOutBack',
            closeEasing: 'easeInSine',
            closeEffect: 'elastic',
            prevEffect: 'fade',
            nextEffect: 'fade',
            openSpeed: 'slow',
            prevSpeed: 'fast',
            nextSpeed: 'fast',
            closeSpeed: 'normal',
            padding: 0,
            margin: 50,
            beforeLoad: function ()
            {
                return !_isDragging;
            },
            beforeShow: function ()
            {
                /* Disable right click */
                $.fancybox.wrap.bind("contextmenu", function (e)
                {
                    return false;
                });
                
                /* Add social buttons to the title */
                if (this.title)
                {
                    this.title += '<br />';
                }
                else {
                    this.title = '';
                }

                // Add tweet button
                this.title += '<a href="https://twitter.com/share" class="twitter-share-button" data-count="none" data-url="' + this.href + '">Tweet</a> ';

                // Add FaceBook like button
                this.title += '<iframe src="//www.facebook.com/plugins/like.php?href=' + this.href + '&amp;layout=button_count&amp;show_faces=true&amp;width=500&amp;action=like&amp;font&amp;colorscheme=light&amp;height=20" scrolling="no" frameborder="0" style="border:none; overflow:hidden; width:110px; height:20px;" allowTransparency="true"></iframe>';
            },
            afterShow: function () {
                // Render tweet button
                twttr.widgets.load();
            },
            helpers: {
                title: {
                    type: 'outside'
                }
            }
        });
    };

    var destroy = function ()
    {
    };

    var _mouseDown = function (e)
    {
        _previousX = _dragStartX = e.pageX;
        _mouseIsDown = true;
        _isDragging = false;
    };

    var _mouseUp = function (e)
    {
        _mouseIsDown = false;
    };

    var _mouseMove = function (e)
    {
        var pictures = _picturebar.find('#pictures');

        if (_mouseIsDown !== true) return;
        _isDragging = true;

        var marginLeft = 0;
        if (pictures[0].style.marginLeft !== undefined && pictures[0].style.marginLeft !== '')
        {
            marginLeft = parseInt(pictures[0].style.marginLeft.replace('px', ''));
        }

        marginLeft += e.pageX - _previousX;
        _rotatePictures(marginLeft);
        _previousX = e.pageX;
    };

    var _rotatePictures = function (newMarging)
    {
        var pictures = _picturebar.find('#pictures');
        pictures[0].style.marginLeft = newMarging + 'px';

        var firstChild = pictures.children().first();
        if (newMarging < -1 * firstChild.outerWidth())
        {
            pictures.append(firstChild);
            pictures[0].style.marginLeft = newMarging + firstChild.outerWidth() + 'px';
        }

        if (newMarging > 0)
        {
            var lastChild = pictures.children().last();
            pictures.prepend(lastChild);
            pictures[0].style.marginLeft = newMarging + -1 * lastChild.outerWidth() + 'px';
        }
    };

    // public API
    return {
        init: init,
        destroy: destroy
    };
})();