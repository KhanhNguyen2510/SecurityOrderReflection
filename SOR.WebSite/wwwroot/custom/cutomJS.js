
$(document).ready(function () {
    $('.taikhoan').click(function (event) {
        event.stopPropagation();
        $('.thongtin-taikhoan').fadeIn(500);
    });
    $(".thongtin-taikhoan").on("click", function (event) {
        event.stopPropagation();
    });
});
$(document).on("click", function () {
    $(".thongtin-taikhoan").fadeOut(500);
});
function myFunction() {
    var x = document.getElementById("myTopnav");
    if (x.className === "topnav") {
        x.className += " responsive";
    } else {
        x.className = "topnav";
    }
}

$('.btnOn').click(function () {
    $('.modal').fadeIn(500);
    $('.overlay').show();
})
$('.close').click(function () {
    $('.modal').fadeOut(500);
    $('.overlay').hide();
})
$('#close').click(function () {
    $('.modal').fadeOut(500);
    $('.overlay').hide();
})