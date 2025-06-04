
public class CardDetails : Entity
{
    private Card? card;

    public CardDetails(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3200;

        this.size = new(750, 322);
        //size card : 219, 322;

        this.encrage = new(0, 0);

        this.geometryTrigger = new Rect( //array clickable for select a card (need re do calcul on event click).
            new(0, 0),
            new(780, 322)
        );
    }


    //set card.
    public void setListCard(Card? card)
    {
        this.card = card;
    }
    

    public float scaleCards = 1f;
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (card == null) //TODO: draw text no-card selected.
            return;

        Card cardNN = card ?? throw new Exception("CardDetails.card is null !");

        Vector sizeAtScreenCard = Card.cardSize * this.scaleCards * CanvasManager.scaleCanvas;
        Vector posCardToDraw = rectDest.posStart + (sizeAtScreenCard * 0.5f);

        cardNN.drawCard(posCardToDraw, scaleCards);
    }
}