
public class MoneyMultiplyDamage : StatusEffect
{
    private int purcentDamageByCoint;

    public MoneyMultiplyDamage(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int purcentDamageByCoint = 1) :
    base(SpriteType.StatusEffect_DamageMultiplyBoostShiny, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.purcentDamageByCoint = purcentDamageByCoint;
    }

    private int getPurcentDamage()
    {
        return (this.getCharacterWhoHasEffect.PO * this.purcentDamageByCoint);
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"les attaque gagne {this.getPurcentDamage()}% de degats.\n" +
            $"le pourcentage depent des pieces d or du personnage.\n" +
            $"chaque piece d or donne {purcentDamageByCoint}% de degats.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Avare";
    }
    public override bool isAMalus()
    {
        return this.purcentDamageByCoint < 0;
    }


    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        atk = (int)(atk * ((float)(this.getPurcentDamage() + 100) / 100f)); // aply multiplier by reference.
    }
}