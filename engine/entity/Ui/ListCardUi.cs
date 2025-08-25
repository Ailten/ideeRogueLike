
public class ListCardUi : Entity
{
    private List<Card> listCard = new();
    private int indexCardSelected = -1;
    public bool isMakeReOrdered = true;
    private bool[] isSolded = new bool[0];

    public int getIndexCardSelected
    {
        get { return indexCardSelected; }
    }
    public bool isCardSelected
    {
        get { return indexCardSelected != -1; }
    }
    public Card getCardSelected
    {
        get { return listCard[indexCardSelected]; }
    }

    public ListCardUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3200;

        this.size = new(0, 0);
        //this.size = new(780, 322);
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
        this.listCard = (this.isMakeReOrdered? listCard.OrderBy(c => c.cardIllu).ToList(): listCard);
        this.unselectCard();
        this.updateGeometryTriggerBasedOnSizeListCard();
    }
    //set list and reset selectoned card (but set if is make re order list).
    public void setListCard(List<Card> listCard, bool isReOrder)
    {
        bool backupReOrder = this.isMakeReOrdered;
        this.isMakeReOrdered = isReOrder;
        setListCard(listCard);
        this.isMakeReOrdered = backupReOrder;
    }
    //unselect the card selected.
    public void unselectCard()
    {
        this.indexCardSelected = -1;
        this.geometryTriggerSecond = null;
    }
    //make selection on a card send.
    public void selectCardOnList(Card cardSelected)
    {
        for (int i = 0; i < listCard.Count; i++)
        {
            if (listCard[i] == cardSelected)
            {
                this.indexCardSelected = i;
                return;
            }
        }
    }
    public void setArrayIsSolded(bool[] isSolded)
    {
        this.isSolded = isSolded;
    }

    //set rect geometryTrigger with custom size.
    public void updateGeometryTriggerBasedOnSizeListCard()
    {
        if (this.listCard.Count == 0)
        { //no colide area when no card in UI.
            this.isActive = false;
            this.geometryTrigger = null;
            return;
        }
        this.isActive = true;
        this.geometryTrigger = new Rect(new Vector(0, 0), this.sizeListCard);
    }


    //action when click on a card of list.
    public Action<Card, bool> clickOnCard = (cardSelected, isLeftClick) => { };
    //action when click on the card currently selected (seconde click).
    public Action<Card, bool> unClickOnCard = (cardSelected, isLeftClick) => { };


    public float scaleCards = 1f;
    public Vector sizeListCard = new(780, 322);
    public float upCardWhenSelected = 20f;
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (listCard.Count == 0) //TODO: draw text no-card.
            return;

        //get area of all cards.
        List<Rect> cardsArrea = getCardsArrea();

        //loop print card (by sending pos center card at screen).
        for (int i = 0; i < listCard.Count; i++)
        {
            listCard[i].drawCardByArrea(
                cardArrea: cardsArrea[i],
                scale: this.scaleCards,
                isMarkedSold: ((this.isSolded.Length == 0)? false: this.isSolded[i])
            );
        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (isClickDown)
            return;

        //get area of all cards.
        List<Rect> cardsArrea = getCardsArrea();

        for (int i = listCard.Count - 1; i >= 0; i--)
        {
            bool isClickOnCurrentCard = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                cardsArrea[i]
            );
            bool isClickableByNotSolded = ((this.isSolded.Length == 0) ? true : !this.isSolded[i]);
            if (isClickOnCurrentCard && isClickableByNotSolded)
            {
                bool isAnUndoClick = this.indexCardSelected == i;
                if (isAnUndoClick)
                {
                    this.unselectCard(); //set index to -1 and reset second geometry trigger.
                    unClickOnCard(listCard[i], isLeftClick);
                    return;
                }

                this.indexCardSelected = i;

                Vector sizeTriggerSecond = new Vector(
                    Card.cardSize.x * this.scaleCards
                );

                //generate second geometry trigger (for card selected up).
                float widthCard = Card.cardSize.x * this.scaleCards;
                float leftCardReplace = Vector.lerpF(0, this.geometryTriggerNN.size.x - widthCard, (float)i / (listCard.Count - 1));
                this.geometryTriggerSecond = new Rect(
                    posStart: this.geometryTriggerNN.posStart + new Vector(leftCardReplace, -this.upCardWhenSelected),
                    size: new Vector(widthCard, this.upCardWhenSelected)
                );

                clickOnCard(listCard[i], isLeftClick);

                return;
            }
        }
    }


    //get area of cards (for rect dest and colide mouse).
    private List<Rect> getCardsArrea()
    {
        //case when no card.
        if (listCard.Count == 0)
            return new();

        //pos screen of full entity.
        Vector posAtScreen = this.pos * CanvasManager.scaleCanvas + CanvasManager.posDecalCanvas;

        //eval size at screen.
        Vector sizeAtScreen = this.sizeListCard * this.scale * CanvasManager.scaleCanvas;

        //size at screen of a card.
        Vector sizeAtScreenCard = Card.cardSize * this.scaleCards * CanvasManager.scaleCanvas;

        //position of start and end position card at screen.
        Vector posStart = posAtScreen + (sizeAtScreenCard * 0.5f);
        Vector posEnd = posAtScreen + sizeAtScreen - (sizeAtScreenCard * 0.5f);

        //replacement up when a card is selected.
        Vector upPosWhenSelected = new Vector(0, -1 * this.upCardWhenSelected * CanvasManager.scaleCanvas);

        //case when only one card (for prevent divid by zero).
        if (listCard.Count == 1)
            return new List<Rect>() { new Rect(
                new Vector(
                    Vector.lerpF(posStart.x, posEnd.x, 0.5f),
                    posStart.y
                ) + (
                    this.indexCardSelected == 0? upPosWhenSelected: new Vector(0, 0)
                ) - sizeAtScreenCard * 0.5f,
                sizeAtScreenCard
            )};

        //loop for every pos card.
        List<Rect> output = new();
        for (int i = 0; i < listCard.Count; i++)
        {
            Vector posCard = new(0, posStart.y);
            posCard.x = Vector.lerpF(posStart.x, posEnd.x, ((float)i) / ((float)listCard.Count - 1));
            if (i == indexCardSelected)
                posCard += upPosWhenSelected;

            output.Add(new Rect(
                posCard - sizeAtScreenCard * 0.5f,
                sizeAtScreenCard
            ));
        }
        return output;
    }
}