
public class ProductSoldedUi : Entity
{
    public ProductSoldedUi(int idLayer, bool isProductEffect) : base(idLayer, ((isProductEffect) ? SpriteType.StatusEffect_Solded : SpriteType.CardBG_Solded))
    {
        this.isUi = true;
        this.zIndex = 2000;

        this.encrage = new(0, 0);

        this.size = (
            (isProductEffect) ? StatusEffectUi.statusEffectSize :
            Card.cardSize
        );

        this.geometryTrigger = (
            (isProductEffect) ? new Rect(new(0, 0), StatusEffectUi.statusEffectSize) :
            new Rect(new(0, 0), Card.cardSize)
        );
    }
}