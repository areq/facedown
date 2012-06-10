$(document).ready(function () {
/*
    var mostrarfotos = function (data, textStatus) {
        $('#listado_album').html(data);
        alert('Load was performed.');
        alert(textStatus);
    }
    */

    $(".album-fotos .verfotos").on("click", function () {
        $.ajax({
            type: 'POST',
            url: '/home/fotos/'+$(this).attr('id'),
            //data: $(this).attr('id'),
            context: document.body,
            success: function (data, textStatus) {
                if (textStatus == 'success') {
                    $('#listado_album').html(data);
                } else {
                    $('#listado_album').prepend('<h2>Hubo un error en el servidor, por favor pruebe más tarde.</h2>')
                }
            }
        });
    });
});	