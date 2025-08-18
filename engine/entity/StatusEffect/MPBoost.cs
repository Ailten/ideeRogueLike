
public class MPBoost : StatusEffect
{
    private int MPUp;

    public MPBoost(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int MPUp = 1) :
    base(SpriteType.StatusEffect_MPBoost, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.MPUp = MPUp;
    }
    public override void ActivateEffect()
    {
        this.getCharacterWhoHasEffect.MP += this.MPUp;
        this.getCharacterWhoHasEffect.MPmax += this.MPUp;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.MPUp} point de deplacement.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Deplacement {(this.isAMalus()? "moin": "plus")}";
    }
    public override bool isAMalus()
    {
        return this.MPUp < 0;
    }


    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false)
    {
        this.getCharacterWhoHasEffect.MP -= this.MPUp; // cancel effect.
        this.getCharacterWhoHasEffect.MPmax -= this.MPUp;
    }

}