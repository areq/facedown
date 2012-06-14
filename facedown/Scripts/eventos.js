$(document).ready(function () {
    
    //Le agrega una clase a la foto seleccionada
    $('#list-album img').click(function () {
        $('#list-album img').removeClass('highlight');
        $(this).addClass('highlight');
    });


});	