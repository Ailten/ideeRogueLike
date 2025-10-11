
public class Option : Layer
{
    private static Option _layer = new Option();
    public static Option layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        // TODO. print many element over all other payer.

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
            LayerManager.transition( // with transition anime.
                idLevelStart: new int[] { Option.layer.idLayer },
                idLevelEnd: new int[0]
            );

            //Option.layer.unActive(); // close the layer without anime.

            buttonExit.setIsOn(false); // lock on "x" char.
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

        LayerManager.isADetailsLayerAreOpen = false;

        base.unActive();
    }

}