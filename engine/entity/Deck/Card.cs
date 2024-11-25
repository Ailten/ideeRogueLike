
public class Card
{

    public CardIllustationType cardIllu; //illustation of the card.

    public int APCorst; //the amount of AP need for use the card.

    public Vector distanceToUse = new(); //distance can be use (x=min, y=max).

    public List<EffectCard> effects = new(); //all effects of a card.


}