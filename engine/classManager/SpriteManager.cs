
public static class SpriteManager 
{

    private static List<Sprite> sprites = new();

    //init all sprite.
    public static void init()
    {
        Sprite s = new Sprite("MainMenu");
        s.addSpriteType(SpriteType.none, new Vector(0, 0), new Vector(0, 0)); //none sprite -> one pixel opacity zero.
        s.addSpriteType(SpriteType.ButtonUi, new Vector(16, 400), new Vector(384, 97));
        s.addSpriteType(SpriteType.ButtonUi_Hover, new Vector(16, 505), new Vector(384, 97));
        s.addSpriteType(SpriteType.ButtonUi_Selected, new Vector(16 ,610), new Vector(384, 97));
        s.addSpriteType(SpriteType.CheckBoxUi, new Vector(429, 400), new Vector(93, 92));
        s.addSpriteType(SpriteType.CheckBoxUi_Hover, new Vector(429, 503), new Vector(93, 92));
        s.addSpriteType(SpriteType.CheckBoxUi_Selected, new Vector(429, 606), new Vector(93, 92));
        s.addSpriteType(SpriteType.SlideBarUi, new Vector(540, 0), new Vector(53, 720));
        s.addSpriteType(SpriteType.SlideBarButtonUi, new Vector(486, 13), new Vector(42, 59));
        s.addSpriteType(SpriteType.SlideBarButtonUi_Hover, new Vector(486, 80), new Vector(42, 59));
        s.addSpriteType(SpriteType.SlideBarButtonUi_Selected, new Vector(486, 147), new Vector(42, 59));
        s.addSpriteType(SpriteType.SeparatorUi, new Vector(21, 333), new Vector(496, 333));
        s.addSpriteType(SpriteType.ColorBlueDark, new Vector(37, 270), new Vector(1,1));
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