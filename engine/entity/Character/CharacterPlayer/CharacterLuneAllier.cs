

public class CharacterLuneAllier : CharacterPlayer
{
    public CharacterLuneAllier(Vector posIndexCel, Character invokedBy, int amountOfHealByCard = 2) : base(SpriteType.Character_LuneAllier, posIndexCel)
    {
        this.MPmax = 0;
        this.MP = MPmax;
        this.APmax = 2;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        this.invokedBy = invokedBy; // invocked.

        this.deck.pickCountByTurn = 1;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Barbak,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Heal, amountOfHealByCard)
            ),
            amountOfCardAdd: 2
        );
    }


    // invoc auto play.
    public override void turn()
    {
        if (deck.cardsInHand.Count == 0) // not enough AP or no card in hands.
        {
            skipTurn();
            return;
        }

        for (int i = deck.cardsInHand.Count - 1; i >= 0; i--)
        {
            Card currentCard = this.deck.cardsInHand[i];
            if (currentCard.APCost > this.AP) // cost to mush AP or not distance expected.
            {
                skipTurn();
                return;
            }

            List<Character> allier = TurnManager.getAllCharacters().Where(c =>
            {
                if (!c.isInRedTeam) // skip mobs.
                    return false;
                if (this == c) // skip self.
                    return false;
                float distCell = (
                    Math.Abs(c.indexPosCel.x - this.indexPosCel.x) +
                    Math.Abs(c.indexPosCel.x - this.indexPosCel.x)
                );
                return ( // distance on right range.
                    distCell >= currentCard.distanceToUse.x &&
                    distCell <= currentCard.distanceToUse.y
                );
            }).OrderBy(c => c.isAPlayer).ToList();
            if (allier.Count == 0)
            {
                skipTurn();
                return;
            }

            this.useACardFromHand(i, allier[0].indexPosCel); // play the card.
        }

        skipTurn();
    }

}