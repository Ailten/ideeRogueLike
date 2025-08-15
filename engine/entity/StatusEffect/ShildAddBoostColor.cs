
public class ShildAddBoostColor : StatusEffect
{
    private CardColor color;
    private int shildBoost;


    public ShildAddBoostColor(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, CardColor color, int shildBoost) :
    base(SpriteType.StatusEffect_ShildAddBoostRed, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.color = color;
        this.shildBoost = shildBoost;

        this.spriteType = ( // overide sprite.
            (color == CardColor.Red) ? SpriteType.StatusEffect_ShildAddBoostRed :
            (color == CardColor.Blue) ? SpriteType.StatusEffect_ShildAddBoostBlue :
            (color == CardColor.Green) ? SpriteType.StatusEffect_ShildAddBoostGreen :
            throw new Exception("ShildAddBoostColor can't be assigne with this color !")
        );
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"diminue les degats subi par les cartes {this.color.getName()}.\n" +
            $"la diminution est de {this.shildBoost}.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Bouclier {(this.isAMalus() ? "moin" : "plus")} {this.color.getName()}";
    }
    public override bool isAMalus()
    {
        return this.shildBoost < 0;
    }
    

    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardColor colorOfCardUsed = refCard?.getCard().cardColor ?? throw new Exception("refCard is null");
        if (colorOfCardUsed.isMatchingColor(this.color))
        {
            atk -= this.shildBoost; // decrease atk by sending reference.
        }
    }
}