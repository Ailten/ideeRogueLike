
public class ListCardUi : Entity
{
    private List<Card> listCard = new();
    private int indexCardSelected = -1;

    public ListCardUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 3200;

        this.size = new(0, 0);
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


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        //draw every card on list.

        //TODO : print card, lerp pos, case with no card.
        // --> include a print card using a function drawCard on object card (todo).
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (!isLeftClick || isClickDown)
            return;

        //TODO : evalue pos mouse and get the card match pos, if found one, execute clickOnCard.
    }
}