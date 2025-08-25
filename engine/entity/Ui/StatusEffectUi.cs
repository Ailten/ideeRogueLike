
// class for print a list of status effects at screen.
public class StatusEffectUi : Entity
{
    private List<StatusEffect> listEffect = new();
    private int indexEffectSelected = -1;
    public bool isEffectSelected
    {
        get { return this.indexEffectSelected >= 0; }
    }
    public int getIndexEffectSelected
    {
        get { return this.indexEffectSelected; }
    }

    public bool isWithDetail = true;
    public float heightSizeDownSelected = 20;
    public Action<StatusEffect, bool> clickOnEffect = (effectClicked, isLeftClick) => { };
    public Action<StatusEffect, bool> unClickOnEffect = (effectClicked, isLeftClick) => { };

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

    public void resetSelection()
    {
        this.indexEffectSelected = -1;
        this.geometryTriggerSecond = null;
    }

    public void updateGeometryTriggerBasedOnList()
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

            bool isNoNeedToPrintArrow = this.listEffect.Count == 1;
            float sizeY = Math.Abs(heightSizeDownSelected);
            if (!isNoNeedToPrintArrow && this.isWithDetail)
                sizeY += heightSizeSpacing + arrowLeftSize.y;

            bool isHeightSizeDownSelectedIsNegatif = heightSizeDownSelected < 0f;
            float posStartY = ((!isHeightSizeDownSelectedIsNegatif) ?
                statusEffectSize.y:
                heightSizeDownSelected
            );

            this.geometryTriggerSecond = new Rect( // set geometry trigger second (for click button left/right).
                new Vector(posStartX, posStartY),
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


    public static Vector statusEffectSize = new(63, 63);
    private static Vector arrowLeftSize = new(32, 16);
    private static float widthSizeSpacing = 10;
    private static float heightSizeSpacing = 10;
    public static Font fontDescription = FontManager.getFontByFontType(FontType.IntensaFuente);
    private static float fontSize = 20f;
    public static float fontSpacing = 2f;

    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        // skip draw if no effect.
        if (this.listEffect.Count == 0)
            return;

        // set basic data for draw.
        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");
        float fontSizeText = fontSize * CanvasManager.scaleCanvas;
        float fontSpacingText = fontSpacing * CanvasManager.scaleCanvas;

        // get Rect to draw.
        List<Rect> effectsArrea = this.getEffectsArrea();

        // draw arrow effect selected (and description).
        if (this.isEffectSelected && this.isWithDetail)
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
                    0,
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
                    0,
                    Raylib_cs.Color.White
                );
            }

            // draw description effect selected.
            string textDescription = this.listEffect[this.indexEffectSelected].getDescription();
            float padding = heightSizeSpacing * this.scale.y * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(fontDescription, textDescription, fontSizeText, fontSpacingText);
            sizeText += padding * (textDescription.Count(c => c == '\n') * 0.5f + 2);
            bool isHeightSizeDownSelectedIsNegatif = heightSizeDownSelected < 0f;
            float posStartBGTextY = ((!isHeightSizeDownSelectedIsNegatif) ?
                effectsArrea[effectsArrea.Count - 2].posStart.y + effectsArrea[effectsArrea.Count - 2].size.y + padding :
                effectsArrea[effectsArrea.Count - 2].posStart.y - padding - sizeText.y
            );
            if (this.listEffect.Count == 1) // re ajust pos Y of description when no arrow print.
            {
                posStartBGTextY += (
                    effectsArrea[effectsArrea.Count - 2].size.y + padding
                ) * ((!isHeightSizeDownSelectedIsNegatif)? -1: 1);
            }
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
                source: spriteEffect.getSpriteTileBySpriteType(currentEffect.getBackgroundSprite()).getRectSource(),
                dest: currentRect,
                origin: origine,
                rotation: 0,
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

            // draw turn until end in bulle.
            if (currentEffect.getTurnUntilEnd != -1)
            {
                Vector sizeTurnIcon = currentRect.size / 2;
                Vector posStartTurnIcon = currentRect.posStart + new Vector(currentRect.size.x, 0); // corner right.
                posStartTurnIcon.x -= sizeTurnIcon.x * 0.666f;
                posStartTurnIcon.y -= sizeTurnIcon.y * 0.333f;
                Rect destTurnIcon = new Rect(posStartTurnIcon, sizeTurnIcon);
                Raylib_cs.Raylib.DrawTexturePro( // draw bg turn icon.
                    texture: spriteEffect.texture,
                    source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_turnBG).getRectSource(),
                    dest: destTurnIcon,
                    origin: origine,
                    rotation: 0,
                    Raylib_cs.Color.White
                );

                // draw text.
                string textTurn = $"{currentEffect.getTurnUntilEnd}T";
                Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(fontDescription, textTurn, fontSizeText, fontSpacingText);
                Vector posStartTextTurn = destTurnIcon.posStart + destTurnIcon.size / 2; // center backgrond turn icon.
                posStartTextTurn -= sizeText / 2;
                Raylib_cs.Raylib.DrawTextEx(
                    font: fontDescription,
                    text: textTurn,
                    position: posStartTextTurn,
                    fontSize: fontSizeText,
                    spacing: fontSpacingText,
                    Raylib_cs.Color.Black
                );
            }
        }
    }


    // click event for select/unselect and move left/right.
    public override void eventMouseClick(bool isLeftClick, bool isClickDown)
    {
        if (isClickDown)
            return;

        List<Rect> effectsArrea = getEffectsArrea();

        // click arrow left/right.
        if (this.isEffectSelected && this.isWithDetail)
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
                this.unClickOnEffect(this.listEffect[i], isLeftClick);
                this.resetSelection();
                this.updateGeometryTriggerBasedOnList();
            }
            else
            {
                this.clickOnEffect(this.listEffect[i], isLeftClick);
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
        float heightSizeDownSelectedScaled = heightSizeDownSelected * this.scale.y * CanvasManager.scaleCanvas;
        posStart.y += heightSizeDownSelectedScaled; // replace down effect area.

        if (!this.isWithDetail)
            return;

        Vector arrowLeftSizeScaled = arrowLeftSize * this.scale.y * CanvasManager.scaleCanvas;
        float heightSizeSpacingScaled = heightSizeSpacing * this.scale.y * CanvasManager.scaleCanvas;

        bool isHeightSizeDownSelectedIsNegatif = heightSizeDownSelected < 0f;

        float posStartYButton = (!isHeightSizeDownSelectedIsNegatif)? (
            statusEffectSizeScaled.y + heightSizeDownSelectedScaled +
            heightSizeSpacingScaled + CanvasManager.posDecalCanvas.y
        ): ( // case replace button arrow up when selected height is negatif (to the top).
            heightSizeDownSelectedScaled - heightSizeSpacingScaled +
            CanvasManager.posDecalCanvas.y
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