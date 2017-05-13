'use strict';

var Song = {
    currentID: '',
    currentElem: '',
    playCurrent: function (currentSong) {
        if (Player.sound._src) {
            Player.sound.stop();
            $(this.currentElem).find('.control').removeClass('pause');
            $(this.currentElem).find('.control').addClass('play');
        }

        var _currentSong = $(currentSong);
        this.currentID = _currentSong.attr('data-id');
        this.currentElem = _currentSong.closest('.single-audio');
        this.manageStateOfPlayButton();
        this.play();
    },
    play: function () {
        Player.play();
    },
    manageStateOfPlayButton: function() {
       $(this.currentElem).find('.control').removeClass('play');
       $(this.currentElem).find('.control').addClass('pause');
    },
    next: function () {
        if ($(this.currentElem).next().length > 0) {
            $(this.currentElem).find('.control').removeClass('pause');
            $(this.currentElem).find('.control').addClass('play');
            this.currentElem = $(this.currentElem).next();
            this.currentID = $(this.currentElem).find('.control.play').attr('data-id');
            this.manageStateOfPlayButton();
        } else {
            $('#next').attr('disabled');
        }
    },
    previous: function () {
        if ($(this.currentElem).prev().length > 0) {
            $(this.currentElem).find('.control').removeClass('pause');
            $(this.currentElem).find('.control').addClass('play');
            this.currentElem = $(this.currentElem).prev();
            this.currentID = $(this.currentElem).find('.control.play').attr('data-id');
            this.manageStateOfPlayButton();
        } else {
            $('#prev').attr('disabled');
        }
    },
    init: function () {
        this.currentElem = $('.single-audio').eq(0);
        this.currentID = this.currentElem.find('.control.play').attr('data-id');
    }
};

var Player = {
    sound: {},
    soundPos: 0,
    playState: false,
    play: function () {
        var self = this;
        if (self.sound) {

        }
        this.sound = new Howl({
            src: ['http://localhost:62316/songs/' + Song.currentID + '.mp3'],
            html5: true,
            onplay: function() {
                $('#songDuration').text(self.formatTime(Math.round(self.sound.duration())));
                requestAnimationFrame(self.step.bind(self));
            },
            onend: function () {
                self.skipNext();
            }
        });
        this.sound.play();
        this.playState = true;
    },
    toggle: function() {
        if (!this.playState) {
            this.play();
            this.playState = true;
        } else {
            this.pause();
            this.soundPos = this.sound
            this.playState = false;
        }
    },
    volume: function (val) {
        Howler.volume(val / 100);
    },
    skipNext: function () {
        this.sound.stop();
        Song.next();
        this.play();
    },
    skipPrevious: function () {
        this.sound.stop();
        Song.previous();
        this.play();
    },
    stop: function() {

    },
    pause: function() {
        this.sound.pause();
    },
    step: function () {
        var self = this;

        var seek = self.sound.seek() || 0;
        $('#currentTime').text(self.formatTime(Math.round(seek)));
        $('#player-slider .ui-slider-handle').css("left", (((seek / self.sound.duration()) * 100 + '%')));

        if (self.sound.playing()) {
            requestAnimationFrame(self.step.bind(self));
        }
    },
    seek: function (val) {
        if (this.sound.playing()) {
            var value = (this.sound.duration() * (val / 100));
            this.sound.seek(value);
        }
    },
    formatTime: function (secs) {
        var minutes = Math.floor(secs / 60) || 0;
        var seconds = (secs - minutes * 60) || 0;

        return minutes + ':' + (seconds < 10 ? '0' : '') + seconds;
    }
};

var toggleButton = function (selector, showMessage, hideMessage) {
    this.selector = selector;
    this.showMessage = showMessage;
    this.hideMessage = hideMessage;
    this.state;
}

toggleButton.prototype.init = function () {
    var self = this;
    self.state = false;
    $(this.selector).click(function () {
        if (!self.state) {
            $(self.selector).text(self.hideMessage);
            $('#song-player').addClass('show-extract');
            self.state = true;
        } else {
            $(self.selector).text(self.showMessage);
            $('#song-player').removeClass('show-extract');
            self.state = false;
        }
    });
}

var extractSong = {
    from: 75,
    to: 150,
    init: function () {
        var self = this;
        $('#extract-slider').slider({
            range: true,
            animate: "fast",
            min: 0,
            max: 60, //Player.sound.duration() || 60,
            values: [75, 150],
            slide: function (event, ui) {
                $("#amount").text("From: " + ui.values[0] + " - to: " + ui.values[1]);
                self.from = ui.values[0];
                self.to = ui.values[1];
            }
        });

        $("#amount").text("Od: " + $('#extract-slider').slider("values", 0) + " - do: " + $('#extract-slider').slider("values", 1));
    },
    download: function () {
        var extractModel = {
            songId: Song.currentID,
            cutFrom: this.from,
            cutTo: this.to,
        };

        CallController('Profile', 'extractSong', "POST", extractModel, null);
    }
};

$(function () {
    extractSong.init();

    Player.volume(50);

    $('#player-slider').slider({
        animate: "fast",
        change: function (event, ui) {
           Player.seek(ui.value);
           
        },
    });

    var extractButton = new toggleButton('#extract-btn', 'extract song', 'hide extract');
        extractButton.init();

    $('#player-volume').slider({
        orientation: "vertical",
        value: 50,
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

    $('#findAuthorSong').autocomplete({
        source: function (request, response) {
            CallController("Home", "Search", "POST", { keyWord: request.term },
                function (data) {
                    response($.map(data, function (item) {
                        return { label: item, value: item };
                }));
            });
        },
        select: function (event, ui) {
            window.location.href = '/search/' + ui.item.value;
        }
    });

    $('#download-sample').click(function () {
        extractSong.download();
    });
});

var CallController = function (controller, action, Type, args, callback) {
    $.ajax({
        url: '/' + controller + '/' + action,
        type: Type,
        dataType: 'json',
        data: args,
        success: function (data) {
            callback(data);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            console.log(jqXhr, textStatus, errorThrown);
        }
    });
};




