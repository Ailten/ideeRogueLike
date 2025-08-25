
public class StatusEffectDetailsUi : Entity
{
    private StatusEffect? effectSelected;
    public float scaleEffectIllu = 1f;
    public bool isPrintDetails = false;

    public StatusEffectDetailsUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(0, 0);
        this.encrage = new(0, 0);
    }


    public void setStatusEffect(StatusEffect? newEffect)
    {
        this.effectSelected = newEffect;
    }
    public StatusEffect? getStatusEffect()
    {
        return this.effectSelected;
    }


    private static float fontSize = 20f;
    private static float fontSpacing = 2f;
    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (this.effectSelected == null) //TODO: draw text no-effect selected.
            return;

        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");

        Vector statusEffectSizeScaled = StatusEffectUi.statusEffectSize * this.scale * CanvasManager.scaleCanvas * this.scaleEffectIllu;

        Rect destDraw = new(
            posToDraw - statusEffectSizeScaled / 2f,
            statusEffectSizeScaled
        );

        Raylib_cs.Raylib.DrawTexturePro( // draw bg effect.
            texture: spriteEffect.texture,
            source: spriteEffect.getSpriteTileBySpriteType(this.effectSelected!.getBackgroundSprite()).getRectSource(),
            dest: destDraw,
            origin: origine,
            rotation: 0,
            Raylib_cs.Color.White
        );
        Raylib_cs.Raylib.DrawTexturePro( // draw effect.
            texture: spriteEffect.texture,
            source: spriteEffect.getSpriteTileBySpriteType(this.effectSelected!.GetSpriteType).getRectSource(),
            dest: destDraw,
            origin: origine,
            rotation: 0,
            Raylib_cs.Color.White
        );

        // draw details effect.
        if (this.isPrintDetails)
        {
            string textDescription = this.effectSelected.getDescription();
            float fontSizeScaled = fontSize * CanvasManager.scaleCanvas;
            float fontSpacingScaled = fontSpacing * CanvasManager.scaleCanvas;
            Vector sizeText = Raylib_cs.Raylib.MeasureTextEx(StatusEffectUi.fontDescription, textDescription, fontSizeScaled, fontSpacingScaled);
            const float paddingUnScaled = 10f;
            float padding = paddingUnScaled * this.scale.x * CanvasManager.scaleCanvas;
            sizeText += padding * (textDescription.Count(c => c == '\n') * 0.5f + 2);

            Vector posDetails = (
                posToDraw +
                new Vector(statusEffectSizeScaled.x / 2, 0) +
                new Vector(padding, 0) +
                new Vector(0, - sizeText.y / 2)
            );

            Raylib_cs.Raylib.DrawRectangle( // draw back text.
                (int)posDetails.x,
                (int)posDetails.y,
                (int)sizeText.x,
                (int)sizeText.y,
                Raylib_cs.Color.Gray
            );

            // draw text (effect details).
            Vector posText = posDetails + padding;
            Raylib_cs.Raylib.DrawTextEx(
                font: StatusEffectUi.fontDescription,
                text: textDescription,
                position: posText,
                fontSize: fontSizeScaled,
                spacing: fontSpacingScaled,
                Raylib_cs.Color.Black
            );
        }
    }
}