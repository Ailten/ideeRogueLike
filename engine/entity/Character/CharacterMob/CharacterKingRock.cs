
public class CharacterKingRock : CharacterMob
{
    public CharacterKingRock(Vector posIndexCel) : base(SpriteType.Character_KingRock, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase_or_firstAttireInLine);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 15;
        this.HP = HPmax;

        //gold can be looted.
        this.PO = RandomManager.rng.Next(5, 9);

        // effects.
        this.statusEffects.Add(new ShildMultBoostColor(this.idEntity, -1, -1, CardColor.Green, 0f)); // imune to blue damage.
        this.statusEffects.Add(new DamageAddByTurn(this.idEntity, -1, -1, CardColor.Green, 1, 3)); // increase atk by 1 eatch 3 turn.

        //set deck.
        this.deck.pickCountByTurn = 2;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_Rock,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 8)
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_BlackHole,
                cardColor: CardColor.Green,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(2, 3),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Attire, 2),
                isInLine: true
            ),
            amountOfCardAdd: 1,
            isSameColor: true
        );
    }
}