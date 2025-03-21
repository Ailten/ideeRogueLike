
public class CharacterSlime : CharacterMob
{

    public CharacterSlime(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.chase);
        //this.logicState.Add(LogicState.bigestHit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = 1;
    }

}