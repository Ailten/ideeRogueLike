
public class CardDetailsFusion : CardDetails
{
    private Card? firstCard, secondCard;
    private int indexFirstCard, indexSecondCard;
    // card in parent class is for card theoricly fusioned.

    public bool isAFirstCard
    {
        get { return this.indexFirstCard != -1; }
    }
    public bool isASecondCard
    {
        get { return this.indexSecondCard != -1; }
    }
    public int getIndexFistCard
    {
        get { return this.indexFirstCard; }
    }
    public int getIndexSecondCard
    {
        get { return this.indexSecondCard; }
    }

    public CardDetailsFusion(int idLayer) : base(idLayer)
    {
        this.firstCard = null;
        this.secondCard = null;
        this.indexFirstCard = -1;
        this.indexSecondCard = -1;
    }


    public void setCardForFusion(Card card, int indexSelected)
    {
        if (this.indexFirstCard == indexSelected)
        {
            removeFirstCard();
            return;
        }
        if (this.indexSecondCard == indexSelected)
        {
            removeSecondCard();
            return;
        }
        if (!this.isAFirstCard)
        {
            setFirstCard(card, indexSelected);
            return;
        }
        setSecondCard(card, indexSelected);
    }
    private void setFirstCard(Card card, int indexSelected)
    {
        this.firstCard = card;
        this.indexFirstCard = indexSelected;
        this.effectValueSelected = 0;
        this.setTheoricalFusion();
    }
    private void setSecondCard(Card card, int indexSelected)
    {
        this.secondCard = card;
        this.indexSecondCard = indexSelected;
        this.effectValueSelected = 0;
        this.setTheoricalFusion();
    }
    private void removeFirstCard()
    {
        this.firstCard = null;
        this.indexFirstCard = -1;
        this.effectValueSelected = 0;
        this.setTheoricalFusion();
    }
    private void removeSecondCard()
    {
        this.secondCard = null;
        this.indexSecondCard = -1;
        this.effectValueSelected = 0;
        this.setTheoricalFusion();
    }
    private void setTheoricalFusion()
    {
        List<KeyValuePair<EffectCard, int>> effectsFusion = new();
        if (this.isAFirstCard)
            effectsFusion.AddRange(this.firstCard?.effects ?? new());
        if (this.isASecondCard)
            effectsFusion.AddRange(this.secondCard?.effects ?? new());
        if (effectsFusion.Count > Card.getMaxEffectByCard) // limite max effects by card.
            effectsFusion.RemoveRange(Card.getMaxEffectByCard, (effectsFusion.Count - Card.getMaxEffectByCard));

        Card fusion = new(
            cardIllu: this.firstCard?.cardIllu ?? SpriteType.CardImg_WoodenSword,
            cardColor: this.firstCard?.cardColor ?? CardColor.Blue,
            cardEdition: this.firstCard?.cardEdition ?? CardEdition.Default,
            APCost: this.firstCard?.APCost ?? 1,
            distanceToUse: this.firstCard?.distanceToUse ?? new(1, 1),
            effects: effectsFusion
        );

        this.card = fusion; // DEBUG : need to be sure it's safe.
        if (!this.isAFirstCard)
            this.card = null;

        this.eventUpdateCardSelected();
    }

    public Action eventUpdateCardSelected = () => { };


    private static Vector replacementSecondCard = new(8, 8);
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        // draw the second card, behind the first one (fusionned).
        if (this.isASecondCard)
        {
            Card secondCardToDraw = this.secondCard ?? throw new Exception("CardDetailsFusion.secondCard is null !");

            Vector sizeAtScreenCard = Card.cardSize * this.scaleCards * CanvasManager.scaleCanvas;
            Vector replacementSCScaled = replacementSecondCard * CanvasManager.scaleCanvas;
            Vector posCardToDraw = rectDest.posStart + (sizeAtScreenCard * 0.5f) + replacementSCScaled;

            secondCardToDraw.drawCard(posCardToDraw, scaleCards);
        }

        // call draw after of parent CardDetails.
        base.drawAfter(posToDraw, rectDest, origine);
    }
}