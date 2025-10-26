
public class CharacterEye : CharacterMob
{
    public CharacterEye(Vector posIndexCel) : base(SpriteType.Character_Eye, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstFourAPCardOnOponent);
        this.logicState.Add(LogicState.firstTwoAPCardOnOponent);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 2;
        this.MP = MPmax;
        this.APmax = 2;
        this.AP = APmax;
        this.HPmax = 35;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(12, 18);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_ShaNoar,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 2),
                effects: new()
                {
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 5),
                    new KeyValuePair<EffectCard, int>(EffectCard.SelfAPBoost, 1),
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Fissure,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Cracked,
                APCost: 4,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.FissureACard, 1)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.0f));
    }
}
