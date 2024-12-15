
public class CharacterMob : Character
{
    
    public CharacterMob(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.isInRedTeam = false;
    }


    //instantiate a CharacterMob.
    public static CharacterPlayer init(SpriteType spriteType, Vector posIndexCel)
    {
        switch(spriteType){

            case(SpriteType.Character_Ailten):
                return new CharacterAilten(spriteType, posIndexCel);

            //TODO : add other CharacterPlayer. (maybe do a Dictionary<SpriteType, Type>)
                
            default:
                throw new Exception("SpriteType has no CharacterPlayer match !");

        }
    }


    public override void turn()
    {
        //TODO.
    }

}