
public class MPBoost : StatusEffect
{
    private int MPUp;

    public MPBoost(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife, int MPUp) :
    base(SpriteType.StatusEffect_MPBoost, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.MPUp = MPUp;

        this.getCharacterWhoHasEffect.MP += this.MPUp;
        this.getCharacterWhoHasEffect.MPmax += this.MPUp;
    }

    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.MPUp} point de deplacement en plus.\n"+
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return "Deplacement plus";
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