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