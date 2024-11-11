
public class Room
{

    public int stage;
    public RoomType roomType;

    public List<Dictionary<int, Cel>> cels = new();

    public bool isARoomTop;
    public bool isARoomRight;
    public bool isARoomDown;
    public bool isARoomLeft;

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
                generateRoom(5);
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
        const int width = 15;
        const int height = 15;

        if(roomType == RoomType.Room_Tuto){ //room tuto.

            cels = new();
            for(int y=0; y<height; y++){
                cels.Add(new());
            	for(int x=0; x<width; x++){

                    int distToCenterX = Math.Abs(x - (width+1)/2); //eval dist.
                    int distToCenterY = Math.Abs(y - (height+1)/2);
                    int dist = Math.Max(distToCenterX, distToCenterY);

                    if(x == (width+1)/2 && y == (height+1)/2 - (rayonSpread+1)){ //map a cel up to exit stage tuto.
                        cels[cels.Count-1].Add(
                            x,
                            new Cel(
                                CelType.Cel_NextStage,
                                x, y
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
                            x, y
                        )
                    );

            	}
            }

            return;
        }

        List<List<bool>> celsBool = new();

        //generate default grid full of false.
        for(int y=0; y<height; y++){
            celsBool.Add(new());
        	for(int x=0; x<width; x++){
        		celsBool[celsBool.Count-1].Add(false);
        	}
        }

        //loop spreading, decreasing the amount of rng.
        for(int r=0; r<rayonSpread; r++){
	        for(int y=0; y<height; y++){
	        	for(int x=0; x<width; x++){
                    int distToCenterX = Math.Abs(x - (width+1)/2);
                    int distToCenterY = Math.Abs(y - (height+1)/2);
                    int dist = Math.Max(distToCenterX, distToCenterY);
                    if(dist != r) //skip if not the current circle target at this loop.
                        continue;
                    int rngCeil = 1000 - (int)(900 * ((float)r / (rayonSpread-1)));
                    int rngGet = RunManager.rngSeed.Next(1000);
                    if(rngGet > rngCeil) //skip if cell rng say no.
                        continue;
                    int countAdj = (
                        ((y>0 && celsBool[y-1][x])? 1: 0) +
                        ((y<(height-1) && celsBool[y+1][x])? 1: 0) +
                        ((x>0 && celsBool[y][x-1])? 1: 0) +
                        ((x<(width-1) && celsBool[y][x+1])? 1: 0)
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

            for(int y=0; y<height; y++){ //find door to room up. --->

                posInLine = new();

            	for(int x=0; x<width; x++){

                    int xcast = ( //remap x and y for scroll order.
                        (d==0)? x: //int x=0; x<width; x++
                        (d==1)? y: //int y=0; y<height; y++
                        (d==2)? (width-1)-x: //x=width-1; x>=0; x--
                        (height-1)-y //int y=height-1; y>=0; y--
                    );
                    int ycast = (
                        (d==0)? y: //int y=0; y<height; y++
                        (d==1)? (width-1)-x: //x=width-1; x>=0; x--
                        (d==2)? (height-1)-y: //int y=height-1; y>=0; y--
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
        for(int y=0; y<height; y++){
            cels.Add(new());
        	for(int x=0; x<width; x++){
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
                        x, y
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

}
