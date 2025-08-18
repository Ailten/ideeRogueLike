
public class HPBoost : StatusEffect
{
    private int HPUp;

    public HPBoost(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int HPUp = 5) :
    base(SpriteType.StatusEffect_HPBoost, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.HPUp = HPUp;
    }
    public override void ActivateEffect()
    {
        this.getCharacterWhoHasEffect.HP += this.HPUp;
        this.getCharacterWhoHasEffect.HPmax += this.HPUp;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.HPUp} point de vie max.\n" +
            this.getDescriptionTurn()
        );
    }
    protected override string getName()
    {
        return $"Vie {(this.isAMalus() ? "moin" : "plus")}";
    }
    public override bool isAMalus()
    {
        return this.HPUp < 0;
    }
    

    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false)
    {
        this.getCharacterWhoHasEffect.HPmax -= this.HPUp; // cancel effect.
        if(this.getCharacterWhoHasEffect.HP > this.getCharacterWhoHasEffect.HPmax) // clamp HP (for no over range HP).
            this.getCharacterWhoHasEffect.HP = this.getCharacterWhoHasEffect.HPmax;
    }
}