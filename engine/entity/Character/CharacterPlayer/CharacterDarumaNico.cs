
public class Character_DarumaNico : CharacterPlayer
{
    public Character_DarumaNico(Vector posIndexCel) : base(SpriteType.Character_DarumaNico, posIndexCel)
    {
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        // special effect.
        this.statusEffects.Add(new APWhenHit(this.idEntity));

        this.deck.pickCountByTurn = 5;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_DarunyaNeko,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.InvokeDarunyaNeko, 2)
            ),
            amountOfCardAdd: 4,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_WoodenSword,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 2)
            ),
            amountOfCardAdd: 2,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_WoodenShild,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Shild, 2)
            ),
            amountOfCardAdd: 4,
            isSameColor: false
        );
    }

}