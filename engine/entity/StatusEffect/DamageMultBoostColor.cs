
public class DamageMultBoostColor : StatusEffect
{
    private CardColor color;
    private float damageMult;

    public DamageMultBoostColor(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, CardColor color, float damageMult) :
    base(SpriteType.StatusEffect_DamageMultiplyBoostRed, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.color = color;
        this.damageMult = damageMult;

        this.spriteType = ( // overide sprite.
            (color == CardColor.Red)? SpriteType.StatusEffect_DamageMultiplyBoostRed:
            (color == CardColor.Blue)? SpriteType.StatusEffect_DamageMultiplyBoostBlue:
            (color == CardColor.Green)? SpriteType.StatusEffect_DamageMultiplyBoostGreen:
            throw new Exception("DamageMultBoostColor can't be assigne with this color !")
        );
    }

    public override string getDescription()
    {
        int purcent = (int)((1f - this.damageMult) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les degats des cartes {this.color.getName()}.\n" +
            $"augmente les degats realise de {purcent}%" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage multi {this.color.getName()}";
    }
    public override bool isAMalus()
    {
        return this.damageMult < 1f;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardColor colorOfCardUsed = refCard?.getCard().cardColor ?? throw new Exception("refCard is null");
        if (colorOfCardUsed.isMatchingColor(this.color))
        {
            atk = (int)(atk * this.damageMult); // aply multiplier by reference.
        }
    }
}