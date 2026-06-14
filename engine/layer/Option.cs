
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

        Vector scaleButton = new(0.9f, 0.9f);

        ButtonUi buttonBackMainMenu = new ButtonUi(idLayer);
        buttonBackMainMenu.text = "retour menu";
        buttonBackMainMenu.pos = new(CanvasManager.centerWindow.x, CanvasManager.sizeWindow.y - 75);
        buttonBackMainMenu.scale = scaleButton;
        buttonBackMainMenu.zIndex = 3200;
        buttonBackMainMenu.eventClick = () =>
        {
            List<int> layerFrom = new List<int>() { Option.layer.idLayer };

            // if call button back main menu from option of main menu.
            if(MainMenu.layer.isActive){
                layerFrom.Add(MainMenu.layer.idLayer);
            }
            else if(RunLayer.layer.isActive){
                layerFrom.Add(RunLayer.layer.idLayer);
                layerFrom.Add(RunHudLayer.layer.idLayer);

                // TODO : save progression ?

            }

            LayerManager.transition(
                idLevelStart: layerFrom.ToArray(),
                idLevelEnd: new int[] { MainMenu.layer.idLayer },
                midAction: () => {

                    // clean the run data loaded (run manager).
                    RunManager.destroyRun();

                }
            );
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