
public static class FontManager
{
    public static Font getDefaultFont
    {
        get { return fonts[0] ?? throw new Exception("no fount instanciate !"); }
    }

    private static List<Font> fonts = new();

    //init all sprite.
    public static void init()
    {
        foreach (FontType fontType in Enum.GetValues(typeof(FontType)))
        {
            new Font(fontType);
        }
    }

    //add a new font in list.
    public static void pushNewFont(Font newFont)
    {
        fonts.Add(newFont);
    }

    //get a font by a fontType.
    public static Font getFontByFontType(FontType fontType)
    {
        return fonts.Find((f) => f.fontType == fontType) ?? throw new Exception("Font not found !");
    }

    //free all font alocated.
    public static void deinit()
    {
        fonts.ForEach((f) =>
        {
            f.deinit();
        });
    }
}