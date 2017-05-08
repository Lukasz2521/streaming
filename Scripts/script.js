$(function () {
    $("#songUpload").validate({
        onfocusout: true,
        rules: {
            Title: {
                required: true,
            },
        },
        messages: {
            Title: {
                required: "Tytuł utworu jest wymagany."
            }
        }
    });

    $('.carousel').carousel();

    $.validator.addMethod('isValidSongExtension', function (value, element) {
        if (value == '')
            return true;
        var extension = value.substr((value.lastIndexOf('.'))).toUpperCase();

        return extension === ".MP3";
    });

    $.validator.addMethod('isValidAvatarExtension', function (value, element) {
        if (value == '')
            return true;
        var extension = value.substr((value.lastIndexOf('.'))).toUpperCase();

        return extension === ".JPG";
    });

    $('#uploadedsong').rules('add', {
        isValidSongExtension: true,
        messages: {
            isValidSongExtension: 'Invalid file extension. File must have .mp3 extension.'
        }
    });

    $('#songavatar').rules('add', {
        isValidAvatarExtension: true,
        messages: {
            isValidAvatarExtension: 'Invalid file extension. File must have .jpg extension.'
        }
    });
});
