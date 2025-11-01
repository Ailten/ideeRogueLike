
public class Eggify : StatusEffect
{
    SpriteType baseAspect;

    public Eggify(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife) :
    base(SpriteType.StatusEffect_Egg, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
    }
    public override void ActivateEffect()
    {
        Character? whoHasEffect = this.getCharacterWhoHasEffect;
        this.baseAspect = whoHasEffect!.spriteType;
        whoHasEffect!.spriteType = SpriteType.Character_Egg;
    }



    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"transform en oeuf.\n" +
            $"imunise au degats et restaure la vie.\n" +
            $"passe le tour.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return "Oeufification";
    }
    public override bool isAMalus()
    {
        return false;
    }


    public override void eventWhenStatusEffectDisapear(
        bool isEndLifeEffect = false,
        bool isEndOfFight = false,
        bool isCharacterWhoHasEffectDie = false,
        bool isCharacterWhoApplyEffectDie = false,
        bool isDestroyByAction = false
    )
    {
        Character? whoHasEffect = this.getCharacterWhoHasEffect;
        if (whoHasEffect is null)
            return;

        whoHasEffect.spriteType = this.baseAspect; // bring back spryte.

        whoHasEffect.giveHeal(whoHasEffect, whoHasEffect.HPmax); // heal.
    }

    public override void eventWhenTargetTakeDamage(ref int atk, ref Character? characterMakeAtk, ref PackageRefCard? refCard)
    {
        atk = 0; // cancel damage.
    }

    public override void eventWhenTargetStartTurn()
    {
        Character? whoHas = this.getCharacterWhoHasEffect;
        if (whoHas is null)
            return;

        whoHas.skipTurn(); // auto skip turn.
    }
}