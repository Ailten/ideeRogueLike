
public class CharacterCrow : CharacterMob
{
    public CharacterCrow(Vector posIndexCel) : base(SpriteType.Character_Crow, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstFourAPCardOnOponent);
        this.logicState.Add(LogicState.firstTwoAPCardOnOponent);
        this.logicState.Add(LogicState.firstTwoAPCardOnOponent);
        this.logicState.Add(LogicState.fuit);

        //stats.
        this.MPmax = 5;
        this.MP = MPmax;
        this.APmax = 6;
        this.AP = APmax;
        this.HPmax = 50;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(10, 18);

        // effects.
        this.AddStatusEffect(new MultDamageByHPLeft(this.idEntity, -1, -1)); // mult damage when low HP.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Vore,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 4,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Faux,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.SendDrama, 4)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_ShaNoar,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.RetMP, 1)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }
}