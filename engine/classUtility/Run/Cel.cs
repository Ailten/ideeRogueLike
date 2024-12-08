
public class Cel : Entity
{
    public CelType celType;
    private Vector _indexPosCel;
    public Vector indexPosCel
    {
        get { return _indexPosCel; }
    }


    public Cel(CelType celType, Vector indexPosCel) : base(RunLayer.layer.idLayer, SpriteType.Cel)
    {

        this.isActive = false;

        this.size = new(126, 126);

        this.pos = Room.getPosAtIndexCelRoom(indexPosCel);

        this.zIndex = 1000;

        this.geometryTrigger = new Rect(
            new(-64, -64), 
            new(126, 126)
        );

        this._indexPosCel = indexPosCel;

        this.celType = celType;
    }


    //draw over the cel.
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if(celType == CelType.Cel) //skip draw effect if cel is basic.
            return;

        SpriteType spriteTypeDrawAfter = Cel.celTypeToSpriteType(celType);

        //draw at screen (with all data eval).
        Raylib_cs.Raylib.DrawTexturePro(
            sprite.texture, //texture.
            sprite.getSpriteTileBySpriteType(spriteTypeDrawAfter).getRectSource(), //rect source from texture.
            rectDest, //rect desintation at screen.
            origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
            Cel.getRotateOfCelType(celType), //rotation.
            Raylib_cs.Color.White //color (already white).
        );

    }


    //cast a celType into a spriteType.
    private static SpriteType celTypeToSpriteType(CelType celtype)
    {

        switch(celtype){

            //door.
            case(CelType.CelDoor_up):
                return SpriteType.Cel_DoorToNextRoom;
            case(CelType.CelDoor_right):
                return SpriteType.Cel_DoorToNextRoom;
            case(CelType.CelDoor_down):
                return SpriteType.Cel_DoorToNextRoom;
            case(CelType.CelDoor_left):
                return SpriteType.Cel_DoorToNextRoom;

            //rope.
            case(CelType.Cel_NextStage):
                return SpriteType.Cel_RopeToNextStage;

            default:
                throw new Exception("SpriteType no match for CelType !");

        }

    }


    //get rotate for a celType.
    private static float getRotateOfCelType(CelType celType)
    {

        switch(celType){

            //default.
            case(CelType.Cel):
                return 0;

            //rotate door.
            case(CelType.CelDoor_up):
                return 0;
            case(CelType.CelDoor_right):
                return 90;
            case(CelType.CelDoor_down):
                return 180;
            case(CelType.CelDoor_left):
                return 270;

            default:
                return 0;

        }

    }


    //event click.
    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        //get character at this pos.
        Character? characterAtCel = TurnManager.getCharacterAtIndexPos(indexPosCel);
        //getCharacter current turn.
        Character characterTurn = TurnManager.getCharacterOfCurrentTurn();

        //ask to print info of the cel (character is has one, or type cel if has one).
        if(!isLeftClick && isClickDown){

            if(characterAtCel != null){ //print info of character.

                //TODO (in specific function).

                return;
            }

            if(celType != CelType.Cel){ //print info of type cel.

                //TODO (in specific function).

                return;
            }

            return;
        }

        //player walk to cel or use card to the cel.
        if(isLeftClick && !isClickDown){

            //reason for cancel action.
            if(!characterTurn.isInRedTeam) //skip action if is not turn player.
                return;
            if(WalkManager.isWalking) //skip action if during a walk.
                return;


            //play a card at this cel.
            if(characterTurn.deck.isACardSelected){

                //TODO (in specific function).

                return;
            }

            //walk to cel (if can).
            PathFindingManager.evalAPath( //eval a pathfinding.
                characterTurn.indexPosCel, //posIndexFrom.
                indexPosCel, //posIndexTo.
                (TurnManager.isInFight? characterTurn.MP: 100) //maxMPCost (only during fight).
            );
            if(PathFindingManager.isPathValid){

                WalkManager.startWalk( //start walk along the path.
                    TurnManager.isInFight //isDecreaseMP.
                );

                return;
            }

            return;
        }

    }


    //ask if cel is odd.
    public bool isOdd()
    {
        return Room.isOddCel(indexPosCel);
    }


    //execute action of type cel to character step into.
    public void doActionTypeCel(Character characterStep)
    {
        switch(celType){

            //default.
            case(CelType.Cel):
                return;

            //door to move another room.
            case(CelType.CelDoor_up):
                if(characterStep.isAPlayer && !TurnManager.isInFight){
                    moveToAnotherRoom(characterStep, 0);
                }
                return;
            case(CelType.CelDoor_right):
                if(characterStep.isAPlayer && !TurnManager.isInFight){
                    moveToAnotherRoom(characterStep, 1);
                }
                return;
            case(CelType.CelDoor_down):
                if(characterStep.isAPlayer && !TurnManager.isInFight){
                    moveToAnotherRoom(characterStep, 2);
                }
                return;
            case(CelType.CelDoor_left):
                if(characterStep.isAPlayer && !TurnManager.isInFight){
                    moveToAnotherRoom(characterStep, 3);
                }
                return;

            //up to next stage.
            case(CelType.Cel_NextStage):
                if(characterStep.isAPlayer){ //only if is a player.
                    LayerManager.transition(() => { //transition layer.
                        characterStep.moveTo(new( //move player to center pos room.
                            Room.midWidthMax,
                            Room.midHeightMax
                        ), false);
                        RunManager.switchToNextStage();
                    });
                }
                return;

            default:
                return;

        }
    }

    //move player to another room (indexDirection is an index in clockwise from 0 to 3).
    private void moveToAnotherRoom(Character characterStep, int indexDirection)
    {
        //eval vector movement in room index.
        Vector moveRoom = (
            (indexDirection==0)? new Vector(0, -1):
            (indexDirection==1)? new Vector(1, 0):
            (indexDirection==2)? new Vector(0, 1):
            new Vector(-1, 0)
        );

        CelType celTypeDestination = (
            (indexDirection==0)? CelType.CelDoor_down:
            (indexDirection==1)? CelType.CelDoor_left:
            (indexDirection==2)? CelType.CelDoor_up:
            CelType.CelDoor_right
        );

        //make transition to another room.
        LayerManager.transition(() => {
            characterStep.moveTo(new( //move player to center pos room.
                Room.midWidthMax,
                Room.midHeightMax
            ), false);
            RunManager.switchToNextRoom(moveRoom);
            characterStep.moveTo( //move player arrow door (from previous room).
                RunManager.currentRoom!.getCelByType(celTypeDestination).indexPosCel, 
                false
            );
        });

    }
    

}