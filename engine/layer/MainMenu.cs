
public class MainMenu : Layer
{

    private static MainMenu _layer = new MainMenu();
    public static MainMenu layer 
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        Vector scaleButton = new(0.9f, 0.9f);

        ButtonUi buttonPlay = new ButtonUi(idLayer);
        buttonPlay.text = "play";
        buttonPlay.pos = CanvasManager.centerWindow;
        buttonPlay.scale = scaleButton;
        buttonPlay.eventClick = () => {
            LayerManager.transition(
                layer.idLayer, //id layer start.
                ChooseCaracter.layer.idLayer //id layer end.
            );
        };

        Vector spacingButton = new(0, buttonPlay.size.y * scaleButton.x + 8);

        ButtonUi buttonOption = new ButtonUi(idLayer);
        buttonOption.text = "option";
        buttonOption.pos = buttonPlay.pos + spacingButton;
        buttonOption.scale = scaleButton;
        buttonOption.eventClick = () =>
        {
            if (LayerManager.isADetailsLayerAreOpen) // can't open two type of detail layer.
                return;
            LayerManager.isADetailsLayerAreOpen = true;

            LayerManager.transition(
                idLevelStart: new int[0],
                idLevelEnd: new int[] { Option.layer.idLayer }
            );
        };
        //buttonOption.setIsDisabled(true);

        ButtonUi buttonSuccesMenu = new ButtonUi(idLayer);
        int purcentCompletion = (int)(SaveManager.getPurcentCompletion() * 100);
        buttonSuccesMenu.text = $"progres {purcentCompletion}%";
        buttonSuccesMenu.pos = buttonOption.pos + spacingButton;
        buttonSuccesMenu.scale = scaleButton;
        buttonSuccesMenu.eventClick = () =>
        {
            if (LayerManager.isADetailsLayerAreOpen) // can't open two type of detail layer.
                return;
            LayerManager.isADetailsLayerAreOpen = true;

            LayerManager.transition(
                idLevelStart: new int[0],
                idLevelEnd: new int[] { SuccessMenu.layer.idLayer }
            );
        };
        //buttonSuccesMenu.setIsDisabled(true);

        ButtonUi buttonExit = new ButtonUi(idLayer);
        buttonExit.text = "quit";
        buttonExit.pos = buttonSuccesMenu.pos + spacingButton;
        buttonExit.scale = scaleButton;
        buttonExit.eventClick = () => {
            CanvasManager.isCloseWindow = true; //close the window.
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