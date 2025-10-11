
public class CharacterChlow : CharacterPlayer
{
    public CharacterChlow(Vector posIndexCel) : base(SpriteType.Character_Chlow, posIndexCel)
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
                cardIllu: SpriteType.CardImg_OsuAime,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 1,
                distanceToUse: new(1, 3),
                effects: new() {
                    new KeyValuePair<EffectCard, int>(EffectCard.PropagatePoison, 3)
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


    public override void addStatusEffectWhenSpawn()
    {
        // special effect.
        if (SaveManager.getSave.succes.Contains(Succes.UseACard_5_OsuAime))
            this.AddStatusEffect(new AddIndirectDamage(this.idEntity, -1, -1, 4));
    }
    
}