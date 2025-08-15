
public class APWhenHit : StatusEffect
{
    private int APUp;

    public APWhenHit(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int APUp = 1) :
    base(SpriteType.StatusEffect_APWhenHit, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.APUp = APUp;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.APUp} pour le tour, a chaque coup recu.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Action par coup";
    }
    public override bool isAMalus()
    {
        return this.APUp < 0;
    }


    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        this.getCharacterWhoHasEffect.increaseAP(this.APUp);
    }
    

}