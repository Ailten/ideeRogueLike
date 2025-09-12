
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

        // draw list effects and cards of character.
        StatusEffectDetailsUi statusEffectDetailsUi = new StatusEffectDetailsUi(this.idLayer); // details effect selected.
        statusEffectDetailsUi.pos = new(CanvasManager.centerWindow.x, CanvasManager.centerWindow.y);
        statusEffectDetailsUi.scaleEffectIllu = 2f;
        statusEffectDetailsUi.zIndex = 3200;
        statusEffectDetailsUi.isPrintDetails = true;

        // todo : card details.

        StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer); // list effects.
        statusEffetUi.setWidthSize(CanvasManager.centerWindow.x - 20);
        statusEffetUi.pos = new(10, 10);
        statusEffetUi.setListEffect(characterSelected.statusEffects);
        statusEffetUi.isWithDetail = false;
        statusEffetUi.zIndex = 3200;
        statusEffetUi.clickOnEffect = (effectClicked, isLeftClick) =>
        {
            statusEffectDetailsUi.setStatusEffect(effectClicked);
            // todo : unselect card details.
        };
        statusEffetUi.unClickOnEffect = (effectClicked, isLeftClick) =>
        {
            statusEffectDetailsUi.setStatusEffect(null);
            // todo : unselect card details.
        };

        // todo : list cards.
        


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

        LayerManager.isADetailsLayerAreOpen = false;

        base.unActive();
    }

}