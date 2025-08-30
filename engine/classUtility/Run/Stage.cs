
public class Stage
{
    public int stage;
    public List<Dictionary<int, Room>> rooms = new();
    public Vector currentIndexRoom = new();

    public List<Vector> roomsWalked = new() { new(midWidthMax, midHeightMax) };

    private int seed;
    private Random _rngSeed = new Random();
    public Random rngSeed
    {
        get { return _rngSeed; }
    }

    private int rayonSpread;


    private static int _widthMax = 15;
    public static int widthMax
    {
        get { return _widthMax; }
    }
    public static int midWidthMax
    {
        get { return (_widthMax - 1) / 2; }
    }
    private static int _heightMax = 15;
    public static int heightMax
    {
        get { return _heightMax; }
    }
    public static int midHeightMax
    {
        get { return (_heightMax - 1) / 2; }
    }


    public Room? currentRoom
    {
        get
        {
            try
            {
                return rooms[(int)currentIndexRoom.y][(int)currentIndexRoom.x];
            }
            catch (KeyNotFoundException)
            {
                return null;
            }
        }
    }

    public Room currentRoomNN
    {
        get
        {
            return currentRoom ?? throw new Exception("currentRoom is null !");
        }
    }

    public Stage(int stage, int seed)
    {

        this.stage = stage;
        this.seed = seed;

        switch (stage)
        {

            case (0): //stage without ennemy, light for begun (tuto single room).
                this.rayonSpread = 1;
                break;

            case (1):
                this.rayonSpread = 4;
                break;

            case (2):
                this.rayonSpread = 5;
                break;

            case (3):
                this.rayonSpread = 5;
                break;

            case (4):
                this.rayonSpread = 6;
                break;

            case (5):
                this.rayonSpread = 6;
                break;

        }

    }


    //generate a random stage.
    public void generateStage()
    {
        _rngSeed = new Random(seed);

        currentIndexRoom = new(midWidthMax, midHeightMax);

        if (rayonSpread == 1) //stage tuto.
        {

            //TODO: hard code a stage tuto on another function.

            rooms = new();
            for (int y = 0; y < heightMax; y++)
            {
                rooms.Add(new());

                if (y == midHeightMax)
                {
                    rooms[rooms.Count - 1].Add(
                        midWidthMax,
                        new Room(
                            false, false, false, false, //room in 4 direction.
                            stage, //stage.
                            RoomType.Room_Tuto,
                            rngSeed.Next()
                        )
                    );
                }

            }

            return;
        }

        //generate default grid full of false.
        List<List<bool>> roomsBool = new();
        for (int y = 0; y < heightMax; y++)
        {
            roomsBool.Add(new());
            for (int x = 0; x < widthMax; x++)
            {
                roomsBool[roomsBool.Count - 1].Add(false);
            }
        }

        //loop spreading, decreasing the amount of rng.
        for (int r = 0; r < rayonSpread; r++)
        {
            for (int y = 0; y < heightMax; y++)
            {
                for (int x = 0; x < widthMax; x++)
                {
                    int distToCenterX = Math.Abs(x - midWidthMax);
                    int distToCenterY = Math.Abs(y - midHeightMax);
                    int dist = distToCenterX + distToCenterY;
                    if (dist != r) //skip if not the current circle target at this loop.
                        continue;

                    float rayonI = Math.Clamp((float)r / (rayonSpread - 1), 0f, 1f); //RNG.
                    int rngCeil = 999 - (int)(800 * rayonI);
                    int rngGet = rngSeed.Next(1000);
                    bool isSafeCrossRoom = (distToCenterX == 0 || distToCenterY == 0) && dist <= 2; //stay a cross room always print.
                    if (rngGet >= rngCeil && !isSafeCrossRoom) //skip if cell rng say no.
                        continue;

                    int countAdj = (
                        ((y > 0 && roomsBool[y - 1][x]) ? 1 : 0) +
                        ((y < (heightMax - 1) && roomsBool[y + 1][x]) ? 1 : 0) +
                        ((x > 0 && roomsBool[y][x - 1]) ? 1 : 0) +
                        ((x < (widthMax - 1) && roomsBool[y][x + 1]) ? 1 : 0)
                    );
                    if (countAdj == 0 && dist != 0) //skip if cell is not adjacent to another valid.
                        continue;

                    roomsBool[y][x] = true;
                }
            }
        }

        //list of border room (for special room).
        List<Vector> posForSpecialRoom = new();
        int secureCountLoop = 0;
        while (posForSpecialRoom.Count != 8)
        {
            secureCountLoop++; //security for infinit loop.
            if (secureCountLoop >= 10000)
                break;

            int rngIndexX = rngSeed.Next(0, widthMax);
            int rngIndexY = rngSeed.Next(0, heightMax);

            if (!roomsBool[rngIndexY][rngIndexX]) //not a room not valid by spreading rng.
                continue;
            if (posForSpecialRoom.Contains(new(rngIndexX, rngIndexY))) //not a room already find for special room.
                continue;
            if (rngIndexX == midWidthMax && rngIndexY == midHeightMax) //not the room spawn.
                continue;

            posForSpecialRoom.Add(new(rngIndexX, rngIndexY));
        }

        //cast roomBool into rooms.
        rooms = new();
        for (int y = 0; y < heightMax; y++)
        {
            rooms.Add(new());
            for (int x = 0; x < widthMax; x++)
            {
                if (!roomsBool[y][x]) //no room.
                    continue;

                RoomType roomType = RoomType.Room; //eval room type.

                Vector posIndexCurrentRoom = new(x, y);
                if (posForSpecialRoom.Contains(posIndexCurrentRoom))
                {
                    for (int i = 0; i < posForSpecialRoom.Count; i++)
                    {
                        bool isMatchIndexPos = (
                            posForSpecialRoom[i].x == posIndexCurrentRoom.x &&
                            posForSpecialRoom[i].y == posIndexCurrentRoom.y
                        );
                        if (isMatchIndexPos)
                        {
                            roomType = getSpecialRoomType(i, stage);
                            break;
                        }
                    }
                }
                else if (y == midHeightMax && x == midWidthMax) //center room type.
                    roomType = RoomType.Room_Center;

                rooms[rooms.Count - 1].Add(
                    x,
                    new Room(
                        isARoomTop: (y > 0 && roomsBool[y - 1][x]),
                        isARoomRight: (x < (widthMax - 1) && roomsBool[y][x + 1]),
                        isARoomDown: (y < (heightMax - 1) && roomsBool[y + 1][x]),
                        isARoomLeft: (x > 0 && roomsBool[y][x - 1]),
                        stage: stage,
                        roomType: roomType,
                        seed: rngSeed.Next()
                    )
                );
            }
        }

        EntityManager.sortAllEntities();

    }


    //call when player walk on an other room.
    public void walkOnAnOtherRoom()
    {
        bool isFirstWalkOnThisRoom = !isContainInRoomsWalked(currentIndexRoom);

        if (isFirstWalkOnThisRoom)
        {

            roomsWalked.Add(currentIndexRoom); //add in list walked.

            //spawn mobs if has one in room.
            if (currentRoomNN.isHasMob)
            {

                //unable button skip turn.
                RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(false);

                //spawn mobs.
                for (int i = 0; i < currentRoomNN.typeMobToSpawn.Count; i++)
                {
                    SpriteType spriteType = currentRoomNN.typeMobToSpawn[i];
                    Vector posIndexCel = currentRoomNN.getCelsByType(CelType.Cel_MobSpawner)[i].indexPosCel;
                    Character newMobSpawn = CharacterMob.init(spriteType, posIndexCel);
                    TurnManager.addCharacterInRoom(newMobSpawn);

                    if (i == currentRoomNN.typeMobToSpawn.Count - 1) //sort all entities zIndex when last mob are spawn.
                        EntityManager.sortAllEntities();
                }

            }

        }
    }

    //return true if the vector ask is in list.
    public bool isContainInRoomsWalked(Vector indexPosAsk)
    {
        for (int i = 0; i < roomsWalked.Count; i++)
        {
            if (roomsWalked[i].x == indexPosAsk.x && roomsWalked[i].y == indexPosAsk.y)
            {
                return true;
            }
        }
        return false;
    }

    //return the distance from the the closest room walk.
    public int getClosestDistFromWalked(Vector indexPosAsk)
    {
        int closestDist = 1000;
        for (int i = 0; i < roomsWalked.Count; i++)
        {
            int dist = (
                Math.Abs((int)roomsWalked[i].x - (int)indexPosAsk.x) +
                Math.Abs((int)roomsWalked[i].y - (int)indexPosAsk.y)
            );
            if (dist < closestDist)
                closestDist = dist;
        }
        return closestDist;
    }


    //get a room by index.
    public Room? getRoom(Vector indexRoom)
    {
        try
        {
            return rooms[(int)indexRoom.y][(int)indexRoom.x];
        }
        catch (KeyNotFoundException)
        {
            return null;
        }
    }


    //reset the obj stage (oposite of generate).
    public void destroyStage()
    {
        rooms.ForEach((dicoR) =>
            dicoR.ToList().ForEach((kvpR) =>
            {
                kvpR.Value.destroyRoom();
            })
        );

        rooms = new();
        roomsWalked = new() { new(midWidthMax, midHeightMax) };
    }

    private RoomType getSpecialRoomType(int indexSpecialRoom, int stage)
    {
        int rngSpecialRoom = rngSeed.Next(1000);
        switch (stage)
        {
            //0.

            case (1):
                return (
                    (indexSpecialRoom == 0) ? RoomType.Room_Boss :
                    (indexSpecialRoom == 1) ? RoomType.Room_Chest :
                    (indexSpecialRoom == 2) ? (rngSpecialRoom < 900 ? RoomType.Room_Chest : RoomType.Room) :
                    (indexSpecialRoom == 3) ? (rngSpecialRoom < 800 ? RoomType.Room_Discard : RoomType.Room) :
                    (indexSpecialRoom == 4) ? RoomType.Room :
                    (indexSpecialRoom == 5) ? RoomType.Room :
                    (indexSpecialRoom == 6) ? RoomType.Room :
                    RoomType.Room
                );

            case (2):
                return (
                    (indexSpecialRoom == 0) ? RoomType.Room_Boss :
                    (indexSpecialRoom == 1) ? RoomType.Room_Chest :
                    (indexSpecialRoom == 2) ? (rngSpecialRoom < 750 ? RoomType.Room_Chest : RoomType.Room) :
                    (indexSpecialRoom == 3) ? (rngSpecialRoom < 600 ? RoomType.Room_Discard : RoomType.Room) :
                    (indexSpecialRoom == 4) ? RoomType.Room :
                    (indexSpecialRoom == 5) ? RoomType.Room :
                    (indexSpecialRoom == 6) ? RoomType.Room :
                    RoomType.Room
                );
            
            case (3):
                return (
                    (indexSpecialRoom == 0) ? RoomType.Room_Boss :
                    (indexSpecialRoom == 1) ? RoomType.Room_Chest :
                    (indexSpecialRoom == 2) ? (rngSpecialRoom < 250 ? RoomType.Room_Chest : RoomType.Room) :
                    (indexSpecialRoom == 3) ? (rngSpecialRoom < 400 ? RoomType.Room_Discard : RoomType.Room) :
                    (indexSpecialRoom == 4) ? RoomType.Room :
                    (indexSpecialRoom == 5) ? RoomType.Room :
                    (indexSpecialRoom == 6) ? RoomType.Room :
                    RoomType.Room
                );
            
            case (4):
                return (
                    (indexSpecialRoom == 0) ? RoomType.Room_Boss :
                    (indexSpecialRoom == 2) ? (rngSpecialRoom < 600 ? RoomType.Room_Chest : RoomType.Room) :
                    (indexSpecialRoom == 2) ? RoomType.Room :
                    (indexSpecialRoom == 3) ? RoomType.Room :
                    (indexSpecialRoom == 4) ? RoomType.Room :
                    (indexSpecialRoom == 5) ? RoomType.Room :
                    (indexSpecialRoom == 6) ? RoomType.Room :
                    RoomType.Room
                );

            case (5):
                return (
                    (indexSpecialRoom == 0) ? RoomType.Room_Boss :
                    (indexSpecialRoom == 2) ? (rngSpecialRoom < 80 ? RoomType.Room_Chest : RoomType.Room) :
                    (indexSpecialRoom == 2) ? RoomType.Room :
                    (indexSpecialRoom == 3) ? RoomType.Room :
                    (indexSpecialRoom == 4) ? RoomType.Room :
                    (indexSpecialRoom == 5) ? RoomType.Room :
                    (indexSpecialRoom == 6) ? RoomType.Room :
                    RoomType.Room
                );

            default:
                throw new Exception("getSpecialRoomType has no implementation for this stage !");
        }
    } 

}