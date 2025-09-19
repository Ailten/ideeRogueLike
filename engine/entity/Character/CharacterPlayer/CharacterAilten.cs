
public class CharacterAilten : CharacterPlayer
{
    public CharacterAilten(Vector posIndexCel) : base(SpriteType.Character_Ailten, posIndexCel)
    {
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        // special effect.
        if (SaveManager.getSave.succes.Contains(Succes.RunPlayed_5))
            this.AddStatusEffect(new BoostChooseSpecialRoom(this.idEntity, chooseBoost: 1));

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



        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_DarunyaNeko,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 1),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.InvokeDarunyaNeko, 0)
            ),
            amountOfCardAdd: 1,
            isSameColor: false
        );
    }

}