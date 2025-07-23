
using Raylib_cs;

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

    public List<StatusEffect> statusEffects = new();
    public virtual string getName
    {
        get{ return this.GetType().ToString().Substring("Character".Length); }
    }


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
    public virtual void makeDamage(Character target, int atk, PackageRefCard? refCard = null)
    {
        target.takeDamage(atk, this, refCard);

        //make an FX star (and sound) for signal make damage.
        FxManager.initOnQueue(FxType.FxStarHit, target.pos);
    }

    //apply damage to character.
    protected virtual void takeDamage(int atk, Character? characterMakeAtk = null, PackageRefCard? refCard = null)
    {
        if (SP > 0)
        {
            int damageAplyToSP = Math.Min(atk, SP);
            atk -= damageAplyToSP;
            takeDamageShild(damageAplyToSP, refCard);

            if (atk == 0)
                return;
        }

        takeDamageToHP(atk, characterMakeAtk, refCard);
    }

    //apply damage to shild.
    private void takeDamageShild(int atk, PackageRefCard? refCard = null)
    {
        int SPdecrement = Math.Min(atk, SP);

        SP -= SPdecrement;

        FxTextHit.initOnlyOneFxAtTime(this.pos, $"-{SPdecrement}", Color.Blue);
    }

    //apply damage to heath point.
    private void takeDamageToHP(int atk, Character? characterMakeAtk = null, PackageRefCard? refCard = null)
    {
        int HPdecrement = Math.Min(atk, HP);
        HP -= HPdecrement;

        FxTextHit.initOnlyOneFxAtTime(this.pos, $"-{HPdecrement}", Color.Red);

        if (HP <= 0)
        {
            death(characterMakeAtk, refCard);
        }
    }

    //call when character dead.
    public virtual void death(Character? characterMakeKill = null, PackageRefCard? refCard = null)
    {
        //hidde.
        isActive = false;

        //execute death of every entity in turn has invoc by this one.
        TurnManager.getAllInvocOfACharacter(this).ForEach((c) => c.death());

        // remove status effect (them self and apply to other).
        TurnManager.endAllStatusEffectWhenCharacterDie(this);

        if (characterMakeKill != null)
        {
            if (this.PO != 0)
                characterMakeKill?.gainGold(this.PO);
        }
        else if (this.isAnInvoc)
        {
            if (this.PO != 0)
                this.invokedBy?.gainGold(this.PO);
        }

        //remove from list turn.
        TurnManager.removeCharacterInRoom(this);

        //TODO : drop of mob, xp gain, kill count ...
    }

    //give shild point.
    public virtual void giveShild(Character target, int shildIncrement, PackageRefCard? refCard = null)
    {
        target.takeShild(shildIncrement, this, refCard);
    }
    //give shild point.
    protected virtual void takeShild(int shildIncrement, Character? characterGiveShild = null, PackageRefCard? refCard = null)
    {
        SP += shildIncrement;

        //make an FX star (and sound) for gain shild.
        FxManager.initOnQueue(FxType.FxShildBuf, this.pos);

        FxTextHit.initOnlyOneFxAtTime(this.pos, $"+{shildIncrement}", Color.Blue);
    }

    //give heal to a target.
    public virtual void giveHeal(Character target, int healIncrement, PackageRefCard? refCard = null)
    {
        target.takeHeal(healIncrement, this, refCard);
    }
    //take heal.
    protected virtual void takeHeal(int healIncrement, Character? characterGiveHeal = null, PackageRefCard? refCard = null)
    {
        int HPIncrement = Math.Min(healIncrement, this.HPmax - this.HP);

        HP += HPIncrement;

        //make an FX heart (and sound) for gain HP healed.
        FxManager.initOnQueue(FxType.FxHeartHeal, this.pos);

        FxTextHit.initOnlyOneFxAtTime(this.pos, $"+{HPIncrement}", Color.Red);
    }

    //gain Gold.
    protected virtual void gainGold(int POIncrement)
    {
        this.PO += POIncrement;

        FxTextHit.initOnlyOneFxAtTime(this.pos, $"{POIncrement}", Color.Gold);
    }


    //do the turn of a character (event when it's the turn of this character) use for execute logic mobs.
    public virtual void turn() { }

    //make skip turn of the character.
    public virtual void skipTurn()
    {
        if (TurnManager.getCharacterOfCurrentTurn().idEntity != idEntity) //can't skip turn if is not the turn of this entity.
            return;
        if (WalkManager.isWalking) //can't skip turn during walk.
            return;

        //status effect when end turn.
        this.statusEffects.ForEach(se => se.eventWhenTargetEndTurn());

        AP = APmax; //refill AP (for next turn).
        MP = MPmax; //refill MP.

        deck.discardOfEndTurn(); //drop full hands in cimetiere.

        TurnManager.endAllStatusEffectBySkipTurn(this.idEntity); //end status effect.

        TurnManager.moveCharacterIndexToNextCharacter(); //switch to next entity turn.
    }

    //make turn start for the character.
    public virtual void startTurn()
    {
        //status effect when start turn.
        this.statusEffects.ForEach(se => se.eventWhenTargetStartTurn());

        SP = 0; //reset shild.

        deck.piocheOfStartTurn();

        RunHudLayer.layer.buttonSkipTurnNN.setIsDisabled(!this.isInRedTeam); //set button skip turn enable or disable based on the team.

        //make an FX sparkle (and sound) for signal start turn.
        new FxTurnOn(this.pos);
    }


    //update turn of character (call by turn manager).
    public virtual void updateTurn()
    {
        if (WalkManager.isWalking)
        {
            WalkManager.updateWalk();
            return;
        }

        //if not in an animation, do logic turn.
        this.turn();
    }


    //move character to a vector index pos.
    public void moveTo(Vector indexPos, bool isActionCel = true)
    {
        indexPosCel = indexPos;
        pos = Room.getPosAtIndexCelRoom(indexPos);

        if (isActionCel)
            RunManager.getCelNN(indexPos).doActionTypeCel(this);
    }

    //decrease MP.
    public void decreaseMP(int decrease)
    {
        MP = Math.Max(MP - decrease, 0);
    }

    //decrease AP.
    public void decreaseAP(int decrease)
    {
        AP = Math.Max(AP - decrease, 0);
    }


    //use a card on a pos.
    public void useACardFromHand(int indexCardFromHand, Vector indexPosTarget)
    {
        Card cardPlay = this.deck.cardsInHand[indexCardFromHand];

        cardPlay.applyCardEffect(this, indexPosTarget, indexCardFromHand);

        if (!TurnManager.isInFight) //cut process defaus card if end fight (so hand is already full remove).
            return;

        this.decreaseAP(cardPlay.APCost);

        if (cardPlay.cardEdition == CardEdition.Cracked)
            this.deck.destroyCardFromHand(indexCardFromHand);
        else
            this.deck.pushCardFromHandIntoCimetier(indexCardFromHand);
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (!TurnManager.isInFight) //skip draw life bar if not in fight.
            return;
        if (WalkManager.isThisCharacterWalk(this)) //skip draw life bar if this character is walking.
            return;
        if (this.HP == this.HPmax) //don't draw bare life if full life.
            return;

        // draw background life bare.
        float borderSpacingPurcent = 5;
        float borderSpacingBrut = rectDest.size.x * (borderSpacingPurcent / 100);
        Rect rectDestBgLifeBar = new Rect(
            rectDest.posStart - (rectDest.size / 2) + borderSpacingBrut,
            new Vector(rectDest.size.x - borderSpacingBrut * 2, borderSpacingBrut)
        );
        Raylib.DrawRectangle(
            (int)rectDestBgLifeBar.posStart.x, (int)rectDestBgLifeBar.posStart.y,
            (int)rectDestBgLifeBar.size.x, (int)rectDestBgLifeBar.size.y,
            Color.Red
        );

        // draw life bare upper.
        int widthSizeLifeBar = (int)(((float)this.HP / this.HPmax) * rectDestBgLifeBar.size.x);
        Raylib.DrawRectangle(
            (int)rectDestBgLifeBar.posStart.x, (int)rectDestBgLifeBar.posStart.y,
            widthSizeLifeBar, (int)rectDestBgLifeBar.size.y,
            Color.Green
        );

    }


    public override string ToString()
    {
        return (
            $"{this.spriteType.ToString().Substring("Character_".Length)}" +
            $"-(hp: {this.HP}/{this.HPmax})"
        );
    }


    //get distance (brut) from other character.
    public int getDistFrom(Character characterB)
    {
        return (
            (int)Math.Abs(this.indexPosCel.x - characterB.indexPosCel.x) +
            (int)Math.Abs(this.indexPosCel.y - characterB.indexPosCel.y)
        );
    }


    // remove a status effect.
    public void dropAStatusEffectByIndex(int indexStatuSEffect)
    {
        this.statusEffects.RemoveAt(indexStatuSEffect);
    }

}