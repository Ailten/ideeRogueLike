
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

        s.addSpriteType(SpriteType.HudHP, new Vector(0, 0), new Vector(219, 246));
        s.addSpriteType(SpriteType.HudSP, new Vector(220, 0), new Vector(149, 110));

        s = new Sprite("Characters");
        s.addSpriteType(SpriteType.Character_Ailten, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Character_Slime, new Vector(128, 0), new Vector(126, 126));

        s = new Sprite("DungeonCels");
        s.addSpriteType(SpriteType.Cel, new Vector(0, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_DoorToNextRoom, new Vector(128, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_RopeToNextStage, new Vector(256, 0), new Vector(126, 126));
        s.addSpriteType(SpriteType.Cel_Invocation, new Vector(384, 0), new Vector(126, 126));

        s = new Sprite("Card");
        s.addSpriteType(SpriteType.CardBG_Blue, new Vector(0, 0), new Vector(219, 322)); // -- encrage : (0.5f, 0.5f).
        s.addSpriteType(SpriteType.CardBG_Red, new Vector(220, 0), new Vector(219, 322));
        s.addSpriteType(SpriteType.CardRecto, new Vector(440, 0), new Vector(219, 322));

        s.addSpriteType(SpriteType.CardImg_Splash, new Vector(0, 323), new Vector(219, 125)); // -- encrage : (0.5f, 0.952f).
        s.addSpriteType(SpriteType.CardImg_WoodenSword, new Vector(220, 323), new Vector(219, 125));
        s.addSpriteType(SpriteType.CardImg_WoodenShild, new Vector(440, 323), new Vector(219, 125));
        
    }

    //push a new sprite in list.
    public static void pushNewSprite(Sprite newSprite)
    {
        sprites.Add(newSprite);
    }

    //find a sprite by send a type of sprite.
    public static Sprite? findBySpriteType(SpriteType spriteType)
    {
        return sprites.Find((s) => {
            return s.spriteTiles.Find((st) => {
                return st.spriteType == spriteType;
            }) != null;
        });
    }

    //deinit all sprite.
    public static void deinit()
    {
        sprites.ForEach((s) => {
            s.deinit();
        });
    }

}
