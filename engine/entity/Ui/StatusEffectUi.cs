
public class StatusEffectUi : Entity
{
    private List<StatusEffect> listEffect = new();
    private int indexEffectSelected = -1;
    public bool isEffectSelected
    {
        get { return this.indexEffectSelected >= 0; }
    }

    public StatusEffectUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(0, 0);
        this.encrage = new(0, 0);
    }


    private float widthSizeGeometry;
    private int maxEffectPrint;
    private bool isOverEffectCount
    {
        get { return this.listEffect.Count > this.maxEffectPrint; }
    }

    // set size.
    public void setWidthSize(float widthSize)
    {
        this.widthSizeGeometry = widthSize;
        this.evalAmountOfEffectPrintable();
        this.resetSelection();
        this.updateGeometryTriggerBasedOnList();
    }

    // set list effect.
    public void setListEffect(List<StatusEffect> listEffect)
    {
        this.listEffect = listEffect;
        this.resetSelection();
        this.updateGeometryTriggerBasedOnList();
    }

    private void resetSelection()
    {
        this.indexEffectSelected = -1;
        this.geometryTriggerSecond = null;
    }

    private void updateGeometryTriggerBasedOnList()
    {
        bool isEmpty = (this.listEffect.Count == 0);
        this.isActive = !isEmpty;

        this.geometryTrigger = new Rect(
            new(0, 0),
            new(
                (
                    (this.isOverEffectCount) ? this.widthSizeGeometry :
                    (statusEffectSize.x + widthSizeSpacing) * (this.listEffect.Count - 1) + statusEffectSize.x
                ),
                statusEffectSize.y
            )
        );

        if (isEmpty || !isEffectSelected)
            this.geometryTriggerSecond = null;
        else
        {
            float posStartX = (isOverEffectCount ?
                Vector.lerpF(0, widthSizeGeometry, (float)this.indexEffectSelected / this.listEffect.Count) :
                (statusEffectSize.x + widthSizeSpacing) * this.indexEffectSelected
            );
            float sizeY = heightSizeDownSelected + heightSizeSpacing + arrowLeftSize.y;

            this.geometryTriggerSecond = new Rect( // set geometry trigger second (for click button left/right).
                new Vector(posStartX, statusEffectSize.y),
                new Vector(statusEffectSize.x, sizeY)
            );
        }
    }

    private void evalAmountOfEffectPrintable()
    {
        this.maxEffectPrint = 0; //default zero.
        float widthSizeCroped = this.widthSizeGeometry;
        if (widthSizeCroped > statusEffectSize.x)
        {
            this.maxEffectPrint++; //first effect.
            widthSizeCroped -= statusEffectSize.x;

            float effectAndSpacingWidth = statusEffectSize.x + widthSizeSpacing;
            while (true)
            {
                if (widthSizeCroped < effectAndSpacingWidth)
                    break;
                this.maxEffectPrint++; //two or more effect (with spacing bewteen eatch).
                widthSizeCroped -= effectAndSpacingWidth;
            }
        }
    }


    private static Vector statusEffectSize = new(63, 63);
    private static Vector arrowLeftSize = new(32, 16);
    private static float widthSizeSpacing = 10;
    private static float heightSizeSpacing = 10;
    private static float heightSizeDownSelected = 20;
    private static Font fontDescription = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 20f;
    public static float fontSpacing = 2f;

    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        // skip draw if no effect.
        if (this.listEffect.Count == 0)
            return;

        // set basic data for draw.
        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");

        // get Rect to draw.
        List<Rect> effectsArrea = this.getEffectsArrea();

        // draw arrow effect selected (and description).
        if (this.isEffectSelected)
        {
            // draw arrow left/right.
            Rect arrowSource = spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_arrowLeftStatusEffect).getRectSource();
            if (this.indexEffectSelected != 0)
            {
                Raylib_cs.Raylib.DrawTexturePro( // draw bg effect.
                    texture: spriteEffect.texture,
                    source: arrowSource,
                    dest: effectsArrea[effectsArrea.Count - 2],
                    origine,
                    rotate,
                    Raylib_cs.Color.White
                );
            }
            if (this.indexEffectSelected != this.listEffect.Count - 1)
            {
                arrowSource.size.x *= -1;
                Raylib_cs.Raylib.DrawTexturePro( // draw bg effect.
                    texture: spriteEffect.texture,
                    source: arrowSource,
                    dest: effectsArrea[effectsArrea.Count - 1],
                    origine,
                    rotate,
                    Raylib_cs.Color.White
                );
            }

            // draw description effect selected.
            string textDescription = this.listEffect[this.indexEffectSelected].getDescription();
            float fontSizeText = fontSize * CanvasManager.scaleCanvas;
            float fontSpacingText = fontSpacing * CanvasManager.scaleCanvas;
            float padding = heightSizeSpacing * this.scale.y * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(fontDescription, textDescription, fontSizeText, fontSpacingText);
            sizeText += padding * (textDescription.Count(c => c == '\n') * 0.5f + 2);
            float posStartBGTextY = effectsArrea[effectsArrea.Count - 2].posStart.y + effectsArrea[effectsArrea.Count - 2].size.y + padding;
            Raylib_cs.Raylib.DrawRectangle( // draw back text.
                (int)posToDraw.x,
                (int)posStartBGTextY,
                (int)sizeText.x,
                (int)sizeText.y,
                Raylib_cs.Color.Gray
            );

            // draw text (effect details).
            Vector posText = new Vector(posToDraw.x, posStartBGTextY) + padding;
            Raylib_cs.Raylib.DrawTextEx(
                font: fontDescription,
                text: textDescription,
                position: posText,
                fontSize: fontSizeText,
                spacing: fontSpacingText,
                Raylib_cs.Color.Black
            );

            effectsArrea.RemoveRange(effectsArrea.Count - 2, 2); //remove.
        }

        // draw effects.
        for (int i = 0; i < effectsArrea.Count; i++)
        {
            Rect currentRect = effectsArrea[i];
            StatusEffect currentEffect = this.listEffect[i];

            Raylib_cs.Raylib.DrawTexturePro( // draw bg effect.
                texture: spriteEffect.texture,
                source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_BGStatusEffect).getRectSource(),
                dest: currentRect,
                origine,
                rotate,
                Raylib_cs.Color.White
            );
            Raylib_cs.Raylib.DrawTexturePro( // draw effect.
                texture: spriteEffect.texture,
                source: spriteEffect.getSpriteTileBySpriteType(currentEffect.GetSpriteType).getRectSource(),
                dest: currentRect,
                origin: origine,
                rotation: 0,
                Raylib_cs.Color.White
            );
        }
    }


    // click event for select/unselect and move left/right.
    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (isClickDown)
            return;

        List<Rect> effectsArrea = getEffectsArrea();

        // click arrow left/right.
        if (this.isEffectSelected)
        {
            // verify match click both arrow.
            bool isClickArrowLeft = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                effectsArrea[effectsArrea.Count - 2]
            );
            bool isClickArrowRight = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                effectsArrea[effectsArrea.Count - 1]
            );
            if (isClickArrowLeft || isClickArrowRight)
            {
                int indexDestination = indexEffectSelected + (isClickArrowLeft ? -1 : 1);

                if (indexDestination < 0 || indexDestination > this.listEffect.Count - 1)
                    return; // try to replace effect selected out of range list.

                StatusEffect tempon = this.listEffect[this.indexEffectSelected]; //switch.
                this.listEffect[this.indexEffectSelected] = this.listEffect[indexDestination];
                this.listEffect[indexDestination] = tempon;

                this.indexEffectSelected = indexDestination; //replace select.
                this.updateGeometryTriggerBasedOnList();

                return;
            }

            effectsArrea.RemoveRange(effectsArrea.Count - 2, 2); //remove.
        }

        // click effects.
        for (int i = effectsArrea.Count - 1; i >= 0; i--)
        {
            // verify match click effect.
            bool isClickOnCurrentEffect = ColideManager.isPosIsInRect(
                MouseManager.getPosMouseAtScreen,
                effectsArrea[i]
            );
            if (!isClickOnCurrentEffect)
                continue;

            // select/unselect effect.
            if (this.indexEffectSelected == i)
            {
                this.resetSelection();
                this.updateGeometryTriggerBasedOnList();
            }
            else
            {
                this.indexEffectSelected = i;
                this.updateGeometryTriggerBasedOnList();
            }

            return;

        }


    }


    private List<Rect> getEffectsArrea()
    {
        // case with no effect on list.
        if (listEffect.Count == 0)
            return new();

        List<Rect> output = new();
        Rect? buttonLeft = null, buttonRight = null;
        Vector posAtScreen = this.pos * CanvasManager.scaleCanvas + CanvasManager.posDecalCanvas;
        Vector statusEffectSizeScaled = statusEffectSize * this.scale * CanvasManager.scaleCanvas;

        if (this.isOverEffectCount)
        {
            float posLastEffect = (widthSizeGeometry - statusEffectSize.x) * this.scale.x * CanvasManager.scaleCanvas;

            for (int i = 0; i < this.listEffect.Count; i++)
            {
                float interpolation = (float)i / (this.listEffect.Count - 1);

                Vector posStart = posAtScreen;
                posStart.x += Vector.lerpF(0, posLastEffect, interpolation);

                // if current effect is selected make button left/right.
                if (i == this.indexEffectSelected)
                    getButtonArea(ref posStart, ref buttonLeft, ref buttonRight, statusEffectSizeScaled);

                output.Add(new Rect(
                    posStart,
                    statusEffectSizeScaled
                ));
            }
        }
        else
        {
            Vector widthSizeSpacingAndEffect = new Vector(statusEffectSize.x + widthSizeSpacing, 0);
            widthSizeSpacingAndEffect = widthSizeSpacingAndEffect * CanvasManager.scaleCanvas;

            for (int i = 0; i < this.listEffect.Count; i++)
            {
                Vector posStart = posAtScreen + widthSizeSpacingAndEffect * i;

                // if current effect is selected make button left/right.
                if (i == this.indexEffectSelected)
                    getButtonArea(ref posStart, ref buttonLeft, ref buttonRight, statusEffectSizeScaled);

                output.Add(new Rect(
                    posStart,
                    statusEffectSizeScaled
                ));
            }
        }

        // add button left and right att end (if as one init).
        if (buttonLeft is not null && buttonRight is not null)
        {
            output.Add(buttonLeft ?? throw new Exception("Rect is null !"));
            output.Add(buttonRight ?? throw new Exception("Rect is null !"));
        }

        return output;
    }
    private void getButtonArea(ref Vector posStart, ref Rect? buttonLeft, ref Rect? buttonRight, Vector statusEffectSizeScaled)
    {
        Vector arrowLeftSizeScaled = arrowLeftSize * this.scale.y * CanvasManager.scaleCanvas;
        float heightSizeDownSelectedScaled = heightSizeDownSelected * this.scale.y * CanvasManager.scaleCanvas;
        float heightSizeSpacingScaled = heightSizeSpacing * this.scale.y * CanvasManager.scaleCanvas;

        posStart.y += heightSizeDownSelectedScaled; // replace down effect area.

        float posStartYButton = (
            statusEffectSizeScaled.y + heightSizeDownSelectedScaled +
            heightSizeSpacingScaled + CanvasManager.posDecalCanvas.y
        );

        buttonLeft = new Rect(
            new Vector(posStart.x, posStartYButton),
            arrowLeftSizeScaled
        );
        buttonRight = new Rect(
            new Vector(posStart.x + statusEffectSizeScaled.x - arrowLeftSizeScaled.x, posStartYButton),
            arrowLeftSizeScaled
        );
    }

}