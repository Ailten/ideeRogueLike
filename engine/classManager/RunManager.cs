
public static class RunManager
{
    private static int _seed;
    public static int seed
    {
        get { return _seed; }
    }
    private static Random _rngSeed = new Random();
    public static Random rngSeed
    {
        get { return _rngSeed; }
    }
    private static int timeStartRun;
    private static int currentIndexStage;
    private static List<Stage> stages = new();

    public static Stage currentStage
    {
        get { return stages[currentIndexStage]; }
    }
    public static Room? currentRoom
    {
        get { return currentStage.currentRoom; }
    }

    public static bool isLastStage
    {
        get { return currentIndexStage == stages.Count - 1; }
    }


    public static void buildNewRun()
    {
        Random rng = new Random(DateTime.Now.Millisecond);
        buildNewRun(rng.Next()); //0 to int32.maxValue.
    }
    public static void buildNewRun(int seed)
    {
        //rng.
        RunManager._seed = seed;
        _rngSeed = new Random(seed);
        Console.WriteLine($"Seed : {seed}");

        //set random seed to randomManager (used to event decide by player).
        RandomManager.setRandomManagerSeed(seed);

        //time.
        timeStartRun = UpdateManager.timeFromStartGame;

        //stage.
        currentIndexStage = 0;
        stages = new();
        const int quantityStage = 6;
        for (int i = 0; i < quantityStage; i++)
        {
            stages.Add(new Stage(i, rngSeed.Next()));
        }

        //load first stage.
        stages[0].generateStage();

        //increase count run save.
        SaveManager.increaseRunCount();

    }

    // free last stage.
    public static void destroyRun()
    {
        //destroy all stage not already destroyed.
        stages.Where(s => s.rooms.Count > 0).ToList().ForEach(s => s.destroyStage());
        stages = new();
    }


    //active all entity cel of a specific room.
    public static void activeAllCelOfARoom(bool editActiveCel)
    {
        activeAllCelOfARoom( //use param of current stage.
            currentIndexStage,
            currentStage.currentIndexRoom,
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
        currentStage.destroyStage(); //destroy last stage.

        currentIndexStage += 1; //go to next stage.

        if (currentIndexStage >= stages.Count) // end of run, win.
            throw new Exception("switchToNextStage next stage is out of range !");

        currentStage.generateStage(); //generate new stage.
        activeAllCelOfARoom(true); //active current room (in next stage).
    }

    //switch to next room (editIndexRoom must be (-1, 0 or 1)).
    public static void switchToNextRoom(Vector editIndexRoom)
    {
        activeAllCelOfARoom(false); //un active current room.
        currentStage.currentIndexRoom += editIndexRoom; //go to next room.

        currentStage.walkOnAnOtherRoom(); //call event walk other room.

        activeAllCelOfARoom(true); //active current room (in next room).
    }


    //get cel by index pos (in current stage, in current room).
    public static Cel? getCel(Vector celPosIndex, bool isCanBeNull = true)
    {
        if (currentRoom == null)
        {
            return null;
        }

        return currentRoom.getCel(celPosIndex);
    }

    //get cel by index pos (not null).
    public static Cel getCelNN(Vector celPosIndex)
    {
        return getCel(celPosIndex) ?? throw new Exception("Cel not found !");
    }
    public static Cel getCelNNCenter()
    {
        return getCelNN(new(Room.midWidthMax, Room.midHeightMax));
    }

    //get stage by index.
    public static Stage getStage(int indexStage)
    {
        return stages[indexStage];
    }


    // event run end.
    public static void endRun(bool isWinByPlayer)
    {
        // eval all new succes unlocked during this run.
        List<Succes> succesUnlocked = Enum.GetValues(typeof(Succes)) // get all value enum on array.
            .Cast<Succes>().ToList() // cast list.
            .Where(s => !SaveManager.getSave.succes.Contains(s)).ToList() // drop all succes already unlocked.
            .Where(s => s.isUnlocked()).ToList(); // drop all succes with condition unlock not reach.

        // get params from run.
        int seedPlayed = _seed; // get seed.
        long timeInRunTick = (long)(timeStartRun - UpdateManager.timeFromStartGame) * 10000; // get time.
        TimeSpan timeInRun = new TimeSpan(ticks: timeInRunTick);

        // add succes unlock and save.
        SaveManager.addSucces(succesUnlocked);
        SaveManager.saveFileSave();


        TurnManager.reset(); //free characters from turnManager.
        RunManager.destroyRun(); //free stages list (recursively).

        // send value to layer EndRunLayer.
        EndRunLayer.layer.setIsRunWin(isWinByPlayer);
        EndRunLayer.layer.setSeedRunEnd(seedPlayed);
        EndRunLayer.layer.setSuccesUnlockDuringTheRun(succesUnlocked);
        EndRunLayer.layer.setTimeInRun(timeInRun);

        // transition layer (RunLayer, RunHudLayer -> EndRunLayer).
        LayerManager.transition(
            idLevelEnd: new int[] { RunLayer.layer.idLayer, RunHudLayer.layer.idLayer },
            idLevelStart: new int[] { EndRunLayer.layer.idLayer }
        );
    }

}