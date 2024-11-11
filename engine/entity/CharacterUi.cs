
using System.Text.RegularExpressions;

public class CharacterUi : Entity
{

    public string pseudo = "";
    private static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 50f;
    private static float fontSpacing = 2f;
    private static Raylib_cs.Color colorText = Raylib_cs.Color.White;

    public bool isDrawPseudo = false;

    public CharacterUi(int idLater, SpriteType spriteType = SpriteType.Character_Ailten) : base(idLater, spriteType)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(126, 126);

        this.geometryTrigger = new Rect(
            new(-64, -64), 
            new(126, 126)
        );
        
        setPseudo();

    }


    //update pseudo str base on sprite name enum.
    private void setPseudo()
    {
        var grpMatch = new Regex("[a-zA-Z0-9]{1,}$").Match(this.spriteType.ToString());
        if(grpMatch.Success){
            pseudo = grpMatch.Groups[0].Value;
        }
    }


    public override void drawAfter(Vector posToDraw)
    {
        if(isDrawPseudo){

            float fontSizeEval = fontSize * scale.y * CanvasManager.scaleCanvas; //eval font size and spacing.
            float fontSpacingEval = fontSpacing * scale.y * CanvasManager.scaleCanvas;
    
            Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
                font,
                pseudo,
                fontSizeEval,
                fontSpacingEval
            );
    
            Vector posReplaceTextAtScreen = new Vector(0, 95); //vector to replace text from center entity.
            posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;
    
            Raylib_cs.Raylib.DrawTextEx(
                font, //font.
                pseudo, //txt.
                posToDraw + posReplaceTextAtScreen - textRectDest * encrage, //pos in canvas.
                fontSizeEval, //font size.
                fontSpacingEval, //space between two letter.
                colorText //color.
            );

        }
    }

}