
public static class TurnManager
{

    private static List<Character> allCharacterInRoom = new();
    private static int indexCharacterTurn = 0;
    public static int getIndexCharacterTurn
    {
        get { return indexCharacterTurn; }
    }

    private static List<Character> allCharacterDead = new();
    private static bool _isInFight = false;
    public static bool isInFight
    {
        get { return _isInFight; }
    }

    private static int turnCount = 0;
    public static int getTurnCount
    {
        get { return turnCount; }
    }


    //reset manager.
    public static void reset()
    {
        allCharacterInRoom = new();
        indexCharacterTurn = 0;
        allCharacterDead = new();
        _isInFight = false;
        turnCount = 0;
    }


    //add a new character in list turn.
    public static void addCharacterInRoom(Character newCharacter)
    {
        allCharacterInRoom.Add(newCharacter);

        if (!isInFight)
            verifyIfFightIsStart();
    }
    public static void addCharacterNextTo(Character newCharacter, int idCharacterBefore)
    {
        for (int i = 0; i < allCharacterInRoom.Count; i++)
        {
            if (allCharacterInRoom[i].idEntity == idCharacterBefore)
            {
                allCharacterInRoom.Insert(i+1, newCharacter); //add at specific index.
                return;
            }
        }
        throw new Exception("Character not found in turn list !");
    }

    //remove a character from list turn.
    public static void removeCharacterInRoom(Character character)
    {
        for (int i = 0; i < allCharacterInRoom.Count; i++)
        {
            if (allCharacterInRoom[i].idEntity == character.idEntity)
            {

                allCharacterDead.Add(allCharacterInRoom[i]); //pool of dead character.
                allCharacterInRoom[i].isActive = false;
                allCharacterInRoom[i].indexPosCel = new(-1, -1);

                allCharacterInRoom.RemoveAt(i); //remove character.

                verifyIfFightIsEnd(); //check end fight.

                if (i < indexCharacterTurn) //replace index at right place.
                    moveCharacterIndex(-1);
                return;
            }
        }
        throw new Exception("Character not found in turn list !");
    }

    //move index character turn to next character.
    public static void moveCharacterIndex(int movement = 1)
    {
        indexCharacterTurn = (indexCharacterTurn + movement + allCharacterInRoom.Count) % allCharacterInRoom.Count;
    }

    //event call when a character skip his turn.
    public static void moveCharacterIndexToNextCharacter()
    {
        moveCharacterIndex(); //move index.
        getCharacterOfCurrentTurn().startTurn(); //call event start turn.
    }

    // edit turn count if is a new table turn.
    public static void turnCountEdit()
    {
        if (indexCharacterTurn == allCharacterInRoom.Count - 1)
            turnCount++;
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
        foreach (Character c in allCharacterInRoom)
        {
            if (teamFind == null)
            { //first.
                teamFind = c.isInRedTeam;
                continue;
            }
            if (c.isInRedTeam != teamFind)
            { //fight not finish.
                return;
            }
        }

        _isInFight = false;

        enfOfFight(teamFind ?? false);

        Deck deckOfMainPlayer = getMainPlayerCharacter().deck; //discard hand at end fight (every card of deck on cimetier).
        deckOfMainPlayer.discardOfEndTurn();
        deckOfMainPlayer.pushAllCardPiocheIntoCimetier();
    }

    //verify if the fight is start.
    private static void verifyIfFightIsStart()
    {
        bool? teamFind = null;
        foreach (Character c in allCharacterInRoom)
        {
            if (teamFind == null)
            { //first.
                teamFind = c.isInRedTeam;
                continue;
            }
            if (c.isInRedTeam != teamFind)
            { //fight start.
                _isInFight = true;

                getMainPlayerCharacter().deck.piocheOfStartTurn(); //pioche first hands of fight.
                RunHudLayer.layer.cardHandListCardUi!.setListCard(getMainPlayerCharacter().deck.cardsInHand); //set card list hand to UI.

                turnCount = 0; //reset turn count.

                return;
            }
        }
    }

    //call when a fight is end.
    private static void enfOfFight(bool isRedWiner)
    {
        // apply effects.

        if (isRedWiner) //player win the fight.
        {

            //TODO. (like increment fight win, or loot).

            deathAllInvoc(); //death all invoc.

            //reset all stats player when fight is end.
            Character player = getMainPlayerCharacter();
            player.HP = player.HPmax; //to think : maybe change refill life every all fight.
            player.AP = player.APmax;
            player.MP = player.MPmax;
            player.SP = 0;

            //disable button skip turn.
            RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(false);

            //clean all cel traps.
            RunManager.currentRoom!.cleanTraps();

            if (RunManager.currentRoom!.roomType == RoomType.Room_Boss) //if is a room boss, spawn roope for next stage.
            {
                RunManager.getCelNNCenter().celType = CelType.Cel_NextStage; //spawn roope on cel center of room.
            }

            // apply effects.
            player.statusEffects.ForEach(e => e.eventWhenPlayerWinFight());

        }
        else //ennemy win the fight.
        {

            //disable button skip turn.
            RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(true);

            // event end run.
            RunManager.endRun(false);

        }

        endAllStatusEffectByEndFight();

        cleanPoolDeadCharacter();
    }

    //make all invocation death.
    private static void deathAllInvoc()
    {
        allCharacterInRoom.ForEach((c) =>
        {
            if (c.invokedBy != null)
                c.death();
        });
    }

    //free every character dead in turn.
    private static void cleanPoolDeadCharacter()
    {
        allCharacterDead.ForEach((c) =>
        {

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


    // end status effect of all character (by skip turn character who apply skip turn).
    public static void endAllStatusEffectBySkipTurn(int characterIdWhoApplyEffect)
    {
        allCharacterInRoom.ForEach(c =>
        {
            for (int i = c.statusEffects.Count - 1; i >= 0; i--)
            {
                if (c.statusEffects[i].getCharacterIdWhoApplyEffect != characterIdWhoApplyEffect)
                    continue; // skip if not apply by the same character.
                if (c.statusEffects[i].getTurnEnd == -1)
                    continue; // effect infinit.
                if (c.statusEffects[i].getTurnEnd > turnCount)
                    continue; // effect life turn is not expired.

                // drop the status effect.
                c.statusEffects[i].eventWhenStatusEffectDisapear(isEndLifeEffect: true);
                c.dropAStatusEffectByIndex(i);
            }
        });
    }
    // end status effect when fight end.
    private static void endAllStatusEffectByEndFight()
    {
        allCharacterInRoom.ForEach(c =>
        {
            for (int i = c.statusEffects.Count - 1; i >= 0; i--)
            {
                if (c.statusEffects[i].getTurnEnd == -1)
                    continue; // skip all effect apply for infinit turn.

                // drop the status effect.
                c.statusEffects[i].eventWhenStatusEffectDisapear(isEndOfFight: true);
                c.dropAStatusEffectByIndex(i);
            }
        });
    }
    // end status effect of the character who die and all apply by this character.
    public static void endAllStatusEffectWhenCharacterDie(Character characterWhoDie)
    {
        // end effect of him self.
        for (int i = characterWhoDie.statusEffects.Count - 1; i >= 0; i--)
        {
            characterWhoDie.statusEffects[i].eventWhenStatusEffectDisapear(isCharacterWhoHasEffectDie: true);
            characterWhoDie.dropAStatusEffectByIndex(i);
        }

        // end effect of other character.
        allCharacterInRoom.ForEach(c =>
        {
            for (int i = c.statusEffects.Count - 1; i >= 0; i--)
            {
                if (c.statusEffects[i].getCharacterIdWhoApplyEffect != characterWhoDie.idEntity)
                    continue; // skip if not apply by the same character.

                // drop the status effect.
                c.statusEffects[i].eventWhenStatusEffectDisapear(isCharacterWhoApplyEffectDie: true);
                c.dropAStatusEffectByIndex(i);
            }
        });

        // edit trigger zone of status effect ui currently print.
        RunHudLayer.layer.statusEffectUi!.updateGeometryTriggerBasedOnList();
    }


}