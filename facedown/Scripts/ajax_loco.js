﻿var album = {};
var img_descarga = {
    fotos: []
};
img_descarga = [
    { 'id': '123',
        'nombre': 'Album Montaña',
        foto: []
    },
    { 'id': '23',
        'nombre': 'Album de Playa',
        foto: []
    },
    { 'id': '44',
        'nombre': 'Album pedorro',
        foto: []
    },
    { 'id': '44132123',
        'nombre': 'Album asdaszd',
        foto: []
    }
];
    $(document).ready(function () {

        $('#list-album img').click(function () {
            $('#list-album img').removeClass('highlight');
            $(this).addClass('highlight');
        });

        $(".verfotos").on("click", function () {
            $('#content').addClass('loading');
            album.id = $(this).attr('id');
            album.nombre = $(this).attr('nombre');
            $.ajax({
                type: 'POST',
                url: '/home/fotos/' + $(this).attr('id'),
                //data: $(this).attr('id'),
                context: document.body,
                success: function (data, textStatus) {
                    if (textStatus == 'success') {
                        $('#fotos-list').html(data);
                        trata_imagenes();
                        $(document).scrollTop(0);
                        $('#content').removeClass('loading');
                    } else {
                        $('#fotos-list').prepend('<h2>Hubo un error en el servidor, por favor pruebe más tarde.</h2>')
                    }
                }
            });
        });

        function trata_imagenes() {
            $("#listado_fotos div img").on("click", function () {
                alert('foto agregada' + this.alt);
                alert(album.id);
                alert(album.nombre);
                /*
                i = img_descarga.count;
                while (i > 0 && ) {
                if(album.id==img_descarga[i].id){
                img_descarga[0].foto.push({
                "id": this.id,
                "nombre": this.alt,
                "src": this.src
                });
                }
                i++;
                };

                */
                img_descarga[0].foto.push({
                    "id": this.id,
                    "nombre": this.alt,
                    "src": this.src
                });

            });
        }

    });	