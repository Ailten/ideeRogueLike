
public class DetailsCharacter : Layer
{
    private static DetailsCharacter _layer = new DetailsCharacter();
    public static DetailsCharacter layer
    {
        get { return _layer; }
    }

    public override void active()
    {
        //init all entities of layer. --->

        if(characterSelected is null)
            throw new Exception("characterSelected is null !");

        CardMenuBGUi bg = new CardMenuBGUi(this.idLayer); // draw back.
        bg.pos = new(0, 0);
        bg.size.x = CanvasManager.sizeWindow.x;
        bg.geometryTrigger = new Rect(new(), CanvasManager.sizeWindow);
        bg.zIndex = 3000;

        CheckBoxUi buttonExit = new CheckBoxUi(idLayer); // button exit.
        buttonExit.zIndex = 3400;
        buttonExit.scale = new(0.5f, 0.5f);
        buttonExit.pos = new(1247, 33);
        buttonExit.eventClick = () =>
        {
            DetailsCharacter.layer.unActive(); // close the layer.
        };

        CharacterUi characterUi = new CharacterUi(idLayer, this.characterSelected.spriteType); // character sprite.
        characterUi.pos = new(126+10, CanvasManager.centerWindow.y);
        characterUi.isDrawPseudo = true;
        characterUi.zIndex = 3200;

        // draw list effects and cards of character.
        const float decalXFromCenter = -60;
        const float decalYFromCenter = -60;
        StatusEffectDetailsUi statusEffectDetailsUi = new StatusEffectDetailsUi(this.idLayer); // details effect selected.
        statusEffectDetailsUi.pos = new(
            CanvasManager.centerWindow.x +decalXFromCenter,
            CanvasManager.centerWindow.y +decalYFromCenter
        );
        statusEffectDetailsUi.scaleEffectIllu = 2f;
        statusEffectDetailsUi.zIndex = 3200;
        statusEffectDetailsUi.isPrintDetails = true;

        CardDetails cardDetails = new CardDetails(this.idLayer); // details card selected.
        cardDetails.pos = new(
            CanvasManager.centerWindow.x - Card.cardSize.x/2 +decalXFromCenter,
            CanvasManager.centerWindow.y - Card.cardSize.y/2 +decalYFromCenter
        );
        cardDetails.zIndex = 3200;

        // list card and effects.
        StatusEffectUi statusEffetUi = new StatusEffectUi(this.idLayer); // list effects.
        ListCardUi cardsUi = new ListCardUi(this.idLayer); // list cards.
        statusEffetUi.setWidthSize(CanvasManager.centerWindow.x - 76);
        statusEffetUi.pos = new(10, 10);
        statusEffetUi.setListEffect(characterSelected.statusEffects);
        statusEffetUi.isWithDetail = false;
        statusEffetUi.zIndex = 3200;
        statusEffetUi.clickOnEffect = (effectClicked, isLeftClick) =>
        {
            statusEffectDetailsUi.setStatusEffect(effectClicked);
            cardDetails.setCard(null);
            cardsUi.unselectCard();
        };
        statusEffetUi.unClickOnEffect = (effectClicked, isLeftClick) =>
        {
            statusEffectDetailsUi.setStatusEffect(null);
            cardDetails.setCard(null);
            cardsUi.unselectCard();
        };

        cardsUi.pos = new(10, 508); // list cards.
        cardsUi.sizeListCard.x = CanvasManager.sizeWindow.x - 20;
        cardsUi.upCardWhenSelected = 45f;
        cardsUi.zIndex = 3200;
        cardsUi.isMakeReOrdered = false;
        cardsUi.setListCard(characterSelected.deck.cardsInAllDeck);
        cardsUi.clickOnCard = (cardClicked, isLeftClick) =>
        {
            cardDetails.setCard(cardClicked);
            statusEffectDetailsUi.setStatusEffect(null);
            statusEffetUi.resetSelection();
        };
        cardsUi.unClickOnCard = (cardClicked, isLeftClick) =>
        {
            cardDetails.setCard(null);
            statusEffectDetailsUi.setStatusEffect(null);
            statusEffetUi.resetSelection();
        };

        characterUi.pos.y += decalYFromCenter; // replace character at center (betwin border left screen and details card selected).
        float posLeftCardDetails = cardDetails.pos.x - 10;
        characterUi.pos.x = Vector.lerpF(10, posLeftCardDetails, 0.5f);


        // TODO : debug pos, up when click (statuseffect and card).



        base.active();
    }


    public Character? characterSelected = null;


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->

        this.characterSelected = null;

        LayerManager.isADetailsLayerAreOpen = false;

        base.unActive();
    }

}