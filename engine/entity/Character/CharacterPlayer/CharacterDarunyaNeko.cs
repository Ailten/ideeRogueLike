
public class CharacterDarunyaNeko : CharacterPlayer
{
    public CharacterDarunyaNeko(Vector posIndexCel, Character invokedBy) : base(SpriteType.Character_DarunyaNeko, posIndexCel)
    {
        this.MPmax = 0;
        this.MP = MPmax;
        this.APmax = 0;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        this.invokedBy = invokedBy; // invocked.

        this.deck.pickCountByTurn = 1;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_DarunyaNeko, //todo: illu explosion.
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.HitAround, 4) //todo: effect explo.
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }


    // override take damage, to include increase AP.
    protected override void takeDamage(int atk, Character? characterMakeAtk = null, PackageRefCard? refCard = null)
    {
        if (this.APmax < 2) // increase AP from 0 to 2.
            this.APmax += 1;

        base.takeDamage(atk, characterMakeAtk, refCard);
    }


    // invoc auto play.
    public override void turn()
    {
        if (this.AP < 2 || deck.cardsInHand.Count == 0) // not enough AP or no card in hands.
        {
            skipTurn();
            return;
        }

        Card currentCard = this.deck.cardsInHand[0];
        if (currentCard.APCost > this.AP || currentCard.distanceToUse.x > 0) // cost to mush AP or not distance expected.
        {
            skipTurn();
            return;
        }

        this.useACardFromHand(0, this.indexPosCel); // play the card.

        skipTurn();
    }
}