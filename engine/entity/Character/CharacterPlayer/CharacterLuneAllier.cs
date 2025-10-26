

public class CharacterLuneAllier : CharacterPlayer
{
    public CharacterLuneAllier(Vector posIndexCel, Character invokedBy, int amountOfHealByCard = 2, CardColor? cardColorUsed = null) : base(SpriteType.Character_LuneAllier, posIndexCel)
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
                cardColor: cardColorUsed ?? StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Heal, amountOfHealByCard)
            ),
            amountOfCardAdd: 2,
            isSameColor: true
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
            if (!TurnManager.isInFight)
                return;
            if (deck.cardsInHand.Count == 0)
                break;
                
            Card currentCard = this.deck.cardsInHand[i];
            if (currentCard.APCost > this.AP) // cost to mush AP or not distance expected.
                continue;

            List<Character> allier = TurnManager.getAllCharacters().Where(c =>
            {
                if (!c.isInRedTeam) // skip mobs.
                    return false;
                float distCell = (
                    Math.Abs(c.indexPosCel.x - this.indexPosCel.x) +
                    Math.Abs(c.indexPosCel.y - this.indexPosCel.y)
                );
                bool isOnRangeCard = ( // distance on right range.
                    distCell >= currentCard.distanceToUse.x &&
                    distCell <= currentCard.distanceToUse.y
                );
                if (!isOnRangeCard) // skip if not in right distance for card.
                    return false;
                if (c.HP >= c.HPmax) // skip character full life.
                    return false;
                return true;
            }).OrderBy(c => c.HPmax-c.HP).ToList(); // order by most HP less.
            if (allier.Count == 0)
                continue;

            this.useACardFromHand(i, allier[0].indexPosCel); // play the card.
        }

        skipTurn();
    }

}