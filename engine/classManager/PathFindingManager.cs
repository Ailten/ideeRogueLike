
public static class PathFindingManager
{

    public static List<Vector> pathFind = new();
    public static bool isPathValid
    {
        get { return pathFind.Count > 0; }
    }

    private static readonly List<Vector> adjacentSquaresPos = new(){new Vector(0, -1), new Vector(1, 0), new Vector(0, 1), new Vector(-1, 0)}; //4 direction, for move during the path finding.

    //eval a path from a posIndex to anoter.
    public static void evalAPath(Vector posFrom, Vector posTo, int maxMPCost = 100)
    {

        //node start and end.
        PathFindingNode nodeStart = new(){
			parent = null,
			pos = posFrom,
			g = 0, h = 0, f = 0
        };
		PathFindingNode nodeEnd = new(){
			parent = null,
			pos = posTo,
			g = 0, h = 0, f = 0
		};

        //list use during the eval path finding.
		List<PathFindingNode> openList = new();
		List<PathFindingNode> closedList = new();

        //start node in list.
		openList.Add(nodeStart);

        //loop until openList was empty.
		while(openList.Count > 0){

            //get the lowest f score.
            PathFindingNode currentNode = openList[0];
			int currentIndex = 0;
			for(int i=0; i<openList.Count; i++){
				if(openList[i].f < currentNode.f){
					currentNode = openList[i];
					currentIndex = i;
				}
			}

            //transfer node from openList to closedList.
			openList.RemoveAt(currentIndex);
			closedList.Add(currentNode);

            //when path is find.
            if(currentNode.pos.x == nodeEnd.pos.x && currentNode.pos.y == nodeEnd.pos.y){
				pathFind = new();
				PathFindingNode? current = currentNode;
				while(current != null){
					pathFind.Add(current.pos);
					current = current.parent;
				}
                pathFind.Reverse(); //reverce order.
				return;
			}

            //loop for add next node in node tree of path.
            List<PathFindingNode> children = new();
            for(int i=0; i<adjacentSquaresPos.Count; i++){ //loop on 4 direction adjacent.
				Vector nodePos = currentNode.pos + adjacentSquaresPos[i];

                //skip the pos if over range room.
				if(nodePos.x >= Room.widthMax || nodePos.x < 0 || nodePos.y >= Room.heightMax || nodePos.y < 0)
					continue;
				
                //skip the pos if no cel at this pos.
				Cel? cel = RunManager.getCel(nodePos);
				if(cel == null)
					continue;

                //skip the pos if cel is busy (a character in this pos).
                Character? character = TurnManager.getCharacterAtIndexPos(nodePos);
                if(character != null)
                    continue;
				
                //push the new children in node tree.
				children.Add(new(){
					parent = currentNode,
					pos = nodePos,
					g = 0, h = 0, f = 0,
				});

            }

            //loop on children.
            for(int i=0; i<children.Count; i++){
				PathFindingNode child = children[i];
				
				bool isFindMatchContinue = false;
				for(int j=0; j<closedList.Count; j++){
					PathFindingNode closedChild = closedList[j];
					if(child.pos.x == closedChild.pos.x && child.pos.y == closedChild.pos.y){
						isFindMatchContinue = true;
						continue;
					}
				}
				if(isFindMatchContinue)
					continue;

                //number of the cel (from start).
				child.g = currentNode.g + 1;

                //skip the pos if to fare.
                if(child.g > maxMPCost)
                    continue;
				
                //eval number for know if pos is more far than last.
				child.h = (int)Math.Pow(child.pos.x - nodeEnd.pos.x, 2) + (int)Math.Pow(child.pos.y - nodeEnd.pos.y, 2);
				child.f = child.g + child.h;
				
                //skip the pos if closest from the start (walk back).
				for(int j=0; j<openList.Count; j++){
					PathFindingNode openNode = openList[j];
					if(child.pos.x == openNode.pos.x && child.pos.y == openNode.pos.y && child.g > openNode.g){
						isFindMatchContinue = true;
						continue;
					}
				}
				if(isFindMatchContinue)
					continue;
				
				openList.Add(child);
					
			}

        }

        pathFind = new(); //reset path empty if no path found.

    }

}

//class used for node durring an eval of path finding.
public class PathFindingNode
{
    public PathFindingNode? parent = null;
    public Vector pos = new();
    public int g = 0, h, f = 0;
}