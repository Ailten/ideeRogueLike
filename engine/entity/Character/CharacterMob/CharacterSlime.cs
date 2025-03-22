
public class CharacterSlime : CharacterMob
{

    public CharacterSlime(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        //this.logicState.Add(LogicState.bigestHit); //TODO ! script the IA execution of mob hit. (and after, the draw card for player).
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 1;
        this.AP = APmax;
        this.HPmax = 3;
        this.HP = HPmax;

        this.deck.pickCountByTurn = 1;
        this.deck.addCardToDeck(new Card(
            SpriteType.CardImg_Splash, 
            SpriteType.CardBG_Blue,
            1,
            new(1, 1),
            new KeyValuePair<EffectCard, int>(EffectCard.hit, 1)
        ), 3);
    }

}