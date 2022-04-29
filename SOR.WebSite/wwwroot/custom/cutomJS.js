function HinhSu() {
    $('#hinhsu').show();
    $('#tenan').hide();
    $('#buonlau').hide();
    $('#khaithac').hide();
    $('#khac').hide();
    $('.hinhsu').addClass('gachduoihinhsu');
    $('.tenan').removeClass('gachduoitenan');
    $('.buonlau').removeClass('gachduoibuonlau');
    $('.khaithac').removeClass('gachduoikhaithac');
    $('.khac').removeClass('gachduoikhac');
}

function TeNan() {
    $('#hinhsu').hide();
    $('#tenan').show();
    $('#buonlau').hide();
    $('#khaithac').hide();
    $('#khac').hide();
    $('.hinhsu').removeClass('gachduoihinhsu');
    $('.tenan').addClass('gachduoitenan');
    $('.buonlau').removeClass('gachduoibuonlau');
    $('.khaithac').removeClass('gachduoikhaithac');
    $('.khac').removeClass('gachduoikhac');
}

function BuonLau() {
    $('#hinhsu').hide();
    $('#tenan').hide();
    $('#buonlau').show();
    $('#khaithac').hide();
    $('#khac').hide();
    $('.hinhsu').removeClass('gachduoihinhsu');
    $('.tenan').removeClass('gachduoitenan');
    $('.buonlau').addClass('gachduoibuonlau');
    $('.khaithac').removeClass('gachduoikhaithac');
    $('.khac').removeClass('gachduoikhac');
}

function KhaiThac() {
    $('#hinhsu').hide();
    $('#tenan').hide();
    $('#buonlau').hide();
    $('#khaithac').show();
    $('#khac').hide();
    $('.hinhsu').removeClass('gachduoihinhsu');
    $('.tenan').removeClass('gachduoitenan');
    $('.buonlau').removeClass('gachduoibuonlau');
    $('.khaithac').addClass('gachduoikhaithac');
    $('.khac').removeClass('gachduoikhac');
}

function Khac() {
    $('#hinhsu').hide();
    $('#tenan').hide();
    $('#buonlau').hide();
    $('#khaithac').hide();
    $('#khac').show();
    $('.hinhsu').removeClass('gachduoihinhsu');
    $('.tenan').removeClass('gachduoitenan');
    $('.buonlau').removeClass('gachduoibuonlau');
    $('.khaithac').removeClass('gachduoikhaithac');
    $('.khac').addClass('gachduoikhac');
}
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