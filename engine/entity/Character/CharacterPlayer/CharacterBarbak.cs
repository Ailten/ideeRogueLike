
public class CharacterBarbak : CharacterPlayer
{
    public CharacterBarbak(Vector posIndexCel) : base(SpriteType.Character_Barbak, posIndexCel)
    {
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        // special effect.
        if(SaveManager.getSave.succes.Contains(Succes.Take_60_Damage))
            this.AddStatusEffect(new MultDamageByHPLeft(this.idEntity, -1, -1));

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
                cardIllu: SpriteType.CardImg_Barbak,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Heal, 6)
            ),
            amountOfCardAdd: 4,
            isSameColor: false
        );
    }
}