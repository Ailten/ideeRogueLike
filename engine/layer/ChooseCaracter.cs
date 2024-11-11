
public class ChooseCaracter : Layer
{

    private static ChooseCaracter _layer = new ChooseCaracter(){ layerName="ChooseCaracter" };
    public static ChooseCaracter layer 
    {
        get { return _layer; }
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
                layer.idLayer, //id layer start.
                RunLayer.layer.idLayer, //id layer end.
                () => { //action to do during black screen transition.
                    RunManager.buildNewRun();
                }
            );
        };


        LittleButtonUi littleButtonLeft = new LittleButtonUi(idLayer);
        littleButtonLeft.text = "<";
        littleButtonLeft.pos = new(
            CanvasManager.sizeWindow.x / 4,
            CanvasManager.centerWindow.y
        );
        littleButtonLeft.eventClick = ()=>{
            //TODO...
        };
        littleButtonLeft.setIsDisabled(true);

        LittleButtonUi littleButtonRight = new LittleButtonUi(idLayer);
        littleButtonRight.text = ">";
        littleButtonRight.pos = new(
            (CanvasManager.sizeWindow.x / 4) *3,
            CanvasManager.centerWindow.y
        );
        littleButtonRight.eventClick = ()=>{
            //TODO...
        };
        littleButtonRight.setIsDisabled(true);


        CharacterUi characterUi = new CharacterUi(idLayer);
        characterUi.pos = CanvasManager.centerWindow;
        characterUi.isDrawPseudo = true;


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

        base.unActive();
    }

}