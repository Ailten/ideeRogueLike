
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

        LittleButtonUi littleButtonLeft = new LittleButtonUi(idLayer);
        littleButtonLeft.text = "<";
        littleButtonLeft.pos = new(
            CanvasManager.sizeWindow.x / 4,
            CanvasManager.centerWindow.y
        );
        littleButtonLeft.eventClick = ()=>{
            //TODO...
        };

        LittleButtonUi littleButtonRight = new LittleButtonUi(idLayer);
        littleButtonRight.text = ">";
        littleButtonRight.pos = new(
            (CanvasManager.sizeWindow.x / 4) *3,
            CanvasManager.centerWindow.y
        );
        littleButtonRight.eventClick = ()=>{
            //TODO...
        };


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