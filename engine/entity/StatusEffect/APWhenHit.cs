
public class APWhenHit : StatusEffect
{
    private int APUp;
    private bool isAPmax;

    public APWhenHit(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int APUp = 1, bool isAPmax = false) :
    base(SpriteType.StatusEffect_APWhenHit, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.APUp = APUp;
        this.isAPmax = isAPmax;
    }

    public override string getDescription()
    {
        string isAPConstant = this.isAPmax ? "constant" : "pour ce tour";
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.APUp} {isAPConstant}, a chaque coup recu.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        if (this.isAPmax)
            return "Action par coup plus";
        return $"Action par coup";
    }
    public override bool isAMalus()
    {
        return this.APUp < 0;
    }


    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        if (this.isAPmax)
            whoHas.APmax += this.APUp; // increase AP max.

        whoHas.increaseAP(this.APUp); // increase AP this turn.
    }
    

}