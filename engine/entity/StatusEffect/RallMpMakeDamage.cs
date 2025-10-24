
public class RallMpMakeDamage : StatusEffect
{
    private int damageByMPDecrease;

    public RallMpMakeDamage(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damageByMPDecrease = 1) :
    base(SpriteType.StatusEffect_RallMpMakeDamage, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageByMPDecrease = damageByMPDecrease;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"retirer des points de mouvement aplique des degats aditionnel.\n" +
            $"{this.damageByMPDecrease} points de degats par point de mouvements." +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return "Domage plantaire";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenDecreaseMPOfATarget(ref int decrease, ref Character whoTakeDecreaseMP)
    {
        if (decrease <= 0)
            return;

        int damage = this.damageByMPDecrease * decrease;

        this.getCharacterWhoHasEffect?.makeDamage(whoTakeDecreaseMP, damage); // trap can find the refCard tracking.
    }
}