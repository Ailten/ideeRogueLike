
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
    public override string getName()
    {
        return "Brulure";
    }
    public override bool isAMalus()
    {
        return true;
    }


    public override void eventWhenTargetEndTurn()
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        this.getCharacterWhoApplyEffect!.makeDamage(whoHas, this.damage);
    }
}