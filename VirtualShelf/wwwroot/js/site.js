// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


function pesquisaFriend() {
    var login = document.getElementById('login').value;

    $.ajax({
        type: 'POST',
        url: "/Usuario/BuscaUsuarios",
        cache: false,
        data: { "txtUsuario": login},
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}


function pesquisaFriendMid() {
    var login = document.getElementById('midia').value;

    $.ajax({
        type: 'POST',
        url: "/Usuario/BuscaUsuarios",
        cache: false,
        data: { "txtMidia": login },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}

function cancelarFriend(id) {
    if (confirm('Confirma remover amizade?'))
        location.href = '/usuario/RemoveAmizade?friendId=' + id;
}


function pedirFriend(id) {
    if (confirm('Confirma pedido de amizade?'))
        location.href = '/usuario/ConviteAmizade?friendId=' + id;
}


function aceitarFriend(id) {
    if (confirm('Confirmar amizade?'))
        location.href = '/usuario/ConcretizaAmizade?friendId=' + id;
}


function removerDaVitrine(id) {
    if (confirm('Confirmar Remoção?'))
        location.href = '/midia/RemoveDaVitrine?idF=' + id;
}


function apagarConta(id) {
    if (confirm('Confirmar deletar conta? Não pode ser desfeito'))
        location.href = '/usuario/Delete?id=' + id;
}



function atualizaGrid() {
    var tipo = document.getElementById('Tipo').value;
    var categoria = document.getElementById('Categoria').value;
    var vitrineId = document.getElementById('vitrineId').value;

    $.ajax({
        type: 'POST',
        url: "/Vitrine/AtualizaGridIndex",
        cache: false,
        data: { "Tipo": tipo, "Categoria": categoria, "userId": vitrineId },
        success: function (dados) {
            $("#conteudoGrid").html(dados);
        }
    });
}



function CarregaTipos() {

    $.ajax({
        type: 'GET',
        url: "/Vitrine/CarregaTipos",
        datatype: "json",
        cache: false,
        success: function (dados) {
            var comboModelo = $("#Tipo");
            preencheDropDownList(comboModelo, dados);
        }
    });
}


function preencheDropDownList(dropdown, dados) {
    dropdown.empty();
    $.each(dados, function () {
        dropdown.append($("<option />").val(this.value).text(this.text));
    });
};


