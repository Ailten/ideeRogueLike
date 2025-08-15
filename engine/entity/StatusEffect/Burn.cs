
public class Burn : StatusEffect
{
    private int damage;

    public Burn(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damage) :
    base(SpriteType.StatusEffect_Burn, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damage = damage;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"inflige {this.damage} degat a la fin du tour de la cible.\n"+
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return "Brulure";
    }
    public override bool isAMalus()
    {
        return true;
    }


    public override void eventWhenTargetEndTurn()
    {
        this.getCharacterWhoApplyEffect!.makeDamage(this.getCharacterWhoHasEffect, this.damage);
    }
}