
public class MultDamageWhenNoMP : StatusEffect
{
    private float damageMult;

    public MultDamageWhenNoMP(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, float damageMult = 1f) :
    base(SpriteType.StatusEffect_RallMpMakeDamage, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageMult = damageMult;
    }


    public override string getDescription()
    {
        int purcent = (int)((this.damageMult - 1f) * 100);
        return (
            $"- {this.getName()} :\n" +
            $"multiplie les domage sur les cible qui n'on aucun points de mouvement.\n" +
            $"augmente les degats realise de {purcent}%" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return "Domage plantaire";
    }
    public override bool isAMalus()
    {
        return false;
    }
    

    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        if (target.MP > 0)
            return;
            
        atk = (int)(atk * this.damageMult); // aply multiplier by reference.
    }
}