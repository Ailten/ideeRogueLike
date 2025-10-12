
public class ShildMultWhenFirst : StatusEffect
{
    private float shildMult;
    private static int cardMinInHand = 5;

    public ShildMultWhenFirst(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, float shildMult = 2f) :
    base(SpriteType.StatusEffect_ShildMultWhenFirst, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.shildMult = shildMult;
    }


    public override string getDescription()
    {
        int purcent = (int)(this.shildMult * 100) - 100;
        return (
            $"- {this.getName()} :\n" +
            $"produit {purcent}% de bouclier aditionnel si {cardMinInHand} cartes ou plus en main.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Bouclier main plaine";
    }
    public override bool isAMalus()
    {
        return this.shildMult > 1f;
    }


    public override void eventWhenGiveAShild(ref Character target, ref int shildIncrement, ref PackageRefCard? refCard)
    {
        Character? characterWhoHas = this.getCharacterWhoHasEffect;
        if (characterWhoHas is null)
            return;

        if (characterWhoHas.deck.cardsInHand.Count >= cardMinInHand)
        {
            shildIncrement = (int)(shildIncrement * this.shildMult);
        }
    }
}