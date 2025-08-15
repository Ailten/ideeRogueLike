
public class StatusEffectDetailsUi : Entity
{
    private StatusEffect? effectSelected;
    public float scaleEffectIllu = 2f;

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


    public override void drawAfter(Vector posToDraw, Rect rectDest, Vector origine)
    {
        if (this.effectSelected == null)
        {
            return;
        }

        Sprite spriteEffect = SpriteManager.findBySpriteType(SpriteType.StatusEffect_BGStatusEffect) ?? throw new Exception("Sprite not found !");

        Vector statusEffectSizeScaled = StatusEffectUi.statusEffectSize * this.scale * CanvasManager.scaleCanvas * this.scaleEffectIllu;

        Rect destDraw = new(
            posToDraw - statusEffectSizeScaled / 2f,
            statusEffectSizeScaled
        );

        Raylib_cs.Raylib.DrawTexturePro( // draw bg effect.
            texture: spriteEffect.texture,
            source: spriteEffect.getSpriteTileBySpriteType(SpriteType.StatusEffect_BGStatusEffect).getRectSource(),
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
    }
}