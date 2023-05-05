$(document).ready(function () {
    $(".owl-carousel").owlCarousel({
        margin: 10,
        center: true,
        responsiveClass: true,
        responsive: {
            0: {
                center: true,
                items: 1,
            },
            320: {
                items: 1,
                center: true,
            },
            600: {
                items: 3,
                center: true,
            },
            1000: {
                items: 4,
                nav: false,
            },
        },
    });
});

$(".color-button").click(function () {
    var values = $(this).data("id");

    $(this).children(".color-id").val(values);
    console.log($(this).children(".color-id").val());
    $(this).css("border-color", "black");
})

$(".size-button").click(function () {
    var values = $(this).data("id");

    $(this).children(".size-id").val(values);
    console.log($(this).children(".size-id").val());
    $(this).css("border-color", "black");
})


