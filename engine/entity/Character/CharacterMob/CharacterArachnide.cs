
public class CharacterArachnide : CharacterMob
{
    public CharacterArachnide(Vector posIndexCel) : base(SpriteType.Character_Arachnide, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstOneAPCardOnOponent);
        this.logicState.Add(LogicState.firstCardPlayableOnEmpty);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 2;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 25;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(8, 14);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Infestation,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.InvokeArachnide, 3)
            ),
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Vore,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.SteelHP, 3),
                    new KeyValuePair<EffectCard, int>(EffectCard.TeleportSwitch, 1)
                }
            ),
            isSameColor: false
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 1.5f));
    }
}