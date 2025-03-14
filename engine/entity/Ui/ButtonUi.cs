
public class ButtonUi : Entity
{

    public string text = "";
    private static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 70f;
    private static float fontSpacing = 2f;
    public Raylib_cs.Color colorText = Raylib_cs.Color.Black;

    protected Dictionary<SpriteType, SpriteType> castSpriteType = new();
    private bool _isDisabled = false;
    public bool isDisabled
    {
        get { return _isDisabled; }
        set { _isDisabled = value; }
    }


    public ButtonUi(int idLayer) : base(idLayer, SpriteType.ButtonUi)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(384, 97);

        this.geometryTrigger = new Rect(
            new(-185, -40), 
            new(370, 80)
        );

        this.castSpriteType.Add(SpriteType.ButtonUi, SpriteType.ButtonUi); //set all cast SpriteType for child.
        this.castSpriteType.Add(SpriteType.ButtonUi_Hover, SpriteType.ButtonUi_Hover);
        this.castSpriteType.Add(SpriteType.ButtonUi_Selected, SpriteType.ButtonUi_Selected);
        this.castSpriteType.Add(SpriteType.ButtonUi_Disabled, SpriteType.ButtonUi_Disabled);
    }

    //empty constructor for child (skip constructor ButtonUi for class child).
    protected ButtonUi(int idLayer, SpriteType spriteType) : base(idLayer, spriteType){}


    public Action eventClick = () => {};


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        float fontSizeEval = fontSize * scale.y * CanvasManager.scaleCanvas; //eval font size and spacing.
        float fontSpacingEval = fontSpacing * scale.y * CanvasManager.scaleCanvas;

        Vector textRectDest = Raylib_cs.Raylib.MeasureTextEx( //get size of rect texture text at screen.
            font,
            text,
            fontSizeEval,
            fontSpacingEval
        );

        Vector posReplaceTextAtScreen = new Vector(0, - 6); //vector to replace text from center entity.
        if(getBaseType() == SpriteType.ButtonUi_Selected)
            posReplaceTextAtScreen += new Vector(-6, 10);
        posReplaceTextAtScreen *= this.scale * CanvasManager.scaleCanvas;

        Raylib_cs.Raylib.DrawTextEx(
            font, //font.
            text, //txt.
            posToDraw + posReplaceTextAtScreen - textRectDest * encrage, //pos in canvas.
            fontSizeEval, //font size.
            fontSpacingEval, //space between two letter.
            colorText //color.
        );
    }

    public override void eventMouseEnter()
    {
        if(getBaseType() == SpriteType.ButtonUi_Disabled)
            return;

        spriteType = castSpriteType[SpriteType.ButtonUi_Hover]; //change sprite.
    }

    public override void eventMouseExit()
    {
        if(getBaseType() == SpriteType.ButtonUi_Disabled)
            return;

        spriteType = castSpriteType[SpriteType.ButtonUi]; //change sprite.
    }

    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if(getBaseType() == SpriteType.ButtonUi_Disabled)
            return;

        if(!isLeftClick) //skip if right click.
            return;

        if(isClickDown){

            spriteType = castSpriteType[SpriteType.ButtonUi_Selected]; //change sprite.

        }else{

            spriteType = castSpriteType[SpriteType.ButtonUi_Hover]; //change sprite.

            eventClick(); //execute action of button.

        }
    }


    //get the button type (reverce get from Dictionary).
    protected SpriteType getBaseType()
    {
        return castSpriteType.FirstOrDefault((keyValue) => keyValue.Value == spriteType).Key;
    }


    //switch state isDisabled.
    public void switchIsDisabled()
    {
        _isDisabled = !isDisabled;
        updateStateIsDisabled();
    }

    //set manualy the bool isDisabled.
    public void setIsDisabled(bool isDisabled)
    {
        _isDisabled = isDisabled;
        updateStateIsDisabled();
    }

    //update the state of button, depend of isDisabled.
    private void updateStateIsDisabled()
    {
        if(isDisabled){
            spriteType = castSpriteType[SpriteType.ButtonUi_Disabled];
        }else{
            spriteType = castSpriteType[SpriteType.ButtonUi];
        }
    }

}