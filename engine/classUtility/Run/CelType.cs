
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
                return false;

            default:
                return true;
        }
    }
}