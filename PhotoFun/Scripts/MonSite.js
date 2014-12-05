$("button.fullscreen").click(function () {
    var imgSrc = $(this).siblings('img').attr("src");
    var nomUtilisateur = $(this).siblings('span.nomUtil').attr("item");
    var commentaire = $(this).siblings('span.commentaire').attr("item");
    var href="/Account/ProfilUtil?nomUtil="+nomUtilisateur;
    $("#PhotoModal img").attr("src", imgSrc);
    $("#nomUtil").attr("href", href);
    document.getElementById('nomUtil').innerHTML = nomUtilisateur;
    document.getElementById('commentaire').innerHTML = commentaire;
});
function reste(texte) {
    var restants = 200 - texte.length;
    document.getElementById('caracteres').innerHTML = restants;
};

function readURL(input) {
    if (input.files && input.files[0]) {

        var reader = new FileReader();
        var image = new Image();
        var imageSrc;

        reader.onload = function (event) {
            imageSrc = event.target.result;
            image.src = imageSrc;
        }
        image.onload = function () {
            $("#photoToShow").attr("src", imageSrc);
        };

        reader.readAsDataURL(input.files[0]);
    }
}

$("#ChangePhotoProfil").change(function () {
    readURL(this);
    $("#PhotoUtil").modal("show");
});
