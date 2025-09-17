
// work as class, because of list inside : for disociate instance use getCopyOfCard().
public class Card
{

    public SpriteType cardIllu; //sprite of illustation card.
    public CardColor cardColor; //sprite of background card.
    public CardEdition cardEdition; //special effect upper the card.
    public bool isRecto = false; //to know if it has to print masked.

    public int APCost; //the amount of AP need for use the card.
    public Vector distanceToUse; //distance can be use (x=min, y=max).

    public List<KeyValuePair<EffectCard, int>> effects = new(); //all effects of a card (with an int of value).
    private static int maxEffectByCard = 5;
    public static int getMaxEffectByCard
    {
        get { return maxEffectByCard; }
    }
    public string getPorteeToUseStr
    {
        get
        {
            string min = Math.Round(distanceToUse.x).ToString();
            string max = Math.Round(distanceToUse.y).ToString();
            string isLine = (isInLine ? "\nen ligne" : "");
            return $"{min}~{max}{isLine}";
        }
    }

    public bool isInLine;

    public uint idCard;
    private static uint idCardCount = 0;


    //default constructor.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), List<KeyValuePair<EffectCard, int>>? effects = null, bool isInLine = false)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = effects ?? new();
        this.isInLine = isInLine;
        //size card : 219, 322;
        this.idCard = idCardCount++;
    }

    //constructor with single effect.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), KeyValuePair<EffectCard, int>? effect = null, bool isInLine = false)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = (effect == null ? new() : new() { effect ?? throw new Exception("Card effect is null !") });
        this.isInLine = isInLine;
        //size card : 219, 322;
        this.idCard = idCardCount++;
    }


    //use a card in battle (do this effect).
    public void applyCardEffect(Character characterLauncher, Vector indexPosTarget, int indexCardHand)
    {
        PackageRefCard refCard = new(characterLauncher, indexCardHand);

        //loop on every effect of the card.
        for (int i = 0; i < this.effects.Count; i++)
        {
            EffectCard currentEffectType = this.effects[i].Key;
            int effectValue = this.effects[i].Value;

            currentEffectType.doEffectCard(
                characterLauncher: characterLauncher,
                indexPosTarget: indexPosTarget,
                effectValue: effectValue,
                refCard: refCard
            );

        }
    }


    public static Vector cardSize = new(219, 322);
    private static Vector cardEncrage = new(0.5f, 0.5f);
    public static Vector illuSize = new(219, 125);
    private static Vector illuEncrage = new(0.5f, 0.952f);
    public static Font font = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 30f;
    public static float fontSizeShorter = 20f;
    public static float fontSpacing = 2f;
    private static Vector APCostEncrage = new(0, 0.5f);
    private static Vector posAPCost = new Vector(22, 26);
    private static Vector porteeToUseEncrage = new(0.5f, 0.5f);
    private static Vector posPorteeToUse = new Vector(193, 25);
    private static Vector nameEncrage = new(0.5f, 0.5f);
    private static Vector posName = new(106, 29);
    public static Vector[] posEffects = new Vector[] { new(34, 185), new(34, 212), new(34, 240), new(34, 268), new(34, 296), };
    public void drawCard(Vector posAtScreen, float scale = 1f, bool isMarkedSold = false)
    {
        SpriteType spriteType;
        Sprite sprite;
        Raylib_cs.Rectangle rectSourceInTexture;

        Vector cardSizeAtScreen = cardSize * scale * CanvasManager.scaleCanvas;
        Raylib_cs.Rectangle rectDestCard = new Raylib_cs.Rectangle(
            posAtScreen - (cardSizeAtScreen * 0.5f),
            cardSizeAtScreen
        );

        if (this.isRecto)
        {
            spriteType = SpriteType.CardBG_Recto;
            sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
            rectSourceInTexture = sprite.getSpriteTileBySpriteType(spriteType).getRectSource();

            // draw back card.
            Raylib_cs.Raylib.DrawTexturePro(
                texture: sprite.texture,
                source: rectSourceInTexture,
                dest: rectDestCard,
                origin: cardEncrage,
                rotation: 0f,
                Raylib_cs.Color.White
            );

        }
        else // draw card normaly.
        {
            // draw illu (behind back card).
            spriteType = this.cardIllu;
            sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
            rectSourceInTexture = sprite.getSpriteTileBySpriteType(spriteType).getRectSource();

            Raylib_cs.Rectangle rectDestIllu = new Raylib_cs.Rectangle(
                posAtScreen + (new Vector(0, 43) * scale * CanvasManager.scaleCanvas) - (cardSizeAtScreen * 0.5f),
                illuSize * scale * CanvasManager.scaleCanvas
            );

            Raylib_cs.Raylib.DrawTexturePro(
                texture: sprite.texture,
                source: rectSourceInTexture,
                dest: rectDestIllu,
                origin: illuEncrage,
                rotation: 0f,
                Raylib_cs.Color.White
            );

            // draw back card.
            spriteType = this.cardColor.getSpriteType();
            sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
            rectSourceInTexture = sprite.getSpriteTileBySpriteType(spriteType).getRectSource();

            Raylib_cs.Raylib.DrawTexturePro(
                texture: sprite.texture,
                source: rectSourceInTexture,
                dest: rectDestCard,
                origin: cardEncrage,
                rotation: 0f,
                Raylib_cs.Color.White
            );

            // draw text (cost AP).
            string text = this.APCost.ToString();
            Vector posText = posAtScreen - (cardSizeAtScreen * 0.5f) + (posAPCost * scale * CanvasManager.scaleCanvas);
            float fontSizeText = fontSize * scale * CanvasManager.scaleCanvas;
            float fontSpacingText = fontSpacing * scale * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(font, text, fontSizeText, fontSpacingText);

            Raylib_cs.Raylib.DrawTextEx(
                font: font,
                text: text,
                position: posText - (sizeText * APCostEncrage),
                fontSize: fontSizeText,
                spacing: fontSpacingText,
                Raylib_cs.Color.Black
            );

            // draw text (distanceToUse).
            text = this.getPorteeToUseStr; // include "in line".
            posText = posAtScreen - (cardSizeAtScreen * 0.5f) + (posPorteeToUse * scale * CanvasManager.scaleCanvas);
            fontSizeText = fontSizeShorter * scale * CanvasManager.scaleCanvas;
            sizeText = Raylib_cs.Raylib.MeasureTextEx(font, text, fontSizeText, fontSpacingText);

            Raylib_cs.Raylib.DrawTextEx(
                font: font,
                text: text,
                position: posText - (sizeText * porteeToUseEncrage),
                fontSize: fontSizeText,
                spacing: fontSpacingText,
                Raylib_cs.Color.Black
            );

            // draw text (name card).
            text = this.cardIllu.getCardName();
            posText = posAtScreen - (cardSizeAtScreen * 0.5f) + (posName * scale * CanvasManager.scaleCanvas);
            sizeText = Raylib_cs.Raylib.MeasureTextEx(font, text, fontSizeText, fontSpacingText);

            Raylib_cs.Raylib.DrawTextEx(
                font: font,
                text: text,
                position: posText - (sizeText * nameEncrage),
                fontSize: fontSizeText,
                spacing: fontSpacingText,
                Raylib_cs.Color.Black
            );

            // draw text (effects).
            List<string> effectsStr = ((effects.Count == 0) ?
                new List<string>() { EffectCard.NoEffect.getName() } :
                effects.Select(e => $"- {e.Key.getName()}" + (e.Value > 0 ? $" ({e.Value})" : "")).ToList()
            );
            for (int i = 0; i < effectsStr.Count; i++)
            {
                text = effectsStr[i];
                posText = posAtScreen - (cardSizeAtScreen * 0.5f) + (posEffects[i] * scale * CanvasManager.scaleCanvas);
                sizeText = Raylib_cs.Raylib.MeasureTextEx(font, text, fontSizeText, fontSpacingText);

                Raylib_cs.Raylib.DrawTextEx(
                    font: font,
                    text: text,
                    position: posText - (sizeText * APCostEncrage),
                    fontSize: fontSizeText,
                    spacing: fontSpacingText,
                    Raylib_cs.Color.Black
                );
            }


            // draw edition.
            if (cardEdition != CardEdition.Default)
            {
                spriteType = this.cardEdition.getSpriteType();
                sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
                rectSourceInTexture = sprite.getSpriteTileBySpriteType(spriteType).getRectSource();

                Raylib_cs.Raylib.DrawTexturePro(
                    texture: sprite.texture,
                    source: rectSourceInTexture,
                    dest: rectDestCard,
                    origin: cardEncrage,
                    rotation: 0f,
                    Raylib_cs.Color.White
                );
            }
        }

        // draw a giant red X on the card.
        if (isMarkedSold)
        {
            spriteType = SpriteType.CardBG_Solded;
            sprite = SpriteManager.findBySpriteType(spriteType) ?? throw new Exception("Sprite not found !");
            rectSourceInTexture = sprite.getSpriteTileBySpriteType(spriteType).getRectSource();

            Raylib_cs.Raylib.DrawTexturePro(
                texture: sprite.texture,
                source: rectSourceInTexture,
                dest: rectDestCard,
                origin: cardEncrage,
                rotation: 0f,
                Raylib_cs.Color.White
            );
        }

        if (!CanvasManager.isDebug)
            return;

        //debug encrage.
        Raylib_cs.Raylib.DrawLine(
            (int)posAtScreen.x - 5, (int)posAtScreen.y - 5,
            (int)posAtScreen.x + 5, (int)posAtScreen.y + 5,
            Raylib_cs.Color.Orange
        );
        Raylib_cs.Raylib.DrawLine(
            (int)posAtScreen.x - 5, (int)posAtScreen.y + 5,
            (int)posAtScreen.x + 5, (int)posAtScreen.y - 5,
            Raylib_cs.Color.Orange
        );

        //debug geometry trigger.
        Raylib_cs.Raylib.DrawRectangleLinesEx(
            rectDestCard,
            1,
            Raylib_cs.Color.Orange
        );

    }

    public void drawCardByArrea(Rect cardArrea, float scale = 1f, bool isMarkedSold = false)
    {
        Vector posCard = cardArrea.posStart + cardArrea.size * 0.5f;
        drawCard(
            posAtScreen: posCard,
            scale: scale,
            isMarkedSold: isMarkedSold
        );
    }


    // increase the value of an effect card.
    public void increaseEffectValue(int indexEffect, int increase = 1)
    {
        KeyValuePair<EffectCard, int> getEffect = this.effects[indexEffect];
        int newValue = getEffect.Value + increase;
        this.effects[indexEffect] = new(getEffect.Key, newValue);
    }


    // get a copy of the card.
    public Card getCopyOfCard()
    {
        Card output = new Card(
            cardIllu: this.cardIllu,
            cardColor: this.cardColor,
            cardEdition: this.cardEdition,
            APCost: this.APCost,
            distanceToUse: this.distanceToUse,
            effects: new()
        );
        for (int i = 0; i < this.effects.Count; i++) //duplicate liste effects.
        {
            output.effects.Add(new KeyValuePair<EffectCard, int>(
                this.effects[i].Key,
                this.effects[i].Value
            ));
        }
        return output;
    }


    public override string ToString()
    {
        return (
            $"{cardIllu.ToString().Substring("CardImg_".Length)}" +
            $"-{cardColor}" +
            $"{(cardEdition != CardEdition.Default ? "-" + cardEdition : "")}"
        );
    }

    public static bool operator ==(Card A, Card B)
    {
        return (A.idCard == B.idCard);
    }
    public static bool operator !=(Card A, Card B)
    {
        return (A.idCard != B.idCard);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;
        if (obj.GetType() != this.GetType())
            return false;
        return this == (Card)obj;
    }

    // no need.
    public override int GetHashCode()
    {
        throw new NotImplementedException();
    }
}


// object use to refer a card when use (becose card is struct).
public class PackageRefCard
{
    public Character character { get; set; }
    public int indexCardHand { get; set; }

    public PackageRefCard(Character character, int indexCardHand)
    {
        this.character = character;
        this.indexCardHand = indexCardHand;
    }

    public Card getCard()
    {
        return this.character.deck.cardsInHand[this.indexCardHand];
    }
}