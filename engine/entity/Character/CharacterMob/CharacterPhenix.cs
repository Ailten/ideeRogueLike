
public class CharacterPhenix : CharacterMob
{
    public CharacterPhenix(Vector posIndexCel) : base(SpriteType.Character_Phenix, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.doNextStateIfHpIsUnderTenPurcent);
        this.logicState.Add(LogicState.firstSelfCard);
        this.logicState.Add(LogicState.firstTwoAPCardOnOponent);
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 4;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 45;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(16, 22);

        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Red, 0.9f)); // res negative on shiny card.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Egg,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Shinny,
                APCost: 3,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Eggify, 2)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Flame,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 6)
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
        
    }
}
