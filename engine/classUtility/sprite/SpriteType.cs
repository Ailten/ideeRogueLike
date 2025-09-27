
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
    UiCoffreWin,
    UiGodRayWin,
    ButtonUiOption_Disabled,
    ButtonUiOption,
    ButtonUiOption_Hover,
    ButtonUiOption_Selected,

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
    MiniMapUI_RoomShop,
    MiniMapUI_RoomCardEffectBoost,
    MiniMapUI_RoomDiscard,
    MiniMapUI_RoomDuplicate,
    MiniMapUI_RoomFusion,
    MiniMapUI_RoomSetCardEdition,



    // --- Characters.
    Character_Ailten,
    Character_Slime,
    Character_Flame,
    Character_Rock,
    Character_Goblin,
    Character_Armor,
    Character_GoblinDeez,
    Character_ArmorDamned,
    Character_KingSlime,
    Character_KingFlame,
    Character_KingRock,
    Character_Squelette,
    Character_Ghost,
    Character_Lish,
    Character_Spectr,
    Character_DarunyaNeko,
    Character_LuneAllier,
    Character_DarumaNico,
    Character_Axolootl,
    Character_Blacacia,
    Character_SacHead,
    Character_Pumkin,
    Character_Crow,
    Character_Arachnide,
    Character_Barbak,


    // --- Dungeons.
    Cel,
    Cel_DoorToNextRoom,
    Cel_RopeToNextStage,
    Cel_Invocation,
    Cel_CenterRoom,
    Cel_Selectable,
    Cel_DoorToNextRoomLock,
    Cel_Coffre,
    Cel_CardEffectBoost,
    Cel_Discard,
    Cel_Duplicate,
    Cel_Shop,
    Cel_SandMPDown,
    Cel_SlimeAPDown,
    Cel_Fusion,
    Cel_SetCardEdition,


    // --- Card.
    CardBG_Blue,
    CardBG_Red,
    CardBG_Green,
    CardBG_PolyChrome,
    CardBG_Recto,
    CardBG_Craced,
    CardBG_Shinny,
    CardBG_Solded,
    CardImg_Splash,
    CardImg_WoodenSword,
    CardImg_WoodenShild,
    CardImg_Flame,
    CardImg_Rock,
    CardImg_LuneAllier,
    CardImg_BlackHole,
    CardImg_Sword,
    CardImg_SanTwitch,
    CardImg_ShaNoar,
    CardImg_Barbak,
    CardImg_AxeOLoot,
    CardImg_BatteBulle,
    CardImg_BlacASiable,
    CardImg_DarunyaNeko,
    CardImg_Explsur,
    CardImg_Drama,
    CardImg_Dash,
    CardImg_AQuiLOs,
    CardImg_Faux,
    CardImg_OsuAime,
    CardImg_Flag,
    CardImg_Vore,
    CardImg_Infestation,


    // --- FX.
    FX_turnOn,
    FX_heartHeal,
    FX_starHit,
    FX_shildBuf,


    // --- StatusEffect.
    StatusEffect_turnBG,
    StatusEffect_arrowRightTimeLine,
    StatusEffect_arrowLeftStatusEffect,
    StatusEffect_Solded,
    StatusEffect_BGTimeLineCharacterBlue,
    StatusEffect_BGTimeLineCharacterRed,
    StatusEffect_BGStatusEffectMalus,
    StatusEffect_BGStatusEffectBuff,
    StatusEffect_TimeLineDelimiterTurn,
    StatusEffect_BGStatusEffect,

    StatusEffect_Burn,
    StatusEffect_MoneyLoot,
    StatusEffect_DamageAddBoostRed,
    StatusEffect_DamageAddBoostBlue,
    StatusEffect_DamageAddBoostGreen,
    StatusEffect_DamageAddBoostShiny,
    StatusEffect_APBoost,
    StatusEffect_BoostChooseSpecialRoom,
    StatusEffect_BoostPickCard,
    StatusEffect_MoneyMultiplyDamage,
    StatusEffect_APWhenHit,
    StatusEffect_HPBoost,
    StatusEffect_BoostIntoInvoke,
    StatusEffect_DamageMultiplyBoostRed,
    StatusEffect_DamageMultiplyBoostBlue,
    StatusEffect_DamageMultiplyBoostGreen,
    StatusEffect_DamageMultiplyBoostShiny,
    StatusEffect_MPBoost,
    StatusEffect_ShildAddBoostRed,
    StatusEffect_ShildAddBoostBlue,
    StatusEffect_ShildAddBoostGreen,
    StatusEffect_ShildAddBoostShiny,
    StatusEffect_BrokeCardGainShild,
    StatusEffect_YingYangShinyCracked,
    StatusEffect_DuplicateCracked,
    StatusEffect_CrackedAddDamage,
    StatusEffect_ShildMultiplyBoostRed,
    StatusEffect_ShildMultiplyBoostBlue,
    StatusEffect_ShildMultiplyBoostGreen,
    StatusEffect_ShildMultiplyBoostShiny,
    StatusEffect_BalanceEffect,
    StatusEffect_ShinyGainAP,
    StatusEffect_SideEyes,
    StatusEffect_RallMpMakeDamage,
    StatusEffect_PushWallMakeSelfHeal,
    StatusEffect_PushWallMakeRallMP,
    StatusEffect_AddIndirectDamage,
    StatusEffect_TakeHealMakeHitAround,
    StatusEffect_MultDamageByHPLeft,
    StatusEffect_TakeHealAddDamage,
    StatusEffect_PropagatePoison,
    StatusEffect_ShildMultWhenFirst,
    StatusEffect_ConvertPurcentHealInShild,
    StatusEffect_FauxEffect,

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
            case (SpriteType.CardImg_Flame):
                return ("Un jet de flame qui brule la cible.");
            case (SpriteType.CardImg_Rock):
                return ("Une attaque avec d enorme pierres");
            case (SpriteType.CardImg_Barbak):
                return ("Une belle piece de viande pleine de saveur.");
            case (SpriteType.CardImg_AxeOLoot):
                return ("Une ache poilue qui aporte la fortune.");
            case (SpriteType.CardImg_BatteBulle):
                return ("Une batte en bulle (peu agressive).");
            case (SpriteType.CardImg_BlacASiable):
                return ("Un bac a sable qui ralenti les deplacement.");
            case (SpriteType.CardImg_DarunyaNeko):
                return ("Un daruma aussi espiegle qu'un chat.");
            case (SpriteType.CardImg_LuneAllier):
                return ("Une gentille lune qui soigne ses allier.");
            case (SpriteType.CardImg_Explsur):
                return ("Une explosion.");
            case (SpriteType.CardImg_Drama):
                return ("Vous feriez mieu de ne pas jouer cette carte.");
            case (SpriteType.CardImg_BlackHole):
                return ("Attire la cible vers le lanceur.");
            case (SpriteType.CardImg_OsuAime):
                return ("Propage la tendinite.");
            case (SpriteType.CardImg_Dash):
                return ("Teleporte le lanceur.");
            case (SpriteType.CardImg_Sword):
                return ("Une epee robuste.");
            case (SpriteType.CardImg_Flag):
                return ("Besoin de suport.");
            case (SpriteType.CardImg_AQuiLOs):
                return ("A qui est cette os ?");
            case (SpriteType.CardImg_SanTwitch):
                return ("Jeff prend 60 pourcents des calories.");
            case (SpriteType.CardImg_Vore):
                return ("Glups.");
            case (SpriteType.CardImg_Infestation):
                return ("Elle s'appelle Djipsi.");
            case (SpriteType.CardImg_ShaNoar):
                return ("Nya.");
            case (SpriteType.CardImg_Faux):
                return ("La meilleur carte du jeu, Faux !");


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
            case (SpriteType.CardImg_Flame):
                return ("Flame");
            case (SpriteType.CardImg_Rock):
                return ("Rock");
            case (SpriteType.CardImg_Barbak):
                return ("Barbak");
            case (SpriteType.CardImg_AxeOLoot):
                return ("Axe aux loots");
            case (SpriteType.CardImg_BatteBulle):
                return ("Batte Bulle");
            case (SpriteType.CardImg_BlacASiable):
                return ("Blac a siable");
            case (SpriteType.CardImg_DarunyaNeko):
                return ("Darunya Neko");
            case (SpriteType.CardImg_LuneAllier):
                return ("Lune Allier");
            case (SpriteType.CardImg_Explsur):
                return ("Explsur");
            case (SpriteType.CardImg_Drama):
                return ("Drama");
            case (SpriteType.CardImg_BlackHole):
                return ("Trou Noir");
            case (SpriteType.CardImg_OsuAime):
                return ("Osu Aime");
            case (SpriteType.CardImg_Dash):
                return ("Dash");
            case (SpriteType.CardImg_Sword):
                return ("Epee");
            case (SpriteType.CardImg_Flag):
                return ("Drapeau");
            case (SpriteType.CardImg_AQuiLOs):
                return ("A qui l os");
            case (SpriteType.CardImg_SanTwitch):
                return ("San-Twitch");
            case (SpriteType.CardImg_Vore):
                return ("Vore");
            case (SpriteType.CardImg_Infestation):
                return ("Infestation");
            case (SpriteType.CardImg_ShaNoar):
                return ("Sha Noar");
            case (SpriteType.CardImg_Faux):
                return ("Faux");


            default:
                return spriteType.ToString().Substring("CardImg_".Length); //default get string from enum name.
        }
    }
}