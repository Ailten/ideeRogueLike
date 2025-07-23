
public class CharacterRock : CharacterMob
{
    public CharacterRock(Vector posIndexCel) : base(SpriteType.Character_Rock, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 0;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 3;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(0, 4);

        //set deck.
        this.deck.pickCountByTurn = 1;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Rock,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 3,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 6)
            ),
            amountOfCardAdd: 3,
            isSameColor: true
        );
    }

}