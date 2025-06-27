
public class Character : Entity
{

    public int HP; //Health Point.
    public int HPmax = 10;

    public int AP; //Action Point.
    public int APmax = 3;

    public int MP; //Movement Point.
    public int MPmax = 3;

    public int SP = 0; //Shild Point.

    public int PO = 0; //Gold Piece (piece d'or, PO).

    public Vector indexPosCel = new(); //position of character in room.

    public bool isInRedTeam; //team in a fight (red of blue).

    public Character? invokedBy = null; //when the character was invoked by another one.
    public bool isAnInvoc
    {
        get { return invokedBy != null; }
    }
    public bool isAPlayer
    {
        get { return isInRedTeam && !isAnInvoc; }
    }

    public Deck deck = new(); //deck of all card.


    public Character(SpriteType spriteType, Vector posIndexCel) : base(RunLayer.layer.idLayer, spriteType)
    {
        this.size = new(126, 126);

        moveTo(posIndexCel, false);

        this.zIndex = 1200;

        this.HP = this.HPmax;
        this.AP = this.APmax;
        this.MP = this.MPmax;
    }


    //apply damage to another Character.
    public virtual void makeDamage(Character target, int atk)
    {
        target.takeDamage(atk, this);
    }

    //apply damage to character.
    protected virtual void takeDamage(int atk, Character? characterMakeAtk=null)
    {
        if(SP > 0){
            int damageAplyToSP = Math.Min(atk, SP);
            atk -= damageAplyToSP;
            takeDamageShild(damageAplyToSP);
        }

        takeDamageToHP(atk, characterMakeAtk);
    }

    //apply damage to shild.
    private void takeDamageShild(int atk)
    {
        SP -= Math.Min(atk, SP);
    }

    //apply damage to heath point.
    private void takeDamageToHP(int atk, Character? characterMakeAtk=null)
    {
        HP -= Math.Min(atk, HP);

        if(HP <= 0){
            death(characterMakeAtk);
        }
    }

    //call when character dead.
    public virtual void death(Character? characterMakeKill=null)
    {
        //hidde.
        isActive = false;

        //execute death of every entity in turn has invoc by this one.
        TurnManager.getAllInvocOfACharacter(this).ForEach((c) => c.death());
        
        //remove from list turn.
        TurnManager.removeCharacterInRoom(this);

        //TODO : drop of mob, xp gain, kill count ...
    }

    //give shild point.
    public virtual void giveShild(Character target, int shildIncrement)
    {
        target.takeShild(shildIncrement, this);
    }
    //give shild point.
    protected virtual void takeShild(int shildIncrement, Character? characterGiveShild)
    {
        SP += shildIncrement;
    }


    //do the turn of a character (event when it's the turn of this character) use for execute logic mobs.
    public virtual void turn(){}

    //make skip turn of the character.
    public virtual void skipTurn()
    {
        if(TurnManager.getCharacterOfCurrentTurn().idEntity != idEntity) //can't skip turn if is not the turn of this entity.
            return;

        AP = APmax; //refill AP (for next turn).
        MP = MPmax; //refill MP.

        deck.discardOfEndTurn(); //drop full hands in cimetiere.

        TurnManager.moveCharacterIndexToNextCharacter(); //switch to next entity turn.
    }

    //make turn start for the character.
    public virtual void startTurn()
    {
        SP = 0; //reset shild.

        deck.piocheOfStartTurn();

        RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(!this.isInRedTeam); //set button skip turn enable or disable based on the team.

        //make an FX sparkle (and sound) for signal start turn.
        new FxTurnOn(this.pos);
    }


    //update turn of character (call by turn manager).
    public virtual void updateTurn()
    {
        if(WalkManager.isWalking){
            WalkManager.updateWalk();
            return;
        }

        //if not in an animation, do logic turn.
        this.turn();
    }


    //move character to a vector index pos.
    public void moveTo(Vector indexPos, bool isActionCel=true)
    {
        indexPosCel = indexPos;
        pos = Room.getPosAtIndexCelRoom(indexPos);

        if(isActionCel)
            RunManager.getCelNN(indexPos).doActionTypeCel(this);
    }

    //decrease MP.
    public void decreaseMP(int decrease)
    {
        MP = Math.Max(MP - decrease, 0);
    }

}