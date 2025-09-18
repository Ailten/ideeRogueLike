
public class CharacterLish : CharacterMob
{
    public CharacterLish(Vector posIndexCel) : base(SpriteType.Character_Lish, posIndexCel)
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
        this.MPmax = 2;
        this.MP = MPmax;
        this.APmax = 6;
        this.AP = APmax;
        this.HPmax = 35;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(10, 15);

        // effects.
        this.AddStatusEffect(new ShildMultBoostShiny(this.idEntity, -1, -1, 2.0f)); // res negative on shiny card.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 1.2f)); // res negative on blue card.
        this.AddStatusEffect(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Red, 1, 6)); // increase atk by 1 eatch 6 turn.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Explsur,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 3,
                distanceToUse: new(1, 3),
                effects: new(){
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 6),
                    new KeyValuePair<EffectCard, int>(EffectCard.HitAround, 4)
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
}