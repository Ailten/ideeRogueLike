
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
            amountOfCardAdd: 6,
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


    // override take damage, to include increase AP.
    protected override void takeDamage(int atk, Character? characterMakeAtk = null, PackageRefCard? refCard = null)
    {
        this.increaseAP(1); // increase AP. TODO : maybe make it as an statusEffect.

        base.takeDamage(atk, characterMakeAtk, refCard);
    }
}