
public class CharacterPlayer : Character
{

    public CharacterPlayer(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.isInRedTeam = true;
    }

    //instantiate a CharacterPlayer.
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

}