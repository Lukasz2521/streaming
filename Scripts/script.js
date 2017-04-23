//$(function () {
//    $('.control').on('mousedown', function () {
//        $(this).toggleClass('pause play');
//    });

//    $(document).on('keyup', function (e) {
//        if (e.which == 32) {
//            $('.control').toggleClass('pause play');
//        }
//    });

//    $('.control').eq(1).click(function () {
//        playSong();
//    });

//    $('#login-btn').click(function () {
//        $('#login-popup').modal();
//    });

//    $.validator.addMethod('isValidSongExtension', function (value, element) {
//        if (value == '')
//            return true;
//        var extension = value.substr((value.lastIndexOf('.'))).toUpperCase();
//        return extension === ".MP3";
//    });

//    $.validator.addMethod('isValidAvatarExtension', function (value, element) {
//        if (value == '')
//            return true;
//        var extension = value.substr((value.lastIndexOf('.'))).toUpperCase();
//       // $.inArray(extension, ".JPG") > -1);
//        return extension === ".JPG";
//    });

//    $('#uploadedsong').rules('add', {
//        isValidSongExtension: true,
//        messages: {
//            isValidSongExtension: 'Invalid file extension. File must have .mp3 extension.'
//        }
//    });

//    $('#songavatar').rules('add', {
//        isValidAvatarExtension: true,
//        messages: {
//            isValidAvatarExtension: 'Invalid file extension. File must have .jpg extension.'
//        }
//    });
//});
