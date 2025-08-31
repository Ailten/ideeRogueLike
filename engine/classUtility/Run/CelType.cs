
public enum CelType
{
    //without any effect.
    Cel,


    //door.
    CelDoor_up,
    CelDoor_right,
    CelDoor_down,
    CelDoor_left,


    Cel_NextStage,

    Cel_CenterRoom,


    Cel_MobSpawner,
    Cel_Coffre,
    Cel_Shop,
    Cel_Discard,
    Cel_Duplicate,


    // celtype who's add by character.
    Cel_SandMPDown,
    Cel_SandMPDown_2,
    Cel_SandMPDown_3,
    Cel_SlimeAPDown,
    Cel_SlimeAPDown_2,
    Cel_SlimeAPDown_3


}


public static class StaticCelType
{
    public static bool isAddByCharacter(this CelType celType)
    {
        switch (celType)
        {
            case (CelType.Cel):
            case (CelType.CelDoor_up):
            case (CelType.CelDoor_right):
            case (CelType.CelDoor_down):
            case (CelType.CelDoor_left):
            case (CelType.Cel_NextStage):
            case (CelType.Cel_CenterRoom):
            case (CelType.Cel_MobSpawner):
            case (CelType.Cel_Coffre):
            case (CelType.Cel_Shop):
            case (CelType.Cel_Discard):
            case (CelType.Cel_Duplicate):
                return false;

            default:
                return true;
        }
    }

    // cast a celType into a spriteType.
    public static SpriteType celTypeToSpriteType(this CelType celType)
    {
        switch (celType)
        {
            case (CelType.CelDoor_up):
            case (CelType.CelDoor_right):
            case (CelType.CelDoor_down):
            case (CelType.CelDoor_left):
                if (TurnManager.isInFight)
                    return SpriteType.Cel_DoorToNextRoomLock; //mark as door lock when in fight.
                return SpriteType.Cel_DoorToNextRoom;

            case (CelType.Cel_NextStage):
                return SpriteType.Cel_RopeToNextStage;
            case (CelType.Cel_MobSpawner):
                return SpriteType.Cel_Invocation;
            case (CelType.Cel_CenterRoom):
                return SpriteType.Cel_CenterRoom;
            case (CelType.Cel_Coffre):
                return SpriteType.Cel_Coffre;
            case (CelType.Cel_Shop):
                return SpriteType.Cel_Shop;
            case (CelType.Cel_Discard):
                return SpriteType.Cel_Discard;
            case (CelType.Cel_Duplicate):
                return SpriteType.Cel_Duplicate;

            case (CelType.Cel_SandMPDown):
            case (CelType.Cel_SandMPDown_2):
            case (CelType.Cel_SandMPDown_3):
                return SpriteType.Cel_SandMPDown;
            case (CelType.Cel_SlimeAPDown):
            case (CelType.Cel_SlimeAPDown_2):
            case (CelType.Cel_SlimeAPDown_3):
                return SpriteType.Cel_SlimeAPDown;

            default:
                throw new Exception("SpriteType no match for CelType !");
        }
    }

    //get rotate for a celType.
    public static float getRotateOfCelType(this CelType celType)
    {
        switch (celType)
        {
            //rotate door.
            case (CelType.CelDoor_up):
                return 0;
            case (CelType.CelDoor_right):
                return 90;
            case (CelType.CelDoor_down):
                return 180;
            case (CelType.CelDoor_left):
                return 270;

            default:
                return 0;
        }
    }
    
    
    public static bool isACelStopWalk(this CelType celType)
    {
        switch (celType)
        {
            case (CelType.CelDoor_up):
            case (CelType.CelDoor_right):
            case (CelType.CelDoor_down):
            case (CelType.CelDoor_left):
            case (CelType.Cel_NextStage):
            case (CelType.Cel_Coffre):
            case (CelType.Cel_Shop):
            case (CelType.Cel_Discard):
            case (CelType.Cel_Duplicate):
                return true;

            default:
                return false;
        }
    }
}