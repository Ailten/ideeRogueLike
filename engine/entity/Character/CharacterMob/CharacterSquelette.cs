
public class CharacterSquelette : CharacterMob
{
    public CharacterSquelette(Vector posIndexCel) : base(SpriteType.Character_Squelette, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.firstCardPlayableOponent);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 5;
        this.AP = APmax;
        this.HPmax = 30;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(7, 11);

        //set deck.
        this.deck.pickCountByTurn = 2;
        CardColor cardColor = StaticCardColor.getRandomColor();
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_AQuiLOs,
                cardColor: cardColor,
                cardEdition: CardEdition.Default,
                APCost: 4,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 1),
                    new KeyValuePair<EffectCard, int>(EffectCard.MPHit, 2)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_AQuiLOs,
                cardColor: cardColor,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.RetResColor, 2)
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );

    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.0f));
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 1.2f));
    }
}