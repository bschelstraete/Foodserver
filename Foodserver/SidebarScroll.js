$(function () {
    if (!(/Android|webOS|iPhone|iPad|iPod|BlackBerry/i.test(navigator.userAgent))) {
        var $sidebar = $("#fixedPosition"),
        $window = $(window),
        offset = $sidebar.offset(),
        topPadding = 20;

        $window.scroll(function () {
            if ($window.scrollTop() > offset.top) {
                $sidebar.stop().animate({
                    marginTop: $window.scrollTop() - offset.top + topPadding
                });
            } else {
                $sidebar.stop().animate({
                    marginTop: 20
                });
            }
        });
    }
});