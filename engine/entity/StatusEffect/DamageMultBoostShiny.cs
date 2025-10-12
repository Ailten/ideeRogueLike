
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
        int purcent = (int)((this.damageMult - 1f) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les degats des cartes brillante.\n" +
            $"augmente les degats realise de {purcent}%" +
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
            
        Card? getCard = refCard!.getCard();
        if (getCard is null)
            return;

        CardEdition editionOfCardUsed = getCard.cardEdition;
        if (editionOfCardUsed == CardEdition.Shinny)
        {
            atk = (int)(atk * this.damageMult); // aply multiplier by reference.
        }
    }
}