
public class DamageAddBoostShiny : StatusEffect
{
    private int damageBoost;


    public DamageAddBoostShiny(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damageBoost) :
    base(SpriteType.StatusEffect_DamageAddBoostShiny, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageBoost = damageBoost;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ogmente les degats des cartes brillante de {this.damageBoost}.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage {(this.isAMalus()? "moin": "plus")} brillante";
    }
    public override bool isAMalus()
    {
        return this.damageBoost < 0;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (refCard is null) // skip if damage is not maked by a card.
            return;

        CardEdition editionOfCardUsed = refCard?.getCard().cardEdition ?? throw new Exception("refCard is null");
        if (editionOfCardUsed == CardEdition.Shinny)
        {
            atk += this.damageBoost; // increase atk by sending reference.
        }
    }
}