
public class StatsCharacterUi : Entity
{

    private static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static Vector encrageText = new(0.5f, 0.5f);
    private static Raylib_cs.Color colorTextHP = new Raylib_cs.Color(255, 205, 205, 255);
    private static Raylib_cs.Color colorTextAP = new Raylib_cs.Color(155, 155, 80, 255);
    private static Raylib_cs.Color colorTextMP = new Raylib_cs.Color(55, 125, 55, 255);
    private static Raylib_cs.Color colorTextSP = new Raylib_cs.Color(55, 55, 125, 255);
    

    public StatsCharacterUi(int idLayer) : base(idLayer, SpriteType.HudHP)
    {
        this.isUi = true;
        this.zIndex = 1800;

        this.encrage = new(0, 0);

        this.size = new(219, 246);
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        Character character = TurnManager.getCharacterOfCurrentTurn();
        if(!character.isInRedTeam){ //print stats of main character durring turn of ennemies.
            character = TurnManager.getMainPlayerCharacter();
        }

        const float fontSpacing = 2f;
        float fontSpacingEval = fontSpacing * scale.y * CanvasManager.scaleCanvas; //eval font spacing.

        // --- print text HP.
        string text = $"{character.HP}/{character.HPmax}";

        const float fontSizeHP = 65f;
        float fontSizeEvalHp = fontSizeHP * scale.y * CanvasManager.scaleCanvas; //eval font size.

        Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            text,
            fontSizeEvalHp,
            fontSpacingEval
        );

        Vector posReplaceTextAtScreen = new Vector(150, 50); //vector to replace text from center entity.
        posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            text, //txt.
            posToDraw + posReplaceTextAtScreen - textRectDest * encrageText, //pos in canvas.
            fontSizeEvalHp, //font size.
            fontSpacingEval, //space between two letter.
            colorTextHP //color.
        );

        // --- print text AP.
        text = $"{character.AP}/{character.APmax}";

        const float fontSizeAP = 65f;
        float fontSizeEvalAp = fontSizeAP * scale.y * CanvasManager.scaleCanvas; //eval font size.

        textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            text,
            fontSizeEvalAp,
            fontSpacingEval
        );

        posReplaceTextAtScreen = new Vector(120, 140); //vector to replace text from center entity.
        posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            text, //txt.
            posToDraw + posReplaceTextAtScreen - textRectDest * encrageText, //pos in canvas.
            fontSizeEvalAp, //font size.
            fontSpacingEval, //space between two letter.
            colorTextAP //color.
        );

        // --- print text MP.
        text = $"{character.MP}/{character.MPmax}";

        const float fontSizeMP = 60f;
        float fontSizeEvalMp = fontSizeMP * scale.y * CanvasManager.scaleCanvas; //eval font size.

        textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            text,
            fontSizeEvalMp,
            fontSpacingEval
        );

        posReplaceTextAtScreen = new Vector(110, 205); //vector to replace text from center entity.
        posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            text, //txt.
            posToDraw + posReplaceTextAtScreen - textRectDest * encrageText, //pos in canvas.
            fontSizeEvalMp, //font size.
            fontSpacingEval, //space between two letter.
            colorTextMP //color.
        );


        // --- shild.
        if(character.SP > 0){

            //pos at screen.
            Vector posAtScreen = new(size.x, 0);
            posAtScreen *= CanvasManager.scaleCanvas;
            posAtScreen += CanvasManager.posDecalCanvas;

            //eval size at screen.
            Vector sizeAtScreen = new Vector(149, 110) * scale * CanvasManager.scaleCanvas;

            //default rect dest.
            Raylib_cs.Rectangle rectDestSP = new Raylib_cs.Rectangle(
                posAtScreen.x, posAtScreen.y,
                sizeAtScreen.x, sizeAtScreen.y
            );

            //default get rect source.
            Raylib_cs.Rectangle rectSourceInTexture = sprite.getSpriteTileBySpriteType(SpriteType.HudSP).getRectSource();

            Raylib_cs.Raylib.DrawTexturePro(
                sprite.texture, //texture.
                rectSourceInTexture, //rect source from texture.
                rectDestSP, //rect desintation at screen.
                origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                rotate, //rotation.
                Raylib_cs.Color.White //color (already white).
            );

            // --- print text SP.
            text = $"{character.MP}/{character.MPmax}";

            const float fontSizeSP = 70f;
            float fontSizeEvalSp = fontSizeSP * scale.y * CanvasManager.scaleCanvas; //eval font size.

            textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
                font,
                text,
                fontSizeEvalMp,
                fontSpacingEval
            );

            posReplaceTextAtScreen = new Vector(300, 50); //vector to replace text from center entity.
            posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

            Raylib_cs.Raylib.DrawTextEx(
                font, //font.
                text, //txt.
                posToDraw + posReplaceTextAtScreen - textRectDest * encrageText, //pos in canvas.
                fontSizeEvalMp, //font size.
                fontSpacingEval, //space between two letter.
                colorTextSP //color.
            );

            //TODO : debug color SP and pos text SP (never try to add shild point of a character so text can be at wrong place).

        }


    }

}