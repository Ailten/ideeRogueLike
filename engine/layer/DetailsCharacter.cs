
public class DetailsCharacter : Layer
{
    private static DetailsCharacter _layer = new DetailsCharacter();
    public static DetailsCharacter layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        if(characterSelected is null)
            throw new Exception("characterSelected is null !");

        CardMenuBGUi bg = new CardMenuBGUi(this.idLayer); // draw back.
        bg.pos = new(0, 0);
        bg.size.x = CanvasManager.sizeWindow.x;
        bg.geometryTrigger = new Rect(new(), CanvasManager.sizeWindow);
        bg.zIndex = 3000;

        CheckBoxUi buttonExit = new CheckBoxUi(idLayer); // button exit.
        buttonExit.zIndex = 3400;
        buttonExit.scale = new(0.5f, 0.5f);
        buttonExit.pos = new(1247, 33);
        buttonExit.eventClick = () =>
        {
            DetailsCharacter.layer.unActive(); // close the layer.
        };

        CharacterUi characterUi = new CharacterUi(idLayer, this.characterSelected.spriteType); // character sprite.
        characterUi.pos = new(126+10, CanvasManager.centerWindow.y);
        characterUi.isDrawPseudo = true;
        characterUi.zIndex = 3200;

        // TODO: draw list effetcs of character.

        // TODO: draw cards of character.
        

        base.active();
    }


    public Character? characterSelected;


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        this.characterSelected = null;

        base.unActive();
    }

}