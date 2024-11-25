
public static class WalkManager
{

    private static bool _isWalking = false;
    public static bool isWalking
    {
        get { return _isWalking; }
    }

    private static int indexWalkInPath;

    private static int timeStartWalk;
    private static readonly int milisecForWalkOneCel = 300;


    //start a new walk.
    public static void startWalk()
    {
        _isWalking = true;
        indexWalkInPath = 0;
        timeStartWalk = RunLayer.layer.milisecInLevel;
    }

    //end the current walk.
    public static void endWalk()
    {
        _isWalking = false;
    }

    public static void updateWalk()
    {
        float i = (float)(RunLayer.layer.milisecInLevel - timeStartWalk) / milisecForWalkOneCel ; //interpolation walk current cel.

        Character characterWalk = TurnManager.getCharacterOfCurrentTurn();

        //end of this cel walk.
        if(i >= 1f){

            timeStartWalk = RunLayer.layer.milisecInLevel; //reset the timer walk between two cel.
            indexWalkInPath += 1; //move to next index cel.

            //end of walk (last cel).
            if(indexWalkInPath >= PathFindingManager.pathFind.Count - 1){

                characterWalk.moveTo(PathFindingManager.pathFind[PathFindingManager.pathFind.Count - 1]); //move to end pos.

                endWalk();
                return;
            }

            characterWalk.moveTo(PathFindingManager.pathFind[indexWalkInPath]); //move to next cel.
            
            return;
        }

        //apply position of walk.
        characterWalk.pos = Vector.lerp(
            Room.getPosAtIndexCelRoom(PathFindingManager.pathFind[indexWalkInPath]),
            Room.getPosAtIndexCelRoom(PathFindingManager.pathFind[indexWalkInPath + 1]),
            i
        );

    }

}