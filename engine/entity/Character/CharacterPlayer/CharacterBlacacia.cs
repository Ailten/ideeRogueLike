
public class CharacterBlacacia : CharacterPlayer
{
    public CharacterBlacacia(Vector posIndexCel) : base(SpriteType.Character_Blacacia, posIndexCel)
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
                cardIllu: SpriteType.CardImg_BlacASiable,
                cardColor: CardColor.Blue,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 3),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.TrapMp, 1)
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


    public override void addStatusEffectWhenSpawn()
    {
        // special effect.
        if(SaveManager.getSave.succes.Contains(Succes.UseACard_5_BlacASiable))
            this.AddStatusEffect(new RallMpMakeDamage(this.idEntity, -1, -1, damageByMPDecrease: 2));
    }
}