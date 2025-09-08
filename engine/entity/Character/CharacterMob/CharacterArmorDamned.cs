
public class CharacterArmorDamned : CharacterMob
{
    public CharacterArmorDamned(Vector posIndexCel) : base(SpriteType.Character_Armor, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.selfShild);
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 25;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(3, 7);

        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Red, 1.8f)); // imune to color damage.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 0.85f)); // imune to color damage.
        this.AddStatusEffect(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Blue, 2, 2)); // increase atk by 2 eatch 2 turn.
        
        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Sword,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4)
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Flag,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(0, 2),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Shild, 2),
                isInLine: false
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }
}