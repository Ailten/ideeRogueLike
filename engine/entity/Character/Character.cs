
public class Character : Entity
{

    public int HP; //Health Point.
    public int HPmax = 10;

    public int AP; //Action Point.
    public int APmax = 3;

    public int MP; //Movement Point.
    public int MPmax = 3;

    public int SP = 0; //Shild Point.

    public Vector indexPosCel = new(); //position of character in room.

    public bool isInRedTeam; //team in a fight (red of blue).

    public Character? invokedBy = null; //when the character was invoked by another one.

    public Deck deck = new(); //deck of all card.


    public Character(SpriteType spriteType, Vector posIndexCel) : base(RunLayer.layer.idLayer, spriteType)
    {
        this.size = new(126, 126);

        moveTo(posIndexCel); //apply pos.

        this.zIndex = 1200;

        this.HP = this.HPmax;
        this.AP = this.APmax;
        this.MP = this.MPmax;
    }


    //apply damage to another Character.
    public void makeDamage(Character target, int atk)
    {
        target.takeDamage(atk, this);
    }

    //apply damage to character.
    private void takeDamage(int atk, Character? characterMakeAtk=null)
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
        int damageAplyToSP = Math.Min(atk, SP);
        SP -= damageAplyToSP;
    }

    //apply damage to heath point.
    private void takeDamageToHP(int atk, Character? characterMakeAtk=null)
    {
        int damageAplyToHP = Math.Min(atk, HP);
        HP -= damageAplyToHP;

        if(HP <= 0){
            death(characterMakeAtk);
        }
    }

    //call when character dead.
    public void death(Character? characterMakeKill=null)
    {
        //hidde.
        isActive = false;

        //execute death of every entity in turn has invoc by this one.
        TurnManager.getAllInvocOfACharacter(this).ForEach((c) => c.death());
        
        //remove from list turn.
        TurnManager.removeCharacterInRoom(this);

        //TODO : drop.
        //TODO : xp gain.
    }


    //do the turn of a character (event when it's the turn of this character).
    public virtual void turn(){}

    //make skip turn of the character.
    public void skipTurn()
    {
        if(TurnManager.getCharacterOfCurrentTurn().idEntity != idEntity) //can't skip turn if is not the turn of this entity.
            return;

        TurnManager.moveCharacterIndex(); //switch to next entity turn.
    }

    //make turn start for the character.
    public void startTurn()
    {
        AP = APmax; //refill AP.
        
        SP = 0; //reset shild.

        deck.piocheOfStartTurn();
    }


    //update turn of character (call by turn manager).
    public virtual void updateTurn()
    {
        if(WalkManager.isWalking){
            WalkManager.updateWalk();
            return;
        }

        //TODO : other stuff.
    }


    //move character to a vector index pos.
    public void moveTo(Vector indexPos)
    {
        indexPosCel = indexPos;
        pos = Room.getPosAtIndexCelRoom(indexPos);
    }

}