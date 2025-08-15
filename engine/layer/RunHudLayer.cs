
using System.Runtime.CompilerServices;
using System.Security.Principal;

public class RunHudLayer : Layer
{

    private static RunHudLayer _layer = new RunHudLayer();
    public static RunHudLayer layer 
    {
        get { return _layer; }
    }


    public override void active()
    {
        //init all entities of layer. --->

        StatsCharacterUi statsCharacterUI = new StatsCharacterUi(idLayer); //sprite of HP and SP player.

        MiniMapUi miniMapUi = new MiniMapUi(idLayer); //mini map.
        miniMapUi.pos = new(240, 355);

        SkipTurnBackUi skipTurnBackUi = new SkipTurnBackUi(idLayer); //background ui for skip turn button.
        skipTurnBackUi.pos = CanvasManager.sizeWindow;
        skipTurnBackUi.isLeftSpriteTo = true;
        skipTurnBackUi.isTopRightSpriteTo = true;

        this.buttonSkipTurn = new ButtonSkipTurnUi(idLayer); //button skip turn.
        this.buttonSkipTurn.pos = CanvasManager.sizeWindow - new Vector(195, 60);
        this.buttonSkipTurn.eventClick = () => { //event skip turn click.
            if(!TurnManager.isInFight)
                return;
            Character currentCharacter = TurnManager.getCharacterOfCurrentTurn();
            if(!currentCharacter.isInRedTeam)
                return;
            currentCharacter.skipTurn();
        };
        this.buttonSkipTurn.setIsDisabled(true);

        DeckButtonUi deckButtonUi = new DeckButtonUi(idLayer); //button deck (pioche).
        deckButtonUi.pos = new(10, CanvasManager.sizeWindow.y -10);

        deckButtonUi = new DeckButtonUi(idLayer); //button deck (defausse).
        deckButtonUi.pos.x = CanvasManager.sizeWindow.x - 125;
        deckButtonUi.pos.y = CanvasManager.sizeWindow.y - 10;
        deckButtonUi.isDeckPioche = false;

        CardMenuBGUi cardMenuBGUi = new CardMenuBGUi(idLayer); //menu card ui.
        cardMenuBGUi.isActive = false;
        cardMenuBGUi.pos = new(240, 0);
        elementsInMenuCardUi.Add(cardMenuBGUi);

        ListCardUi cardMenuListCardUi = new ListCardUi(idLayer); //list card ui.
        cardMenuListCardUi.isActive = false;
        cardMenuListCardUi.pos = new(250, 388);
        cardMenuListCardUi.upCardWhenSelected = 45f;
        cardMenuListCardUi.clickOnCard = (cardClicked, isLeftClick) =>
        {
            RunHudLayer.layer.setCardSelectedToMenuCardUi(cardClicked);
        };
        cardMenuListCardUi.unClickOnCard = (cardClicked, isLeftClick) =>
        {
            RunHudLayer.layer.setCardSelectedToMenuCardUi(null);
        };
        elementsInMenuCardUi.Add(cardMenuListCardUi);

        CheckBoxUi cardMenuButtonExit = new CheckBoxUi(idLayer); //button exit card menu.
        cardMenuButtonExit.isActive = false;
        cardMenuButtonExit.zIndex = 3400;
        cardMenuButtonExit.scale = new(0.5f, 0.5f);
        cardMenuButtonExit.pos = new(1007, 33);
        cardMenuButtonExit.eventClick = () =>
        {
            cardMenuButtonExit.switchIsOn(); //stay on "X".
            RunHudLayer.layer.setCardSelectedToMenuCardUi(null); //unselected card.
            RunHudLayer.layer.activeMenuCardUi(false); //close the card menu.
        };
        elementsInMenuCardUi.Add(cardMenuButtonExit);

        CardDetails cardDetails = new CardDetails(idLayer); //card details.
        cardDetails.isActive = false;
        cardDetails.pos = new(250, 10);
        elementsInMenuCardUi.Add(cardDetails); //TODO: click on effects of card selected, and print details at right.

        cardHandListCardUi = new ListCardUi(idLayer); //list card ui (hand).
        cardHandListCardUi.pos = new(150, 620);
        cardHandListCardUi.sizeListCard.x += 75;
        cardHandListCardUi.updateGeometryTriggerBasedOnSizeListCard();
        cardHandListCardUi.zIndex = 2010; //same has button deck ui.
        cardHandListCardUi.upCardWhenSelected = 65f;
        cardHandListCardUi.isMakeReOrdered = false;
        cardHandListCardUi.setListCard(TurnManager.getMainPlayerCharacter().deck.cardsInHand); //link card hands list to list UI.
        cardHandListCardUi.clickOnCard = (cardClicked, isLeftClick) =>
        {
            if (!isLeftClick) // print details card hand. 
            {
                List<Card> cardToPrint = TurnManager.getMainPlayerCharacter().deck.cardsInHand;
                RunHudLayer.layer.setListCardToMenuCardUi(cardToPrint, isReOrder: false);
                RunHudLayer.layer.setCardSelectedToMenuCardUi(cardClicked);
                RunHudLayer.layer.selectCardOnListToMenuCardUi(cardClicked);
                RunHudLayer.layer.activeMenuCardUi(true);

                cardHandListCardUi.unselectCard(); //unselect card.
            }
        };

        this.statusEffectUi = new StatusEffectUi(idLayer); // status effect ui.
        this.statusEffectUi.pos = new(380, 5);
        this.statusEffectUi.setWidthSize(720);

        TimeLineUi timeLineUi = new TimeLineUi(idLayer); // time line ui.
        timeLineUi.pos = new(CanvasManager.sizeWindow.x, 110);



        base.active();
    }


    private ButtonSkipTurnUi? buttonSkipTurn = null;
    public ButtonSkipTurnUi buttonSkipTurnNN
    {
        get { return buttonSkipTurn ?? throw new Exception(); }
    }

    private List<Entity> elementsInMenuCardUi = new();
    public void activeMenuCardUi(bool isActive = true)
    {
        elementsInMenuCardUi.ForEach(e => e.isActive = isActive);
    }
    public void setListCardToMenuCardUi(List<Card> listCardToPrint)
    {
        foreach (Entity e in elementsInMenuCardUi){
            (e as ListCardUi)?.setListCard(listCardToPrint);
        }
    }
    public void setListCardToMenuCardUi(List<Card> listCardToPrint, bool isReOrder)
    {
        foreach (Entity e in elementsInMenuCardUi){
            (e as ListCardUi)?.setListCard(listCardToPrint, isReOrder);
        }
    }
    public void setCardSelectedToMenuCardUi(Card? cardSelected)
    {
        foreach (Entity e in elementsInMenuCardUi){
            (e as CardDetails)?.setCard(cardSelected);
        }
    }
    public void selectCardOnListToMenuCardUi(Card cardSelected)
    {
        foreach (Entity e in elementsInMenuCardUi){
            (e as ListCardUi)?.selectCardOnList(cardSelected);
        }
    }
    public ListCardUi? cardHandListCardUi;
    public ListCardUi cardHandListCardUiNN
    {
        get { return cardHandListCardUi ?? throw new Exception("RunHudLayer.cardHandListCardUi is null !"); }
    }

    public StatusEffectUi? statusEffectUi = null;


    public override void update()
    {
        //do the update. --->

        base.update();
    }

    public override void unActive()
    {
        //free all entities of layer. --->
        this.buttonSkipTurn = null;
        this.elementsInMenuCardUi = new();
        this.cardHandListCardUi = null;
        this.statusEffectUi = null;

        base.unActive();
    }

}