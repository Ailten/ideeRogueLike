
public class HPBoost : StatusEffect
{
    private int HPUp;
    private int totalHPUp;

    public HPBoost(int characterIdWhoHasEffect, int characterIdWhoApplyEffect = -1, int turnLife = -1, int HPUp = 5) :
    base(SpriteType.StatusEffect_HPBoost, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.HPUp = HPUp;
        this.totalHPUp = 0;
    }
    //public override void ActivateEffect()
    //{
    //    this.getCharacterWhoHasEffect.HP += this.HPUp;
    //    this.getCharacterWhoHasEffect.HPmax += this.HPUp;
    //}


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne {this.HPUp} point de vie max par combat gagnie.\n" +
            $"{this.totalHPUp} points de vie total ajoute.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return $"Vie {(this.isAMalus() ? "moin" : "plus")}";
    }
    public override bool isAMalus()
    {
        return this.totalHPUp < 0;
    }


    public override void eventWhenPlayerWinFight()
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        this.totalHPUp += this.HPUp;
        whoHas.HP += this.HPUp;
        whoHas.HPmax += this.HPUp;
    }

    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false)
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        whoHas.HPmax -= this.totalHPUp; // cancel effect.
        if (whoHas.HP > whoHas.HPmax) // clamp HP (for no over range HP).
            whoHas.HP = whoHas.HPmax;
    }
}