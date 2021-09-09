// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

<<<<<<< HEAD
// Write your JavaScript code.
=======
<<<<<<< HEAD
// Write your JavaScript code.
=======
// Write your JavaScript code.

// Time Ponto Eletrônico -- //Views/Home/Index.cshtml
function myTime() {
    var d = new Date(), displayDate;
    if (navigator.userAgent.toLowerCase().indexOf('firefox') > -1) {
        displayDate = d.toLocaleTimeString('pt-BR');
    }
    else {
        displayDate = d.toLocaleTimeString('pt-BR', { timeZone: 'America/Sao_Paulo' });
    }
    document.getElementById("clock").innerHTML = displayDate;
}

$().ready(function () {
    myTime();
    var myVar = setInterval(myTime, 1000);
});
>>>>>>> 81c457c2bb58ecd8df79506aabdf98b654dbe569
>>>>>>> 8cbb14a6b62aad532c12d5dd4d1a61cc6c06cf14
