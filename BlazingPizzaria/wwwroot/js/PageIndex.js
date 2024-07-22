function ExpandCollapseCard(cardId) {
    var card = document.getElementById("card-" + cardId);
    if (card) {
        card.classList.toggle("expanded");
    }
}
function ToggleCardExpansion(cardId) {
    var cards = document.querySelectorAll('.pizza-card');
    cards.forEach(function (card) {
        if (card.id == "card-" + cardId) {
            card.classList.toggle('expanded');
            if (card.classList.contains('expanded')) {
                card.scrollIntoView({ behavior: 'smooth', block: 'center' });
            }

        } else {
            card.classList.remove('expanded');
        }
    });

}

