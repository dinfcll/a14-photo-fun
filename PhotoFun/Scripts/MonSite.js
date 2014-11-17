$("button.fullscreen").click(function () {
    var imgSrc = $(this).siblings('img').attr("src");
    $("#PhotoModal img").attr("src", imgSrc);
});  