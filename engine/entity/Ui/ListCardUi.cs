
public class ListCardUi : Entity
{
    private List<Card> listCard = new();
    private int indexCardSelected = -1;

    public ListCardUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3200;

        this.size = new(780, 322);
        //size card : 219, 322;

        this.encrage = new(0, 0);

        this.geometryTrigger = new Rect( //array clickable for select a card (need re do calcul on event click).
            new(0, 0),
            new(780, 322)
        );
    }


    //set list and reset selectioned card.
    public void setListCard(List<Card> listCard)
    {
        this.listCard = listCard.OrderBy(c => c.cardIllu).ToList();
        this.indexCardSelected = -1;
    }


    //action when click on a card of list.
    public Action<Card> clickOnCard = (cardSelected) => { };
    //action when click on the card currently selected (seconde click).
    public Action<Card> unClickOnCard = (cardSelected) => { };


    public float scaleCards = 1f;
    public float upCardWhenSelected = 20f;
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (listCard.Count == 0) //TODO: print text no-card.
            return;

        //get area of all cards.
        List<Rect> cardsArrea = getCardsArrea();

        //loop print card (by sending pos center card at screen).
        for (int i = 0; i < listCard.Count; i++)
        {
            listCard[i].drawCardByArrea(
                cardArrea: cardsArrea[i],
                scale: this.scaleCards
            );
        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (!isLeftClick || isClickDown)
            return;

        //get area of all cards.
        List<Rect> cardsArrea = getCardsArrea();

        for (int i = listCard.Count - 1; i >= 0; i--)
        {
            bool isClickOnCurrentCard = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                cardsArrea[i]
            );
            if (isClickOnCurrentCard)
            {
                bool isAnUndoClick = indexCardSelected == i;
                indexCardSelected = (isAnUndoClick)? -1: i;

                if (isAnUndoClick)
                    unClickOnCard(listCard[i]);
                else
                    clickOnCard(listCard[i]);

                return;
            }
        }
    }


    //get area of cards (for rect dest and colide mouse).
    private List<Rect> getCardsArrea()
    {
        //pos screen of full entity.
        Vector posAtScreen = this.pos * CanvasManager.scaleCanvas + CanvasManager.posDecalCanvas;

        //eval size at screen.
        Vector sizeAtScreen = this.size * this.scale * CanvasManager.scaleCanvas;

        //rect dest of full entity.
        Rect rectDest = new Rect(
            new Vector(posAtScreen.x, posAtScreen.y),
            new Vector(sizeAtScreen.x, sizeAtScreen.y)
        );

        //size at screen of a card.
        Vector sizeAtScreenCard = Card.cardSize * this.scaleCards * CanvasManager.scaleCanvas;

        //position of start and end position card at screen.
        Vector posStart = rectDest.posStart + (sizeAtScreenCard * 0.5f);
        Vector posEnd = rectDest.posStart + rectDest.size - (sizeAtScreenCard * 0.5f);

        List<Rect> output = new();

        for (int i = 0; i < listCard.Count; i++)
        {
            Vector posCard = new(0, posStart.y);
            posCard.x = Vector.lerpF(posStart.x, posEnd.x, ((float)i) / ((float)listCard.Count - 1));
            if (i == indexCardSelected)
                posCard.y -= this.upCardWhenSelected * CanvasManager.scaleCanvas;

            output.Add(new Rect(
                posCard - sizeAtScreenCard * 0.5f,
                sizeAtScreenCard
            ));
        }

        return output;
    }
}