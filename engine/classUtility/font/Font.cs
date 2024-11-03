
public class Font 
{

    public FontType fontType;
    public Raylib_cs.Font fontObj;


    public Font(FontType fontType)
    {
        this.fontType = fontType;
        this.fontObj = Raylib_cs.Raylib.LoadFont($"assets/font/{fontType}.ttf");

        FontManager.pushNewFont(this);
    }


    public void deinit()
    {
        Raylib_cs.Raylib.UnloadFont(fontObj);
    }


    public static implicit operator Raylib_cs.Font(Font a) => a.fontObj;
}