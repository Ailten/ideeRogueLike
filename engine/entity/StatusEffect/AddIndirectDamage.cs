
public class AddIndirectDamage : StatusEffect
{
    private int damageBoost;


    public AddIndirectDamage(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damageBoost) :
    base(SpriteType.StatusEffect_AddIndirectDamage, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageBoost = damageBoost;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"ogmente les degats des attaque en dehor du tour du lanceur.\n" +
            $" {this.damageBoost} dommage aditionnel.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Domage indirect";
    }
    public override bool isAMalus()
    {
        return this.damageBoost < 0;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (TurnManager.getCharacterOfCurrentTurn() != this.getCharacterWhoHasEffect)
            return;

        atk += this.damageBoost; // increase atk by sending reference.
    }
    
}