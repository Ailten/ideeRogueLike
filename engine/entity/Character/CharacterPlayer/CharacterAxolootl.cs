
public class CharacterAxolootl : CharacterPlayer
{
    public CharacterAxolootl(Vector posIndexCel) : base(SpriteType.Character_Axolootl, posIndexCel)
    {
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        // special effect.
        if(SaveManager.getSave.succes.Contains(Succes.Take_100_Coin))
            this.AddStatusEffect(new MoneyMultiplyDamage(this.idEntity, -1, -1, purcentDamageByCoint: 1));

        this.deck.pickCountByTurn = 5;
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_WoodenSword,
                cardColor: StaticCardColor.getRandomColor(),
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Hit, 2)
            ),
            amountOfCardAdd: 4,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_AxeOLoot,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effects: new() {
                    new KeyValuePair<EffectCard, int>(EffectCard.MoneyLoot, 2),
                    new KeyValuePair<EffectCard, int>(EffectCard.Hit, 4)
                }
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