
public class ShildMultBoostShiny : StatusEffect
{
    private float shildMult;

    public ShildMultBoostShiny(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, float shildMult) :
    base(SpriteType.StatusEffect_ShildMultiplyBoostShiny, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.shildMult = shildMult;
    }


    public override string getDescription()
    {
        int purcent = (int)((1f - this.shildMult) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les degats subit par des cartes Brillante.\n" +
            $"donne {purcent}% de resistance au degat subits.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Bouclier multi brillante";
    }
    public override bool isAMalus()
    {
        return this.shildMult < 1f;
    }


    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        Card? getCard = refCard!.getCard();
        if (getCard is null)
            return;

        CardEdition editionOfCardUsed = getCard.cardEdition;
        if (editionOfCardUsed == CardEdition.Shinny)
        {
            atk = (int)(atk * this.shildMult); // aply multiplier by reference.
        }
    }

}