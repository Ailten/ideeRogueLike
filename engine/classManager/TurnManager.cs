
public static class TurnManager
{

    private static List<Character> allCharacterInRoom = new();
    private static int indexCharacterTurn = 0;

    private static List<Character> allCharacterDead = new();
    private static bool _isInFight = false;
    public static bool isInFight
    {
        get { return _isInFight; }
    }


    //reset manager.
    public static void reset()
    {
        allCharacterInRoom = new();
        indexCharacterTurn = 0;
        allCharacterDead = new();
        _isInFight = false;
    }


    //add a new character in list turn.
    public static void addCharacterInRoom(Character newCharacter)
    {
        allCharacterInRoom.Add(newCharacter);

        if(!isInFight)
            verifyIfFightIsStart();
    }
    public static void addCharacterNextTo(Character newCharacter, Character characterBefore)
    {
        for(int i = 0; i < allCharacterInRoom.Count; i++){
            if(allCharacterInRoom[i].idEntity == characterBefore.idEntity){
                allCharacterInRoom.Insert(i, newCharacter); //add at specific index.
                return;
            }
        }
        throw new Exception("Character not found in turn list !");
    }

    //remove a character from list turn.
    public static void removeCharacterInRoom(Character character)
    {
        for(int i = 0; i < allCharacterInRoom.Count; i++){
            if(allCharacterInRoom[i].idEntity == character.idEntity){

                allCharacterDead.Add(allCharacterInRoom[i]); //pool of dead character.
                allCharacterInRoom[i].isActive = false;
                allCharacterInRoom[i].indexPosCel = new(-1, -1);

                allCharacterInRoom.RemoveAt(i); //remove character.

                verifyIfFightIsEnd(); //check end fight.

                if(i > indexCharacterTurn) //replace index at right place.
                    moveCharacterIndex(-1);
                return;
            }
        }
        throw new Exception("Character not found in turn list !");
    }

    //move index character turn to next character.
    public static void moveCharacterIndex(int movement=1)
    {
        indexCharacterTurn = (indexCharacterTurn + movement + allCharacterInRoom.Count) %allCharacterInRoom.Count;
    }

    //event call when a character skip his turn.
    public static void moveCharacterIndexToNextCharacter()
    {
        moveCharacterIndex(); //move index.
        getCharacterOfCurrentTurn().startTurn(); //call event start turn.
    }

    //get a list of all invoc of a specific character.
    public static List<Character> getAllInvocOfACharacter(Character characterInovcator)
    {
        return allCharacterInRoom.Where((c) => c.invokedBy != null && c.invokedBy.idEntity == characterInovcator.idEntity).ToList();
    }


    //verify if the current fight is end.
    private static void verifyIfFightIsEnd()
    {
        bool? teamFind = null;
        foreach(Character c in allCharacterInRoom){
            if(teamFind == null){ //first.
                teamFind = c.isInRedTeam;
                continue;
            }
            if(c.isInRedTeam != teamFind){ //fight not finish.
                return;
            }
        }

        _isInFight = false;

        enfOfFight(teamFind ?? false);
    }

    //verify if the fight is start.
    private static void verifyIfFightIsStart()
    {
        bool? teamFind = null;
        foreach(Character c in allCharacterInRoom){
            if(teamFind == null){ //first.
                teamFind = c.isInRedTeam;
                continue;
            }
            if(c.isInRedTeam != teamFind){ //fight start.
                _isInFight = true;
                return;
            }
        }
    }

    //call when a fight is end.
    private static void enfOfFight(bool isRedWiner)
    {

        if(isRedWiner){ //player win the fight.

            //TODO. (like increment fight win, or loot).

            deathAllInvoc(); //death all invoc.

        }else{ //ennemy win the fight.
            
            //TODO. (game over screen).

        }

        cleanPoolDeadCharacter();

        //disable button skip turn.
        RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(true);
    }

    //make all invocation death.
    private static void deathAllInvoc()
    {
        allCharacterInRoom.ForEach((c) => {
            if(c.invokedBy != null)
                c.death();
        });
    }

    //free every character dead in turn.
    private static void cleanPoolDeadCharacter()
    {
        allCharacterDead.ForEach((c) => {

            RunLayer.layer.entities.Remove(c); //free entity from list layer.

            EntityManager.entities.Remove(c); //free entity from entity manager.

        });

        allCharacterDead = new(); //reset list dead.
    }

    
    //get entity of current turn.
    public static Character getCharacterOfCurrentTurn()
    {
        return allCharacterInRoom[indexCharacterTurn];
    }


    //update action turn of every character.
    public static void updateTurn()
    {
        allCharacterInRoom[indexCharacterTurn].updateTurn();
    }


    //get character in a pos index.
    public static Character? getCharacterAtIndexPos(Vector posIndexCel)
    {
        return allCharacterInRoom.Find((c) => c.indexPosCel.x == posIndexCel.x && c.indexPosCel.y == posIndexCel.y);
    }


    //get main play character.
    public static Character getMainPlayerCharacter()
    {
        return allCharacterInRoom.Find((c) => c.isAPlayer) ?? throw new Exception("MainPlayer not found !");
    }


    //get all character in room.
    public static List<Character> getAllCharacters()
    {
        return allCharacterInRoom;
    }


}