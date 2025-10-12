
public class DamageAddBoostColor : StatusEffect
{
    public CardColor color;
    private int damageBoost;


    public DamageAddBoostColor(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, CardColor color, int damageBoost) :
    base(SpriteType.StatusEffect_DamageAddBoostRed, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.color = color;
        this.damageBoost = damageBoost;

        this.spriteType = ( // overide sprite.
            (color == CardColor.Red) ? SpriteType.StatusEffect_DamageAddBoostRed :
            (color == CardColor.Blue) ? SpriteType.StatusEffect_DamageAddBoostBlue :
            (color == CardColor.Green) ? SpriteType.StatusEffect_DamageAddBoostGreen :
            throw new Exception("DamageAddBoostColor can't be assigne with this color !")
        );
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ogmente les degats des cartes {this.color.getName()} de {this.damageBoost}.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage {(this.isAMalus() ? "moin" : "plus")} {this.color.getName()}";
    }
    public override bool isAMalus()
    {
        return this.damageBoost < 0;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        Card? getCard = refCard!.getCard();
        if (getCard is null)
            return;

        CardColor colorOfCardUsed = getCard.cardColor;
        if (colorOfCardUsed.isMatchingColor(this.color))
        {
            atk += this.damageBoost; // increase atk by sending reference.
        }
    }
}
