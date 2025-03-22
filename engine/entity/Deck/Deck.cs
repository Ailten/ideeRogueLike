

public class Deck
{
    private List<Card> cardsInHand = new();
    private List<Card> cardsInPioche = new();
    private List<Card> cardsInCimetier = new();

    private int indexCardHandSelected = -1; //index of hand, for card selected.

    public int pickCountByTurn = 3; //amount of card peak by turn.

    public bool isACardSelected //bool for know if a card is selected.
    {
        get { return indexCardHandSelected >= 0 && indexCardHandSelected < cardsInHand.Count; }
    }
    public int countCardInFullDeck //amount of all card in deck.
    {
        get { return cardsInHand.Count + cardsInPioche.Count + cardsInCimetier.Count; }
    }


    public Deck()
    {

    }


    //add a new card to deck (in cimetier).
    public void addCardToDeck(Card card, int multiAdd = 1)
    {
        for(int i=0; i<multiAdd; i++)
            this.cardsInCimetier.Add(card);
    }


    //make the deck pioche
    public void piocheOfStartTurn()
    {
        for(int i=0; i<pickCountByTurn; i++){
            piocheACard();
        }
    }

    //pioche a card from
    public void piocheACard()
    {
        if(cardsInPioche.Count == 0){ //if no card in pioche.

            if(cardsInCimetier.Count == 0) //if cimetier has also no card, can't pioche.
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
        cardsInPioche = cardsInCimetier; //copy cimetier into pioche.
        cardsInCimetier = new(); //clean cimetier.
    }


    //shuffle cimetier.
    public void shuffleCimetier()
    {
        cardsInCimetier = cardsInCimetier.OrderBy((c) => RandomManager.rng.Next()).ToList();
    }
    //shuffle pioche.
    public void shufflePioche()
    {
        cardsInPioche = cardsInPioche.OrderBy((c) => RandomManager.rng.Next()).ToList();
    }

    
    //push the card selected in the cimetier.
    public void pushCardSelectedIntoCimetier()
    {
        cardsInCimetier.Add(cardsInHand[indexCardHandSelected]); //add card from hand to cimetier.
        cardsInHand.RemoveAt(indexCardHandSelected); //remove card use from hand.
    }

    //defaus a card from hand to cimetier (base on index in hand).
    public void pushCardHandIntoCimetier(int indexHand)
    {
        cardsInCimetier.Add(cardsInHand[indexHand]); //add card from hand to cimetier.
        cardsInHand.RemoveAt(indexHand); //remove card use from hand.
    }

}