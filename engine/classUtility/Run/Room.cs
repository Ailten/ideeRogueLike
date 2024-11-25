
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
        get { return (_widthMax+1)/2; }
    }
    private static int _heightMax = 15;
    public static int heightMax
    {
        get { return _heightMax; }
    }
    public static int midHeightMax
    {
        get { return (_heightMax+1)/2; }
    }


    public Room(bool isARoomTop, bool isARoomRight, bool isARoomDown, bool isARoomLeft, int stage, RoomType roomType)
    {
        this.stage = stage;
        this.roomType = roomType;

        this.isARoomTop = isARoomTop;
        this.isARoomRight = isARoomRight;
        this.isARoomDown = isARoomDown;
        this.isARoomLeft = isARoomLeft;

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
                generateRoom(6);
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

        if(roomType == RoomType.Room_Tuto){ //room tuto.

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

        List<List<bool>> celsBool = new();

        //generate default grid full of false.
        for(int y=0; y<heightMax; y++){
            celsBool.Add(new());
        	for(int x=0; x<widthMax; x++){
        		celsBool[celsBool.Count-1].Add(false);
        	}
        }

        //loop spreading, decreasing the amount of rng.
        for(int r=0; r<rayonSpread; r++){
	        for(int y=0; y<heightMax; y++){
	        	for(int x=0; x<widthMax; x++){
                    int distToCenterX = Math.Abs(x - midWidthMax);
                    int distToCenterY = Math.Abs(y - midHeightMax);
                    int dist = Math.Max(distToCenterX, distToCenterY);
                    if(dist != r) //skip if not the current circle target at this loop.
                        continue;
                    int rngCeil = 1000 - (int)(900 * ((float)r / (rayonSpread-1)));
                    int rngGet = RunManager.rngSeed.Next(1000);
                    if(rngGet > rngCeil) //skip if cell rng say no.
                        continue;
                    int countAdj = (
                        ((y>0 && celsBool[y-1][x])? 1: 0) +
                        ((y<(heightMax-1) && celsBool[y+1][x])? 1: 0) +
                        ((x>0 && celsBool[y][x-1])? 1: 0) +
                        ((x<(widthMax-1) && celsBool[y][x+1])? 1: 0)
                    );
                    if(countAdj < 1 && dist != 0) //skip if cell is not adjacent to another valid.
                        continue;
                    celsBool[y][x] = true;
	        	}
	        }
        }

        //get door for other room.
        Dictionary<int, Vector> posForDoor = new();
        List<Vector> posInLine = new();

        for(int d=0; d<4; d++){ //loop on 4 direction.

            bool isNeedThisDoor = ( //skip this direction if no need door for it.
                (d==0)? isARoomTop: //up.
                (d==1)? isARoomRight: //right.
                (d==2)? isARoomDown: //down.
                isARoomLeft //left.
            );
            if(!isNeedThisDoor)
                continue;

            for(int y=0; y<heightMax; y++){ //find door to room up. --->

                posInLine = new();

            	for(int x=0; x<widthMax; x++){

                    int xcast = ( //remap x and y for scroll order.
                        (d==0)? x: //int x=0; x<width; x++
                        (d==1)? y: //int y=0; y<height; y++
                        (d==2)? (widthMax-1)-x: //x=width-1; x>=0; x--
                        (heightMax-1)-y //int y=height-1; y>=0; y--
                    );
                    int ycast = (
                        (d==0)? y: //int y=0; y<height; y++
                        (d==1)? (widthMax-1)-x: //x=width-1; x>=0; x--
                        (d==2)? (heightMax-1)-y: //int y=height-1; y>=0; y--
                        x //int x=0; x<width; x++
                    );

                    if(celsBool[ycast][xcast])
                        posInLine.Add(new(xcast, ycast));
                }
                if(posInLine.Count == 0)
                    continue;

                for(int i=0; i<posInLine.Count; i++){ //pop of line if already assigned to another door.
                    foreach(KeyValuePair<int, Vector> posForDoorKeyValue in posForDoor){
                        if(posInLine[i].x == posForDoorKeyValue.Value.x && posInLine[i].y == posForDoorKeyValue.Value.y){
                            posInLine.RemoveAt(i);
                            i--;
                            break;
                        }
                    }
                }
                if(posInLine.Count == 0)
                    continue;

                break; //break loop y, when find a pos for door at this direction.
            }

            posForDoor.Add( //add pos door up.
                d, 
                posInLine[posInLine.Count/2]
            );
        }

        // TODO : add monster if is a normal type room.
        if(roomType == RoomType.Room){

            //TODO.

        }

        //cast celsBool into cels.
        cels = new();
        for(int y=0; y<heightMax; y++){
            cels.Add(new());
        	for(int x=0; x<widthMax; x++){
                if(!celsBool[y][x]){ //skip cel.
                    continue;
                }

                int indexCelType = 0;
                foreach(KeyValuePair<int, Vector> posForDoorKeyValue in posForDoor){ //set type for door.
                    if(posForDoorKeyValue.Value.x == x && posForDoorKeyValue.Value.y == y){
                        indexCelType = posForDoorKeyValue.Key + 1;
                    }
                }

                //instancie cel.
                cels[cels.Count-1].Add(
                    x,
                    new Cel(
                        (CelType)indexCelType,
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


    //get cel by index pos.
    public Cel? getCel(Vector indexPosCel)
    {
        try{
            return cels[(int)indexPosCel.y][(int)indexPosCel.x]; 
        }catch(KeyNotFoundException){
            return null;
        }
    }

}
