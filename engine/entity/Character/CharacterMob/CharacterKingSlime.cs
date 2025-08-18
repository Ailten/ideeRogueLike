
public class CharacterKingSlime : CharacterMob
{
    public CharacterKingSlime(Vector posIndexCel) : base(SpriteType.Character_KingSlime, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        this.logicState.Add(LogicState.firstHit);
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

        // effects.
        this.AddStatusEffect(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Blue, 0f)); // imune to blue damage.
        this.AddStatusEffect(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Blue, 1, 3)); // increase atk by 1 eatch 3 turn.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Splash,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 3),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 3)
            ),
            amountOfCardAdd: 3,
            isSameColor: true
        );
    }
}