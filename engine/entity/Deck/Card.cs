
using System.Drawing;

public struct Card
{

    public SpriteType cardIllu; //sprite of illustation card.
    public CardColor cardColor; //sprite of background card.
    public CardEdition cardEdition; //special effect upper the card.
    public bool isRecto = false; //to know if it has to print masked.

    public int APCost; //the amount of AP need for use the card.
    public Vector distanceToUse; //distance can be use (x=min, y=max).

    public List<KeyValuePair<EffectCard, int>> effects; //all effects of a card (with an int of value).


    //default constructor.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), List<KeyValuePair<EffectCard, int>>? effects = null)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = effects ?? new();
    }

    //constructor with single effect.
    public Card(SpriteType cardIllu, CardColor? cardColor = null, CardEdition cardEdition = CardEdition.Default, int APCost = 0, Vector distanceToUse = new(), KeyValuePair<EffectCard, int>? effect = null)
    {
        this.cardIllu = cardIllu;
        this.cardColor = cardColor ?? StaticCardColor.getRandomColor();
        this.cardEdition = cardEdition;
        this.APCost = APCost;
        this.distanceToUse = distanceToUse;
        this.effects = new();
        if (effect != null)
            this.effects.Add(effect ?? throw new Exception("Card effect is null !"));
    }


    //use a card in battle (do this effect).
    public static void useACard(Character characterLauncher, Card cardToUse, Vector posTarget)
    {
        //loop on every effect of the card.
        for (int i = 0; i < cardToUse.effects.Count; i++)
        {
            EffectCard currentEffectType = cardToUse.effects[i].Key;
            int currentEffectValue = cardToUse.effects[i].Value;

            Character? characterTarget = TurnManager.getCharacterAtIndexPos(posTarget);

            switch (currentEffectType)
            {
                case (EffectCard.hit):
                    if (characterTarget == null)
                        continue;
                    effectCardHit(characterLauncher, currentEffectValue, characterTarget);
                    break;

                case (EffectCard.shild):
                    if (characterTarget == null)
                        continue;
                    effectCardShild(characterLauncher, currentEffectValue, characterTarget);
                    break;

                default:
                    throw new Exception($"useACard find a EffectCard with no effect {currentEffectType} !");
            }

        }
    }
    private static void effectCardHit(Character characterLauncher, int effectValue, Character characterTarget)
    {
        characterLauncher.makeDamage(characterTarget, effectValue);
    }
    private static void effectCardShild(Character characterLauncher, int effectValue, Character characterTarget)
    {
        characterLauncher.giveShild(characterTarget, effectValue);
    }


    public static Vector cardSize = new(219, 322);
    private static Vector cardEncrage = new(0.5f, 0.5f);
    public static Vector illuSize = new(219, 125);
    private static Vector illuEncrage = new(0.5f, 0.952f);
    public void drawCard(Vector posAtScreen, float scale = 1f)
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

            return;
        }

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

        if (!CanvasManager.isDebug)
            return;

        //debug encrage.
        Raylib_cs.Raylib.DrawLine(
            (int)posAtScreen.x -5, (int)posAtScreen.y -5,
            (int)posAtScreen.x +5, (int)posAtScreen.y +5,
            Raylib_cs.Color.Orange
        );
        Raylib_cs.Raylib.DrawLine(
            (int)posAtScreen.x -5, (int)posAtScreen.y +5,
            (int)posAtScreen.x +5, (int)posAtScreen.y -5,
            Raylib_cs.Color.Orange
        );

        //debug geometry trigger.
        Raylib_cs.Raylib.DrawRectangleLinesEx(
            rectDestCard,
            1,
            Raylib_cs.Color.Orange
        );

    }

    public void drawCardByArrea(Rect cardArrea, float scale = 1f)
    {
        Vector posCard = cardArrea.posStart + cardArrea.size * 0.5f;
        drawCard(
            posAtScreen: posCard,
            scale: scale
        );
    }


}