
public class CharacterMob : Character
{
    private static List<MobType> allMobTypes = new(){ //list of all mob type and their dificulty.
        new MobType(SpriteType.Character_Slime, 1, false),
        new MobType(SpriteType.Character_Slime, 1, true), // todo : make a proper boss.

        new MobType(SpriteType.Character_Slime, 2, false), // todo : make other mob for up dificulty.
        new MobType(SpriteType.Character_Slime, 2, true),
        new MobType(SpriteType.Character_Slime, 3, false),
        new MobType(SpriteType.Character_Slime, 3, true),
        new MobType(SpriteType.Character_Slime, 4, false),
        new MobType(SpriteType.Character_Slime, 4, true),
        new MobType(SpriteType.Character_Slime, 5, false),
        new MobType(SpriteType.Character_Slime, 5, true),
        new MobType(SpriteType.Character_Slime, 6, false),
        new MobType(SpriteType.Character_Slime, 6, true),
        new MobType(SpriteType.Character_Slime, 7, false),
        new MobType(SpriteType.Character_Slime, 7, true),
        new MobType(SpriteType.Character_Slime, 8, false),
        new MobType(SpriteType.Character_Slime, 8, true),
        new MobType(SpriteType.Character_Slime, 9, false),
        new MobType(SpriteType.Character_Slime, 9, true),
        new MobType(SpriteType.Character_Slime, 10, false),
        new MobType(SpriteType.Character_Slime, 10, true),
    };
    
    public CharacterMob(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.isInRedTeam = false;
    }


    //instantiate a CharacterMob.
    public static CharacterMob init(SpriteType spriteType, Vector posIndexCel)
    {
        switch(spriteType){

            case(SpriteType.Character_Slime):
                return new CharacterSlime(spriteType, posIndexCel);

            //TODO : add other CharacterMob. (maybe do a Dictionary<SpriteType, Type>)
                
            default:
                throw new Exception("SpriteType has no CharacterMob match !");

        }
    }


    //pick a random type mob.
    public static SpriteType generateRandomMobType(int dificulty, bool isBoss=false)
    {
        //filter list type mob (for math to condition param).
        List<MobType> mobTypeFiltered = allMobTypes.Where((mt) => {
            return mt.dificulty == dificulty && mt.isBoss == isBoss; 
        }).ToList();

        //error if none found after filter.
        if(mobTypeFiltered.Count == 0)
            throw new Exception("generateRandomMobType return a empty list filtered !");

        //return a random element in list filtered (only the spriteType).
        return mobTypeFiltered[RunManager.rngSeed.Next(mobTypeFiltered.Count)].spriteType;
    }


    public override void turn()
    {
        //TODO.
    }

}