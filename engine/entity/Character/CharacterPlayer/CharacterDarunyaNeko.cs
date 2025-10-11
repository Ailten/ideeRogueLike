
public class CharacterDarunyaNeko : CharacterPlayer
{
    public CharacterDarunyaNeko(Vector posIndexCel, Character invokedBy, int APMaxStart = 0) : base(SpriteType.Character_DarunyaNeko, posIndexCel)
    {
        this.MPmax = 0;
        this.MP = MPmax;
        this.APmax = APMaxStart;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        this.invokedBy = invokedBy; // invocked.

        this.deck.pickCountByTurn = 3;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Explsur,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.HitAround, 4) //todo: effect explo.
            ),
            amountOfCardAdd: 3
        );
    }


    // invoc auto play.
    public override void turn()
    {
        if (this.AP < 2 || deck.cardsInHand.Count == 0) // not enough AP or no card in hands.
        {
            skipTurn();
            return;
        }

        for (int i = deck.cardsInHand.Count - 1; i >= 0; i--)
        {
            if (!TurnManager.isInFight)
                return;
            if (deck.cardsInHand.Count == 0)
                break;

            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP || currentCard.distanceToUse.x > 0) // cost to mush AP or not distance expected.
            {
                skipTurn();
                return;
            }

            this.useACardFromHand(i, this.indexPosCel); // play the card.
        }

        skipTurn();
    }


    public override void addStatusEffectWhenSpawn()
    {
        // special effect.
        this.AddStatusEffect(new APWhenHit(this.idEntity, isAPmax: true));
    }
}