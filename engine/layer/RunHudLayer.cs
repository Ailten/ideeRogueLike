
using System.Runtime.CompilerServices;

public class RunHudLayer : Layer
{

    private static RunHudLayer _layer = new RunHudLayer(){ layerName="RunHudLayer" };
    public static RunHudLayer layer 
    {
        get { return _layer; }
    }


    public override void active()
    {
        //init all entities of layer. --->

        StatsCharacterUi statsCharacterUI = new StatsCharacterUi(idLayer); //sprite of HP and SP player.

        MiniMapUi miniMapUi = new MiniMapUi(idLayer); //mini map.
        miniMapUi.pos = new(1280, 0);

        SkipTurnBackUi skipTurnBackUi = new SkipTurnBackUi(idLayer); //background ui for skip turn button.
        skipTurnBackUi.pos = CanvasManager.sizeWindow;
        skipTurnBackUi.isLeftSPriteTo = true;

        this.buttonSkipTurn = new ButtonSkipTurnUi(idLayer);
        this.buttonSkipTurn.pos = CanvasManager.sizeWindow - new Vector(195, 60);
        this.buttonSkipTurn.eventClick = () => { //event skip turn click.
            if(!TurnManager.isInFight)
                return;
            Character currentCharacter = TurnManager.getCharacterOfCurrentTurn();
            if(!currentCharacter.isInRedTeam)
                return;
            currentCharacter.skipTurn();
        };
        this.buttonSkipTurn.setIsDisabled(true);

        
        base.active();
    }


    private ButtonSkipTurnUi? buttonSkipTurn = null;
    public ButtonSkipTurnUi buttonSkipTurnNN
    {
        get { return buttonSkipTurn ?? throw new Exception(); }
    }


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->
        this.buttonSkipTurn = null;

        base.unActive();
    }

}