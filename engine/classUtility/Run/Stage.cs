
public class Stage
{
    public int stage;
    public List<Dictionary<int, Room>> rooms = new();
    public Vector currentIndexRoom = new();

    public List<Vector> roomsWalked = new(){ new(midWidthMax, midHeightMax) };

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


    public Room? currentRoom
    {
        get { 
            try{
                return rooms[(int)currentIndexRoom.y][(int)currentIndexRoom.x]; 
            }catch(KeyNotFoundException){
                return null;
            }
        }
    }

    public Room currentRoomNN
    {
        get {
            return currentRoom ?? throw new Exception("currentRoom is null !");
        }
    }

    public Stage(int stage, int seed)
    {

        this.stage = stage;
        this.seed = seed;

        switch(stage){

            case(0): //stage without ennemy, light for begun (tuto single room).
                this.rayonSpread = 1;
                break;

            case(1):
                this.rayonSpread = 4;
                break;

            case(2):
                this.rayonSpread = 5;
                break;

            case(3):
                this.rayonSpread = 5;
                break;

            case(4):
                this.rayonSpread = 6;
                break;

            case(5):
                this.rayonSpread = 6;
                break;

        }

    }


    //generate a random stage.
    public void generateStage()
    {

        _rngSeed = new Random(seed);

        currentIndexRoom = new(midWidthMax, midHeightMax);

        if(rayonSpread == 1){ //stage tuto.

            rooms = new();
            for(int y=0; y<heightMax; y++){
                rooms.Add(new());

                if(y == midHeightMax){
                    rooms[rooms.Count-1].Add(
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

        List<List<bool>> roomsBool = new();

        //generate default grid full of false.
        for(int y=0; y<heightMax; y++){
            roomsBool.Add(new());
        	for(int x=0; x<widthMax; x++){
        		roomsBool[roomsBool.Count-1].Add(false);
        	}
        }

        //loop spreading, decreasing the amount of rng.
        for(int r=0; r<rayonSpread; r++){
	        for(int y=0; y<heightMax; y++){
	        	for(int x=0; x<widthMax; x++){
                    int distToCenterX = Math.Abs(x - midWidthMax);
                    int distToCenterY = Math.Abs(y - midHeightMax);
                    int dist = distToCenterX + distToCenterY;
                    if(dist != r) //skip if not the current circle target at this loop.
                        continue;

                    const int radiusSafe = 1; //RNG.
                    float rayonI = Math.Clamp((float)(r-radiusSafe) / (rayonSpread-1-radiusSafe), 0f, 1f);
                    int rngCeil = 1000 - (int)(800 * rayonI); 
                    int rngGet = rngSeed.Next(1000);

                    if(rngGet > rngCeil) //skip if cell rng say no.
                        continue;
                    int countAdj = (
                        ((y>0 && roomsBool[y-1][x])? 1: 0) +
                        ((y<(heightMax-1) && roomsBool[y+1][x])? 1: 0) +
                        ((x>0 && roomsBool[y][x-1])? 1: 0) +
                        ((x<(widthMax-1) && roomsBool[y][x+1])? 1: 0)
                    );
                    if(countAdj < 1 && dist != 0) //skip if cell is not adjacent to another valid.
                        continue;
                    roomsBool[y][x] = true;
	        	}
	        }
        }

        //list of border room (for special room).
        List<Vector> posForSpecialRoom = new();
        for(int y=0; y<heightMax; y++){
        	for(int x=0; x<widthMax; x++){
                if(!roomsBool[y][x]) //skip if not a room.
                    continue;
                int countAdj = (
                    ((y>0 && roomsBool[y-1][x])? 1: 0) +
                    ((y<(heightMax-1) && roomsBool[y+1][x])? 1: 0) +
                    ((x>0 && roomsBool[y][x-1])? 1: 0) +
                    ((x<(widthMax-1) && roomsBool[y][x+1])? 1: 0)
                );
                if(countAdj <= 2) //add in room can be special.
                    posForSpecialRoom.Add(new(x, y));
            }
        }

        //no need to verify if as anefor in list, until use 4 or less.

        //random order the list.
        posForSpecialRoom = posForSpecialRoom.OrderBy(_ => rngSeed.Next(0, 100) < 50).ToList();

        //cast roomBool into rooms.
        rooms = new();
        for(int y=0; y<heightMax; y++){
            rooms.Add(new());
        	for(int x=0; x<widthMax; x++){
                if(!roomsBool[y][x]){ //no room.
                    continue;
                }
                
                int roomTypeIndex = 0; //eval room type.
                for(int i=0; i<4; i++){
                    if(i >= posForSpecialRoom.Count)
                        break;
                    if(posForSpecialRoom[i].x == x && posForSpecialRoom[i].y == y)
                        roomTypeIndex = i + 1;
                }

                if(y == midHeightMax && x == midWidthMax) //center room type.
                    roomTypeIndex = (int)RoomType.Room_Center;

        		rooms[rooms.Count-1].Add(
                    x,
                    new Room(
                        (y>0 && roomsBool[y-1][x]), //is room up.
                        (x<(widthMax-1) && roomsBool[y][x+1]), //is room right.
                        (y<(heightMax-1) && roomsBool[y+1][x]), //is room down.
                        (x>0 && roomsBool[y][x-1]), //is room left.
                        stage, //stage.
                        (RoomType)roomTypeIndex,
                        rngSeed.Next()
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

        if(isFirstWalkOnThisRoom){

            roomsWalked.Add(currentIndexRoom); //add in list walked.

            //spawn mobs if has one in room.
            if(currentRoomNN.isHasMob){

                //unable button skip turn.
                RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(false);

                //spawn mobs.
                for(int i=0; i<currentRoomNN.typeMobToSpawn.Count; i++){
                    SpriteType spriteType = currentRoomNN.typeMobToSpawn[i];
                    Vector posIndexCel = currentRoomNN.getCelsByType(CelType.Cel_MobSpawner)[i].indexPosCel;
                    Character newMobSpawn = CharacterMob.init(spriteType, posIndexCel);
                    TurnManager.addCharacterInRoom(newMobSpawn);
                }

            }

        }
    }

    //return true if the vector ask is in list.
    public bool isContainInRoomsWalked(Vector indexPosAsk)
    {
        for(int i=0; i<roomsWalked.Count; i++){
            if(roomsWalked[i].x == indexPosAsk.x && roomsWalked[i].y == indexPosAsk.y){
                return true;
            }
        }
        return false;
    }

    //return the distance from the the closest room walk.
    public int getClosestDistFromWalked(Vector indexPosAsk)
    {
        int closestDist = 1000;
        for(int i=0; i<roomsWalked.Count; i++){
            int dist = (
                Math.Abs((int)roomsWalked[i].x - (int)indexPosAsk.x) +
                Math.Abs((int)roomsWalked[i].y - (int)indexPosAsk.y)
            );
            if(dist < closestDist)
                closestDist = dist;
        }
        return closestDist;
    }


    //get a room by index.
    public Room? getRoom(Vector indexRoom)
    {
        try{
            return rooms[(int)indexRoom.y][(int)indexRoom.x]; 
        }catch(KeyNotFoundException){
            return null;
        }
    }


    //reset the obj stage (oposite of generate).
    public void destroyStage()
    {
        rooms.ForEach((dicoR) => 
            dicoR.ToList().ForEach((kvpR) => {
                kvpR.Value.destroyRoom();
            })
        );

        rooms = new();
        roomsWalked = new(){ new(midWidthMax, midHeightMax) };
    }


    /* --- demo JS.

let stage = [];

for(let y=0; y<15; y++){
    stage.push([]);
	for(let x=0; x<15; x++){
		stage[stage.length-1].push(' ');
	}
}

for(let i=0; i<5; i++){
	for(let y=0; y<15; y++){
		for(let x=0; x<15; x++){
            let distToCenterX = Math.abs(x - 8);
            let distToCenterY = Math.abs(y - 8);
            let dist = distToCenterX + distToCenterY;
            if(dist != i)
                continue;
            let rng = 100 - dist * 12;
            if(Math.random() > rng/100)
                continue;
            let countAdj = (
                ((stage[y-1][x] == '#')? 1: 0) +
                ((stage[y+1][x] == '#')? 1: 0) +
                ((stage[y][x-1] == '#')? 1: 0) +
                ((stage[y][x+1] == '#')? 1: 0)
            );
            if(countAdj < 1 && dist != 0)
                continue;
            stage[y][x] = '#';
		}
	}
}

let printStage = '';
for(let y=0; y<15; y++){
    for(let x=0; x<15; x++){
        printStage+=stage[y][x];
    }
    printStage+='\n';
}
console.log(printStage);
    
    */

    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############
    //##############

}