
public class CharacterPumkin : CharacterMob
{
    public CharacterPumkin(Vector posIndexCel) : base(SpriteType.Character_Pumkin, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.doNextStateIf30purcent);
        this.logicState.Add(LogicState.firstInvokeOnEmpty);
        this.logicState.Add(LogicState.doNextStateIf30purcent);
        this.logicState.Add(LogicState.firstInvokeOnEmpty);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 5;
        this.AP = APmax;
        this.HPmax = 45;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(10, 18);

        //set deck.
        this.deck.pickCountByTurn = 3;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Infestation,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.InvokeArachnide, 3)
            ),
            amountOfCardAdd: 2,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Infestation,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Shinny,
                APCost: 2,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.InvokeArachnide, 3),
                    new KeyValuePair<EffectCard, int>(EffectCard.SelfHeal, 5)
                }
            ),
            amountOfCardAdd: 2,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Vore,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 4,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.SteelHP, 5)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new PropagatePoison(this.idEntity, -1, -1, 3));
    }
}