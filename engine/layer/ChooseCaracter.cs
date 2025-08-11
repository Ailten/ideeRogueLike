
public class ChooseCaracter : Layer
{

    private static ChooseCaracter _layer = new ChooseCaracter(){ layerName="ChooseCaracter" };
    public static ChooseCaracter layer 
    {
        get { return _layer; }
    }


    public int characterPlayerChooseIndex = 0;
    public List<SpriteType> characterPlayerCanBeChoose = new();
    public SpriteType characterPlayerChoose
    {
        get { return characterPlayerCanBeChoose[characterPlayerChooseIndex]; }
    }

    public LittleButtonUi? _littleButtonLeft;
    public LittleButtonUi littleButtonLeft
    {
        get { return _littleButtonLeft ?? throw new Exception("littleButtonLeft is null !"); }
    }
    public LittleButtonUi? _littleButtonRight;
    public LittleButtonUi littleButtonRight
    {
        get { return _littleButtonRight ?? throw new Exception("littleButtonRight is null !"); }
    }


    public override void active()
    {
        //init all entities of layer. --->

        ButtonUi buttonBackMainMenu = new ButtonUi(idLayer);
        buttonBackMainMenu.text = "back";

        buttonBackMainMenu.pos = new(0, CanvasManager.sizeWindow.y); //pos bottom left of window.
        buttonBackMainMenu.pos += (buttonBackMainMenu.size / 2) * new Vector(1, -1); //replace a corner (fake gizmo).
        buttonBackMainMenu.pos += new Vector(10, -10); //border 10 from window.

        buttonBackMainMenu.eventClick = () => {
            LayerManager.transition(
                layer.idLayer, //id layer start.
                MainMenu.layer.idLayer //id layer end.
            );
        };

        ButtonUi buttonNext = new ButtonUi(idLayer);
        buttonNext.text = "next";

        buttonNext.pos = CanvasManager.sizeWindow; //pos bottom right of window.
        buttonNext.pos += (buttonNext.size / 2) * -1; //replace a corner (fake gizmo).
        buttonNext.pos += -10; //border 10 from window.

        buttonNext.eventClick = () => {
            LayerManager.transition(
                new int[]{layer.idLayer}, //id layer start.
                new int[]{ //id layer end.
                    RunLayer.layer.idLayer, //main game layer.
                    RunHudLayer.layer.idLayer //hud for stats in fight.
                },
                () =>
                { //action to do during black screen transition.
                    RunManager.buildNewRun();
                    CardManager.initCards(); //init cards for pool.
                }
            );
        };


        _littleButtonLeft = new LittleButtonUi(idLayer);
        littleButtonLeft.text = "<";
        littleButtonLeft.pos = new(
            CanvasManager.sizeWindow.x / 4,
            CanvasManager.centerWindow.y
        );
        littleButtonLeft.eventClick = ()=>{

            ChooseCaracter.layer.characterPlayerChooseIndex -= 1; //move index choose.

            if(ChooseCaracter.layer.characterPlayerChooseIndex == 0) //disable left.
                ChooseCaracter.layer.littleButtonLeft.setIsDisabled(true);

            if(ChooseCaracter.layer.characterPlayerChooseIndex < ChooseCaracter.layer.characterPlayerCanBeChoose.Count -1) //unable right.
                ChooseCaracter.layer.littleButtonRight.setIsDisabled(false);

        };
        littleButtonLeft.setIsDisabled(true);

        _littleButtonRight = new LittleButtonUi(idLayer);
        littleButtonRight.text = ">";
        littleButtonRight.pos = new(
            (CanvasManager.sizeWindow.x / 4) *3,
            CanvasManager.centerWindow.y
        );
        littleButtonRight.eventClick = ()=>{

            ChooseCaracter.layer.characterPlayerChooseIndex += 1; //move index choose.

            if(ChooseCaracter.layer.characterPlayerChooseIndex > 0) //enable left.
                ChooseCaracter.layer.littleButtonLeft.setIsDisabled(false);

            if(ChooseCaracter.layer.characterPlayerChooseIndex == ChooseCaracter.layer.characterPlayerCanBeChoose.Count -1) //disable right.
                ChooseCaracter.layer.littleButtonRight.setIsDisabled(true);

        };


        CharacterUi characterUi = new CharacterUi(idLayer);
        characterUi.pos = CanvasManager.centerWindow;
        characterUi.isDrawPseudo = true;


        characterPlayerChooseIndex = 0; //reset default choose.

        characterPlayerCanBeChoose = new(); //list of character playable.
        characterPlayerCanBeChoose.Add(SpriteType.Character_Ailten);
        characterPlayerCanBeChoose.AddRange( // add sprite type of character player unlock from succes.
            SaveManager.getSave.succes
                .Select(s => s.getCharacterUnlocked())
                .Where(c => c != null).Cast<SpriteType>()
        );

        if (characterPlayerCanBeChoose.Count == 1) //enable button right.
            littleButtonRight.setIsDisabled(true);


        base.active();
    }

    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        _littleButtonLeft = null;
        _littleButtonRight = null;

        base.unActive();
    }

}