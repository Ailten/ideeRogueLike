
public class Burn : StatusEffect
{
    private int damage;

    public Burn(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damage) : base(characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damage = damage;
    }


    public override void eventWhenTargetEndTurn()
    {
        this.getCharacterWhoApplyEffect?.makeDamage(this.getCharacterHasEffect, damage);
    }
}