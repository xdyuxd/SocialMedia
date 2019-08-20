$(document).ready(function editbio() {
    document.getElementById("#bio-text").contenteditable = true;
});

$("#bio-edit-button").on("click", function () {
    editbio();
});