
using System.Runtime.CompilerServices;
using System.Security.Principal;

public class RunHudLayer : Layer
{

    private static RunHudLayer _layer = new RunHudLayer(){ layerName="RunHudLayer" };
    public static RunHudLayer layer 
    {
        get { return _layer; }
    }


    public override void active()
    {
        //init all entities of layer. --->

        StatsCharacterUi statsCharacterUI = new StatsCharacterUi(idLayer); //sprite of HP and SP player.

        MiniMapUi miniMapUi = new MiniMapUi(idLayer); //mini map.
        miniMapUi.pos = new(1280, 0);

        SkipTurnBackUi skipTurnBackUi = new SkipTurnBackUi(idLayer); //background ui for skip turn button.
        skipTurnBackUi.pos = CanvasManager.sizeWindow;
        skipTurnBackUi.isLeftSPriteTo = true;

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
        cardMenuListCardUi.clickOnCard = (cardClicked) =>
        {
            Console.WriteLine(cardClicked.cardIllu); // TODO: print card upper and details.
        };
        cardMenuListCardUi.unClickOnCard = (cardClicked) =>
        {
            Console.WriteLine(cardClicked.cardIllu); // TODO: 
        };
        elementsInMenuCardUi.Add(cardMenuListCardUi);

        CheckBoxUi cardMenuButtonExit = new CheckBoxUi(idLayer); //button exit card menu.
        cardMenuButtonExit.isActive = false;
        cardMenuButtonExit.zIndex = 3200;
        cardMenuButtonExit.scale = new(0.5f, 0.5f);
        cardMenuButtonExit.pos = new(1007, 33);
        cardMenuButtonExit.eventClick = () =>
        {
            cardMenuButtonExit.switchIsOn(); //stay on "X".
            RunHudLayer.layer.activeMenuCardUi(false); //close the card menu.
        };
        elementsInMenuCardUi.Add(cardMenuButtonExit);


        
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

        base.unActive();
    }

}