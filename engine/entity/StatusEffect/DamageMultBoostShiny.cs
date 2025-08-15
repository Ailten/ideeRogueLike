
public class DamageMultBoostShiny : StatusEffect
{
    private float damageMult;

    public DamageMultBoostShiny(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, float damageMult) :
    base(SpriteType.StatusEffect_DamageMultiplyBoostShiny, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageMult = damageMult;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les degats des cartes brillante par {this.damageMult}.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage multi brillante";
    }
    public override bool isAMalus()
    {
        return this.damageMult < 1f;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardEdition editionOfCardUsed = refCard?.getCard().cardEdition ?? throw new Exception("refCard is null");
        if (editionOfCardUsed == CardEdition.Shinny)
        {
            atk = (int)(atk * this.damageMult); // aply multiplier by reference.
        }
    }
}