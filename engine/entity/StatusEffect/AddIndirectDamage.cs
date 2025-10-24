
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
    public override string getName()
    {
        return $"Domage indirect";
    }
    public override bool isAMalus()
    {
        return this.damageBoost < 0;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        if (TurnManager.getCharacterOfCurrentTurn() == whoHas)
            return;

        atk += this.damageBoost; // increase atk by sending reference.
    }
    
}