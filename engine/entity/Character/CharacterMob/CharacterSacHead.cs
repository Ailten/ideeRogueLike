
public class CharacterSacHead : CharacterMob
{
    public CharacterSacHead(Vector posIndexCel) : base(SpriteType.Character_SacHead, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 2;
        this.MP = MPmax;
        this.APmax = 5;
        this.AP = APmax;
        this.HPmax = 35;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(8, 14);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Sword,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 4,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 12),
                    new KeyValuePair<EffectCard, int>(EffectCard.TeleportSwitch, 0)
                }
            ),
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_ShaNoar,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(2, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.RetAP, 1)
            ),
            isSameColor: false
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.0f));
    }
}