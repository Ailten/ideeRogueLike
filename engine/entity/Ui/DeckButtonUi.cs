
public class DeckButtonUi : Entity
{
    private bool isMouseHover = false;
    public bool isDeckPioche = true; //disting button deck pioche, frome defausse.

    private static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 35f;
    private static float fontSpacing = 2f;
    public static Raylib_cs.Color colorText = new Raylib_cs.Color(118, 101, 61, 255);


    public DeckButtonUi(int idLayer) : base(idLayer, SpriteType.DeckIcon)
    {
        this.isUi = true;
        this.zIndex = 2010;

        this.encrage = new(0, 1);

        this.size = new(115, 99);

        this.geometryTrigger = new Rect(
            new(0, -99),
            new(115, 99)
        );
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (!isMouseHover)
            return;

        Deck deckMainPlayerCharacter = TurnManager.getMainPlayerCharacter().deck;
        string text = (this.isDeckPioche ?
            deckMainPlayerCharacter.getCardsInPioche.Count:
            deckMainPlayerCharacter.getCardsInCimetier.Count
        ).ToString();

        float fontSizeEval = fontSize * scale.y * CanvasManager.scaleCanvas; //eval font size and spacing.
        float fontSpacingEval = fontSpacing * scale.y * CanvasManager.scaleCanvas;

        Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            text,
            fontSizeEval,
            fontSpacingEval
        );

        Vector posReplaceTextAtScreen = new Vector(57, -114); //vector to replace text from center entity.
        posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

        Vector encrageText = new(0.5f, 1);

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            text, //txt.
            posToDraw + posReplaceTextAtScreen - textRectDest * encrageText, //pos in canvas.
            fontSizeEval, //font size.
            fontSpacingEval, //space between two letter.
            colorText //color.
        );
    }


    public override void eventMouseEnter()
    {
        isMouseHover = true;
    }

    public override void eventMouseExit()
    {
        isMouseHover = false;
    }

    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (!isLeftClick || isClickDown)
            return;
            
        isMouseHover = false;

        Deck deckMainPlayerCharacter = TurnManager.getMainPlayerCharacter().deck;
        List<Card> listCardToPrint = new List<Card>(this.isDeckPioche ?
            deckMainPlayerCharacter.getCardsInPioche:
            deckMainPlayerCharacter.getCardsInCimetier
        );
        RunHudLayer.layer.setListCardToMenuCardUi(listCardToPrint);
        RunHudLayer.layer.setCardSelectedToMenuCardUi(null);
        RunHudLayer.layer.activeMenuCardUi(true);
    }

}