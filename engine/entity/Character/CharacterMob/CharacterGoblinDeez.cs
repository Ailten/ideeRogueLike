
public class CharacterGoblinDeez : CharacterMob
{
    public CharacterGoblinDeez(Vector posIndexCel) : base(SpriteType.Character_Goblin, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstAttire);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 4;
        this.AP = APmax;
        this.HPmax = 30;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(8, 14);

        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 1.1f)); // imune to blue damage.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Green, 0.9f)); // imune to blue damage.
        
        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_WoodenSword,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 3,
                distanceToUse: new(1, 1),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4),
                    new KeyValuePair<EffectCard, int>(EffectCard.Push, 2)
                }
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Rock,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(2, 2),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Attire, 1)
                },
                isInLine: true
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }
}