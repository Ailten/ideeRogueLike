
public class Room
{

    public int stage;
    public RoomType roomType;

    public List<Dictionary<int, Cel>> cels = new();

    public bool isARoomTop;
    public bool isARoomRight;
    public bool isARoomDown;
    public bool isARoomLeft;


    private static int _widthMax = 15;
    public static int widthMax
    {
        get { return _widthMax; }
    }
    public static int midWidthMax
    {
        get { return (_widthMax-1)/2; }
    }
    private static int _heightMax = 15;
    public static int heightMax
    {
        get { return _heightMax; }
    }
    public static int midHeightMax
    {
        get { return (_heightMax-1)/2; }
    }


    private List<SpriteType> _typeMobToSpawn = new();
    public List<SpriteType> typeMobToSpawn
    {
        get { return _typeMobToSpawn; }
    }
    public bool isHasMob
    {
        get { return typeMobToSpawn.Count > 0; }
    }


    private int seed;
    private Random _rngSeed = new Random();
    public Random rngSeed
    {
        get { return _rngSeed; }
    }


    public Room(bool isARoomTop, bool isARoomRight, bool isARoomDown, bool isARoomLeft, int stage, RoomType roomType, int seed)
    {
        this.stage = stage;
        this.roomType = roomType;

        this.isARoomTop = isARoomTop;
        this.isARoomRight = isARoomRight;
        this.isARoomDown = isARoomDown;
        this.isARoomLeft = isARoomLeft;

        this.seed = seed;

        switch(stage){

            case(0):
                generateRoom(2);
                break;

            case(1):
                generateRoom(4);
                break;

            case(2):
                generateRoom(5);
                break;

            case(3):
                generateRoom(5);
                break;

            case(4):
                generateRoom(6);
                break;

            case(5):
                generateRoom(7);
                break;

        }

    }


    //generate a random room.
    private void generateRoom(int rayonSpread=7)
    {
        _rngSeed = new Random(seed);

        if(roomType == RoomType.Room_Tuto){ //room tuto.

            //TODO: generate rooms for tuto stage.

            cels = new();
            for(int y=0; y<heightMax; y++){
                cels.Add(new());
            	for(int x=0; x<widthMax; x++){

                    int distToCenterX = Math.Abs(x - midWidthMax); //eval dist.
                    int distToCenterY = Math.Abs(y - midHeightMax);
                    int dist = Math.Max(distToCenterX, distToCenterY);

                    if(x == midWidthMax && y == midHeightMax - (rayonSpread+1)){ //map a cel up to exit stage tuto.
                        cels[cels.Count-1].Add(
                            x,
                            new Cel(
                                CelType.Cel_NextStage,
                                new(x, y)
                            )
                        );
                        continue;
                    }

                    if(dist > rayonSpread) //skip if not the current circle target at this loop.
                        continue;

                    //instancie cel.
                    cels[cels.Count-1].Add(
                        x,
                        new Cel(
                            CelType.Cel,
                            new(x, y)
                        )
                    );

            	}
            }

            return;
        }


        //generate default grid full of false.
        List<List<bool>> celsBool = new();
        for(int y=0; y<heightMax; y++){
            celsBool.Add(new());
        	for(int x=0; x<widthMax; x++){
        		celsBool[celsBool.Count-1].Add(false);
        	}
        }

        //loop spreading, decreasing the amount of rng.
	    for(int y=0; y<heightMax; y++){
	    	for(int x=0; x<widthMax; x++){
                int distToCenterX = Math.Abs(x - midWidthMax);
                int distToCenterY = Math.Abs(y - midHeightMax);
                float dist = (float) Math.Sqrt(
                    Math.Pow((float)distToCenterX / rayonSpread, 2) +
                    Math.Pow((float)distToCenterY / rayonSpread, 2)
                );
                if(dist > 1) //skip if not the current circle target at this loop.
                    continue;

                float rayonI = dist / 2 + 0.5f; //adapt rayonI for randomise only the farest cells.
                int rngCeil = 999 - (int)(800 * rayonI); 
                int rngGet = rngSeed.Next(1000);
                bool isSafeCrossCel = ( //patern always cell valid.
                    (distToCenterX == 0 || distToCenterY == 0) ||
                    (distToCenterX <= 2 && distToCenterY <= 1) //rectangle center safe.
                );
                if (rngGet >= rngCeil && !isSafeCrossCel) //skip if cell rng say no.
                    continue;

                celsBool[y][x] = true;
	    	}
	    }

        //get door for other room.
        List<KeyValuePair<CelType, Vector>> posForDoor = new();
        if (this.isARoomTop)
            posForDoor.Add(new(CelType.CelDoor_up, new(midWidthMax, midHeightMax - rayonSpread)));
        else
        {
            celsBool[midHeightMax - rayonSpread][midWidthMax] = false;
            celsBool[midHeightMax - rayonSpread +1][midWidthMax] = false;
        }
        if (this.isARoomRight)
            posForDoor.Add(new(CelType.CelDoor_right, new(midWidthMax + rayonSpread, midHeightMax)));
        else
        {
            celsBool[midHeightMax][midWidthMax + rayonSpread] = false;
            celsBool[midHeightMax][midWidthMax + rayonSpread -1] = false;
        }
        if(this.isARoomDown)
            posForDoor.Add(new(CelType.CelDoor_down, new(midWidthMax, midHeightMax + rayonSpread)));
        else
        {
            celsBool[midHeightMax + rayonSpread][midWidthMax] = false;
            celsBool[midHeightMax + rayonSpread -1][midWidthMax] = false;
        }
        if(this.isARoomLeft)
            posForDoor.Add(new(CelType.CelDoor_left, new(midWidthMax - rayonSpread, midHeightMax)));
        else
        {
            celsBool[midHeightMax][midWidthMax - rayonSpread] = false;
            celsBool[midHeightMax][midWidthMax - rayonSpread +1] = false;
        }

        //add monster if is a normal type room.
        List<Vector> posCelForMobSpawner = new();
        this._typeMobToSpawn = new();
        if (roomType == RoomType.Room)
        {

            int amountOfMobInRoom = rngSeed.Next(1, 5);

            for (int i = 0; i < amountOfMobInRoom; i++)
            {

                Vector posForMobSpawner = new( //random pos in rectangle safe.
                    midWidthMax + rngSeed.Next(-2, 3),
                    midHeightMax + rngSeed.Next(-1, 2)
                );

                if (posCelForMobSpawner.Contains(posForMobSpawner))
                {
                    i--;
                    continue;
                }

                posCelForMobSpawner.Add(posForMobSpawner);

            }

            for (int i = 0; i < posCelForMobSpawner.Count; i++) //generate random type mob for the room.
            {
                typeMobToSpawn.Add(CharacterMob.generateRandomMobType(this, stage, false));
            }

        }
        else if (roomType == RoomType.Room_Boss) // spawner for boss.
        {

            posCelForMobSpawner.Add(new Vector(midWidthMax, midHeightMax));
            typeMobToSpawn.Add(CharacterMob.generateRandomMobType(this, stage, true));

        }

        //TODO: else if for other type special room.

        //cast celsBool into cels.
        this.cels = new();
        for(int y=0; y<heightMax; y++){
            cels.Add(new());
        	for(int x=0; x<widthMax; x++){
                if(!celsBool[y][x]){ //skip cel.
                    continue;
                }

                CelType celType = CelType.Cel;
                foreach(KeyValuePair<CelType, Vector> posForDoorKeyValue in posForDoor){ //set type for door.
                    if(posForDoorKeyValue.Value.x == x && posForDoorKeyValue.Value.y == y){
                        celType = posForDoorKeyValue.Key;
                    }
                }

                if(celType == CelType.Cel){
                    foreach(Vector posForMobSpawner in posCelForMobSpawner){ //set type for mob spawner.
                        if(posForMobSpawner.x == x && posForMobSpawner.y == y){
                            celType = CelType.Cel_MobSpawner;
                        }
                    }
                }

                if(celType == CelType.Cel && roomType==RoomType.Room_Center && x==midWidthMax && y==midHeightMax){
                    celType = CelType.Cel_CenterRoom; //print center room for spawn stage cel.
                }

                //instancie cel.
                cels[cels.Count-1].Add(
                    x,
                    new Cel(
                        celType,
                        new(x, y)
                    )
                );

        	}
        }

    }


    //set state active of all entity cel of the room.
    public void editActiveCels(bool editActive)
    {
        cels.ForEach((celLine) => { //loop on line cel (y).
            foreach(KeyValuePair<int, Cel> cel in celLine) //loop on column cel (x).
            {
                cel.Value.isActive = editActive; //edit isActive.
            }
        });
    }


    //cast a vector (index pos cel in list) into a pos world of the cel.
    public static Vector getPosAtIndexCelRoom(Vector indexPosInRoom)
    {
        return indexPosInRoom * 126;
    }

    //ask if a pos index is a cel odd or not.
    public static bool isOddCel(Vector index)
    {
        return index.x % 2 != index.y % 2;
    }


    //get cel by index pos.
    public Cel? getCel(Vector indexPosCel)
    {
        try{
            return cels[(int)indexPosCel.y][(int)indexPosCel.x]; 
        }catch(KeyNotFoundException){
            return null;
        }
    }


    //return the first cel in a room (find by typeCel).
    public Cel getCelByType(CelType celTypeSearch)
    {
        for(int i=0; i<cels.Count; i++){
            foreach(KeyValuePair<int, Cel> keyValuePairCel in cels[i]){
                if(keyValuePairCel.Value.celType == celTypeSearch){
                    return keyValuePairCel.Value;
                }
            }
        }
        throw new Exception("GetCelByType Cel not found !");
    }

    //return a list of all cel in a room (matching typeCel).
    public List<Cel> getCelsByType(CelType celTypeSearch)
    {
        List<Cel> output = new();
        for(int i=0; i<cels.Count; i++){
            foreach(KeyValuePair<int, Cel> keyValuePairCel in cels[i]){
                if(keyValuePairCel.Value.celType == celTypeSearch){
                    output.Add(keyValuePairCel.Value);
                }
            }
        }
        return output;
    }


    //return the sprite type for mini map (depend on door).
    public SpriteType getSpriteTypeOfMiniMap()
    {
        //cast 4 bool door into an int (byte).
        int doorByte = (
            ((isARoomTop  )? 1: 0) +
            ((isARoomRight)? 2: 0) +
            ((isARoomDown )? 4: 0) +
            ((isARoomLeft )? 8: 0)
        );

        switch(doorByte){
            case(0):
                return SpriteType.MiniMapUi_RoomBG_;
            case(1):
                return SpriteType.MiniMapUi_RoomBG_U;
            case(2):
                return SpriteType.MiniMapUi_RoomBG_R;
            case(3):
                return SpriteType.MiniMapUi_RoomBG_UR;
            case(4):
                return SpriteType.MiniMapUi_RoomBG_D;
            case(5):
                return SpriteType.MiniMapUi_RoomBG_UD;
            case(6):
                return SpriteType.MiniMapUi_RoomBG_RD;
            case(7):
                return SpriteType.MiniMapUi_RoomBG_URD;
            case(8):
                return SpriteType.MiniMapUi_RoomBG_L;
            case(9):
                return SpriteType.MiniMapUi_RoomBG_UL;
            case(10):
                return SpriteType.MiniMapUi_RoomBG_RL;
            case(11):
                return SpriteType.MiniMapUi_RoomBG_URL;
            case(12):
                return SpriteType.MiniMapUi_RoomBG_DL;
            case(13):
                return SpriteType.MiniMapUi_RoomBG_UDL;
            case(14):
                return SpriteType.MiniMapUi_RoomBG_RDL;
            case(15):
                return SpriteType.MiniMapUi_RoomBG_URDL;
            default:
                throw new Exception("SpriteTypeOfMiniMap generate a door byte invalide !");
        }
    }


    //return sprite type of the type room (for minimap).
    public SpriteType? getSpriteTypeOfMiniMapTypeRoom()
    {
        switch(roomType){
            //case(RoomType.Room_Center):
            //    return SpriteType.MiniMapUI_RoomCenter;
            case(RoomType.Room_Chest):
                return SpriteType.MiniMapUI_RoomChest;
            case(RoomType.Room_Boss):
                return SpriteType.MiniMapUI_RoomBoss;
            case(RoomType.Room_Boost):
                return SpriteType.MiniMapUI_RoomBoost;
            case(RoomType.Room_Fusion):
                return SpriteType.MiniMapUI_RoomFusion;
            default:
                return null;
        }
    }


    //free room.
    public void destroyRoom()
    {

        EntityManager.removeManyEntities(
            cels.SelectMany((dicoC) => 
                dicoC.ToList().Select((kvpC) => (Entity)kvpC.Value)
            ).ToList()
        );

        cels = new();

        _typeMobToSpawn = new();
    }

}
