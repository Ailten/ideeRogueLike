
public class CharacterGhost : CharacterMob
{
    public CharacterGhost(Vector posIndexCel) : base(SpriteType.Character_Ghost, posIndexCel)
    {
        //IA logic.
        this.logicState.Add(LogicState.firstRetMP);
        this.logicState.Add(LogicState.firstHit);
        this.logicState.Add(LogicState.fuit);
        this.logicState.Add(LogicState.skipTurn);

        //stats.
        this.MPmax = 1;
        this.MP = MPmax;
        this.APmax = 5;
        this.AP = APmax;
        this.HPmax = 25;
        this.HP = HPmax;
        

    }
}