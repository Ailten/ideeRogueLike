
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

            //skip action if is not turn player.
            if(!characterTurn.isInRedTeam)
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
                characterTurn.MP //maxMPCost.
            );
            if(PathFindingManager.isPathValid){
                
                WalkManager.startWalk();

                return;
            }

            return;
        }

    }
    

}