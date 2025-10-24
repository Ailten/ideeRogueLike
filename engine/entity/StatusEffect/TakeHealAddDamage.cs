
public class TakeHealAddDamage : StatusEffect
{
    private int damageGain;

    public TakeHealAddDamage(int characterIdWhoHasEffect, int characterIdWhoApplyEffect, int turnLife) :
    base(SpriteType.StatusEffect_TakeHealAddDamage, characterIdWhoHasEffect, characterIdWhoApplyEffect, turnLife)
    {
        this.damageGain = 0;
    }


    public override string getDescription()
    {
        return (
            $"- {this.getName()} :\n" +
            $"gagne des bonnus degats par soin subi.\n" +
            $"seul les soin sur HP manquant contabilise.\n" +
            $"remet le bonnus a 0 en fin de tour.\n" +
            $"{this.damageGain} domage boost ce tour.\n" +
            this.getDescriptionTurn()
        );
    }
    public override string getName()
    {
        return "Soin Muscle";
    }
    public override bool isAMalus()
    {
        return this.damageGain < 0;
    }


    public override void eventWhenTakeAHeal(ref int healIncrement, ref Character? characterGiveHeal, ref PackageRefCard? refCard)
    {
        Character? characterHasEffect = this.getCharacterWhoHasEffect;
        if (characterHasEffect is null)
            return;

        int HPLeft = characterHasEffect.HPmax - characterHasEffect.HP;
        int healTakableByHPLeft = Math.Min(healIncrement, HPLeft);

        this.damageGain += healTakableByHPLeft;
    }

    public override void eventWhenTargetMakeDamage(ref Character target, ref int atk, ref PackageRefCard? refCard)
    {
        atk += this.damageGain; // increase atk by sending reference.
    }

    public override void eventWhenTargetEndTurn()
    {
        this.damageGain = 0;
    }
}