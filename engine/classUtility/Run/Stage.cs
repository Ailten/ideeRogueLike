
public class Stage
{
    public int stage;
    public List<Dictionary<int, Room>> rooms = new();
    public Vector currentIndexRoom = new();

    public Stage(int stage)
    {
        this.stage = stage;

        switch(stage){

            case(0): //stage without ennemy, light for begun (tuto single room).
                generateStage(1);
                break;

            case(1):
                generateStage(3);
                break;

            case(2):
                generateStage(4);
                break;

            case(3):
                generateStage(4);
                break;

            case(4):
                generateStage(5);
                break;

            case(5):
                generateStage(5);
                break;

        }

    }


    //generate a random stage.
    private void generateStage(int rayonSpread)
    {
        const int width = 15; //max size.
        const int height = 15;
        currentIndexRoom = new((width+1)/2, (height+1)/2);

        if(rayonSpread == 1){ //stage tuto.

            rooms = new();
            for(int y=0; y<height; y++){
                rooms.Add(new());

                if(y == (height+1)/2){
                    rooms[rooms.Count-1].Add(
                        (width+1)/2,
                        new Room(
                            false, false, false, false, //room in 4 direction.
                            stage, //stage.
                            RoomType.Room_Tuto
                        )
                    );
                }

            }

            return;
        }

        List<List<bool>> roomsBool = new();

        //generate default grid full of false.
        for(int y=0; y<height; y++){
            roomsBool.Add(new());
        	for(int x=0; x<width; x++){
        		roomsBool[roomsBool.Count-1].Add(false);
        	}
        }

        //loop spreading, decreasing the amount of rng.
        for(int r=0; r<rayonSpread; r++){
	        for(int y=0; y<height; y++){
	        	for(int x=0; x<width; x++){
                    int distToCenterX = Math.Abs(x - (width+1)/2);
                    int distToCenterY = Math.Abs(y - (height+1)/2);
                    int dist = distToCenterX + distToCenterY;
                    if(dist != r) //skip if not the current circle target at this loop.
                        continue;
                    int rngCeil = 1000 - (int)(600 * ((float)r / (rayonSpread-1)));
                    int rngGet = RunManager.rngSeed.Next(1000);
                    if(rngGet > rngCeil) //skip if cell rng say no.
                        continue;
                    int countAdj = (
                        ((y>0 && roomsBool[y-1][x])? 1: 0) +
                        ((y<(height-1) && roomsBool[y+1][x])? 1: 0) +
                        ((x>0 && roomsBool[y][x-1])? 1: 0) +
                        ((x<(width-1) && roomsBool[y][x+1])? 1: 0)
                    );
                    if(countAdj < 1 && dist != 0) //skip if cell is not adjacent to another valid.
                        continue;
                    roomsBool[y][x] = true;
	        	}
	        }
        }

        //list of border room (for special room).
        List<Vector> posForSpecialRoom = new();
        for(int y=0; y<height; y++){
        	for(int x=0; x<width; x++){
                if(!roomsBool[y][x]) //skip if not a room.
                    continue;
                int countAdj = (
                    ((y>0 && roomsBool[y-1][x])? 1: 0) +
                    ((y<(height-1) && roomsBool[y+1][x])? 1: 0) +
                    ((x>0 && roomsBool[y][x-1])? 1: 0) +
                    ((x<(width-1) && roomsBool[y][x+1])? 1: 0)
                );
                if(countAdj <= 2) //add in room can be special.
                    posForSpecialRoom.Add(new(x, y));
            }
        }

        //no need to verify if as anefor in list, until use 4 or less.

        //random order the list.
        posForSpecialRoom = posForSpecialRoom.OrderBy(_ => RunManager.rngSeed.Next(0, 100) < 50).ToList();

        //cast roomBool into rooms.
        rooms = new();
        for(int y=0; y<height; y++){
            rooms.Add(new());
        	for(int x=0; x<width; x++){
                if(!roomsBool[y][x]){ //no room.
                    continue;
                }
                
                int roomTypeIndex = 0; //eval room type.
                for(int i=0; i<4; i++){
                    if(posForSpecialRoom[i].x == x && posForSpecialRoom[i].y == y)
                        roomTypeIndex = i + 1;
                }

                if(y == (height+1)/2 && x == (width+1)/2) //center room type.
                    roomTypeIndex = (int)RoomType.Room_Center;

        		rooms[rooms.Count-1].Add(
                    x,
                    new Room(
                        (y>0 && roomsBool[y-1][x]), //is room up.
                        (x<(width-1) && roomsBool[y][x+1]), //is room right.
                        (y<(height-1) && roomsBool[y+1][x]), //is room down.
                        (x>0 && roomsBool[y][x-1]), //is room left.
                        stage, //stage.
                        (RoomType)roomTypeIndex
                    )
                );
        	}
        }
        
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