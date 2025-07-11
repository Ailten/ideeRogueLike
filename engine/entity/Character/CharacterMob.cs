
public class CharacterMob : Character
{

    protected List<LogicState> logicState = new();
    private int logicStateIndex = 0;
    private Character? targetOfState = null;

    private static List<MobType> allMobTypes = new(){ //list of all mob type and their dificulty.
        new MobType(SpriteType.Character_Slime, 1, false),
        new MobType(SpriteType.Character_Slime, 1, true),

        // TODO : make a proper boss, and other dificulty mob.

        new MobType(SpriteType.Character_Slime, 2, false),
        new MobType(SpriteType.Character_Slime, 2, true),
        new MobType(SpriteType.Character_Slime, 3, false),
        new MobType(SpriteType.Character_Slime, 3, true),
        new MobType(SpriteType.Character_Slime, 4, false),
        new MobType(SpriteType.Character_Slime, 4, true),
        new MobType(SpriteType.Character_Slime, 5, false),
        new MobType(SpriteType.Character_Slime, 5, true),
        new MobType(SpriteType.Character_Slime, 6, false),
        new MobType(SpriteType.Character_Slime, 6, true),
        new MobType(SpriteType.Character_Slime, 7, false),
        new MobType(SpriteType.Character_Slime, 7, true),
        new MobType(SpriteType.Character_Slime, 8, false),
        new MobType(SpriteType.Character_Slime, 8, true),
        new MobType(SpriteType.Character_Slime, 9, false),
        new MobType(SpriteType.Character_Slime, 9, true),
        new MobType(SpriteType.Character_Slime, 10, false),
        new MobType(SpriteType.Character_Slime, 10, true),
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
                return new CharacterSlime(spriteType, posIndexCel);

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
    private void logicStateFirstHit()
    {
        for (int i = 0; i < this.deck.cardsInHand.Count; i++)
        {
            Card currentCard = this.deck.cardsInHand[i];

            if (currentCard.APCost > this.AP) //cost to mush ap.
                continue;
            if (!currentCard.effects.Select(e => e.Key).Contains(EffectCard.Hit)) //card has no effect "hit".
                continue;

            Character? target = TurnManager.getAllCharacters().Where(c =>
            {
                if (!c.isInRedTeam) //skip other mobs.
                    return false;
                int dist = this.getDistFrom(c);
                if (dist < currentCard.distanceToUse.x || dist > currentCard.distanceToUse.y) //to close or to fare for use this card.
                    return false;
                return true;
            }).OrderBy(c => c.isAPlayer).FirstOrDefault();
            if (target == null)
                continue;

            this.useACardFromHand(i, target.indexPosCel); //play the card.
        }

        this.nextLogicState(); //move to next state.
    }

}