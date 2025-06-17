
using System.Globalization;

public class CardDetails : Entity
{
    private Card? card;
    private EffectCard? effectSelected;

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
        this.effectSelected = null;
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

        // draw effect text (on a right box).
        if (effectSelected != null)
        {
            // get values for draw text details effect selected.
            string text = effectSelected?.getDetails() ?? throw new Exception("CardDetails.effectSelected is null !");
            Vector posText = posToDraw + (new Vector(1, 0) * (sizeAtScreenCard.x + 10));
            float fontSizeText = Card.fontSizeShorter * scaleCards * CanvasManager.scaleCanvas;
            float fontSpacingText = Card.fontSpacing * scaleCards * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(Card.font, text, fontSizeText, fontSpacingText);
            const float padding = 10;

            // draw back text.
            sizeText += padding * (text.Count(c => c == '\n') * 0.5f + 2);
            Raylib_cs.Raylib.DrawRectangle(
                (int)posText.x, 
                (int)posText.y, 
                (int)sizeText.x, 
                (int)sizeText.y, 
                Raylib_cs.Color.Gray
            );

            // draw text (effect details).
            posText += padding;
            Raylib_cs.Raylib.DrawTextEx(
                font: Card.font,
                text: text,
                position: posText,
                fontSize: fontSizeText,
                spacing: fontSpacingText,
                Raylib_cs.Color.Black
            );
        }
    }


    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (isClickDown)
            return;

        if (card == null)
                return;
        Card cardNN = card ?? throw new Exception("CardDetails.card is null !");

        if (cardNN.isRecto)
            return;

        List<Rect> rectEffects = getRectEffects();

        for (int i = 0; i < rectEffects.Count; i++)
        {
            bool isClickOnCurrentEffect = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                rectEffects[i]
            );
            if (isClickOnCurrentEffect)
            {
                EffectCard effect = ((i==0 && cardNN.effects.Count == 0)?
                    EffectCard.NoEffect:
                    cardNN.effects[i].Key
                );
                effectSelected = (effectSelected==effect)? null: effect;
            }
        }
    }

    //get rects of text effects in card details.
    private List<Rect> getRectEffects()
    {
        if (card == null)
            return new();
        Card cardNN = card ?? throw new Exception("CardDetails.card is null !");

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

        //cast effects in list string.
        List<string> effectsStr = ((cardNN.effects.Count == 0) ?
            new List<string>() { "(no-effect)" } :
            cardNN.effects.Select(e => "- " + e.Key.ToString() + " (" + e.Value.ToString() + ")").ToList()
        );

        //set value fix for rect text effect.
        Vector encrageText = new(0, 0.5f);
        float fontSizeText = Card.fontSizeShorter * scaleCards * CanvasManager.scaleCanvas;
        float fontSpacingText = Card.fontSpacing * scaleCards * CanvasManager.scaleCanvas;

        List<Rect> output = new();
        for (int i = 0; i < effectsStr.Count; i++)
        {
            Vector posText = posAtScreen + (Card.posEffects[i] * this.scaleCards * CanvasManager.scaleCanvas);
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(Card.font, effectsStr[i], fontSizeText, fontSpacingText);
            posText -= (sizeText * encrageText);

            output.Add(new Rect(posText, sizeText));
        }
        return output;

    }
}