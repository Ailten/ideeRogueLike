
public class CharacterMob : Character
{

    protected List<LogicState> logicState = new();
    private int logicStateIndex = 0;
    private Character? targetOfState = null;

    private static List<MobType> allMobTypes = new(){ //list of all mob type and their dificulty.

        //stage 0 (tuto).

        new MobType(SpriteType.Character_Slime, 1, false), //stage 1.
        new MobType(SpriteType.Character_Rock, 1, false),
        new MobType(SpriteType.Character_Flame, 1, false),
        new MobType(SpriteType.Character_KingSlime, 1, true),
        new MobType(SpriteType.Character_KingFlame, 1, true),
        new MobType(SpriteType.Character_KingRock, 1, true),

        new MobType(SpriteType.Character_Goblin, 2, false), //stage 2.
        new MobType(SpriteType.Character_Armor, 2, false),
        new MobType(SpriteType.Character_GoblinDeez, 2, true),
        new MobType(SpriteType.Character_ArmorDamned, 2, true),

        new MobType(SpriteType.Character_Squelette, 3, false), //stage 3.
        new MobType(SpriteType.Character_Ghost, 3, false),
        new MobType(SpriteType.Character_Lish, 3, true),
        new MobType(SpriteType.Character_Spectr, 3, true),

        new MobType(SpriteType.Character_Pumkin, 4, false), //stage 4.
        new MobType(SpriteType.Character_Arachnide, 4, false),
        new MobType(SpriteType.Character_SacHead, 4, true),
        new MobType(SpriteType.Character_Crow, 4, true),

        // TODO : make a proper boss, and other dificulty mob.
        new MobType(SpriteType.Character_Slime, 5, false),
        new MobType(SpriteType.Character_Slime, 5, true)
    };

    public CharacterMob(SpriteType spriteType, Vector posIndexCel) : base(spriteType, posIndexCel)
    {
        this.isInRedTeam = false;
    }


    //instantiate a CharacterMob.
    public static CharacterMob init(SpriteType spriteType, Vector posIndexCel)
    {
        switch (spriteType)
        {

            case (SpriteType.Character_Slime):
                return new CharacterSlime(posIndexCel);
            case (SpriteType.Character_Rock):
                return new CharacterRock(posIndexCel);
            case (SpriteType.Character_Flame):
                return new CharacterFlame(posIndexCel);
            case (SpriteType.Character_KingSlime):
                return new CharacterKingSlime(posIndexCel);
            case (SpriteType.Character_KingFlame):
                return new CharacterKingFlame(posIndexCel);
            case (SpriteType.Character_KingRock):
                return new CharacterKingRock(posIndexCel);

            case (SpriteType.Character_Goblin):
                return new CharacterGoblin(posIndexCel);
            case (SpriteType.Character_GoblinDeez):
                return new CharacterGoblinDeez(posIndexCel);
            case (SpriteType.Character_Armor):
                return new CharacterArmor(posIndexCel);
            case (SpriteType.Character_ArmorDamned):
                return new CharacterArmorDamned(posIndexCel);

            case (SpriteType.Character_Squelette):
                return new CharacterSquelette(posIndexCel);
            case (SpriteType.Character_Ghost):
                return new CharacterGhost(posIndexCel);
            case (SpriteType.Character_Lish):
                return new CharacterLish(posIndexCel);
            case (SpriteType.Character_Spectr):
                return new CharacterSpectr(posIndexCel);

            case (SpriteType.Character_Pumkin):
                return new CharacterPumkin(posIndexCel);
            case (SpriteType.Character_Arachnide):
                return new CharacterArachnide(posIndexCel);
            case (SpriteType.Character_SacHead):
                return new CharacterSacHead(posIndexCel);
            case (SpriteType.Character_Crow):
                return new CharacterCrow(posIndexCel);

            //add heer new CharacterMob.

            default:
                throw new Exception("SpriteType has no CharacterMob match !");

        }
    }


    //pick a random type mob.
    public static SpriteType generateRandomMobType(Room room, int dificulty, bool isBoss = false)
    {
        //filter list type mob (for math to condition param).
        List<MobType> mobTypeFiltered = allMobTypes.Where((mt) =>
        {
            return mt.dificulty == dificulty && mt.isBoss == isBoss;
        }).ToList();

        //error if none found after filter.
        if (mobTypeFiltered.Count == 0)
            throw new Exception("generateRandomMobType return a empty list filtered !");

        //return a random element in list filtered (only the spriteType).
        return mobTypeFiltered[room.rngSeed.Next(mobTypeFiltered.Count)].spriteType;
    }


    //default process of turn of ennemy.
    public override void turn()
    {
        switch (this.logicState[this.logicStateIndex])
        {
            case (LogicState.skipTurn):
                this.logicStateSkipTurn();
                break;
            case (LogicState.chase):
                this.logicStateChase();
                break;
            case (LogicState.firstHit):
                this.logicStateFirstHit();
                break;
            case (LogicState.firstCardPlayableOponent):
                this.logicStateFirstCardPlayableOponent();
                break;
            case (LogicState.firstAttire):
                this.logicStateFirstAttire();
                break;
            case (LogicState.shildAlly):
                this.logicStateShildAlly();
                break;
            case (LogicState.fuit):
                this.logicStateFuit();
                break;
            case (LogicState.selfShild):
                this.logicStateSelfShild();
                break;
            case (LogicState.firstRetMP):
                this.logicStateFirstRetMP();
                break;
            case (LogicState.firstCardPlayableOnEmpty):
                this.logicStateFirstCardPlayableOnEmpty();
                break;
            case (LogicState.firstOneAPCardOnOponent):
                this.logicStateFirstAPCardOnOponent(1);
                break;
            case (LogicState.firstTwoAPCardOnOponent):
                this.logicStateFirstAPCardOnOponent(2);
                break;
            case (LogicState.firstTreeAPCardOnOponent):
                this.logicStateFirstAPCardOnOponent(3);
                break;
            case (LogicState.firstFourAPCardOnOponent):
                this.logicStateFirstAPCardOnOponent(4);
                break;


            case (LogicState.chase_or_firstAttire):
                if (RandomManager.rng.Next(2) == 0)
                    this.logicStateChase();
                else
                    this.logicStateFirstAttire();
                break;

            case (LogicState.chase_ifCardInHand):
                if (this.deck.cardsInHand.Count > 0)
                    this.logicStateChase();
                else
                    this.nextLogicState();
                break;

            default:
                throw new Exception("doLogicState has no execution to this enum !");
        }
    }
    private void nextLogicState()
    {
        this.logicStateIndex++;
        this.targetOfState = null;
    }
    private void logicStateSkipTurn()
    {
        this.logicStateIndex = 0; //reset logic index.
        this.skipTurn();
    }
    private void logicStateChase(int distanceAlowFromTarget = 1)
    {
        if (targetOfState == null)
        {

            //get all oponents (sort by poxi).
            List<Character> oponents = TurnManager.getAllCharacters().Where((c) => //filter.
                c.isInRedTeam != this.isInRedTeam
            ).OrderBy((c) =>  //order by proximity.
                Vector.distance(this.pos, c.pos)
            ).ToList();

            foreach (Character oponent in oponents)
            {

                //walk to cel (if can).
                PathFindingManager.evalAPath( //eval a pathfinding.
                    this.indexPosCel, //posIndexFrom.
                    oponent.indexPosCel, //posIndexTo.
                    100, //infinit length path.
                    distanceAlowFromTarget //the distance alow to target for stop path.
                );

                if (!PathFindingManager.isPathValid)
                    continue;

                this.targetOfState = oponent;

                PathFindingManager.editPathToStayFirstsCels(this.MP); //remove elemnt path until stay as much as current caracter can walk.

                WalkManager.startWalk(); //start walk along the path.

                return;
            }

            if (this.targetOfState == null)
            { //if no one oponent can be chase.
                this.nextLogicState();
                return;
            }

        }

        if (!WalkManager.isWalking)
        { //when the chase phase is end
            this.nextLogicState();
            return;
        }
    }
    private void logicStateFuit()
    {
        if (targetOfState == null)
        {

            //get all oponents (sort by poxi).
            List<Character> oponents = TurnManager.getAllCharacters().Where((c) => //filter.
                c.isInRedTeam != this.isInRedTeam
            ).OrderBy((c) =>  //order by proximity.
                Vector.distance(this.pos, c.pos)
            ).ToList();

            if (oponents.Count == 0)
            {
                this.nextLogicState();
                return;
            }

            this.targetOfState = oponents[0];

            Vector directionToTarget = this.targetOfState.indexPosCel - this.indexPosCel;
            Vector absDirectionToTarget = new(Math.Abs(directionToTarget.x), Math.Abs(directionToTarget.y));
            if (absDirectionToTarget.x > absDirectionToTarget.y)
                directionToTarget.y = 0;
            else
                directionToTarget.x = 0;
            directionToTarget.x = (directionToTarget.x > 1) ? 1 : -1;
            directionToTarget.y = (directionToTarget.y > 1) ? 1 : -1;

            bool isTakeThisPath = false;
            for (int i = 1; i < this.MP; i++)
            {
                Vector posDestWalk = this.indexPosCel + directionToTarget * i;

                Cel? celDest = RunManager.getCel(posDestWalk);
                if (celDest is null)
                    break;
                if (TurnManager.getCharacterAtIndexPos(posDestWalk) is not null)
                    break;

                PathFindingManager.evalAPath( //eval a pathfinding.
                    this.indexPosCel, //posIndexFrom.
                    posDestWalk, //posIndexTo.
                    i + 1, //infinit length path.
                    0 //the distance alow to target for stop path.
                );

                if (isTakeThisPath)
                    break;

                if (!PathFindingManager.isPathValid)
                {
                    if (i == 1)
                    {
                        this.nextLogicState();
                        return;
                    }

                    isTakeThisPath = true; // take the last path found.
                    i -= 2;
                }
            }

            PathFindingManager.editPathToStayFirstsCels(this.MP); //remove elemnt path until stay as much as current caracter can walk.

            WalkManager.startWalk(); //start walk along the path.

            return;

        }

        if (!WalkManager.isWalking)
        { //when the chase phase is end
            this.nextLogicState();
            return;
        }
    }
    private void logicStateFirstHit()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;
            if (!currentCard.effects.Select(e => e.Key).Contains(EffectCard.Hit)) //card has no effect "hit".
                continue;

            Character? target = TurnManager.getAllCharacters()
                .Where(c => this.isCardCanBePlayOnThisCharacter(currentCard, c, true))
                .OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateFirstCardPlayableOponent()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;
            if (currentCard.distanceToUse.x == 0) //is a cart usable on it self.
                continue;

            Character? target = TurnManager.getAllCharacters()
                .Where(c => this.isCardCanBePlayOnThisCharacter(currentCard, c, true))
                .OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateFirstAttire()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;
            if (!currentCard.effects.Select(e => e.Key).Contains(EffectCard.Attire)) //card has no effect "hit".
                continue;

            Character? target = TurnManager.getAllCharacters()
                .Where(c => this.isCardCanBePlayOnThisCharacter(currentCard, c, true))
                .OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateShildAlly()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (!currentCard.effects.Select(ev => ev.Key).Contains(EffectCard.Shild))
                continue;

            List<Character> allys = TurnManager.getAllCharacters()
                .Where(c =>
                {
                    if (c.isInRedTeam != this.isInRedTeam) // not take the oponent.
                        return false;
                    if (c == this) // not take the same as him self.
                        return false;
                    int dist = this.getDistFrom(c);
                    if (dist < currentCard.distanceToUse.x || dist > currentCard.distanceToUse.y) // out of range dist card use.
                        return false;
                    return true;
                }).OrderBy(c => (c.HP - c.HPmax)).ToList(); // sort by most HP left.

            if (allys.Count == 0)
                continue;

            this.useACardFromHand(i, allys[0].indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateSelfShild()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (!currentCard.effects.Select(ev => ev.Key).Contains(EffectCard.Shild))
                continue;

            if (currentCard.distanceToUse.x != 0)
                continue;

            this.useACardFromHand(i, this.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateFirstRetMP()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;
            if (!currentCard.effects.Select(e => e.Key).Contains(EffectCard.RetMP)) //card has no effect "hit".
                continue;

            Character? target = TurnManager.getAllCharacters()
                .Where(c => this.isCardCanBePlayOnThisCharacter(currentCard, c, true))
                .OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateFirstCardPlayableOnEmpty()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;

            bool isCardUsed = false;

            // brows all posIndex of po of this card.
            Vector.foreachCel(this.indexPosCel, currentCard.distanceToUse, (posCel) =>
            {
                if (isCardUsed)
                    return;

                Cel? currentCel = RunManager.getCel(posCel);
                if (currentCel is null)
                    return;

                if (TurnManager.getCharacterAtIndexPos(posCel) is not null)
                    return;

                this.useACardFromHand(i, posCel); //play the card.
                isCardUsed = true;
            });

            if (isCardUsed)
                break;
        }

        this.nextLogicState(); //move to next state.
    }
    private void logicStateFirstAPCardOnOponent(int APtarget)
    {
        if (this.AP < APtarget) //cost to mush ap.
        {
            this.nextLogicState(); //move to next state.
            return;
        }

        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost != APtarget) // right AP cost card filter.
                continue;

            Character? target = TurnManager.getAllCharacters()
                .Where(c => this.isCardCanBePlayOnThisCharacter(currentCard, c, true))
                .OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
            break;
        }

        this.nextLogicState(); //move to next state.
    }


    // return true if a card can be play on this character.
    private bool isCardCanBePlayOnThisCharacter(Card card, Character target, bool isFocusARedTeam)
    {
        if (target.isInRedTeam ^ isFocusARedTeam) //skip other mobs.
            return false;
        int dist = this.getDistFrom(target);
        if (dist < card.distanceToUse.x || dist > card.distanceToUse.y) //to close or to fare for use this card.
            return false;
        if (card.isInLine && !this.isAlignTo(target.indexPosCel)) // skip if not aligned.
            return false;
        return true;
    }

}