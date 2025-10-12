
public class ShildAddBoostShiny : StatusEffect
{
    private int shildBoost;


    public ShildAddBoostShiny(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int shildBoost) :
    base(SpriteType.StatusEffect_ShildAddBoostShiny, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.shildBoost = shildBoost;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"diminue les degats subi par les cartes brillante.\n" +
            $"la diminution est de {this.shildBoost}.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Bouclier {(this.isAMalus() ? "moin" : "plus")} brillante";
    }
    public override bool isAMalus()
    {
        return this.shildBoost < 0;
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
            atk -= this.shildBoost; // decrease atk by sending reference.
        }
    }
}