
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
    private static bool isDecreaseMP;


    //start a new walk.
    public static void startWalk(bool isDecreaseMP = true)
    {
        _isWalking = true;
        indexWalkInPath = 0;
        timeStartWalk = UpdateManager.timeSpeedForAnime(RunLayer.layer.milisecInLevel);
        WalkManager.isDecreaseMP = isDecreaseMP;
    }

    //end the current walk.
    public static void endWalk()
    {
        _isWalking = false;
    }

    public static void updateWalk()
    {
        int timeLayerSpeeded = UpdateManager.timeSpeedForAnime(RunLayer.layer.milisecInLevel); //get the time including speed game.
        float i = (float)(timeLayerSpeeded - timeStartWalk) / milisecForWalkOneCel; //interpolation walk current cel.

        Character characterWalk = TurnManager.getCharacterOfCurrentTurn();

        //end of this cel walk.
        if (i >= 1f)
        {

            timeStartWalk = RunLayer.layer.milisecInLevel; //reset the timer walk between two cel.
            indexWalkInPath += 1; //move to next index cel.

            characterWalk.scale = new(1, 1); //reset zoom.
            characterWalk.rotate = 0; //reset rotate.

            //end of walk (last cel).
            if (indexWalkInPath >= PathFindingManager.pathFind.Count - 1)
            {

                characterWalk.moveTo(PathFindingManager.pathFind[PathFindingManager.pathFind.Count - 1]); //move to end pos.

                if (isDecreaseMP)
                    characterWalk.decreaseMP(indexWalkInPath); //decrease MP by cost walking.

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

        //curve zoom.
        float scaleCurv = (float)Math.Pow(1 - Math.Abs((i * 2) - 1), 2);
        characterWalk.scale = Vector.lerp(
            new(1, 1),
            new(1.3f, 1.3f),
            scaleCurv
        );

        //rotate.
        characterWalk.rotate = scaleCurv * 30f * (Room.isOddCel(PathFindingManager.pathFind[indexWalkInPath]) ? 1 : -1);

    }


    // ask if a character is currently walking (is turn and walking).
    public static bool isThisCharacterWalk(Character characterAsk)
    {
        return isWalking && TurnManager.getCharacterOfCurrentTurn() == characterAsk;
    }

}