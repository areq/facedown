var album = {};
var i = 0;
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
var albums_ids = new Array();

$(document).ready(function () {



    $('#descargar').click(function () {
        var check_input = $(":checkbox:checked");
        alert(check_input.length);
        alert(check_input)
        $.each(check_input, function (key, value) {

            $.ajax({
                type: "POST",
                url: "/home/downloadFotos/",
                data: value.value,
                success: function (data, textStatus) {
                    alert(i);
                }
            })

        });
        /*
        for (i = 0; i < check_input.length; i++) {
        $.ajax({
        type: "POST",
        url: "/home/downloadFotos/" + check_input[i].attr('name'),

        success: function (data, textStatus) {
        alert(i);
        }
        })
        }*/
        /*
        //window.location.href = check_input.val();
        
        DownloadFile('http://a4.sphotos.ak.fbcdn.net/hphotos-ak-prn1/25332_362663434595_6414959_n.jpg', 'foto.jpg');
        $.get('http://a4.sphotos.ak.fbcdn.net/hphotos-ak-prn1/25332_362663434595_6414959_n.jpg'
        );
       
        alert('entro2!');
        alert(check_input.val());
        */
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

    /*$('descargar').click(function () {

    var data = { 'user_ids[]': [] };
    $(":checked").each(function () {
    data['albums[]'].push($(this).val());
    });
    alert(data);
    $.ajax({
    type: 'POST',
    url: '/home/downloadFotos/',
    data: data,
    success: alert('el ajax llego!')
    });
    alert('...');
    });
    */




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
            /*$(".mas-fotos").on("click", function () {
            $(this).detach();
            mostrarMasFotos($(this));
            });*/
        }));

    }
});	