
public class CharacterKingFlame : CharacterMob
{
    public CharacterKingFlame(Vector posIndexCel) : base(SpriteType.Character_KingFlame, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstCardPlayableOponent);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 2;
        this.AP = APmax;
        this.HPmax = 15;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(5, 9);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Flame,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Burn, 3)
            ),
            amountOfCardAdd: 3,
            isSameColor: true
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Red, 0f));
        this.AddStatusEffect(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Red, 1, 3));
    }
}