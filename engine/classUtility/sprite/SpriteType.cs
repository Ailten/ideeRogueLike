
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
    MiniMapUI_RoomDiscard,
    MiniMapUI_RoomDuplicate,



    // --- Characters.
    Character_Ailten,
    Character_Slime,
    Character_Flame,
    Character_Rock,
    Character_KingSlime,
    Character_KingFlame,
    Character_KingRock,


    // --- Dungeons.
    Cel,
    Cel_DoorToNextRoom,
    Cel_RopeToNextStage,
    Cel_Invocation,
    Cel_CenterRoom,
    Cel_Selectable,
    Cel_DoorToNextRoomLock,
    Cel_Coffre,
    Cel_Fusion,
    Cel_Discard,
    Cel_Duplicate,
    Cel_Boost,


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
    CardImg_Flame,
    CardImg_Rock,
    CardImg_Meat,
    CardImg_AxeOLoot,


    // --- FX.
    FX_turnOn,
    FX_heartHeal,
    FX_starHit,
    FX_shildBuf,


    // --- StatusEffect.
    StatusEffect_turnBG,
    StatusEffect_arrowRightTimeLine,
    StatusEffect_arrowLeftStatusEffect,
    StatusEffect_BGTimeLineCharacterBlue,
    StatusEffect_BGTimeLineCharacterRed,
    StatusEffect_TimeLineDelimiterTurn,
    StatusEffect_BGStatusEffect,

    StatusEffect_Burn,
    StatusEffect_MoneyLoot

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

    public static string getCardName(this SpriteType spriteType)
    {
        switch (spriteType)
        {
            case (SpriteType.CardImg_WoodenSword):
                return ("Epee de bois");
            case (SpriteType.CardImg_WoodenShild):
                return ("Bouclier de bois");
            case (SpriteType.CardImg_Splash):
                return ("Splash");
            case (SpriteType.CardImg_Meat):
                return ("Viande batue");
            default:
                return spriteType.ToString().Substring("CardImg_".Length); //default get string from enum name.
        }
    }
}