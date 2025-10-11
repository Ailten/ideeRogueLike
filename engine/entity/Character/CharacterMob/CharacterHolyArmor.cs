
public class CharacterHolyArmor : CharacterMob
{
    public CharacterHolyArmor(Vector posIndexCel) : base(SpriteType.Character_HolyArmor, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.shildAlly);
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.shildAlly);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 2;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 38;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(13, 18);

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Sword,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Shinny,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 8)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Flag,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Shinny,
                APCost: 2,
                distanceToUse: new(1, 2),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Shild, 4),
                    new KeyValuePair<EffectCard, int>(EffectCard.SelfHeal, 2)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 0.2f));
    }
}
