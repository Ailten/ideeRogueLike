
public static class RunManager
{
    private static int seed;
    private static Random _rngSeed = new Random();
    public static Random rngSeed
    {
        get { return _rngSeed; }
    }
    private static int timeStartRun;
    private static int currentIndexStage;
    private static List<Stage> stages = new();

    public static void buildNewRun()
    {
        Random rng = new Random(new DateTime().Millisecond);
        buildNewRun(rng.Next()); //0 to int32.maxValue.
    }
    public static void buildNewRun(int seed)
    {
        //rng.
        RunManager.seed = seed;
        _rngSeed = new Random(seed);

        //time.
        timeStartRun = UpdateManager.timeFromStartGame;

        //stage.
        currentIndexStage = 0;
        stages = new();
        const int quantityStage = 6;
        for(int i=0; i<quantityStage; i++){
            stages.Add(new Stage(i));
        }

    }


    //active all entity cel of a specific room.
    public static void activeAllCelOfARoom(bool editActiveCel)
    {
        activeAllCelOfARoom( //use param of current stage.
            currentIndexStage, 
            stages[currentIndexStage].currentIndexRoom, 
            editActiveCel
        );
    }
    private static void activeAllCelOfARoom(int indexStage, Vector indexRoom, bool editActiveCel)
    {
        stages[indexStage].rooms[(int)indexRoom.y][(int)indexRoom.x].editActiveCels(editActiveCel);
    }

    //switch to next stage.
    public static void switchToNextStage()
    {
        activeAllCelOfARoom(false); //un active current room.
        currentIndexStage++; //go to next stage.
        activeAllCelOfARoom(true); //active current room (in next stage).
    }

    //switch to next room (editIndexRoom must be (-1, 0 or 1)).
    public static void switchToNextRoom(Vector editIndexRoom)
    {
        activeAllCelOfARoom(false); //un active current room.
        stages[currentIndexStage].currentIndexRoom += editIndexRoom; //go to next room.
        activeAllCelOfARoom(true); //active current room (in next room).
    }

}