
public class Burn : StatusEffect
{
    private int damage;

    public Burn(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damage) :
    base(SpriteType.StatusEffect_Burn, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damage = damage;
    }

    protected override string getDescription()
    {
        return (
            $"- {this.GetType().ToString()} :\n" +
            "inflige 1 degat a la fin du tour de la cible."
        );
        // TODO: dure N tour(s).
        // TODO: (lance par X).
        // TODO: applique a Y.
    }


    public override void eventWhenTargetEndTurn()
    {
        this.getCharacterWhoApplyEffect?.makeDamage(this.getCharacterHasEffect, this.damage);
    }
}