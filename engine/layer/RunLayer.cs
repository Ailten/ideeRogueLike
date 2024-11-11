
public class RunLayer : Layer
{

    private static RunLayer _layer = new RunLayer(){ layerName="RunLayer" };
    public static RunLayer layer 
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->
        
        RunManager.activeAllCelOfARoom(true); //active all cels in current room of current stage.

        MouseManager.isWheelMoveTheZoomCam = true; //active wheel mouse zoom cam.
        MouseManager.isRightMouseMovePosCam = true; //active right mouse move cam.

        //TODO : player.

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