
public class PropagatePoison : StatusEffect
{

    private int damage;

    public PropagatePoison(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int damage) :
    base(SpriteType.StatusEffect_PropagatePoison, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damage = damage;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"inflige {this.damage} degat au debut du tour de la cible.\n" +
            $"propage l'effet au case adjacente au debut du tour de la cible.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return "Propagation";
    }
    public override bool isAMalus()
    {
        return true;
    }


    public override void eventWhenTargetEndTurn()
    {
        Character? charWhoApply = this.getCharacterWhoApplyEffect;
        Character charWhoHas = this.getCharacterWhoHasEffect;

        // do damage.
        if (charWhoApply is null)
            charWhoHas.takeDamage(this.damage);
        else
            charWhoApply.makeDamage(charWhoHas, this.damage);

        // propagate.
        Vector.foreachCel(charWhoHas.indexPosCel, new Vector(1,1), (posCel) =>
        {
            Character? characterTargetAdj = TurnManager.getCharacterAtIndexPos(posCel);
            if (characterTargetAdj != null)
                characterTargetAdj.AddStatusEffect(new PropagatePoison(
                    this.getCharacterIdWhoHasEffect,
                    this.getCharacterIdWhoApplyEffect,
                    2,
                    this.damage
                ));
        });
    }
}