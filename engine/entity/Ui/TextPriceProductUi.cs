
public class TextPriceProductUi : Entity
{
    private string price;
    public TextPriceProductUi(int idLayer, string price) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3250;

        this.size = new(0, 0);

        this.price = price;
    }


    private static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    public float fontSize = 50f;
    private static float fontSpacing = 2f;
    public Raylib_cs.Color colorText = Raylib_cs.Color.Gold;

    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float fontSizeScaled = fontSize * scale.y * CanvasManager.scaleCanvas;
        float fontSpacingScaled = fontSpacing * scale.y * CanvasManager.scaleCanvas;

        Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            this.price,
            fontSizeScaled,
            fontSpacingScaled
        );

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            this.price, //txt.
            posToDraw - textRectDest * encrage, //pos in canvas.
            fontSizeScaled, //font size.
            fontSpacingScaled, //space between two letter.
            colorText //color.
        );
    }
}