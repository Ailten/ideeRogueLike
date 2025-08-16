
public class ShildMultBoostColor : StatusEffect
{
    public CardColor color;
    private float shildMult;

    public ShildMultBoostColor(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, CardColor color, float shildMult) :
    base(SpriteType.StatusEffect_ShildMultiplyBoostRed, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.color = color;
        this.shildMult = shildMult;

        this.spriteType = ( // overide sprite.
            (color == CardColor.Red) ? SpriteType.StatusEffect_ShildMultiplyBoostRed :
            (color == CardColor.Blue) ? SpriteType.StatusEffect_ShildMultiplyBoostBlue :
            (color == CardColor.Green) ? SpriteType.StatusEffect_ShildMultiplyBoostGreen :
            throw new Exception("ShildMultBoostColor can't be assigne with this color !")
        );
    }


    public override string getDescription()
    {
        int purcent = (int)((1f - this.shildMult) * 100) * -1;
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les degats subit par des cartes {this.color.getName()}.\n" +
            $"donne {purcent}% de resistance au degat subits.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Bouclier multi {this.color.getName()}";
    }
    public override bool isAMalus()
    {
        return this.shildMult < 1f;
    }
    

    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardColor colorOfCardUsed = refCard!.getCard().cardColor;
        if (colorOfCardUsed.isMatchingColor(this.color))
        {
            atk = (int)(atk * this.shildMult); // aply multiplier by reference.
        }
    }
}