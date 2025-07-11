
public class CharacterSlime : CharacterMob
{

    public CharacterSlime(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 1;
        this.AP = APmax;
        this.HPmax = 3;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(0, 4);

        //set deck.
        this.deck.pickCountByTurn = 1;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Splash,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 1)
            ),
            amountOfCardAdd: 3,
            isSameColor: false
        );
    }

}