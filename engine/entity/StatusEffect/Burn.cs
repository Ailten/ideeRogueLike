
public class Burn : StatusEffect
{
    private int damage;

    public Burn(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damage) :
    base(SpriteType.StatusEffect_Burn, characterIdWhoHasEffect, characterIdWhoApplyEffect, -1) //turnLife //debug.
    {
        this.damage = damage;
    }

    protected override string getDescription()
    {
        return (
            $"- {this.GetType().ToString()} :\n" +
            "inflige 1 degat a la fin du tour de la cible."
        );
    }


    public override void eventWhenTargetEndTurn()
    {
        //debug.
        //this.getCharacterWhoApplyEffect?.makeDamage(this.getCharacterWhoHasEffect, this.damage);
    }
}