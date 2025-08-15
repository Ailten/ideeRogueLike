
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
        buttonOption.eventClick = () => {
            //TODO : option menu (a full menu).
        };
        buttonOption.setIsDisabled(true);

        ButtonUi buttonOptionTwitch = new ButtonUi(idLayer);
        int purcentCompletion = (int)(SaveManager.getPurcentCompletion() * 100);
        buttonOptionTwitch.text = $"progres {purcentCompletion}%";
        buttonOptionTwitch.pos = buttonOption.pos + spacingButton;
        buttonOptionTwitch.scale = scaleButton;
        buttonOptionTwitch.eventClick = () => {
            //TODO : success and contains progression (resume + note about how complet it).
        };
        buttonOptionTwitch.setIsDisabled(true);

        ButtonUi buttonExit = new ButtonUi(idLayer);
        buttonExit.text = "quit";
        buttonExit.pos = buttonOptionTwitch.pos + spacingButton;
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