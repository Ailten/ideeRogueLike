
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

        s.addSpriteType(SpriteType.HudHP, new Vector(0, 0), new Vector(219, 246));
        s.addSpriteType(SpriteType.HudSP, new Vector(220, 0), new Vector(149, 110));
        s.addSpriteType(SpriteType.HudPO, new Vector(676, 127), new Vector(134, 69));

        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Disabled, new Vector(533, 0), new Vector(142, 108)); //sprites skip and all.
        s.addSpriteType(SpriteType.ButtonUiSkipTurn, new Vector(533, 109), new Vector(142, 108));
        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Hover, new Vector(533, 218), new Vector(142, 108));
        s.addSpriteType(SpriteType.ButtonUiSkipTurn_Selected, new Vector(533, 327), new Vector(142, 108));

        s.addSpriteType(SpriteType.SkipTurnBack, new Vector(676, 0), new Vector(269, 126));
        s.addSpriteType(SpriteType.DeckIcon, new Vector(306, 111), new Vector(115, 99));

        s = new Sprite("Characters");
        s.addSpriteType(SpriteType.Character_Ailten, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Slime, new Vector(128, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Flame, new Vector(256, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Rock, new Vector(384, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingSlime, new Vector(128, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingFlame, new Vector(256, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_KingRock, new Vector(384, 128), new Vector(126, 126));

        s = new Sprite("DungeonCels");
        s.addSpriteType(SpriteType.Cel, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_DoorToNextRoom, new Vector(128, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_RopeToNextStage, new Vector(256, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Invocation, new Vector(384, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_CenterRoom, new Vector(0, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Selectable, new Vector(128, 128), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_DoorToNextRoomLock, new Vector(256, 128), new Vector(126, 126));

        s = new Sprite("Card");
        s.addSpriteType(SpriteType.CardBG_Blue, new Vector(0, 0), new Vector(219, 322)); // -- encrage : (0.5f, 0.5f).
        s.addSpriteType(SpriteType.CardBG_Red, new Vector(220, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Green, new Vector(440, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_PolyChrome, new Vector(660, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Recto, new Vector(0, 702), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Craced, new Vector(220, 702), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardBG_Shinny, new Vector(440, 702), new Vector(219, 322));
        //-- placeholder last bg.

        s.addSpriteType(SpriteType.CardImg_Splash, new Vector(0, 323), new Vector(219, 125)); // -- encrage : (0.5f, 0.952f).
        s.addSpriteType(SpriteType.CardImg_WoodenSword, new Vector(220, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_WoodenShild, new Vector(440, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Flame, new Vector(220, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Rock, new Vector(440, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_Meat, new Vector(0, 449), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_AxeOLoot, new Vector(0, 575), new Vector(219, 125));

        s = new Sprite("FX");
        s.addSpriteType(SpriteType.FX_turnOn, new Vector(0, 0), new Vector(63, 63));
        s.addSpriteType(SpriteType.FX_heartHeal, new Vector(0, 64), new Vector(63, 62));
        s.addSpriteType(SpriteType.FX_starHit, new Vector(64, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.FX_shildBuf, new Vector(191, 0), new Vector(126, 126));

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
