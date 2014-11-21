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
