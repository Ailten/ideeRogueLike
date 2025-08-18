
public class APBoost : StatusEffect
{
    private int APUp;

    public APBoost(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int APUp = 1) :
    base(SpriteType.StatusEffect_APBoost, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.APUp = APUp;
    }
    public override void ActivateEffect()
    {
        this.getCharacterWhoHasEffect.AP += this.APUp;
        this.getCharacterWhoHasEffect.APmax += this.APUp;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.APUp} point d action.\n"+
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Action {(this.isAMalus()? "moin": "plus")}";
    }
    public override bool isAMalus()
    {
        return this.APUp < 0;
    }


    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false)
    {
        this.getCharacterWhoHasEffect.AP -= this.APUp; // cancel effect.
        this.getCharacterWhoHasEffect.APmax -= this.APUp;
    }

}