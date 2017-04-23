var Song = {
    currentID: '',
    currentElem: '',
    playCurrent: function (currentSong) {
        var _currentSong = $(currentSong);
        this.currentID = _currentSong.attr('data-id');
        this.currentElem = _currentSong.closest('.single-audio');
        this.play();
    },
    play: function () {
        Player.play();
    },
    next: function () {
        if (this.currentElem !== 'undefined') {
            this.currentElem = $(this.currentElem).next();
            this.currentID = $(this.currentElem).find('.control.play').attr('data-id');
        }
    },
    previous: function () {
        if (this.currentElem !== 'undefined') {
            this.currentElem = $(this.currentElem).prev();
            this.currentID = $(this.currentElem).find('.control.play').attr('data-id');
        }
    },
    init: function () {

    }
};

var Player = {
    sound: {},
    nextBtn: $('#next'),
    prevBtn: $('#prev'),
    play: function () {
        var self = this;
        this.sound = new Howl({
            src: ['http://localhost:62316/songs/' + Song.currentID + '.mp3'],
            html5: true,
            onplay: function() {
                console.log(self.sound.duration());
            },
            onend: function () {

            }
        });
        this.sound.play();
    },
    volume: function (val) {
        Howler.volume(val / 100);
    },
    skipNext: function () {
        this.sound.stop();
        Song.next();
        this.sound.play();
    },
    skipPrevious: function () {
        this.sound.stop();
        Song.previous();
        this.sound.play();
    },
    toggle: function() {

    },
    formatTime: function () {

    }
};

$(function () {

    var CallController = function (controller, action, args) {
        $.ajax({
            url: '/' + controller + '/' + action,
            type: "post",
            dataType: 'json',
            contentType: 'application/json',
            data: JSON.stringify(88),
            success: function (data) {
                console.log(data);
            },
            error: function (jqXhr, textStatus, errorThrown) {
                console.log(errorThrown);
            }
        });
    };

    $('#player-slider').slider({
        change: function (event, ui) {
            console.log(ui.value);
        }
    });

    $('#player-volume').slider({
        orientation: "vertical",
        slide: function (event, ui) {
            Player.volume(ui.value);
        }
    });

    $('.volume').hover(
        function () {
            $('#volume-control').addClass('active');
        }, function () {
            $('#volume-control').removeClass('active');
        }
    );
});




