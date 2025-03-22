
public class CharacterAilten : CharacterPlayer
{

    public CharacterAilten(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.MPmax = 3;
        this.MP = MPmax;
        this.APmax = 3;
        this.AP = APmax;
        this.HPmax = 10;
        this.HP = HPmax;

        this.deck.pickCountByTurn = 3;
        this.deck.addCardToDeck(new Card(
            SpriteType.CardImg_WoodenSword, 
            SpriteType.CardBG_Blue,
            1,
            new(1, 1),
            new KeyValuePair<EffectCard, int>(EffectCard.hit, 2)
        ), 6);
        this.deck.addCardToDeck(new Card(
            SpriteType.CardImg_WoodenShild, 
            SpriteType.CardBG_Blue,
            1,
            new(0, 0),
            new KeyValuePair<EffectCard, int>(EffectCard.shild, 2)
        ), 6);
    }

}