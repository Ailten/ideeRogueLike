
public class RunLayer : Layer
{

    private static RunLayer _layer = new RunLayer();
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

        CameraManager.resetPosCam(Room.getPosAtIndexCelRoom(new( //replace cam at center Room.
            Room.midWidthMax,
            Room.midHeightMax
        )));

        //player.
        CharacterPlayer player = CharacterPlayer.init(
            ChooseCaracter.layer.characterPlayerChoose, //character player.
            new( //pos index cel (default center room).
                Room.midWidthMax,
                Room.midHeightMax
            )
        );

        //turn manager.
        TurnManager.reset();
        TurnManager.addCharacterInRoom(player);


        //TODO : Card in Hands, list of player turn to play (time-line at right of screen), a button pause/option at up right of screen.

        

        base.active();
    }

    public override void update()
    {
        //do the update. --->

        //update action turn of every character.
        TurnManager.updateTurn();

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        base.unActive();
    }

}