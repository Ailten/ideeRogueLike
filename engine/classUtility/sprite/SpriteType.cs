
public enum SpriteType
{

    // --- exeption for no sprite.
    none,


    // --- UI.
    ButtonUi,
    ButtonUi_Hover,
    ButtonUi_Selected,
    ButtonUi_Disabled,
    CheckBoxUi,
    CheckBoxUi_Hover,
    CheckBoxUi_Selected,
    CheckBoxUi_Disabled,
    SlideBarUi,
    SlideBarButtonUi,
    SlideBarButtonUi_Hover,
    SlideBarButtonUi_Selected,
    SlideBarButtonUi_Disabled,
    HudHP,
    HudSP,
    HudPO,
    ButtonUiSkipTurn,
    ButtonUiSkipTurn_Hover,
    ButtonUiSkipTurn_Selected,
    ButtonUiSkipTurn_Disabled,
    SkipTurnBack,
    DeckIcon,

    MiniMapUi_RoomBG_,
    MiniMapUi_RoomBG_UR,
    MiniMapUi_RoomBG_UD,
    MiniMapUi_RoomBG_UL,
    MiniMapUi_RoomBG_U,
    MiniMapUi_RoomBG_R,
    MiniMapUi_RoomBG_D,
    MiniMapUi_RoomBG_L,
    MiniMapUi_RoomBG_RD,
    MiniMapUi_RoomBG_RL,
    MiniMapUi_RoomBG_DL,
    MiniMapUi_RoomBG_URDL,
    MiniMapUi_RoomBG_URD,
    MiniMapUi_RoomBG_RDL,
    MiniMapUi_RoomBG_UDL,
    MiniMapUi_RoomBG_URL,
    MiniMapUI_RoomCenter,
    MiniMapUI_PosPlayer,
    MiniMapUI_RoomChest,
    MiniMapUI_RoomBoss,
    MiniMapUI_RoomBoost,
    MiniMapUI_RoomFusion,


    // --- Characters.
    Character_Ailten,
    Character_Slime,


    // --- Dungeons.
    Cel,
    Cel_DoorToNextRoom,
    Cel_RopeToNextStage,
    Cel_Invocation,
    Cel_CenterRoom,
    Cel_Selectable,
    Cel_DoorToNextRoomLock,


    // --- Card.
    CardBG_Blue,
    CardBG_Red,
    CardBG_Green,
    CardBG_PolyChrome,
    CardBG_Recto,
    CardBG_Craced,
    CardBG_Shinny,
    CardImg_Splash,
    CardImg_WoodenSword,
    CardImg_WoodenShild,
    //-- placehoder last img.
    CardImg_Meat,


    // --- FX.
    FX_turnOn,
    FX_starHit,
    FX_shildBuf

}

public static class StaticSpriteType
{
    public static string getDescription(this SpriteType spriteType)
    {
        switch (spriteType)
        {
            case (SpriteType.CardImg_WoodenSword):
                return ("Une simple epee de bois.");
            case (SpriteType.CardImg_WoodenShild):
                return ("Un simple bouclier de bois.");
            case (SpriteType.CardImg_Splash):
                return ("Un jet d'eau visqueu qui inflige des degats.");
            case (SpriteType.CardImg_Meat):
                return ("Une belle piece de viande juteuse.");
            default:
                return "";
                //throw new Exception("SpriteType getDescription has no description for this spriteType !");
        }
    }
}