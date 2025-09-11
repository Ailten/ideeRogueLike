
public class Cel : Entity
{
    public CelType celType;
    private Vector _indexPosCel;
    public Vector indexPosCel
    {
        get { return _indexPosCel; }
    }
    public int idCharacterWhoApplyCelType = 0; // only use when a character apply a trap.


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
        // apply effect on cel : if card selected can be used on it.
        if (TurnManager.isInFight && RunHudLayer.layer.cardHandListCardUiNN.isCardSelected)
        {
            bool isCardOnRange = isCelIsOnRangeOfACard(RunHudLayer.layer.cardHandListCardUiNN.getCardSelected);
            if (isCardOnRange) //right dist to use card selected, so print fx on cel.
            {
                //draw fx cel selectable.
                Raylib_cs.Raylib.DrawTexturePro(
                    sprite.texture, //texture.
                    sprite.getSpriteTileBySpriteType(SpriteType.Cel_Selectable).getRectSource(), //rect source from texture.
                    rectDest, //rect desintation at screen.
                    origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
                    this.rotate, //rotation.
                    Raylib_cs.Color.White //color (already white).
                );
            }
        }

        if (celType == CelType.Cel) //skip draw effect if cel is basic.
            return;

        SpriteType spriteTypeDrawAfter = celType.celTypeToSpriteType();

        //draw at screen (with all data eval).
        Raylib_cs.Raylib.DrawTexturePro(
            sprite.texture, //texture.
            sprite.getSpriteTileBySpriteType(spriteTypeDrawAfter).getRectSource(), //rect source from texture.
            rectDest, //rect desintation at screen.
            origine, //origine, like encrage by adapt at sprite draw in screen (for rotation aply).
            celType.getRotateOfCelType(), //rotation.
            Raylib_cs.Color.White //color (already white).
        );

    }


    //event click.
    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        //get character at this pos.
        Character? characterAtCel = TurnManager.getCharacterAtIndexPos(indexPosCel);
        //getCharacter current turn.
        Character characterTurn = TurnManager.getCharacterOfCurrentTurn();

        //ask to print info of the cel (character is has one, or type cel if has one).
        if (!isLeftClick && isClickDown)
        {

            if (characterAtCel != null)
            { //print info of character.

                if (LayerManager.isADetailsLayerAreOpen) // can't open two type of detail layer.
                    return;
                LayerManager.isADetailsLayerAreOpen = true;

                DetailsCharacter.layer.characterSelected = characterAtCel; // active layer.
                DetailsCharacter.layer.active();
                return;
            }

            if (celType != CelType.Cel)
            { //print info of type cel.

                //TODO (in specific function).

                return;
            }

            return;
        }

        //player walk to cel or use card to the cel.
        if (isLeftClick && !isClickDown)
        {

            //reason for cancel action.
            if (!characterTurn.isInRedTeam) //skip action if is not turn player.
                return;
            if (WalkManager.isWalking) //skip action if during a walk.
                return;


            //play a card at this cel.
            ListCardUi handUiPlayer = RunHudLayer.layer.cardHandListCardUiNN;
            if (handUiPlayer.isCardSelected) //characterTurn.deck.isACardSelected
            {
                Card cardSelected = handUiPlayer.getCardSelected;

                if (WalkManager.isWalking) //cancel action if during a walk.
                    return;

                if (cardSelected.APCost > characterTurn.AP) //cost AP card over range character.
                    return;

                bool isRandeToUseCard = isCelIsOnRangeOfACard(cardSelected);
                if (!isRandeToUseCard) //cancel because out of range distance to use.
                    return;

                int indexCardSelected = handUiPlayer.getIndexCardSelected;
                characterTurn.useACardFromHand(indexCardSelected, this.indexPosCel); //play the card at this pos.

                handUiPlayer.setListCard(characterTurn.deck.cardsInHand); //update hand change by the card played.

                if (characterTurn.isAPlayer)
                {
                    SaveManager.increaseCardPlayed(cardSelected.cardIllu); // increase card played for stats save.
                    if (cardSelected.cardEdition != CardEdition.Default)
                        SaveManager.increaseCardEditionPlayed(cardSelected.cardEdition);
                }

                return;
            }

            //walk to cel (if can).
            PathFindingManager.evalAPath( //eval a pathfinding.
                characterTurn.indexPosCel, //posIndexFrom.
                indexPosCel, //posIndexTo.
                (TurnManager.isInFight ? characterTurn.MP : 100) //maxMPCost (only during fight).
            );
            if (PathFindingManager.isPathValid)
            {

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
        switch (celType)
        {

            //default.
            case (CelType.Cel):
                return;

            //door to move another room.
            case (CelType.CelDoor_up):
            case (CelType.CelDoor_right):
            case (CelType.CelDoor_down):
            case (CelType.CelDoor_left):
                int directionDoor = (
                    (celType == CelType.CelDoor_up) ? 0 :
                    (celType == CelType.CelDoor_right) ? 1 :
                    (celType == CelType.CelDoor_down) ? 2 :
                    3
                );
                if (characterStep.isAPlayer && !TurnManager.isInFight)
                {
                    moveToAnotherRoom(characterStep, directionDoor);
                }
                return;

            //up to next stage.
            case (CelType.Cel_NextStage):
                if (characterStep.isAPlayer) //only if is a player.
                {

                    SpecialRoom.layer.cleanSpecialRoomDataOfStage(); // clean special room data.

                    if (RunManager.isLastStage)
                    {
                        RunManager.endRun(false); // event end run.
                        return;
                    }

                    LayerManager.transition(() => // transition layer.
                    {
                        characterStep.moveTo(new( // move player to center pos room.
                            Room.midWidthMax,
                            Room.midHeightMax
                        ), false);

                        characterStep.HPmax += 10; // upgrade stats.
                        characterStep.HP = characterStep.HPmax;

                        RunManager.switchToNextStage(); // edit index stage to next one.
                    });

                }
                return;

            //walk to a chest.
            case (CelType.Cel_Coffre):
            case (CelType.Cel_Shop):
            case (CelType.Cel_Discard):
            case (CelType.Cel_Duplicate):
            case (CelType.Cel_CardEffectBoost):
            case (CelType.Cel_Fusion):
            case (CelType.Cel_SetCardEdition):

                if (LayerManager.isADetailsLayerAreOpen) // can't open two type of detail layer.
                    return;
                LayerManager.isADetailsLayerAreOpen = true;

                SpecialRoom.layer.active(); // active layer SpecialRoom when walk on chest cel.
                
                return;

            case (CelType.Cel_SandMPDown):
            case (CelType.Cel_SandMPDown_2):
            case (CelType.Cel_SandMPDown_3):
                if (!TurnManager.isInFight)
                    return;
                int intencitySandMP = (
                    (celType == CelType.Cel_SandMPDown) ? 1 :
                    (celType == CelType.Cel_SandMPDown_2) ? 2 :
                    3
                );
                characterStep.decreaseMP(intencitySandMP, idCharacterWhoDoDecreaseMP: this.idCharacterWhoApplyCelType);
                this.celType = CelType.Cel;
                this.idCharacterWhoApplyCelType = -1;
                return;
            case (CelType.Cel_SlimeAPDown):
            case (CelType.Cel_SlimeAPDown_2):
            case (CelType.Cel_SlimeAPDown_3):
                if (!TurnManager.isInFight)
                    return;
                int intencitySlimeAP = (
                    (celType == CelType.Cel_SlimeAPDown) ? 1 :
                    (celType == CelType.Cel_SlimeAPDown_2) ? 2 :
                    3
                );
                characterStep.decreaseAP(intencitySlimeAP, idCharacterWhoDoDecreaseMP: this.idCharacterWhoApplyCelType);
                this.celType = CelType.Cel;
                this.idCharacterWhoApplyCelType = -1;
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
            (indexDirection == 0) ? new Vector(0, -1) :
            (indexDirection == 1) ? new Vector(1, 0) :
            (indexDirection == 2) ? new Vector(0, 1) :
            new Vector(-1, 0)
        );

        CelType celTypeDestination = (
            (indexDirection == 0) ? CelType.CelDoor_down :
            (indexDirection == 1) ? CelType.CelDoor_left :
            (indexDirection == 2) ? CelType.CelDoor_up :
            CelType.CelDoor_right
        );

        //make transition to another room.
        LayerManager.transition(() =>
        {
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

    private bool isCelIsOnRangeOfACard(Card card, Character? caracterWhoUseCard = null)
    {
        caracterWhoUseCard ??= TurnManager.getCharacterOfCurrentTurn();
        float dist = (
            Math.Abs(caracterWhoUseCard.indexPosCel.x - this.indexPosCel.x) +
            Math.Abs(caracterWhoUseCard.indexPosCel.y - this.indexPosCel.y)
        );
        Vector cardPortee = card.distanceToUse;
        bool isInRightDist = (dist >= cardPortee.x && dist <= cardPortee.y);
        bool isInRightLine = (card.isInLine) ? (
            caracterWhoUseCard.indexPosCel.x == this.indexPosCel.x ||
            caracterWhoUseCard.indexPosCel.y == this.indexPosCel.y
        ): true;
        return (isInRightDist && isInRightLine);
    }
}