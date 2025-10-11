
public class CharacterLunAlly : CharacterPlayer
{
    public CharacterLunAlly(Vector posIndexCel) : base(SpriteType.Character_LunAlly, posIndexCel)
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
                cardIllu: SpriteType.CardImg_Barbak,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(0, 0),
                effect: new KeyValuePair<EffectCard, int>(EffectCard.Heal, 6)
            ),
            amountOfCardAdd: 2,
            isSameColor: false
        );
        this.deck.addCardToDeck(
            new Card(
                cardIllu: SpriteType.CardImg_LuneAllier,
                cardColor: CardColor.Red,
                cardEdition: CardEdition.Default,
                APCost: 2,
                distanceToUse: new(1, 2),
                effects: new() {
                    new KeyValuePair<EffectCard, int>(EffectCard.InvokeLuneAllier, 2)
                }
            ),
            amountOfCardAdd: 4,
            isSameColor: false
        );
    }


    public override void addStatusEffectWhenSpawn()
    {
        // special effect.
        if(SaveManager.getSave.succes.Contains(Succes.Heal_50))
            this.AddStatusEffect(new TakeHealAddDamage(this.idEntity, -1, -1));
    }
}