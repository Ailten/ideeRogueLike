
public class CharacterSpectr : CharacterMob
{
    public CharacterSpectr(Vector posIndexCel) : base(SpriteType.Character_Spectr, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.firstRetMP);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.chase_ifCardInHand);
        this.logicState.Add(LogicState.firstRetMP);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 6;
        this.AP = APmax;
        this.HPmax = 30;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(10, 15);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Splash,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 3,
                distanceToUse: new(1, 3),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 3),
                    new KeyValuePair<EffectCard, int>(EffectCard.SelfHeal, 3)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Drama,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 3),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.RetMP, 1),
                    new KeyValuePair<EffectCard, int>(EffectCard.RetAP, 1)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );

    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.5f));
        this.AddStatusEffect(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Blue, 1, 6));
    }
}