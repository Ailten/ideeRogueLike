
public class CharacterArmor : CharacterMob
{
    public CharacterArmor(Vector posIndexCel) : base(SpriteType.Character_Armor, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.shildAlly);
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.shildAlly);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 12;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(3, 7);
        
        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Sword,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 5)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Flag,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Shild, 1),
                isInLine: false
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Red, 1.3f));
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 0.9f));
    }
}