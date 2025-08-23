
public static class SpriteManager
{

    private static List<Sprite> sprites = new();

    //init all sprite.
    public static void init()
    {
        Sprite s = new Sprite("UI");
        s.addSpriteType(SpriteType.none, new Vector(0, 0), new Vector(0, 0)); //none sprite -> one pixel opacity zero.

        s.addSpriteType(SpriteType.ButtonUi_Disabled, new Vector(0, 247), new Vector(384, 97));
        s.addSpriteType(SpriteType.ButtonUi, new Vector(0, 345), new Vector(384, 97));
        s.addSpriteType(SpriteType.ButtonUi_Hover, new Vector(0, 443), new Vector(384, 97));
        s.addSpriteType(SpriteType.ButtonUi_Selected, new Vector(0, 541), new Vector(384, 97));

        s.addSpriteType(SpriteType.CheckBoxUi_Disabled, new Vector(385, 247), new Vector(93, 92));
        s.addSpriteType(SpriteType.CheckBoxUi, new Vector(385, 345), new Vector(93, 92));
        s.addSpriteType(SpriteType.CheckBoxUi_Hover, new Vector(385, 443), new Vector(93, 92));
        s.addSpriteType(SpriteType.CheckBoxUi_Selected, new Vector(385, 541), new Vector(93, 92));

        s.addSpriteType(SpriteType.SlideBarUi, new Vector(479, 0), new Vector(53, 720));

        s.addSpriteType(SpriteType.SlideBarButtonUi_Disabled, new Vector(220, 111), new Vector(42, 59));
        s.addSpriteType(SpriteType.SlideBarButtonUi, new Vector(263, 111), new Vector(42, 59));
        s.addSpriteType(SpriteType.SlideBarButtonUi_Hover, new Vector(220, 171), new Vector(42, 59));
        s.addSpriteType(SpriteType.SlideBarButtonUi_Selected, new Vector(263, 171), new Vector(42, 59));

        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_, new Vector(370, 0), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_UR, new Vector(386, 0), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_UD, new Vector(402, 0), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_UL, new Vector(418, 0), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_U, new Vector(370, 16), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_R, new Vector(386, 16), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_D, new Vector(402, 16), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_L, new Vector(418, 16), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_RD, new Vector(370, 32), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_RL, new Vector(386, 32), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_DL, new Vector(402, 32), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_URDL, new Vector(418, 32), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_URD, new Vector(370, 48), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_RDL, new Vector(386, 48), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_UDL, new Vector(402, 48), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUi_RoomBG_URL, new Vector(418, 48), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomCenter, new Vector(370, 64), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUI_PosPlayer, new Vector(386, 64), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomChest, new Vector(402, 64), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomBoss, new Vector(418, 64), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomBoost, new Vector(370, 80), new Vector(16, 16)); //line mini map.
        s.addSpriteType(SpriteType.MiniMapUI_RoomFusion, new Vector(386, 80), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomDiscard, new Vector(402, 80), new Vector(16, 16));
        s.addSpriteType(SpriteType.MiniMapUI_RoomDuplicate, new Vector(418, 80), new Vector(16, 16));

        s.addSpriteType(SpriteType.HudHP, new Vector(0, 0), new Vector(219, 246));
        s.addSpriteType(SpriteType.HudSP, new Vector(220, 0), new Vector(149, 110));
        s.addSpriteType(SpriteType.HudPO, new Vector(676, 127), new Vector(134, 69));

        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Disabled, new Vector(533, 0), new Vector(142, 108)); //sprites skip and all.
        s.addSpriteType(SpriteType.ButtonUiSkipTurn, new Vector(533, 109), new Vector(142, 108));
        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Hover, new Vector(533, 218), new Vector(142, 108));
        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Selected, new Vector(533, 327), new Vector(142, 108));

        s.addSpriteType(SpriteType.SkipTurnBack, new Vector(676, 0), new Vector(269, 126));
        s.addSpriteType(SpriteType.DeckIcon, new Vector(306, 111), new Vector(115, 99));

        s.addSpriteType(SpriteType.UiCoffreWin, new Vector(0, 639), new Vector(355, 274)); 
        s.addSpriteType(SpriteType.UiGodRayWin, new Vector(356, 639), new Vector(122, 274));

        s = new Sprite("Characters");
        s.addSpriteType(SpriteType.Character_Ailten, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Slime, new Vector(128, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Flame, new Vector(256, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Rock, new Vector(384, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_DarunyaNeko, new Vector(0, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingSlime, new Vector(128, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingFlame, new Vector(256, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingRock, new Vector(384, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_LuneAllier, new Vector(0, 256), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_DarumaNico, new Vector(128, 256), new Vector(126, 126));

        s = new Sprite("DungeonCels");
        s.addSpriteType(SpriteType.Cel, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_DoorToNextRoom, new Vector(128, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_RopeToNextStage, new Vector(256, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Invocation, new Vector(384, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_CenterRoom, new Vector(0, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Selectable, new Vector(128, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_DoorToNextRoomLock, new Vector(256, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Coffre, new Vector(384, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Fusion, new Vector(0, 256), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Discard, new Vector(128, 256), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Boost, new Vector(256, 256), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Duplicate, new Vector(384, 256), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_SandMPDown, new Vector(0, 384), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_SlimeAPDown, new Vector(128, 384), new Vector(126, 126));

        s = new Sprite("Card");
        s.addSpriteType(SpriteType.CardBG_Blue, new Vector(0, 0), new Vector(219, 322)); // -- encrage : (0.5f, 0.5f).
        s.addSpriteType(SpriteType.CardBG_Red, new Vector(220, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Green, new Vector(440, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_PolyChrome, new Vector(660, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Recto, new Vector(880, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Craced, new Vector(1100, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Shinny, new Vector(1320, 0), new Vector(219, 322));
        //-- placeholder last bg.

        s.addSpriteType(SpriteType.CardImg_Splash, new Vector(0, 323), new Vector(219, 125)); // -- encrage : (0.5f, 0.952f).
        s.addSpriteType(SpriteType.CardImg_WoodenSword, new Vector(220, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_WoodenShild, new Vector(440, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_DarunyaNeko, new Vector(660, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Drama, new Vector(880, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Barbak, new Vector(0, 449), new Vector(219, 125)); // (l2).
        s.addSpriteType(SpriteType.CardImg_Flame, new Vector(220, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Rock, new Vector(440, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_LuneAllier, new Vector(660, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_BlackHole, new Vector(880, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_AxeOLoot, new Vector(0, 575), new Vector(219, 125)); // (l3).
        s.addSpriteType(SpriteType.CardImg_BatteBulle, new Vector(220, 575), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_BlacASiable, new Vector(440, 575), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Explsur, new Vector(660, 575), new Vector(219, 125));

        s = new Sprite("FX");
        s.addSpriteType(SpriteType.FX_turnOn, new Vector(0, 0), new Vector(63, 63));
        s.addSpriteType(SpriteType.FX_heartHeal, new Vector(0, 64), new Vector(63, 62));
        s.addSpriteType(SpriteType.FX_starHit, new Vector(64, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.FX_shildBuf, new Vector(191, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.StatusEffect_turnBG, new Vector(318, 0), new Vector(32, 32)); //little spirte ui.
        s.addSpriteType(SpriteType.StatusEffect_arrowRightTimeLine, new Vector(318, 33), new Vector(32, 32));
        s.addSpriteType(SpriteType.StatusEffect_arrowLeftStatusEffect, new Vector(318, 66), new Vector(32, 16));
        s.addSpriteType(SpriteType.StatusEffect_BGTimeLineCharacterBlue, new Vector(351, 0), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BGTimeLineCharacterRed, new Vector(415, 0), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BGStatusEffectMalus, new Vector(351, 64), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BGStatusEffectBuff, new Vector(415, 64), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_TimeLineDelimiterTurn, new Vector(479, 0), new Vector(32, 126));
        s.addSpriteType(SpriteType.StatusEffect_BGStatusEffect, new Vector(0, 127), new Vector(63, 63)); //second line (l1).
        s.addSpriteType(SpriteType.StatusEffect_Burn, new Vector(64, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_MoneyLoot, new Vector(128, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageAddBoostRed, new Vector(192, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageAddBoostBlue, new Vector(256, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageAddBoostGreen, new Vector(320, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageAddBoostShiny, new Vector(384, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_APBoost, new Vector(448, 127), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_APWhenHit, new Vector(0, 191), new Vector(63, 63)); // (l2).
        s.addSpriteType(SpriteType.StatusEffect_HPBoost, new Vector(64, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BoostIntoInvoke, new Vector(128, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageMultiplyBoostRed, new Vector(192, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageMultiplyBoostBlue, new Vector(256, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageMultiplyBoostGreen, new Vector(320, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_DamageMultiplyBoostShiny, new Vector(384, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_APBoost, new Vector(448, 191), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BoostChooseSpecialRoom, new Vector(0, 255), new Vector(63, 63)); // (l3).
        s.addSpriteType(SpriteType.StatusEffect_BoostPickCard, new Vector(64, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_MoneyMultiplyDamage, new Vector(128, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildAddBoostRed, new Vector(192, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildAddBoostBlue, new Vector(256, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildAddBoostGreen, new Vector(320, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildAddBoostShiny, new Vector(384, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BrokeCardGainShild, new Vector(448, 255), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_YingYangShinyCracked, new Vector(0, 319), new Vector(63, 63)); // (l4).
        s.addSpriteType(SpriteType.StatusEffect_DuplicateCracked, new Vector(64, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_CrackedAddDamage, new Vector(128, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildMultiplyBoostRed, new Vector(192, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildMultiplyBoostBlue, new Vector(256, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildMultiplyBoostGreen, new Vector(320, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShildMultiplyBoostShiny, new Vector(384, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_BalanceEffect, new Vector(448, 319), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_ShinyGainAP, new Vector(0, 383), new Vector(63, 63)); // (l5).
        s.addSpriteType(SpriteType.StatusEffect_SideEyes, new Vector(64, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_RallMpMakeDamage, new Vector(128, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_PushWallMakeSelfHeal, new Vector(192, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_PushWallMakeRallMP, new Vector(256, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_AddIndirectDamage, new Vector(320, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_TakeHealMakeHitAround, new Vector(384, 383), new Vector(63, 63));
        s.addSpriteType(SpriteType.StatusEffect_, new Vector(448, 383), new Vector(63, 63));

    }

    //push a new sprite in list.
    public static void pushNewSprite(Sprite newSprite)
    {
        sprites.Add(newSprite);
    }

    //find a sprite by send a type of sprite.
    public static Sprite? findBySpriteType(SpriteType spriteType)
    {
        return sprites.Find((s) =>
        {
            return s.spriteTiles.Find((st) =>
            {
                return st.spriteType == spriteType;
            }) != null;
        });
    }

    //deinit all sprite.
    public static void deinit()
    {
        sprites.ForEach((s) =>
        {
            s.deinit();
        });
    }

}
