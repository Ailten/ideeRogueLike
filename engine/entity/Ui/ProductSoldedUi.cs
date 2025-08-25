
// only for StatusEffect.
public class ProductSoldedUi : Entity
{
    public ProductSoldedUi(int idLayer) : base(idLayer, SpriteType.StatusEffect_Solded)
    {
        this.isUi = true;
        this.zIndex = 3250;

        this.encrage = new(0, 0);

        this.size = StatusEffectUi.statusEffectSize;

        this.geometryTrigger = new Rect(new(0, 0), StatusEffectUi.statusEffectSize);
    }
}