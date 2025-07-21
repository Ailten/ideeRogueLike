
public class StatusEffectUi : Entity
{
    private List<StatusEffect> listEffect = new();

    public StatusEffectUi(int idLayer) : base(idLayer, SpriteType.none)
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.size = new(0, 0);
        this.encrage = new(0, 0);
    }


    private int amountOfEffectPrint;
    private int indexPosScrollBar = 0;


    private static Vector statusEffectSize = new(63, 63);
    private static Vector arrowLeftSize = new(32, 16);
    private static float widthSizeSpacing = 20;
    private static float heightSizeSpacing = 10;
    private static float heightArrowLeft = 16;


    // set size.
    public void setAmountOfEffectPrint(int amountEffect)
    {
        this.amountOfEffectPrint = amountEffect;

        this.geometryTrigger = new Rect(
            new(0, 0),
            new(
                (statusEffectSize.x * amountEffect) + (widthSizeSpacing * Math.Max(0, amountEffect - 1)),
                statusEffectSize.y + heightArrowLeft + (heightSizeSpacing * 1)
            )
        );

        this.indexPosScrollBar = 0;
    }

    // set list effect.
    public void setListEffect(List<StatusEffect> listEffect)
    {
        this.listEffect = listEffect;

        this.indexPosScrollBar = 0;
    }


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (this.listEffect.Count == 0)
            return;

        // print scroll bar.
        bool isScrollBarEnable = (listEffect.Count > amountOfEffectPrint);
        if (isScrollBarEnable)
        {
            // TODO : draw horizontal scroll bar.
        }

        // print effects.
        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");
        Vector statusEffectSizeScaled = statusEffectSize * scale * CanvasManager.scaleCanvas;
        Vector leftArrowSizeScaled = arrowLeftSize * scale * CanvasManager.scaleCanvas;
        float widthSizeSpacingScaled = widthSizeSpacing * scale.x * CanvasManager.scaleCanvas;
        float heightSizeSpacingScaled = heightSizeSpacing * scale.y * CanvasManager.scaleCanvas;
        float heightArrowLeftScaled = heightArrowLeft * scale.y * CanvasManager.scaleCanvas;

        for (int i = 0; i < amountOfEffectPrint; i++)
        {
            int indexEffect = indexPosScrollBar + i;
            if (indexEffect >= this.listEffect.Count)
                continue;
            StatusEffect effect = this.listEffect[indexEffect];

            // draw back effect.
            Rect rectDestDraw = new(
                posToDraw + new Vector((widthSizeSpacingScaled + statusEffectSizeScaled.x) * i, 0),
                statusEffectSizeScaled
            );

            Raylib_cs.Raylib.DrawTexturePro(
                texture: spriteEffect.texture,
                source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_BGStatusEffect).getRectSource(),
                dest: rectDestDraw,
                origine,
                rotate,
                Raylib_cs.Color.White
            );
            Raylib_cs.Raylib.DrawTexturePro(
                texture: spriteEffect.texture,
                source: spriteEffect.getSpriteTileBySpriteType(effect.GetSpriteType).getRectSource(),
                dest: rectDestDraw,
                origin: origine,
                rotation: 0,
                Raylib_cs.Color.White
            );

            // TODO : draw sphere turn until end effect.

            // TODO : draw arrow left and right.
        }

        // print details effect selected.
        // TODO : print details effect selected.

    }


}