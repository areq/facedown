var album = {};

var albums_ids = new Array();

$(document).ready(function () {

    //Al clickear el boton de download hace un pedido GET al controller.
    $('#descargar').click(function () {
        var check_input = $(":checkbox:checked");

        $.each(check_input, function (key, img) {
            var nombre = img.name;
            var url = "/home/downloadFotos/?dire=" + img.value+"&nombre="+nombre;
            window.open(url);
        });
    });

    //Pedido ajax por las fotos
    $(".verfotos").on("click", function () {
        $(document).scrollTop(0);
        $('#content').addClass('loading');
        if (album !== undefined) {
            $('#content').find('#album-' + album.id).hide("slow");
        }
        album.id = $(this).attr('id');
        album.nombre = $(this).attr('nombre');

        result = $.inArray(album.id, albums_ids);

        if (result >= 0) {
            $('#content').find('#album-' + album.id).show("slow");
            $('#content').removeClass('loading');
        } else {
            $.ajax({
                type: 'POST',
                url: '/home/fotos/' + $(this).attr('id'),
                context: document.body,
                success: function (data, textStatus) {
                    if (textStatus == 'success') {
                        var titulo = '<h2>' + album.nombre + '</h2>';
                        var x = $('<div>').attr('id', 'album-' + album.id);
                        x.append(data).prepend(titulo);
                        $('#fotos-list').append(x);
                        albums_ids.push(album.id);
                        escucharScroll();
                        $('#content').removeClass('loading');
                        fotos_checkbox();
                    } else {
                        $('#fotos-list').prepend('<h2>Hubo un error en el servidor, por favor pruebe más tarde.</h2>')
                    }
                }
            });

        }

    });

    //marca los li que tienen fotos seleccionadas.
    function fotos_checkbox() {
        $('.foto-checkbox').change(function () {
            if ($(this).is(':checked')) {
                $(this).parent().parent().addClass('selected');
            } else {
                $(this).parent().parent().removeClass('selected');
            }
        });
    }

    //funcion que muestra mas fotos.
    function mostrarMasFotos() {
        $.ajax({
            type: 'POST',
            url: '/home/fotos/mas_fotos',
            context: document.body,
            success: function (data, textStatus) {
                if (textStatus == 'success') {
                    $('#content').find('#album-' + album.id).append(data);
                } else {
                    $('#listado_album').prepend('<h2>Hubo un error en el servidor, por favor pruebe más tarde.</h2>')
                }
            }
        })
    }

    //al mover el scroll ejecuta este evento para pedir más fotos.
    function escucharScroll() {
        $(this).on("scroll", (function () {
                var pos = $(window).scrollTop() + $(window).height();
                var tot = $(document).height();
                if (pos == tot) {
                    mostrarMasFotos();
                }
        }));

    }
});	