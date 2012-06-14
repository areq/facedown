var album = {};

var albums_ids = new Array();

$(document).ready(function () {

    $('#descargar').click(function () {
        var check_input = $(":checkbox:checked");

        $.each(check_input, function (key, img) {

            $.ajax({
                type: "POST",
                url: "/home/downloadFotos/",
                data: { dire: img.value },
                cache: false,
                success: function (specialties) {
                },
                error: function (response) {
                    alert('fallo');
                }
            });
        });
    });


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

    function trata_imagenes() {
        $("#listado_fotos div img").on("click", function () {
            alert('foto agregada' + this.alt);
            alert(album.id);
            alert(album.nombre);
            img_descarga[0].foto.push({
                "id": this.id,
                "nombre": this.alt,
                "src": this.src
            });

        });
    }

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

    function escucharScroll() {
        $(this).on("scroll", (function () {
            if (i == 0) {
                i = 1;
                var pos = $(window).scrollTop() + $(window).height();
                var tot = $(document).height();
                if (pos == tot) {
                    mostrarMasFotos();
                }
            }
            i = 0;
        }));

    }
});	