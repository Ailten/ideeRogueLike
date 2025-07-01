

public class Deck
{
    public List<Card> cardsInHand = new();
    public List<Card> cardsInPioche = new();
    public List<Card> cardsInCimetier = new();

    public int pickCountByTurn = 5; //amount of card peak by turn.

    public int countCardInFullDeck //amount of all card in deck.
    {
        get { return cardsInHand.Count + cardsInPioche.Count + cardsInCimetier.Count; }
    }
    public int getAmountCardsInAllDeck
    {
        get { return cardsInHand.Count + cardsInPioche.Count + cardsInCimetier.Count; }
    }


    public Deck()
    {

    }


    //add a new card to deck (in cimetier).
    public void addCardToDeck(Card card, int amountOfCardAdd = 1, bool isSameColor = false, bool isIncludePolyChrome = false)
    {
        for (int i = 0; i < amountOfCardAdd; i++)
        {
            Card c = card;
            if (!isSameColor)
                c.cardColor = StaticCardColor.getRandomColor(isIncludePolyChrome);
            this.cardsInCimetier.Add(c);
        }
    }


    //make the deck pioche
    public void piocheOfStartTurn()
    {
        for (int i = 0; i < pickCountByTurn; i++)
        {
            piocheACard();
        }
    }

    //make discard all card in hand into defausse. (except if card have a special effect or somthing)
    public void discardOfEndTurn()
    {
        for (int i = cardsInHand.Count - 1; i >= 0; i--)
        {
            pushCardFromHandIntoCimetier(i);
        }
    }

    //pioche a card from
    public void piocheACard()
    {
        if (cardsInPioche.Count == 0)
        { //if no card in pioche.

            if (cardsInCimetier.Count == 0) //if cimetier has also no card, can't pioche.
                return;

            shuffleCimetierIntoPioche(); //re fill the pioche with the cimetier.
        }

        cardsInHand.Add(cardsInPioche[0]); //add card from pioche to hand.
        cardsInPioche.RemoveAt(0); //remove card pick from pioche.
    }

    //shuffle the cimetier and place it in pioche.
    public void shuffleCimetierIntoPioche()
    {
        shuffleCimetier(); //shuffle cimetier.
        cardsInPioche.AddRange(cardsInCimetier); //copy cimetier into pioche.
        cardsInCimetier.RemoveAll(_ => true); //clean cimetier.
    }


    //shuffle cimetier.
    public void shuffleCimetier()
    {
        List<Card> cardsCimetierShuffle = cardsInCimetier.OrderBy((c) => RandomManager.rng.Next()).ToList();
        cardsInCimetier.RemoveAll(_ => true);
        cardsInCimetier.AddRange(cardsCimetierShuffle);
    }
    //shuffle pioche.
    public void shufflePioche()
    {
        List<Card> cardsPiocheShuffle = cardsInPioche.OrderBy((c) => RandomManager.rng.Next()).ToList();
        cardsInPioche.RemoveAll(_ => true);
        cardsInPioche.AddRange(cardsPiocheShuffle);
    }


    //push the card selected in the cimetier.
    public void pushCardFromHandIntoCimetier(int indexCard)
    {
        cardsInCimetier.Add(cardsInHand[indexCard]); //add card from hand to cimetier.
        cardsInHand.RemoveAt(indexCard); //remove card use from hand.
    }

    //remove all card from pioche into cimetier.
    public void pushAllCardPiocheIntoCimetier()
    {
        cardsInCimetier.AddRange(cardsInPioche);
        cardsInPioche.RemoveAll(_ => true);
    }


    public override string ToString()
    {
        return $"d: \n" +
        $"  pioc: [{string.Join(", ", this.cardsInPioche.Select(c=>c.ToString()))}]\n"+
        $"  hand: [{string.Join(", ", this.cardsInHand.Select(c=>c.ToString()))}]\n"+
        $"  defo: [{string.Join(", ", this.cardsInCimetier.Select(c=>c.ToString()))}]\n";
    }

}