
public class Font 
{

    public FontType fontType;
    public Raylib_cs.Font fontObj;


    public Font(FontType fontType)
    {
        this.fontType = fontType;
        string pathFontFile = $"assets/font/{fontType}.ttf";

#if DEBUG
        pathFontFile = $"/home/faouzi/Documents/ideeRogueLike/assets/font/{fontType}.ttf";
#endif

        this.fontObj = Raylib_cs.Raylib.LoadFont(pathFontFile);

        FontManager.pushNewFont(this);
    }


    public void deinit()
    {
        Raylib_cs.Raylib.UnloadFont(fontObj);
    }


    public static implicit operator Raylib_cs.Font(Font a) => a.fontObj;
}